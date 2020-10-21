using System;

namespace 整数数组
{
    class Program
    {
        static void Main(string[] args)
        {
            int i,max,min,sum=0,avg;
            int[] a = new int[5];
            for( i = 0; i < a.Length; i++)
            {
                a[i] = i;
            }
            min = max = a[0];
            foreach(int num in a)
            {
                if (num > max) max = num;
                if (num < min) min = num;
                sum += num;
            }
            avg = sum / a.Length;
            Console.WriteLine("最大值：" + max + " 最小值：" + min + " 平均值：" + avg + " 总和：" + sum);
        }
    }
}
