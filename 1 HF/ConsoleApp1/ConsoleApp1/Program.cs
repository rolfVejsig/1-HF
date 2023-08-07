using System.Collections.Generic;
using System;
using System.Data.SqlTypes;
using System.Text;
using System.ComponentModel;
using System.Reflection.Metadata;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Multiplication table
            void Multiplicationtable()
            {
                for (int i = 1; i <= 10; i++)
                {
                    for (int j = 1; j <= 10; j++)
                    {
                        int result = i * j;
                        Console.Write($"{result,-3}"); 
                    }
                    Console.WriteLine(""); 
                }
            }
            Multiplicationtable();

            //The biggest number
            //Given an array of integers, write a method that returns the biggest number in this array.
            static int TheBiggestNumber(int[] arr)
            {
                int arrMax = arr[0];
                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i] > arrMax)
                    {
                        arrMax = arr[i];
                    }
                }
                return arrMax;
            }
            Console.WriteLine("\n");
            Console.WriteLine(TheBiggestNumber(new int[] { 190, 291, 145, 209, 280, 200 }));
            Console.WriteLine(TheBiggestNumber(new int[] { -9, -2, -7, -8, -4 }));

            //Two 7s next to each other
            //Given an array of positive digits, write a method that returns number of times that two 7's are next to each other in an array.
            static int Two7sNextToEachOther(int[] arr)
            {
                int NumberOfSevens = 0;
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (arr[i] == 7 && arr[i + 1] == 7)
                    {
                        NumberOfSevens++;
                    }
                }
                return NumberOfSevens;
            }
            Console.WriteLine("\n");
            Console.WriteLine(Two7sNextToEachOther(new int[] { 8, 2, 5, 7, 9, 0, 7, 7, 3, 1}));
            Console.WriteLine(Two7sNextToEachOther(new int[] { 9, 4, 5, 3, 7, 7, 7, 3, 2, 5, 7, 7 }));

            //Three increasing adjacent
            //Given an array of numbers, write a method that checks if there are three adjacent numbers where second is greater by 1 than the first one and third is greater by 1 than the second one.
            static bool ThreeAdjacentNumbers(int[] arr)
            {
                for (int i = 0; i < arr.Length - 2; i++)
                {
                    if (arr[i] + 1 == arr[i + 1] && arr[i + 1] + 1 == arr[i + 2])
                    {
                        return true;
                    }
                }
                return false;
            }

            Console.WriteLine("\n");
            Console.WriteLine(ThreeAdjacentNumbers(new int[] { 45, 23, 44, 68, 65, 70, 80, 81, 82 }));
            Console.WriteLine(ThreeAdjacentNumbers(new int[] { 7, 3, 5, 8, 9, 3, 1, 4 }));

            //Sieve of Eratosthenes
            //Given an integer n (n>2), write a method that returns prime numbers from range [2, n].
            static void SieveOfEratosthenes(int n)
            {
                bool[] prime = new bool[n + 1];
                for (int i = 0; i < n; i++)
                {
                    prime[i] = true;
                }

                for (int j = 2; j * j <= n; j++)
                {
                    if (prime[j] == true)
                    {
                        for (int i = j * j; i <= n; i += j)
                        {
                            prime[i] = false;
                        }
                    }
                }

                for (int i = 2; i <= n; i++)
                {
                    if (prime[i] == true)
                    {
                        Console.Write(i + " ");
                    }
                }
            }

            Console.WriteLine("\n");
            SieveOfEratosthenes(30);

            //Extract string M
            //Given a string, write a method that returns substring from between two double hash signs (#). expected input ExtractString("##abc##def") expected output "abc"
            static string ExtractString(string str)
            {
                int firstHash = str.IndexOf("##");

                if (firstHash >= 0)
                {
                    int secondHash = str.IndexOf("##", firstHash + 2);

                    if (secondHash >= 0) 
                    {
                        return str.Substring(firstHash + 2, secondHash - firstHash - 2);
                    }
                }
                return string.Empty; 
            }

            Console.WriteLine("\n");
            Console.WriteLine(ExtractString("##abc##def"));
            Console.WriteLine(ExtractString("12####78"));
            Console.WriteLine(ExtractString("gar##d#en"));
            Console.WriteLine(ExtractString("++##--##++"));

            //Full sequence of letters M
            //Given a string of two letters, where first one occurs before the second in the alphabet, write a method that returns full sequence of letters starting from first and ending at the second one.
            static string FullSequenceOfLetters(string str)
            {
                string result = string.Empty;
                for (char i = str[0]; i <= str[1]; i++)
                {
                    result += i;
                }
                return result;
            }

            Console.WriteLine("\n");
            Console.WriteLine(FullSequenceOfLetters("ds"));
            Console.WriteLine(FullSequenceOfLetters("or"));

            //Sum and average
            //Given two integers n and m(n <= m), make a method that returns sum of all integers and average from range[n, m].
            static void SumAndAverage(decimal n, decimal m)
            {
                decimal sum = 0;
                decimal count = 0;
                for (decimal i = n; i <= m; i++)
                {
                    sum += i;
                    count++;
                }
                Console.WriteLine($"Sum: {sum} Average: {sum / count}");
            }

            Console.WriteLine("\n");
            SumAndAverage(11, 66);
            SumAndAverage(-10, 0);

            //Draw triangle
            //Write a method that draws triangle shape like below.
            static void DrawTriangle()
            {
                for (int i = 1; i <= 19; i++)
                {
                    for (int j = 1; j <= i; j++)
                    {
                        Console.Write("*");
                    }
                    Console.WriteLine("");
                }
            }

            Console.WriteLine("\n");
            DrawTriangle();
        }
    }
}