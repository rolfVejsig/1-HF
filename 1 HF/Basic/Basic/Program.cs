namespace Basic
{
    internal class Program
    {

        //hej igen :)
        static void Main(string[] args)
        {
            //Add two numbers
            //Given three numbers, write a method that adds two first ones and multiplies them by a third one.
            static int addThreeNumbers(int num1, int num2, int num3)
            {
                return (num1 + num2) * num3;
            }

            Console.WriteLine(addThreeNumbers(2, 4, 5));

            //Celsius to Fahrenheit
            //Given a temperature in Celsius degrees, write a method that converts it to Fahrenheit degrees. Remember that temperature below -271.15°C (absolute zero) does not exist!

            static string celsiusToFahrenheit(decimal celsius)
            {

                if (celsius < -271.15m)
                {
                    return "Temperature below absolute zero!";
                }
                else
                {
                    return (celsius * 9 / 5 + 32).ToString();
                }
            }

            Console.WriteLine(celsiusToFahrenheit(0));
            Console.WriteLine(celsiusToFahrenheit(100));
            Console.WriteLine(celsiusToFahrenheit(-300));

            //Elementary operations
            //Given two integers, write a method that returns results of their elementary arithmetic operations: addition, substraction, multiplication, division. Remember that you can't divide any number by 0!

            static decimal[] elementaryArithmetic(decimal num1, decimal num2)
            {
                decimal[] result = new decimal[4];
                result[0] = num1 + num2;
                result[1] = num1 - num2;
                result[2] = num1 * num2;
                if (num1 != 0 && num2 != 0)
                {
                    result[3] = num1 / num2;
                }
                else
                {
                    result[3] = 0;
                }
                return result;
            }

            decimal[] result = elementaryArithmetic(0, 5);
            Console.WriteLine($"{result[0]}, {result[1]}, {result[2]}, {result[3]}");

            //Is result the same
            //Given two different arithmetic operations (addition, subtraction, multiplication, division), write a method that checks if they return the same result.

            static bool isResultTheSame(int num1, int num2)
            {
                if (num1 == num2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            Console.WriteLine(isResultTheSame(2 + 2, 2 * 2));
            Console.WriteLine(isResultTheSame(9 / 3, 16 - 1));


            //Modulo operations
            //Given three integers, write a method that returns first number divided modulo by second one and these divided modulo by third one.
            static int ModuloOperations(int num1, int num2, int num3)
            {
                return (num1 % num2) % num3;
            }

            Console.WriteLine(ModuloOperations(8, 5, 2));

            //The cube of
            //Given a number, write a method that returns its cube.
            static decimal TheCubeOf(decimal num)
            {
                return num * num * num;
            }

            Console.WriteLine(TheCubeOf(2));
            Console.WriteLine(TheCubeOf(-5.5m));

            //Swap two numbers
            //Given two integers, write a method that swaps them using temporary variable.
            static string SwapTwonumbers(int num1, int num2)
            {
                string before = $"before num1 = {num1}, num2 = {num2}";
                int temp = num1;
                num1 = num2;
                num2 = temp;
                string after = $"; after num1 = {num1}, num2 = {num2}";

                return before + after;

            }
            Console.WriteLine(SwapTwonumbers(87, 45));
            Console.WriteLine(SwapTwonumbers(-13, 2));        





        }
    }
}







