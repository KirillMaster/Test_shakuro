using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TG.Exam.OOP
{

    public class Entity
    {
        private static string GetPropertyValues(Object obj)
        {
            string result = string.Empty;

            Type t = obj.GetType();
            result += string.Format("Type is: {0}\n", t.Name);
            PropertyInfo[] props = t.GetProperties();
            result += string.Format("Properties (N = {0}):\n", props.Length);

            foreach (var prop in props)
            {
                if (prop.GetIndexParameters().Length == 0)
                {
                    result += string.Format("   {0} ({1}): {2}\n", prop.Name,
                                      prop.PropertyType.Name,
                                      prop.GetValue(obj));
                }
                else
                {
                    result += string.Format("   {0} ({1}): <Indexed>\n", prop.Name,
                                      prop.PropertyType.Name);
                }
            }     
            
            return result;
        }

        /// <summary>
        /// В условии не написано, что должен делать метод ToString2(). 
        /// Поэтому привожу реализацию, как её понимаю я.
        /// Может быть автором задания подразумевался полиморфизм с использованием интерфейса,
        /// а не такое наследование с использоваием рефлексии. Но поскольку по условию не понятно, что делает метод 
        /// ToString2(), я рассудил, что у него может быть общая реализация по всей иерархии классов, тогда нет смысла
        /// использовать полиморфизм.
        /// 
        /// </summary>
        /// <returns>Строковое описание свойств объекта.</returns>
        public string ToString2()
        {
            return GetPropertyValues(this);
        }
    }

    public class Employee : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
    }

    public class SalesManager : Employee
    {
        public int BonusPerSale { get; set; }
        public int SalesThisMonth { get; set; }
    }

    public class CustomerServiceAgent : Employee
    {
        public int Customers { get; set; }
    }

    public class Dog : Entity
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
