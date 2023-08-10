namespace Terningespil
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            Console.Write("Number of dice: ");
            int numDice = int.Parse(Console.ReadLine());

            int throws = 0;
            while (true)
            {
                throws++;
                int[] diceRolls = new int[numDice];
                for (int i = 0; i < numDice; i++)
                {
                    diceRolls[i] = random.Next(1, 7);
                }

                bool allSixes = true;
                foreach (int result in diceRolls)
                {
                    if (result != 6)
                    {
                        allSixes = false;
                        break;
                    }
                }

                if (allSixes)
                {
                    Console.WriteLine($"It took {throws} throws");
                    break;
                }
            }
        }
    }
}
