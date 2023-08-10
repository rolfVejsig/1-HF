using System;

namespace Odd_Man_Out
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(OddManOut(new int[] {9,3,9,3,9,7,9,}));
        }

        static int OddManOut(int[] A)
        {
            Array.Sort(A);

            for (int i = 0; i < A.Length; i += 2 )
            {
                if (i + 1 == A.Length || A[i] != A[i + 1])
                {
                    return A[i];
                }
            }

            throw new ArgumentException("No odd man out");
        }
    }
}
