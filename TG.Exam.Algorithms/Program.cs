﻿using System;
using System.Diagnostics;
using System.Linq;

namespace TG.Exam.Algorithms
{
    public static class Program
    {
        /// <summary>
        /// Данный метод осуществляет рекурсивное накопление суммы путем сложения входных аргументов с-1 раз.
        /// С каждым следующим вызовом сумма накапливается путем передачи её значения во второй аргумент (b), а старое значение
        /// второго аргумента передаётся в первый аргумент (a). Таким образом, получается сдвиговое накопление:
        /// 7 + 2
        ///     2 + 9 (пред. сумма 7 и 2)
        ///         9 + 11 (пред. сумма 2 и 9)
        ///             11 + 20 (пред. сумма 11 и 9)
        ///                  20 + 31  (пред. сумма 11 и 20)
        ///                       51 + 31 = 82 результат.
        /// Плюсы: читаемость для программиста.
        /// Минусы: при увеличении числа итераций будет переполнение стека + хранение в памяти состояния рекурсивных вызовов.
        /// </summary>
        /// <param name="a">Начальное значение первого слагаемого. </param>
        /// <param name="b">Начальное значение второго слагаемого. </param>
        /// <param name="c">Число итераций. </param>
        /// <returns></returns>
        public static int Foo(int a, int b, int c)
        {
            if (1 < c)
                return Foo(b, b + a, c - 1);
            else
                return a;
        }

        /// <summary>
        ///  Данная реализация осуществляет то же самое, что и метод Foo, тем же самым способом, но только с использованием цикла.
        ///  Вместо swap'а значений использовано вычитание.
        ///  Плюсы: алгоритм работает без исключений при большом числе итераций. Экономия памяти,
        ///  т.к. не нужно запоминать контекст предыдущих рекурсивных вызовов.
        ///  Минусы: можно уменьшить число итераций, пожертвовав читаемостью кода.
        /// </summary>
        /// <param name="a">Начальное значение первого слагаемого. </param>
        /// <param name="b">Начальное значение второго слагаемого. </param>
        /// <param name="c">Число итераций. </param>
        /// <returns></returns>
        public static int Foo2(int a, int b, int c)
        {   
            for(int i = 1; i < c; i++)
            {
               b =  a + b;
               a = b - a;
            }
            return a;
        }

        /// <summary>
        /// Реализация с уменьшением числа итераций. Рассмотрим, как строится сумма в алгоритме Foo
        /// 7 + 2
        ///     2 + 9 (пред. сумма 7 и 2)
        ///     ---------------------------
        ///         9 + 11 (пред. сумма 2 и 9)
        ///             11 + 20 (пред. сумма 11 и 9)
        ///             -----------------
        ///                  20 + 31  (пред. сумма 11 и 20)
        ///                       31 + 51 = 82 результат.
        /// Можно разложить на подсуммы:
        /// 7 + 2 + 2   
        /// 9 + 11 + 11 
        /// 20 + 31 + 31 
        /// Видно, что одно число участвует в алгоритме дважды:
        /// 1) когда это число появляется во втором аргументе в виде суммы предыдущих двух чисел;
        /// 2) после свапа оно вновь используется при вычислениях уже в первом аргументе.
        /// 
        /// Это можно использовать, уменьшив число итераций вдвое.
        /// Можно перескакивать между итерациями через одну путём умножения b на 2.
        /// Получаем:
        /// b = (7 + 2) + 2  <==> (a + 2*b)
        /// в b лежит 11. Для следующей итерации необходимо получить 9.
        /// (b-a)/2 + a == 9.
        /// И т.д.
        /// 
        /// Таким образом значение накопленной суммы для четных итераций лежит в переменной b,
        /// а для нечетных лежит в переменной а.
        /// 
        /// Плюсы: скорость (вдвое меньше итераций). Нет проигрыша по памяти.
        /// Минусы: низкая читаемость кода для программиста.
        /// </summary>
        /// <param name="a">Начальное значение первого слагаемого. </param>
        /// <param name="b">Начальное значение второго слагаемого. </param>
        /// <param name="c">Число итераций. </param>
        /// <returns></returns>
        public static int Foo3(int a, int b, int c)
        {
            int odd = c % 2;
            int iterationCount = c / 2 + odd;

            for (int i = 1; i < iterationCount; i++)
            {
                b = a + 2 * b;
                a = (b - a) / 2 + a;
            }

            return odd == 0 ? b : a;
        }

        /// <summary>
        /// Алгоритм сортирует массив по возрастанию.
        /// Плюсы: просто написать.
        /// Минусы:  Большая сложность алгоритма. 
        /// </summary>
        /// <param name="arr">Входной массив.</param>
        /// <returns>Отсортироанный массив. </returns>
        public static int[] Bar(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                for (int j = 0; j < arr.Length - 1; j++)
                    if (arr[j] > arr[j + 1])
                    {
                        int t = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = t;
                    }
            return arr;
        }

        /// <summary>
        /// В качестве альтернативы предлагаю встроенную сортировку, т.к. она оптимизирована.
        /// </summary>
        /// <param name="arr">Входной массив.</param>
        /// <returns>Отсортированный массив.</returns>
        public static int[] Bar2(int [] arr)
        {
           Array.Sort(arr);
           return arr;
        }

        public static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();
            Console.WriteLine("Foo result: {0}", Foo(7, 2, 10000));
            stopWatch.Stop();
            Console.WriteLine("Foo RunTime " + stopWatch.ElapsedTicks + " ticks");

            stopWatch.Restart();
            Console.WriteLine("Foo2 result: {0}", Foo2(7, 2, 10000));
            stopWatch.Stop();
            Console.WriteLine("Foo2 RunTime " + stopWatch.ElapsedTicks + " ticks");

            stopWatch.Restart();
            Console.WriteLine("Foo3 result: {0}", Foo3(7, 2, 10000));
            stopWatch.Stop();
            Console.WriteLine("Foo3 RunTime " + stopWatch.ElapsedTicks + " ticks");

            Console.WriteLine("Bar result: {0}", string.Join(", ", Bar(new[] { 7, 2, 8 })));

            Console.ReadKey();
        }
    }
}
