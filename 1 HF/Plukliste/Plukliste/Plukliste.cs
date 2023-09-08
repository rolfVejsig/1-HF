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

            // Setup initial directories and files
            CreateImportDirectoryIfNotExists();
            fileList = GetFileList();

            // Main loop
            while (userKey != 'Q')
            {
                // Display file content to the user
                DisplayFileContent(fileList, ref fileIndex);
                // Display available options to the user
                DisplayOptions(fileIndex, fileList.Count, defaultConsoleColor);

                // Read user input
                userKey = ReadUserInput();
                // Perform action based on user input
                HandleUserInput(ref fileIndex, fileList, userKey, defaultConsoleColor);
            }
        }

        // Create the "import" directory if it does not exist
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

        // Read and return user input, converted to uppercase
        static char ReadUserInput() => char.ToUpper(Console.ReadKey().KeyChar);

        // Display the contents of the current file
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

                // Deserialize and display the contents of the XML file
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
            // Display basic information and items in the list
            Console.WriteLine("\n{0, -13}{1}", "Name:", plukList.Name);
            Console.WriteLine("{0, -13}{1}", "Forsendelse:", plukList.Forsendelse);
            Console.WriteLine("\n{0,-7}{1,-9}{2,-20}{3}", "Antal", "Type", "Produktnr.", "Navn");
            foreach (var item in plukList.Lines)
            {
                Console.WriteLine("{0,-7}{1,-9}{2,-20}{3}", item.Amount, item.Type, item.ProductID, item.Title);
            }
        }

        // Display the available options to the user
        static void DisplayOptions(int currentIndex, int fileCount, ConsoleColor defaultColor)
        {
            // Display menu options based on the current state
            Console.WriteLine("\n\nOptions:");
            PrintColoredText("Q: Quit", ConsoleColor.Green, defaultColor);
            if (currentIndex >= 0) PrintColoredText("A: Afslut plukseddel", ConsoleColor.Green, defaultColor);
            if (currentIndex > 0) PrintColoredText("F: Forrige plukseddel", ConsoleColor.Green, defaultColor);
            if (currentIndex < fileCount - 1) PrintColoredText("N: Næste plukseddel", ConsoleColor.Green, defaultColor);
            PrintColoredText("G: Genindlæs pluksedler", ConsoleColor.Green, defaultColor);
        }

        // Print text in a specified color
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

        // Move the current file to the "import" directory
        static void MoveFileToImportDirectory(string filePath)
        {
            // Prepare destination path and move the file
            string fileName = Path.GetFileName(filePath);
            string destinationPath = Path.Combine("import", fileName);
            File.Move(filePath, destinationPath);
            Console.WriteLine($"Plukseddel {filePath} afsluttet.");
        }
    }
}
