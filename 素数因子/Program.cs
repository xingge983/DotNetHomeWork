using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 素数因子
{
    class Program
    {
        static int Input()
        {
            Console.Write("输入数据：");
            return int.Parse(Console.ReadLine());
        }

        static bool isPrime(int i)
        {
            for(int j = 2; j < i/2; j++)
            {
                if (i % j == 0) return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            int i = Input();
            if (isPrime(i))
            {
                Console.WriteLine(i);
            }
            else
            {
                if (i % 2 == 0) Console.WriteLine(2);
                for(int j = 3; j <= i / 2; j+=2)
                {
                    if (i % j == 0)
                    {
                        i /= j;
                        Console.WriteLine(j);
                    }
                }

            }
        }
    }
}
