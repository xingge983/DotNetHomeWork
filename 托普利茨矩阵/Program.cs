using System;

namespace 托普利茨矩阵
{
    class Program {
        static bool isToeplitzMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < matrix.GetLength(1) - 1; j++)
                {
                    if (matrix[i, j] != matrix[i + 1, j + 1])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        static void Main(string[] args)
        {
            int[,] matrix =
              {
                {1,2,3,4 },
                {5,1,2,3 },
                {9,5,1,2 }
            };
            Console.WriteLine(isToeplitzMatrix(matrix));
        }
    }
}
