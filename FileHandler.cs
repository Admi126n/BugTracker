using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker
{
    static class FileHandler
    {
        private static string fieldsFile = "file.txt";
        private static string maxNumbersFile = "file.config";
        public static void WriteFieldsToFile(List<Field> fields, string outputPath = "file.txt")
        {
            if (fields != null)
            {
                StringBuilder csv = new StringBuilder();
                foreach (Field field in fields)
                {
                    csv.Append(string.Format("{0};{1};{2};{3};{4};{5}\n",
                        field.GetNumber(),
                        (int)field.GetType(),
                        (int)field.GetPriority(),
                        (int)field.GetStatus(),
                        field.GetTitle(),
                        field.GetDescription()));
                }
                File.WriteAllText(outputPath, csv.ToString());
            }
        }

        public static List<Field> ReadFieldsFromFile(string filePath= "file.txt")
        {
            List<Field> fields = new List<Field>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string currentLine;
                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        List<string> lineToList = new List<string>(currentLine.Split(';'));
                        fields.Add(new Field(lineToList));
                    }
                }
                return fields;
            }
            catch (FileNotFoundException)
            {
                return fields;
            }
        }

        public static void ReadMaxNumbersFromFile(string filePath = "file.config")
        {
            int maxId = 0;
            int maxIs = 0;

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string currentLine;
                    if ((currentLine = sr.ReadLine()) != null)
                    {
                        List<string> lineToList = new List<string>(currentLine.Split(';'));
                        try
                        {
                            maxId = Int16.Parse(lineToList[0]);
                            maxIs = Int16.Parse(lineToList[1]);
                        }
                        catch (FormatException) { }
                        Field.SetIdMaxNumber(maxId);
                        Field.SetisMaxNumber(maxIs);
                    }
                }
            }
            catch (FileNotFoundException) { }
        }

        public static void SaveMaxNumbersToFile(string filePath= "file.config")
        {
            File.WriteAllText(filePath, string.Format("{0};{1}", Field.GetIdMaxNumber(), Field.GetIsMaxNumber()));
        }

        public static void GenerateTexFile(string outputPath, List<Field> fields)
        {

        }

        public static void PrintWorkspaces(string filepath="Test")
        {
            string[] directories = Directory.GetDirectories(Directory.GetCurrentDirectory());
            //var files = Directory.GetFiles(Directory.GetCurrentDirectory());
            int dirsNumber = directories.Length;

            List<string> workspaces = new List<string>();

            if (dirsNumber == 0)
            {
                Console.WriteLine("No workspaces");
                _ = Console.ReadLine();
                return;
            }
            else
            {
                for (int i = 0; i < dirsNumber; i++)
                {
                    string[] WorkspaceName = directories[i].Split('\\');
                    workspaces.Add(WorkspaceName[WorkspaceName.Length - 1]);
                }
            }
            for (int i = 0; i < dirsNumber; i++)
            {
                Console.WriteLine(string.Format("{0} - {1}", i + 1, workspaces[i]));
            }
            _ = Console.ReadLine();
        }
    }
}
