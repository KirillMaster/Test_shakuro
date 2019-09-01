using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TG.Exam.SQL
{
    public class DAL
    {
        private SqlConnection GetConnection() 
        {
            var connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            var con = new SqlConnection(connectionString);

            con.Open();

            return con;
        }

        private DataSet GetData(string sql)
        {
            var ds = new DataSet();

            using (var con = GetConnection())
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    using (var adp = new SqlDataAdapter(cmd))
                    {
                        adp.Fill(ds);
                    }
                }
            }

            return ds;
        }

        private void Execute(string sql)
        {
            using (var con = GetConnection())
            {
                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetAllOrders()
        {
            var sql = "Select * FROM dbo.Orders";

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }
        public DataTable GetAllOrdersWithCustomers()
        {
            var sql = "SELECT o.OrderId, c.CustomerId, o.OrderDate, c.CustomerFirstName, c.CustomerLastName FROM dbo.Orders o" +
                " JOIN dbo.Customers c ON o.OrderCustomerId = c.CustomerId";

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }

        public DataTable GetAllOrdersWithPriceUnder(int price)
        {
            string command = "SELECT o.OrderId, o.OrderDate, o.OrderCustomerId, Sum(i.ItemPrice * oi.Count) as OrderSum " +
              "FROM dbo.OrdersItems oi JOIN dbo.Items i  On i.ItemId = oi.ItemId JOIN dbo.Orders o ON oi.OrderId = o.OrderId " +
              "GROUP BY o.OrderId, o.OrderDate, OrderCustomerId " +
              "HAVING Sum(i.ItemPrice * oi.Count) < {0} " +
              "ORDER BY OrderId";


            var sql = string.Format(command, price);

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }

        public void DeleteCustomer(int orderId)
        {
            var sql = "DELETE c FROM dbo.Customers c JOIN dbo.Orders o ON o.OrderCustomerId = c.CustomerId WHERE o.OrderId = {0}";

            sql = string.Format(sql,orderId);

            Execute(sql);
        }

        internal DataTable GetAllItemsAndTheirOrdersCountIncludingTheItemsWithoutOrders()
        {

            string sql = "SELECT i.[ItemId], [ItemName], [ItemPrice], COUNT(DISTINCT OrderId) as Count " +
                             "FROM dbo.Items i LEFT JOIN dbo.OrdersItems oi ON i.ItemId = oi.ItemId " +
                             "GROUP BY i.ItemId, ItemName, ItemPrice " +
                             "ORDER BY i.ItemId ";

            var ds = GetData(sql);

            var result = ds.Tables.OfType<DataTable>().FirstOrDefault();

            return result;
        }
    }
}
