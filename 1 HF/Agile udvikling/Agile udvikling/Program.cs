using System;

namespace Agile_udvikling
{
    public class Program
    {
        public static string GenerateMedarbejderID(string fornavn, string efternavn)
        {
            Random random = new Random();
            int randomTal = random.Next(0, 10000);

            string forkortetFornavn = fornavn.Length >= 4 ? fornavn.Substring(0, 4) : fornavn;
            string forkortetEfternavn = efternavn.Length >= 4 ? efternavn.Substring(0, 4) : efternavn;

            return $"{forkortetFornavn.ToUpper()}{forkortetEfternavn.ToUpper()}{randomTal}";
        }

        static void Main(string[] args)
        {
            Console.Write("Indtast dit fornavn: ");
            string fornavn = Console.ReadLine();

            Console.Write("Indtast dit efternavn: ");
            string efternavn = Console.ReadLine();

            string medarbejderID = GenerateMedarbejderID(fornavn, efternavn);

            Console.WriteLine($"Velkommen til: {medarbejderID}");
        }
    }
}
