using System.Linq;

namespace AkhmerovHomeWork6.Tasks
{
    using System;
    using static System.Console;

    class Task2
    {
        public static int[] treeArr;
        private static bool access;

        public Task2(int[] elements)
        {
            treeArr = CreateTree(elements);
        }

        /// <summary>
        /// Стартовый метод
        /// </summary>
        /// <param name="array">Массив чисел</param>

        public void Start(int[] array)
        {
            var rnd = new Random();
            foreach (var el in array)
            {
                Insert(el);
            }

            PrintTree(1);

            WriteLine("\n\nОбход дерева (Левый, Корень, Правый):");
            BypassLeRoRi(1);
            WriteLine("\n\nОбход дерева (Правый, Корень, Левый):");
            BypassRiRoLe(1);
            WriteLine("\n\nОбход дерева (Корень, Левый, Правый):");
            BypassRoLeRi(1);
            WriteLine("\n\nОбход дерева (Корень, Правый, Левый):");
            BypassRoRiLe(1);

            var searchNum = array[rnd.Next(array.Length)];
            WriteLine("\n\nПоиск числа {0}", searchNum);
            Search(1, searchNum);

            searchNum = WrongNum(array);
            WriteLine("\nПоиск числа {0}", searchNum);
            Search(1, searchNum);
        }

        /// <summary>
        /// Создание массива дерева.
        /// </summary>
        /// <param name="array">Массив с элементами, необходимыми для добавления в дерево поика</param>
        /// <returns></returns>

        static int[] CreateTree(int[] array)
        {
            var size = 1;

            for (; size <= array.Length;)
            {
                size *= 2;
            }

            size *= 2;

            var temp = new int[size];

            for (var i = 0; i < size; i++)
            {
                temp[i] = -1;
            }

            return temp;
        }

        /// <summary>
        /// Вставка узла
        /// </summary>
        /// <param name="el">Вставляемый элемент</param>
        /// <param name="arr">Массив дерева поиска</param>

        static void Insert(int el)
        {
            var index = 1;

            if (treeArr[index] == -1)
            {
                treeArr[index] = el;
                return;
            }

            while (treeArr[index] != -1)
            {
                index = el <= treeArr[index] ? index * 2 : index * 2 + 1;
            }

            treeArr[index] = el;
        }


        #region Распечатка и обходы дерева

        /// <summary>
        /// Распечатка дерева поиска
        /// </summary>
        /// <param name="i">Индекс элемента</param>

        public void PrintTree(int i)
        {
            if (Check(i))
            {
                Write($"{treeArr[i]}");

                if (Check(2 * i) || Check(2 * i + 1))
                {
                    Write("(");

                    if (Check(2 * i))
                    {
                        PrintTree(2 * i);
                    }
                    else
                    {
                        Write("NULL");
                    }

                    Write(",");

                    if (Check(2 * i + 1))
                    {
                        PrintTree(2 * i + 1);
                    }
                    else
                    {
                        Write("NULL");
                    }

                    Write(")");
                }
            }
        }

        /// <summary>
        /// Обход дерева (Левый, Корень, Правый)
        /// </summary>
        /// <param name="i">Индекс</param>

        static void BypassLeRoRi(int i)
        {
            if (Check(i))
            {
                if (Check(2 * i))
                {
                    BypassLeRoRi(2 * i);
                }

                Write($"{treeArr[i]} ");

                if (Check(2 * i + 1))
                {
                    BypassLeRoRi(2 * i + 1);
                }
            }
        }

        /// <summary>
        /// Обход дерева (Правый, Корень, Левый)
        /// </summary>
        /// <param name="i">Индекс</param>

        static void BypassRiRoLe(int i)
        {
            if (Check(i))
            {
                if (Check(2 * i + 1))
                {
                    BypassRiRoLe(2 * i + 1);
                }

                Write($"{treeArr[i]} ");

                if (Check(2 * i))
                {
                    BypassRiRoLe(2 * i);
                }
            }
        }

        /// <summary>
        /// Обход дерева (Корень, Левый, Правый)
        /// </summary>
        /// <param name="i">Индекс</param>

        static void BypassRoLeRi(int i)
        {
            if (Check(i))
            {
                Write($"{treeArr[i]} ");

                if (Check(2 * i) || Check(2 * i + 1))
                {
                    if (Check(2 * i))
                    {
                        BypassRoLeRi(2 * i);
                    }

                    if (Check(2 * i + 1))
                    {
                        BypassRoLeRi(2 * i + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Обход дерева (Корень, Правый, Левый)
        /// </summary>
        /// <param name="i">Индекс</param>

        static void BypassRoRiLe(int i)
        {
            if (Check(i))
            {
                Write($"{treeArr[i]} ");

                if (Check(2 * i) || Check(2 * i + 1))
                {
                    if (Check(2 * i + 1))
                    {
                        BypassRoRiLe(2 * i + 1);
                    }

                    if (Check(2 * i))
                    {
                        BypassRoRiLe(2 * i);
                    }
                }
            }
        }

        #endregion

        #region Оптимизация дерева

        /// <summary>
        /// Создание сбалансированного массива для оптимального создания дерева поиска
        /// </summary>
        /// <param name="arr">Массив с числами</param>
        /// <param name="midArr">Оптимизированный масси</param>
        /// <returns></returns>

        public static int[] CreateMiddleArray(int[] arr, int[] midArr)
        {
            var mid = GetMiddleNumber(arr);

            var minCount = 0;
            var maxCount = 0;
            var count = 0;

            GetMinMaxCount(ref minCount, ref maxCount, arr, mid);

            midArr[count] = mid;
            count++;

            var minArr = GetMinHalfArr(arr, minCount);
            
            var maxArr = GetMaxHalfArr(arr, maxCount);
            

            while (true)
            {
                if (minArr.Length > 0)
                {
                    var minMid = minArr.Length != 1 ? GetMiddleNumber(minArr) : minArr[0];

                    midArr[count] = minMid;

                    if (arr.Length >= 1)
                    {
                        minArr = CutArr(minArr, minMid);
                    }
                    else
                    {
                        return midArr;
                    }

                    count++;
                    if (count == midArr.Length)
                    {
                        return midArr;
                    }
                }

                if (maxArr.Length <= 0) continue;

                var maxMid = maxArr.Length != 1 ? GetMiddleNumber(maxArr) : maxArr[0];

                midArr[count] = maxMid;

                if (arr.Length >= 1)
                {
                    maxArr = CutArr(maxArr, maxMid);
                }
                else
                {
                    return midArr;
                }

                count++;

                if (count == midArr.Length)
                {
                    return midArr;
                }
            }
        }

        /// <summary>
        /// Получить среднее число
        /// </summary>
        /// <param name="arr">Массив с числами</param>
        /// <returns></returns>

        private static int GetMiddleNumber(int[] arr)
        {
            var temp = CopyArr(arr);
            var num = 0;

            for (var i = 0; i < arr.Length; i += 2)
            {
                var min = GetMinNum(temp);

                if (temp.Length != 1)
                {
                    temp = CutArr(temp, min);
                }
                else
                {
                    return min;
                }
                var max = GetMaxNum(temp);

                if (temp.Length != 1)
                {
                    temp = CutArr(temp, max);
                }
                else
                {
                    return max;
                }

                num = max;
            }

            return num;
        }

        /// <summary>
        /// Получить количество минимальных и максимальных элементов отталкиваясь от среднего числа
        /// </summary>
        /// <param name="minCount">Количество минимальных чисел</param>
        /// <param name="maxCount">Количество максимальных чисел</param>
        /// <param name="arr">Массив с числами</param>
        /// <param name="mid">Среднее число</param>

        static void GetMinMaxCount(ref int minCount, ref int maxCount, int[] arr, int mid)
        {
            foreach (var el in arr)
            {
                if (el <= mid)
                {
                    minCount++;
                }
            }

            if (minCount > 0)
            {
                minCount--;
            }

            foreach (var el in arr)
            {
                if (el > mid)
                {
                    maxCount++;
                }
            }

            if (minCount == 0 || maxCount == 0)
            {
                var stop = true;
            }
        }

        /// <summary>
        /// Получить массив минимальных чисел, отталкиваясь от среднего числа (поделить пополам)
        /// </summary>
        /// <param name="arr">Массив с числами</param>
        /// <param name="minCount">Количество минимальных чисел</param>
        /// <returns></returns>

        private static int[] GetMinHalfArr(int[] arr, int minCount)
        {
            var temp = new int[minCount];
            var tempArr = CopyArr(arr);

            for (var i = 0; i < minCount; i++)
            {
                var min = GetMinNum(tempArr);
                temp[i] = min;
                tempArr = CutArr(tempArr, min);
            }

            return temp;
        }

        /// <summary>
        /// Получить массив из максимальных чисел, отталкиваясь от среднего числа (поделить пополам)
        /// </summary>
        /// <param name="arr">Массив чисел</param>
        /// <param name="maxCount">Количество максимальных чисел</param>
        /// <returns></returns>

        static int[] GetMaxHalfArr(int[] arr, int maxCount)
        {
            var temp = new int[maxCount];
            var tempArr = CopyArr(arr);
            
            for (var i = 0; i < maxCount; i++)
            {
                var max = GetMaxNum(tempArr);
                temp[i] = max;
                tempArr = CutArr(tempArr, max);
            }

            return temp;
        }

        #endregion

        #region Вспомогательные методы

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

        /// <summary>
        /// Вырезать элемент из массива
        /// </summary>
        /// <param name="arr">Обрезаемый массив</param>
        /// <param name="num">Элемент, который необходимо вырезать</param>
        /// <returns></returns>

        static int[] CutArr(int[] arr, int num)
        {
            var temp = new int[arr.Length - 1];
            var count = 0;


            foreach (var el in arr)
            {
                if (el == num) continue;

                temp[count] = el;

                count++;
            }

            return temp;
        }

        /// <summary>
        /// Получить минимальное число в массиве
        /// </summary>
        /// <param name="arr">Массив</param>
        /// <returns></returns>

        static int GetMinNum(int[] arr)
        {
            var min = -1;

            foreach (var el in arr)
            {
                if (min < 0)
                {
                    min = el;
                }

                if (min >= el)
                {
                    min = el;
                }
            }
            
            return min;
        }

        /// <summary>
        /// Получить максимальное число в массиве
        /// </summary>
        /// <param name="arr">Массив</param>
        /// <returns></returns>

        static int GetMaxNum(int[] arr)
        {
            var max = -1;

            foreach (var el in arr)
            {
                if (max < 0)
                {
                    max = el;
                }

                if (max <= el)
                {
                    max = el;
                }
            }

            return max;
        }

        #endregion

        #region Поиск числа

        /// <summary>
        /// Поиск в дереве
        /// </summary>
        /// <param name="i">Индекс</param>
        /// <param name="num">Число, которое необходимо найти</param>

        static void Search(int i, int num)
        {
            if (i == 1 && access)
            {
                access = false;
            }

            if (num == treeArr[i])
            {
                WriteLine("Число найдено! Индекс числа: {0}", i);
                access = true;
                return;
            }

            if (Check(i))
            {
                if (Check(2 * i) || Check(2 * i + 1))
                {
                    if (num <= treeArr[i])
                    {
                        Search(2 * i, num);
                    }
                    else
                    {
                        Search(2 * i + 1, num);
                    }
                }
            }

            if (i == 1 && !access)
            {
                WriteLine("Число отсутствует!\n");
            }



        }

        /// <summary>
        /// Составление отсутствующего числа для проверки поиска
        /// </summary>
        /// <param name="array">Массив</param>
        /// <returns></returns>

        static int WrongNum(int[] array)
        {
            var rnd = new Random();
            var cont = true;
            var flag = false;
            int searchNum = 0;
            while (cont)
            {
                searchNum = rnd.Next(1, 101);
                for (var i = 0; i < array.Length; i++)
                {
                    if (searchNum == array[i])
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag)
                {
                    flag = false;
                    continue;
                }

                cont = false;
            }

            return searchNum;
        }

        #endregion

        static bool Check(int i) => i < treeArr.Length && treeArr[i] != -1;
    }
}
