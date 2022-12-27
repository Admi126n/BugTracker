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
        public static List<Field> ReadFieldsFromFile(string filePath)
        {
            List<Field> fields = new List<Field>();

            filePath = string.Format("{0}\\{1}", 
                filePath, 
                "fields.csv");

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
            catch (Exception e) when (e is FileNotFoundException)
            {
                return fields;
            }
        }

        public static void WriteFieldsToFile(List<Field> fields, string outputPath)
        {
            if (fields != null)
            {
                outputPath = string.Format("{0}\\{1}", 
                    outputPath, 
                    "fields.csv");
                
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

        public static void ReadMaxNumbersFromFile(string filePath)
        {
            int maxId = 0;
            int maxIs = 0;
            filePath = string.Format("{0}\\{1}", 
                filePath, 
                ".config");
            
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
                        catch (Exception e) when (e is FormatException || e is OverflowException) { }
                        Field.SetIdMaxNumber(maxId);
                        Field.SetIsMaxNumber(maxIs);
                    }
                }
            }
            catch (Exception e) when (e is FileNotFoundException) { }
        }

        public static void WriteMaxNumbersToFile(string filePath)
        {
            filePath = string.Format("{0}\\{1}", 
                filePath, 
                ".config");

            File.WriteAllText(filePath, string.Format("{0};{1}", 
                Field.GetIdMaxNumber(), 
                Field.GetIsMaxNumber()));
        }

        public static void GenerateTexFile(string outputPath, List<Field> fields)
        {

        }
    }
}
