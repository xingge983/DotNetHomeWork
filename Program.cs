using System;

namespace Homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a = new int[100];
            for(int i = 0; i < 100; i++)
            {
                a[i] = 1;
            }
            for(int i = 2; i < 100; i++)
            {
                for(int j = 2; i*j < 100; j++)
                {
                    a[i * j] = 0;
                }
            }

            for(int i = 2; i < 100; i++)
            {
                if (a[i] == 1)
                    Console.WriteLine(i);
            }
        }
    }
}
