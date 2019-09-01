using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TG.Exam.Algorithms;

namespace TG.Exam.UnitTests
{
    [TestClass]
    public class AlgorithmsTests
    {
        /// <summary>
        /// Метод проверяет равенство результата работы алгоритма Foo2 результату работы Foo с агрументами из условия задания.
        /// </summary>
        [TestMethod]
        public void Check_Foo2_WithDefaultArguments()
        {
            int fooRes = Program.Foo(7,2,8);
            int foo2Res = Program.Foo2(7,2,8);
            Assert.AreEqual(fooRes, foo2Res);
        }

        /// <summary>
        /// Метод проверяет равенство результата работы алгоритма Foo2 результату работы Foo с отрицательными аргументами.
        /// </summary>
        [TestMethod]
        public void Check_Foo2_WithNegativeArguments()
        {
            int fooRes = Program.Foo(-7, -2, 8);
            int foo2Res = Program.Foo2(-7, -2, 8);
            Assert.AreEqual(fooRes, foo2Res);
        }

        /// <summary>
        /// Метод проверяет равенство результата работы алгоритма Foo2 результату работы Foo с одним положительным и одним отрицательным аргументом.
        /// </summary>
        [TestMethod]
        public void Check_Foo2_WithNegativeAndPositiveArgument()
        {
            int fooRes = Program.Foo(-7, 2, 8);
            int foo2Res = Program.Foo2(-7, 2, 8);
            Assert.AreEqual(fooRes, foo2Res);
        }

        /// <summary>
        /// Метод проверяет равенство результата работы алгоритма Foo2 результату работы Foo с одним положительным и одним отрицательным аргументом.
        /// </summary>
        [TestMethod]
        public void Check_Foo2_WithPositiveAndNegativeArgument()
        {
            int fooRes = Program.Foo(7, -2, 8);
            int foo2Res = Program.Foo2(7, -2, 8);
            Assert.AreEqual(fooRes, foo2Res);
        }

        /// <summary>
        /// Метод проверяет равенство результата работы алгоритма Foo2 результату работы Foo с нечетным числом итераций.
        /// </summary>
        [TestMethod]
        public void Check_Foo2_WithOddIterationsCount()
        {
            int fooRes = Program.Foo(7, -2, 7);
            int foo2Res = Program.Foo2(7, -2, 7);
            Assert.AreEqual(fooRes, foo2Res);
        }

        /// <summary>
        /// Метод проверяет равенство результата работы алгоритма Foo2 результату работы Foo с отрицательным числом итераций.
        /// </summary>
        [TestMethod]
        public void Check_Foo2_WithNegativeIterationsCount()
        {
            int fooRes = Program.Foo(7, -2, -1);
            int foo2Res = Program.Foo2(7, -2, -1);
            Assert.AreEqual(fooRes, foo2Res);
        }

        /// <summary>
        /// Метод проверяет равенство результата работы алгоритма Foo2 результату работы Foo с агрументами из условия задания.
        /// </summary>
        [TestMethod]
        public void Check_Foo3_WithDefaultArguments()
        {
            int fooRes = Program.Foo(7, 2, 8);
            int foo3Res = Program.Foo3(7, 2, 8);
            Assert.AreEqual(fooRes, foo3Res);
        }

        /// <summary>
        /// Метод проверяет равенство результата работы алгоритма Foo2 результату работы Foo с отрицательными аргументами.
        /// </summary>
        [TestMethod]
        public void Check_Foo3_WithNegativeArguments()
        {
            int fooRes = Program.Foo(-7, -2, 8);
            int foo3Res = Program.Foo3(-7, -2, 8);
            Assert.AreEqual(fooRes, foo3Res);
        }

        /// <summary>
        /// Метод проверяет равенство результата работы алгоритма Foo2 результату работы Foo с одним положительным и одним отрицательным аргументом.
        /// </summary>
        [TestMethod]
        public void Check_Foo3_WithNegativeAndPositiveArgument()
        {
            int fooRes = Program.Foo(-7, 2, 8);
            int foo3Res = Program.Foo3(-7, 2, 8);
            Assert.AreEqual(fooRes, foo3Res);
        }

        /// <summary>
        /// Метод проверяет равенство результата работы алгоритма Foo2 результату работы Foo с одним положительным и одним отрицательным аргументом.
        /// </summary>
        [TestMethod]
        public void Check_Foo3_WithPositiveAndNegativeArgument()
        {
            int fooRes = Program.Foo(7, -2, 8);
            int foo3Res = Program.Foo3(7, -2, 8);
            Assert.AreEqual(fooRes, foo3Res);
        }

        /// <summary>
        /// Метод проверяет равенство результата работы алгоритма Foo2 результату работы Foo с нечетным числом итераций.
        /// </summary>
        [TestMethod]
        public void Check_Foo3_WithOddIterationsCount()
        {
            int fooRes = Program.Foo(7, -2, 7);
            int foo3Res = Program.Foo3(7, -2, 7);
            Assert.AreEqual(fooRes, foo3Res);
        }

        /// <summary>
        /// Метод проверяет равенство результата работы алгоритма Foo2 результату работы Foo с отрицательным числом итераций.
        /// </summary>
        [TestMethod]
        public void Check_Foo3_WithNegativeIterationsCount()
        {
            int fooRes = Program.Foo(7, -2, -1);
            int foo3Res = Program.Foo3(7, -2, -1);
            Assert.AreEqual(fooRes, foo3Res);
        }
    }
}
