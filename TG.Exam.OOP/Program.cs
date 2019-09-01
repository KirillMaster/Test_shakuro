using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TG.Exam.OOP
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Entity> objs = GetObjects();

            objs.ForEach(o =>
            {
                Console.WriteLine("Object:\r\n{0}", o.ToString2());
            });

            Console.ReadKey();
        }


        /// <summary>
        /// Если в условии подразумевался полиморфизм, то я бы сделал через интерфейс,
        /// и у каждого объекта сделал бы свой метод ToString2(). Но условие слишком размыто
        /// , поэтому делаю через базовый класс и рефлексию. 
        /// </summary>
        /// <returns></returns>
        private static List<Entity> GetObjects()
        {
            var objs = new List<Entity>
            {
                new Employee 
                {
                    FirstName = "Employee1FN",
                    LastName = "Employee1LN",
                    Salary = 5000
                },
                new SalesManager
                {
                    FirstName = "SalesManager1FN",
                    LastName = "SalesManager1LN",
                    Salary = 8000
                },
                new CustomerServiceAgent
                {
                    FirstName = "Developer1FN",
                    LastName = "Developer1LN",
                    Salary = 12000
                },
                new Dog
                {
                    Age = 2,
                    Name = "Dog1N"
                },
            };

            return objs;
        }
    }
}
