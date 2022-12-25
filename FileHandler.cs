using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker
{
    static class FileHandler
    {
        public static void WriteToFile(List<Field> fields, string outputPath="C:\\Users\\48784\\Desktop\\test.txt")
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

        public static List<Field> ReadFromFile(string filePath="C:\\Users\\48784\\Desktop\\test.txt")
        {
            List<Field> fields = new List<Field>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string currentLine;
                    // currentLine will be null when the StreamReader reaches the end of file
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
                Console.WriteLine("\nFile read error!\nPress enter to continue\n");
                _ = Console.ReadLine();
                Console.Clear();
                return fields;
            }
        }

        public static void GenerateTexFile(string outputPath, List<Field> fields)
        {

        }

        public static int LoadInitialNumbers(string filePath = "C:\\Users\\48784\\Desktop\\.config")
        {
            return 0;
        }

        public static void SaveNumbers(string filePath="C:\\Users\\48784\\Desktop\\.config")
        {
            File.WriteAllText(filePath, string.Format("{0};{1}", Field.GetIdMaxNumber(), Field.GetIsMaxNumber()));
        }
    }
}
