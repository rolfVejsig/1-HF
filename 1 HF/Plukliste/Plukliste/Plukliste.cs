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
            char userKey = ' ';
            List<string> fileList;
            int fileIndex = -1;
            ConsoleColor defaultConsoleColor = Console.ForegroundColor;

            CreateImportDirectoryIfNotExists();
            fileList = GetFileList();

            while (userKey != 'Q')
            {
                DisplayFileContent(fileList, ref fileIndex);
                DisplayOptions(fileIndex, fileList.Count, defaultConsoleColor);

                userKey = ReadUserInput();
                HandleUserInput(ref fileIndex, fileList, userKey, defaultConsoleColor);
            }
        }

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

        static List<string> GetFileList() => Directory.EnumerateFiles("export").ToList();

        static char ReadUserInput() => char.ToUpper(Console.ReadKey().KeyChar);

        static void DisplayFileContent(List<string> fileList, ref int fileIndex)
        {
            if (fileList.Count == 0)
            {
                Console.WriteLine("No files found.");
            }
            else
            {
                if (fileIndex == -1) fileIndex = 0;
                Console.WriteLine($"Plukliste {fileIndex + 1} af {fileList.Count}");
                Console.WriteLine($"\nfile: {fileList[fileIndex]}");

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

        static void PrintPlukList(Pluklist plukList)
        {
            Console.WriteLine("\n{0, -13}{1}", "Name:", plukList.Name);
            Console.WriteLine("{0, -13}{1}", "Forsendelse:", plukList.Forsendelse);

            Console.WriteLine("\n{0,-7}{1,-9}{2,-20}{3}", "Antal", "Type", "Produktnr.", "Navn");
            foreach (var item in plukList.Lines)
            {
                Console.WriteLine("{0,-7}{1,-9}{2,-20}{3}", item.Amount, item.Type, item.ProductID, item.Title);
            }
        }

        static void DisplayOptions(int currentIndex, int fileCount, ConsoleColor defaultColor)
        {
            Console.WriteLine("\n\nOptions:");
            PrintColoredText("Q: Quit", ConsoleColor.Green, defaultColor);
            if (currentIndex >= 0)
            {
                PrintColoredText("A: Afslut plukseddel", ConsoleColor.Green, defaultColor);
            }
            if (currentIndex > 0)
            {
                PrintColoredText("F: Forrige plukseddel", ConsoleColor.Green, defaultColor);
            }
            if (currentIndex < fileCount - 1)
            {
                PrintColoredText("N: Næste plukseddel", ConsoleColor.Green, defaultColor);
            }
            PrintColoredText("G: Genindlæs pluksedler", ConsoleColor.Green, defaultColor);
        }

        static void PrintColoredText(string text, ConsoleColor color, ConsoleColor defaultColor)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = defaultColor;
        }

        static void HandleUserInput(ref int currentIndex, List<string> fileList, char userKey, ConsoleColor defaultColor)
        {
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

        static void MoveFileToImportDirectory(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            string destinationPath = Path.Combine("import", fileName);
            File.Move(filePath, destinationPath);
            Console.WriteLine($"Plukseddel {filePath} afsluttet.");
        }
    }
}
