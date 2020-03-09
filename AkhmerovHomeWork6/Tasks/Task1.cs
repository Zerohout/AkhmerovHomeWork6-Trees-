using System.Threading;

namespace AkhmerovHomeWork6.Tasks
{
    using System;
    using static System.Console;

    class Task1
    {
        public int Start(string str)
        {
            if (str == null)
            {
                Write("Введите строку: ");
                str = ReadLine();
            }

            var count = 1;
            var temp2 = 0;

            foreach (var s in str)
            {
                var temp1 = s * count;
                if (temp2 == 0)
                {
                    temp2 = temp1 >> count;
                }
                else
                {
                    temp2 = temp2 * (s%str.Length) + (temp1 >> count);
                }

                count++;
            }

            Write("Результат Хеш-функции: ");

            return temp2;
        }
    }
}
