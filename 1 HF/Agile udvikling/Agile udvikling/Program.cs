using System;
using MedarbejderIDLibrary;

namespace Agile_udvikling
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Indtast dit fornavn: ");
            string fornavn = Console.ReadLine();

            Console.Write("Indtast dit efternavn: ");
            string efternavn = Console.ReadLine();

            string medarbejderID = MedarbejderIDGenerator.GenerateMedarbejderID(fornavn, efternavn);

            Console.WriteLine($"Dit medarbejder ID er: {medarbejderID}");
        }
    }
}
