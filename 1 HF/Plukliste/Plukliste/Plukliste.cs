using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Plukliste
{
    class PluklisteProgram
    {
        static void Main()
        {
            // Declare variables
            char userKey = ' ';
            List<string> fileList;
            int fileIndex = -1;
            ConsoleColor defaultConsoleColor = Console.ForegroundColor;

            // Setup directories and files
            CreateImportDirectoryIfNotExists();
            fileList = GetFileList();

            // Main loop
            while (userKey != 'Q')
            {
                // Show file content to the user
                DisplayFileContent(fileList, ref fileIndex);
                // Show available options to the user
                DisplayOptions(fileIndex, fileList.Count, defaultConsoleColor);

                // Read user input and perform actions based on their choice
                userKey = ReadUserInput();
                HandleUserInput(ref fileIndex, fileList, userKey, defaultConsoleColor);
            }
        }

        // Create the import directory if it does not exist
        static void CreateImportDirectoryIfNotExists()
        {
            Directory.CreateDirectory("import");
            if (!Directory.Exists("export"))
            {
                Console.WriteLine("Directory \"export\" not found");
                Console.ReadLine();
                Environment.Exit(1);
            }
        }

        // Fetch the list of files from the "export" directory
        static List<string> GetFileList() => Directory.EnumerateFiles("export").ToList();

        // Convert input to uppercase
        static char ReadUserInput() => char.ToUpper(Console.ReadKey().KeyChar);

        // Show what is in the current file
        static void DisplayFileContent(List<string> fileList, ref int fileIndex)
        {
            // No files found case
            if (fileList.Count == 0)
            {
                Console.WriteLine("No files found.");
            }
            else
            {
                // Display current file index and name
                if (fileIndex == -1) fileIndex = 0;
                Console.WriteLine($"Plukliste {fileIndex + 1} af {fileList.Count}");
                Console.WriteLine($"\nfile: {fileList[fileIndex]}");

                // Show the contents of the XML file
                using (FileStream fileStream = File.OpenRead(fileList[fileIndex]))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Pluklist));
                    Pluklist? plukList = (Pluklist?)serializer.Deserialize(fileStream);

                    if (plukList != null && plukList.Lines != null)
                    {
                        PrintPlukList(plukList);
                    }
                }
            }
        }

        // Print the pluklist details
        static void PrintPlukList(Pluklist plukList)
        {
            // Show  information and items in the list
            Console.WriteLine("\n{0, -13}{1}", "Name:", plukList.Name);
            Console.WriteLine("{0, -13}{1}", "Forsendelse:", plukList.Forsendelse);
            Console.WriteLine("\n{0,-7}{1,-9}{2,-20}{3}", "Antal", "Type", "Produktnr.", "Navn");
            foreach (var item in plukList.Lines)
            {
                Console.WriteLine("{0,-7}{1,-9}{2,-20}{3}", item.Amount, item.Type, item.ProductID, item.Title);
            }
        }

        // Show the available options to the user
        static void DisplayOptions(int currentIndex, int fileCount, ConsoleColor defaultColor)
        {
            // Show menu options 
            Console.WriteLine("\n\nOptions:");
            PrintColoredText("Q: Quit", ConsoleColor.Green, defaultColor);
            if (currentIndex >= 0) PrintColoredText("A: Afslut plukseddel", ConsoleColor.Green, defaultColor);
            if (currentIndex > 0) PrintColoredText("F: Forrige plukseddel", ConsoleColor.Green, defaultColor);
            if (currentIndex < fileCount - 1) PrintColoredText("N: Næste plukseddel", ConsoleColor.Green, defaultColor);
            PrintColoredText("G: Genindlæs pluksedler", ConsoleColor.Green, defaultColor);
        }

        // choose color and print text
        static void PrintColoredText(string text, ConsoleColor color, ConsoleColor defaultColor)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = defaultColor;
        }

        // Handle user actions based on their input
        static void HandleUserInput(ref int currentIndex, List<string> fileList, char userKey, ConsoleColor defaultColor)
        {
            // Perform actions based on user choices
            Console.ForegroundColor = ConsoleColor.Red;
            switch (userKey)
            {
                case 'G':
                    fileList = GetFileList();
                    currentIndex = -1;
                    Console.WriteLine("Pluklister genindlæst");
                    break;
                case 'F':
                    if (currentIndex > 0) currentIndex--;
                    break;
                case 'N':
                    if (currentIndex < fileList.Count - 1) currentIndex++;
                    break;
                case 'A':
                    MoveFileToImportDirectory(fileList[currentIndex]);
                    fileList.RemoveAt(currentIndex);
                    if (currentIndex == fileList.Count) currentIndex--;
                    break;
            }
            Console.ForegroundColor = defaultColor;
        }

        // Move file to import directory
        static void MoveFileToImportDirectory(string filePath)
        {
            // Move the file
            string fileName = Path.GetFileName(filePath);
            string destinationPath = Path.Combine("import", fileName);
            File.Move(filePath, destinationPath);
            Console.WriteLine($"Plukseddel {filePath} afsluttet.");
        }
    }
}
/*
 Æmdringer i forhold til den originale kode:

 Struktur og Opdeling: Koden er opdelt i mindre hjælpemetoder (CreateImportDirectoryIfNotExists, GetFileList, DisplayFileContent, osv.) for at gøre Main() metoden mere læsbar og forståelig.

Variabel Navngivning: Variabelnavne er ændret til at være mere beskrivende. For eksempel er readKey blevet til userKey, og files er blevet til fileList.

Brug af using-Statement: For at sikre, at FileStream frigøres korrekt, er der anvendt et using-statement.

Console Farvehåndtering: Håndteringen af konsolens forgrundsfarve er trukket ud i en separat metode (PrintColoredText), der tager farven som en parameter. Dette gør det lettere at ændre farverne senere.

Brug af Environment.Exit(1): Tilføjet en exit-kode, når "export" mappen ikke findes, hvilket gør det klart, at programmet ikke har fuldført succesfuldt.

File Handling: Filbehandling er nu mere robust, inklusiv korrekt sletning af filer fra listen og flytning af filer til "import" mappen.

Brug af char.ToUpper(): Brug af char.ToUpper() metode for at gøre brugerinput til stort bogstav, i stedet for den oprindelige 'hack'.
*/