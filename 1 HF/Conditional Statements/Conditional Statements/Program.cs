using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Threading;

namespace Conditional_Statements
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Absolute value
            //Given an integer, write a method that returns its absolute value.

            static int AbsoluteValue (int num1)
            {
                return Math.Abs(num1);
            }

            Console.WriteLine(AbsoluteValue(6832));
            Console.WriteLine(AbsoluteValue(-392));

            //Divisible by 2 or 3
            //Given two integers, write a method that returns their multiplication if they are both divisible by 2 or 3, otherwise returns thier sum.
            static int DivisibleBy2Or3(int num1, int num2)
            {
                if ((num1 % 2 == 0 || num1 % 3 == 0) && (num2 % 2 == 0 || num2 % 3 == 0))
                {
                    return num1 * num2;
                }
                else
                {
                    return num1 + num2;
                }
            }

            Console.WriteLine(DivisibleBy2Or3(15, 30));
            Console.WriteLine(DivisibleBy2Or3(2, 90));
            Console.WriteLine(DivisibleBy2Or3(7, 12));

            //If consists of uppercase letters
            //Given a 3 characters long string, write a method that checks if it consists only of uppercase letters.

            static bool IfConsistsOfUppercaseLetters(string str)
            {
                if (str.Length == 3)
                {
                    foreach (char c in str)
                    {
                        if (!Char.IsUpper(c))
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }

            Console.WriteLine(IfConsistsOfUppercaseLetters("xyz"));
            Console.WriteLine(IfConsistsOfUppercaseLetters("DOG"));
            Console.WriteLine(IfConsistsOfUppercaseLetters("L9#"));

            //If greater than third one
            //Given an array of 3 integers, write a method that checks if multiplication or sum of two first numbers is greater than third one excpetec output and input IfGreaterThanThirdOne([2, 7, 12]) == true.

            static bool IfGreaterThanThirdOne(int[] arr)
            {
                if (arr.Length == 3)
                {
                    if (arr[0] * arr[1] > arr[2] || arr[0] + arr[1] > arr[2])
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                } 
            }

            Console.WriteLine(IfGreaterThanThirdOne(new int[] {2,7,12}));
            Console.WriteLine(IfGreaterThanThirdOne(new int[] {-5,-8,50}));

            //If number is even
            //Given an integer, write a method that checks if it is even.

            static bool IfNumberIsEven(int num1)
            {
                if (num1 % 2 == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            Console.WriteLine(IfNumberIsEven(721));
            Console.WriteLine(IfNumberIsEven(1248));

            //If sorted ascending
            //Given an array of three integers, write a method that checks if they are sorted in ascending order.
            static bool IfSortedAscending(int[] arr)
            {
                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i - 1] > arr[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            Console.WriteLine(IfSortedAscending(new int[] {3,7,10}));
            Console.WriteLine(IfSortedAscending(new int[] {74,62,99}));

            //Positive, negative or zero
            //Given a number, write a method that checks if it is positive, negative or zero.

            static string PositiveNegativeOrZero(decimal num1)
            {
                if (num1 > 0)
                {
                    return "positive";
                }
                else if (num1 < 0)
                {
                    return "negative";
                }
                else
                {
                    return "zero";
                }
            }

            Console.WriteLine(PositiveNegativeOrZero(5.24m));
            Console.WriteLine(PositiveNegativeOrZero(0));
            Console.WriteLine(PositiveNegativeOrZero(-994.53m));

            static bool IfYearIsLeap (int year)
            {
                if ((year % 400 == 0) || (year % 4 == 0 && year % 100 != 0))
                {
                    return true;
                }
                else 
                { 
                    return false; 
                }
            }

            Console.WriteLine(IfYearIsLeap(2016));
            Console.WriteLine(IfYearIsLeap(2018));
        }
    }
}