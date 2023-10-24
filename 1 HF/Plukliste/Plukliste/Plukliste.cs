using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using CsvRecord;


namespace Plukliste
{
    class PluklisteProgram
    {
        const string OpgadeVejledningTemplatePath = @"C:\Users\HFGF\Downloads\templates\PRINT-OPGRADE.html";
        const string OpsigelseVejledningTemplatePath = @"C:\Users\HFGF\Downloads\templates\PRINT-OPSIGELSE.html";
        const string WelcomeVejledningTemplatePath = @"C:\Users\HFGF\Downloads\templates\PRINT-WELCOME.html";
        const string OutputDirectory = @"C:\Users\HFGF\Downloads\Vejledninger";

        static void Main()
        {
            const string importDirectory = "import";
            const string exportDirectory = @"C:\Users\HFGF\Downloads\test filer 2";

            char userInput = ' ';
            List<string> files = GetExportedFiles(exportDirectory);
            int currentIndex = -1;

            if (!Directory.Exists(exportDirectory))
            {
                Console.WriteLine("Directory \"export\" not found");
                Console.ReadLine();
                return;
            }

            Directory.CreateDirectory(importDirectory);

            while (userInput != 'Q')
            {
                if (files.Count == 0)
                {
                    Console.WriteLine("No files found.");
                }
                else
                {
                    if (currentIndex == -1) currentIndex = 0;
                    PrintPlukliste(files[currentIndex]);
                }

                PrintOptions(currentIndex, files.Count);
                userInput = Console.ReadKey().KeyChar;
                HandleUserInput(ref files, ref currentIndex, userInput, importDirectory);
                Console.Clear();
            }
        }

        static List<string> GetExportedFiles(string directoryPath)
        {
            return Directory.EnumerateFiles(directoryPath).ToList();
        }

        static void PrintPlukliste(string filePath)
        {
            if (Path.GetExtension(filePath).Equals(".xml", StringComparison.OrdinalIgnoreCase))
            {
                // Handle XML file
                using (FileStream file = File.OpenRead(filePath))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer =
                        new System.Xml.Serialization.XmlSerializer(typeof(Pluklist));
                    var plukliste = xmlSerializer.Deserialize(file) as Pluklist;

                    if (plukliste != null && plukliste.Lines != null)
                    {
                        Console.WriteLine($"\nfile: {filePath}");
                        Console.WriteLine($"\nName: {plukliste.Name}");
                        Console.WriteLine($"Forsendelse: {plukliste.Forsendelse}");
                        Console.WriteLine("\n{0,-7}{1,-9}{2,-20}{3}", "Antal", "Type", "Produktnr.", "Navn");
                        foreach (var item in plukliste.Lines)
                        {
                            Console.WriteLine("{0,-7}{1,-9}{2,-20}{3}", item.Amount, item.Type, item.ProductID, item.Title);
                        }
                    }
                }
            }
            else if (Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                // Handle CSV file
                var records = ReadCsvFile(filePath);
                Console.WriteLine($"\nfile: {filePath}");
                Console.WriteLine("\n{0,-7}{1,-9}{2,-20}{3}", "Antal", "Type", "Produktnr.", "Navn");
                foreach (var record in records)
                {
                    Console.WriteLine("{0,-7}{1,-9}{2,-20}{3}", record.Amount, record.Type, record.ProductId, record.Description);
                }
            }
            else
            {
                Console.WriteLine($"Unsupported file format: {filePath}");
            }
        }


        static void PrintOptions(int currentIndex, int totalFiles)
        {
            Console.WriteLine($"\nPlukliste {currentIndex + 1} af {totalFiles}");
            Console.WriteLine("\nOptions:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Q)uit");
            if (currentIndex >= 0)
            {
                Console.WriteLine("(A) Afslut plukseddel");
            }
            if (currentIndex > 0)
            {
                Console.WriteLine("(F) Forrige plukseddel");
            }
            if (currentIndex < totalFiles - 1)
            {
                Console.WriteLine("(N) Næste plukseddel");
            }
            Console.WriteLine("(G) Genindlæs pluksedler");
            Console.WriteLine("\n(P) print alle vejledninger");
            Console.ForegroundColor = Console.ForegroundColor;
        }

        static void HandleUserInput(ref List<string> files, ref int currentIndex, char userInput, string importDirectory)
        {
            switch (char.ToUpper(userInput))
            {
                case 'G':
                    files = GetExportedFiles(@"C:\Users\HFGF\Downloads\test filer 2");
                    currentIndex = -1;
                    Console.WriteLine("Pluklister genindlæst");
                    break;
                case 'F':
                    if (currentIndex > 0) currentIndex--;
                    break;
                case 'N':
                    if (currentIndex < files.Count - 1) currentIndex++;
                    break;
                case 'A':
                    if (currentIndex >= 0 && currentIndex < files.Count)
                    {
                        MoveAndCompletePlukliste(files[currentIndex], importDirectory);
                        files.Remove(files[currentIndex]);
                        if (currentIndex == files.Count) currentIndex--;
                    }
                    break;
                case 'P':
                    GenerateAndPrintVejledninger(files);
                    break;

            }
        }

        static string GetVejledningTemplate(Pluklist plukliste)
        {
            foreach (var line in plukliste.Lines)
            {
                if (line.Type == ItemType.Print)
                {
                    string[] splitProductID = line.ProductID.Split('-');
                    if (splitProductID.Length > 1)
                    {
                        string vejledningType = splitProductID[1].ToUpper();
                        switch (vejledningType)
                        {
                            case "OPGRADE":
                                return File.ReadAllText(OpgadeVejledningTemplatePath);
                            case "OPSIGELSE":
                                return File.ReadAllText(OpsigelseVejledningTemplatePath);
                            case "WELCOME":
                                return File.ReadAllText(WelcomeVejledningTemplatePath);
                        }
                    }
                }
            }

            throw new InvalidOperationException("Ingen passende vejledningstype fundet.");
        }

        static List<Pluklist> ReadDataFromFile(string filePath)
        {
            List<Pluklist> pluklists = new List<Pluklist>();

            if (Path.GetExtension(filePath).Equals(".xml", StringComparison.OrdinalIgnoreCase))
            {
                // Read data from XML file
                using (FileStream file = File.OpenRead(filePath))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer =
                        new System.Xml.Serialization.XmlSerializer(typeof(Pluklist));
                    var pluklist = xmlSerializer.Deserialize(file) as Pluklist;
                    if (pluklist != null)
                    {
                        pluklists.Add(pluklist);
                    }
                }
            }
            else if (Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                // Read data from CSV file
                var records = ReadCsvFile(filePath);
                foreach (var record in records)
                {
                    Pluklist pluklist = new Pluklist
                    {
                        Name = record.ProductId,
                        Forsendelse = record.Type,
                        Adresse = record.Description
                    };

                    Item line = new Item
                    {
                        ProductID = record.ProductId,
                        Title = record.Description,
                        Type = (ItemType)Enum.Parse(typeof(ItemType), record.Type),
                        Amount = record.Amount
                    };

                    pluklist.AddItem(line);
                    pluklists.Add(pluklist);
                }
            }
            else
            {
                Console.WriteLine($"Unsupported file format: {filePath}");
            }

            return pluklists;
        }

        static List<CsvRecord.Record> ReadCsvFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            }))
            {
                csv.Context.RegisterClassMap<RecordMap>(); 
                return csv.GetRecords<CsvRecord.Record>().ToList();
            }
        }


        static void GenerateAndPrintVejledninger(List<string> files)
        {
            foreach (var filePath in files)
            {
                List<Pluklist> pluklists;
                if (Path.GetExtension(filePath).Equals(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    using (FileStream file = File.OpenRead(filePath))
                    {
                        System.Xml.Serialization.XmlSerializer xmlSerializer =
                            new System.Xml.Serialization.XmlSerializer(typeof(Pluklist));
                        var pluklist = xmlSerializer.Deserialize(file) as Pluklist;
                        pluklists = new List<Pluklist> { pluklist };
                    }
                }
                else if (Path.GetExtension(filePath).Equals(".csv", StringComparison.OrdinalIgnoreCase))
                {
                    pluklists = ReadDataFromFile(filePath);
                }
                else
                {
                    Console.WriteLine($"Unsupported file format: {filePath}");
                    continue;
                }

                foreach (var plukliste in pluklists)
                {
                    bool vejledningGenereret = false;

                    foreach (var line in plukliste.Lines)
                    {
                        if (line.Type == ItemType.Print)
                        {
                            string vejledningHtmlTemplate = GetVejledningTemplate(plukliste);
                            string address = plukliste.Adresse;
                            string name = plukliste.Name;

                            string updatedHtmlContent = vejledningHtmlTemplate
                                .Replace("[Adresse]", address)
                                .Replace("[Name]", name)
                                .Replace("[Plukliste]", GeneratePluklisteContent(plukliste));

                            string outputFilePath = Path.Combine(OutputDirectory, $"{name}_Vejledning.html");
                            File.WriteAllText(outputFilePath, updatedHtmlContent);
                            Console.WriteLine($"Vejledning gemt: {outputFilePath}");

                            vejledningGenereret = true;
                            break; // Stop searching for more vejledninger when the first one is found
                        }
                    }

                    if (!vejledningGenereret)
                    {
                        Console.WriteLine($"No vejledning found for plukliste: {plukliste.Name}");
                    }
                }
            }
        }

        static string GeneratePluklisteContent(Pluklist plukliste)
        {
            StringBuilder pluklisteContent = new StringBuilder();

            pluklisteContent.AppendLine($"{plukliste.Name}");
            pluklisteContent.AppendLine($"Forsendelse: {plukliste.Forsendelse}");
            pluklisteContent.AppendLine("\n{0,-7}{1,-9}{2,-20}{3}");

            foreach (var item in plukliste.Lines)
            {
                pluklisteContent.AppendLine($"{item.Amount,-7}{item.Type,-9}{item.ProductID,-20}{item.Title}");
            }

            return pluklisteContent.ToString();
        }

        static void MoveAndCompletePlukliste(string filePath, string importDirectory)
        {
            var fileName = Path.GetFileName(filePath);
            if (fileName != null)
            {
                File.Move(filePath, Path.Combine(importDirectory, fileName));
                Console.WriteLine($"Plukseddel {fileName} afsluttet.");
            }
        }
    }
}
