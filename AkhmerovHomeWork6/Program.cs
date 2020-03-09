namespace AkhmerovHomeWork6
{
    using System;
    using Tasks;
    using static System.Console;
    using static Tasks.Task2;

    class Program
    {
        static void Main(string[] args)
        {
            var array = CreateArray(10, 100);
            Write($"\n\nСтандартный массив. Элементов {array.Length}, Состав:\n");

            foreach (var el in array)
            {
                Write("{0} ", el);
            }

            var operArr = CopyArr(array);
            var middleArray = CreateMiddleArray(operArr, new int[operArr.Length]);
            Write($"\n\nСбалансированный массив. Элементов {middleArray.Length}, Состав:\n");

            foreach (var el in middleArray)
            {
                Write("{0} ", el);
            }

            var findTree = new Task2(middleArray);
            WriteLine($"\n\nЭлементов: {middleArray.Length}. Длина дерева поиска: {treeArr.Length}");

            findTree.Start(middleArray);

            var task1 = new Task1();
            WriteLine($"\n{task1.Start("Супер-Пупер проверка хеш-функции")}");

            ReadKey();
        }

        /// <summary>
        /// Создать и заполнить массив с числами
        /// </summary>
        /// <param name="size">Размер массива</param>
        /// <param name="maxValue">Максимальное случайное число</param>
        /// <returns></returns>

        static int[] CreateArray(int size, int maxValue)
        {
            var rnd = new Random();
            var root = new int[size];

            for (var i = 0; i < size; i++)
            {
                root[i] = rnd.Next(1, maxValue + 1);
            }

            return root;
        }

        /// <summary>
        /// Копировать массив
        /// </summary>
        /// <param name="arr">Шаблон массива</param>
        /// <returns></returns>

        static int[] CopyArr(int[] arr)
        {
            var temp = new int[arr.Length];

            for (var i = 0; i < arr.Length; i++)
            {
                temp[i] = arr[i];
            }

            return temp;
        }
    }
}
