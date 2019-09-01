using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.ServiceModel;
using log4net;
using log4net.Config;

namespace TG.Exam.Refactoring
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class OrderService : IOrderService
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(OrderService));

        readonly string connectionString = ConfigurationManager.ConnectionStrings["OrdersDBConnectionString"].ConnectionString;

        private IDictionary<int, Order> cache;

        private static readonly string getOrderQueryTemplate = 
                  "SELECT OrderId, OrderCustomerId, OrderDate" +
                  "  FROM dbo.Orders where OrderId='{0}'";

        private Stopwatch stopWatch = new Stopwatch();

        public OrderService()
        {
            cache  = new Dictionary<int, Order>();
            BasicConfigurator.Configure();
        }

        //Проблемы в данном коде:
        //1) Если закешированное значение изменилось в БД, то пользователь получит неактуальные данные.
        //   Как это исправить - зависит от предметной области и задачи. Можно добавить кэшу время жизни и чистить его периодически
        //   или отказаться от кеширования в принципе, если пользователю всегда нужны актуальные данные.
        //   Чтобы кэширование вообще заработало, надо изменить InstanceContextMode у сервиса, чтобы он был синглтоном.
        //
        //2) Нет ErrorResponse для пользователя. Если на сервере произошла ошибка при обработке данных,
        //   пользователь получит стандартный пакет  с ошибкой без объяснения. Лучше бы вывести какой-нибудь ErrorCode или придумать формат,
        //   который поможет клиентской стороне обрабатывать такое поведение. Нужна дополнительная обработка ошибок. 
        //   Добавлю FaultContract с сообщением. 
        //  

        //3) С точки зрения рефакторинга, данный метод необходимо уменьшить в размере, т.к. его объем очень велик (более одного экрана) путём разбиения.
        //
        //4) Перед операциями с бизнес-логикой надо валидировать orderId, чтобы не выполнять лишних действий, если orderId не целочисленный.
        //   Также отсутствие валидации входных данных, значение которых передаются в SQL скрипты, потенциально может привести к SQL-инъекции.
        //
        //5) queryTemplate всегда один и тот же, надо вынести в свойство класса.
        //
        //6) StopWatch создается один раз. При этом после запуска таймера используется не метод Restart,
        //   а метод Start, что не обнулит счетчик, а продолжит его. Соответственно, Elapsed будет накапливаться. 
        //   Возможно это не было ожидаемым поведением. Необходимо исправить.
        //
        //7) Debug.Assert возможно в продакшн коде будет лишним. Убрать.
        //
        //8) Логгер лучше бы писал в Кибану или куда-то, где можно агрегировать логи, а кеш сделать с помощью memcached или Redis.
        //
        //9) Необходимо закрыть connection. Он открывается, но не закрывается.
        //Код, который реализует IDisposable, особенно с неконтролируемыми ресурсами надо использовать через using.
        //


        /// <summary>
        /// Возвращает закешированное значение объекта заказа. Может вернуть null.
        /// </summary>
        /// <param name="orderId">Id заказа.</param>
        /// <returns>Может вернуть null.</returns>
        private Order GetCachedOrder(int orderId)
        {
            stopWatch.Restart();
            lock (cache)
            {
                if (cache.ContainsKey(orderId))
                {
                    stopWatch.Stop();
                    logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
                    return cache[orderId];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Получает значение заказа из БД. Может вернуть null.
        /// </summary>
        /// <param name="orderId">Id заказа.</param>
        /// <returns></returns>
        private Order GetOrderFromDatabase(int orderId)
        {
            Order res = null;

            stopWatch.Restart();
            string query = string.Format(getOrderQueryTemplate, orderId);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand(query, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        res = new Order
                        {
                            OrderId = (int)reader[0],
                            OrderCustomerId = (int)reader[1],
                            OrderDate = (DateTime)reader[2]
                        };

                        lock (cache)
                        {
                            if (!cache.ContainsKey(orderId))
                                cache[orderId] = res;
                        }

                        stopWatch.Stop();
                        logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);
                    }
                }

                con.Close();
            }
            
            return res;
        }

        /// <summary>
        /// Метод загружает заказ из БД или из кэша. 
        /// </summary>
        /// <param name="orderId">OrderId</param>
        /// <returns></returns>
        public Order LoadOrder(string orderId)
        {
            int id = 0;

            try
            {
                stopWatch.Restart();

                bool result = int.TryParse(orderId, out id);
            
                if (!result || id < 0)
                {
                   string message = "Error: orderId must be an integer positive value";
                   throw new FaultException<ApplicationFault>(
                       new ApplicationFault(message), new FaultReason(message));
                }

                Order res = GetCachedOrder(id);

                if (res != null)
                {
                    return res;
                }

                res = GetOrderFromDatabase(id);

                stopWatch.Stop();
                logger.InfoFormat("Elapsed - {0}", stopWatch.Elapsed);

                return res;
            }
            catch (SqlException ex)
            {
                logger.Error(ex.Message);
                string message = $"Error: Db error while getting Order with Id {id}";
                throw new FaultException<ApplicationFault>(
                    new ApplicationFault(message), new FaultReason(message));
            }
        }
    }
}
