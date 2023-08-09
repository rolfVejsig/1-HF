namespace Strings
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Add separator
            //Given a string and a separator, write a method that adds separator between each adjacent characters in a string
            static string AddSeparator(string str, string separator)
            {
                string result = "";
                for (int i = 0; i < str.Length; i++)
                {
                    result += str[i];
                    if (i != str.Length - 1)
                    {
                        result += separator;
                    }
                }
                return result;
            }

            Console.WriteLine(AddSeparator("ABCD", "^"));
            Console.WriteLine(AddSeparator("chocolate", "-"));

            //Is palindrome
            //Given a string, write a method that checks if it is a palindrome (is read the same backward as forward). Assume that string may consist only of lower-case letters.
            static bool IsPalindrome(string str)
            {
                for (int i = 0; i < str.Length / 2; i++)
                {
                    if (str[i] != str[str.Length - 1 - i])
                    {
                        return false;
                    }
                }
                return true;
            }

            Console.WriteLine("\n");
            Console.WriteLine(IsPalindrome("eye"));
            Console.WriteLine(IsPalindrome("home"));

            //Length of string
            //Given a string, write a method that returns its length. Do not use library methods
            static int LengthOfAString(string str)
            {
                int length = 0;
                foreach (char c in str)
                {
                    length++;
                }
                return length;
            }

            Console.WriteLine("\n");
            Console.WriteLine(LengthOfAString("computer"));
            Console.WriteLine(LengthOfAString("ice cream"));

            //String in reverse order
            //Given a string, write a method that returns that string in reverse order.
            static string StringInReverseOrder(string str)
            {
                string result = "";
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    result += str[i];
                }
                return result;
            }

            Console.WriteLine(StringInReverseOrder("qwerty"));
            Console.WriteLine(StringInReverseOrder("oe93 kr"));

            //Number of words
            //Given a string, write a method that counts its number of words. Assume there are no leading and trailing whitespaces and there is only single whitespace between two consecutive words.
            static int NumberOfWords(string str)
            {
                int count = 0;
                bool isWord = false;
                foreach (char c in str)
                {
                    if (c != ' ' && !isWord)
                    {
                        isWord = true;
                        count++;
                    }
                    else if (c == ' ')
                    {
                        isWord = false;
                    }
                }
                return count;
            }

            Console.WriteLine("\n");
            Console.WriteLine(NumberOfWords("This is sample sentence"));
            Console.WriteLine(NumberOfWords("OK"));

            //Revert words order
            //Given a string, write a method that returns new string with reverted words order. Pay attention to the punctuation at the end of the sentence (period)
            static string RevertWordsOrder(string str)
            {
                string result = "";
                string word = "";
                for (int i = str.Length - 1; i >= 0; i--)
                {
                    if (str[i] != ' ')
                    {
                        word = str[i] + word;
                    }
                    else
                    {
                        result += word + " ";
                        word = "";
                    }
                }
                result += word;
                return result;
            }

            Console.WriteLine("\n");
            Console.WriteLine(RevertWordsOrder("John Doe."));
            Console.WriteLine(RevertWordsOrder("A, B. C"));

            //How many occurrences
            //Given a string and substring, write a method that returns number of occurrences of substring in the string. Assume that both are case-sensitive. You may need to use library function here.
            static int HowManyOccurrences(string str, string substr)
            {
                int count = 0;
                for (int i = 0; i < str.Length - substr.Length + 1; i++)
                {
                    if (str.Substring(i, substr.Length) == substr)
                    {
                        count++;
                    }
                }
                return count;
            }

            Console.WriteLine("\n");
            Console.WriteLine(HowManyOccurrences("do it now", "do"));
            Console.WriteLine(HowManyOccurrences("empty", "d"));

            //Sort characters descending
            //Given a string, write a method that returns array of chars (ASCII characters) sorted in descending order.
            static char[] SortCharactersDescending(string str)
            {
                char[] chars = str.ToCharArray();
                Array.Sort(chars);
                Array.Reverse(chars);
                return chars;
            }

            Console.WriteLine("\n");
            Console.WriteLine(SortCharactersDescending("onomatopoeia"));
            Console.WriteLine(SortCharactersDescending("fohjwf42os"));

            //Compress string
            //Given a non-empty string, write a method that returns it in compressed format.
            static string CompressString(string str)
            {
                string result = "";
                int count = 1;
                for (int i = 0; i < str.Length - 1; i++)
                {
                    if (str[i] == str[i + 1])
                    {
                        count++;
                    }
                    else
                    {
                        result += str[i] + count.ToString();
                        count = 1;
                    }
                }
                result += str[str.Length - 1] + count.ToString();
                return result;
            }

            Console.WriteLine("\n");
            Console.WriteLine(CompressString("kkkktttrrrrrrrrrr"));
            Console.WriteLine(CompressString("p555ppp7www"));
        }
    }
}