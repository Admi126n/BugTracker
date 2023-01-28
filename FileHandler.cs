using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BugTracker
{
    /// <summary>
    /// Class with methods needed to read and write data to files
    /// </summary>
    static class FileHandler
    {
        /// <summary>
        /// Reads the file to which the path was given and returns fields list
        /// </summary>
        /// <param name="filePath">path to .csv file with saved configuration</param>
        /// <returns>A list of Field objects</returns>
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

        /// <summary>
        /// Write given fields list to .csv file in given directory
        /// </summary>
        /// <param name="fields">list of Field objects</param>
        /// <param name="outputPath">path to output directory</param>
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

        /// <summary>
        /// Reads .config file from given directory and sets idMaxNumber and bugMaxNumber fields from Field class
        /// </summary>
        /// <param name="sourcePath">path to directory with .config file</param>
        public static void ReadMaxNumbersFromFile(string sourcePath)
        {
            int maxId = 0;
            int maxBug = 0;
            sourcePath = string.Format("{0}\\{1}",
                sourcePath,
                ".config");
            
            try
            {
                using (StreamReader sr = new StreamReader(sourcePath))
                {
                    string currentLine;
                    if ((currentLine = sr.ReadLine()) != null)
                    {
                        List<string> lineToList = new List<string>(currentLine.Split(';'));
                        try
                        {
                            maxId = Int16.Parse(lineToList[0]);
                            maxBug = Int16.Parse(lineToList[1]);
                        }
                        catch (Exception e) when (e is FormatException || e is OverflowException) { }
                        Field.SetIdMaxNumber(maxId);
                        Field.SetBugMaxNumber(maxBug);
                    }
                    else
                    {
                        Field.SetIdMaxNumber(0);
                        Field.SetBugMaxNumber(0);
                    }
                }
            }
            catch (Exception e) when (e is FileNotFoundException) 
            {
                Field.SetIdMaxNumber(0);
                Field.SetBugMaxNumber(0);
            }
        }

        /// <summary>
        /// Write idMaxNumber and bugMaxNumber fields from Field class to .config file in given output directory
        /// </summary>
        /// <param name="outputPath">path to directory</param>
        public static void WriteMaxNumbersToFile(string outputPath)
        {
            outputPath = string.Format("{0}\\{1}",
                outputPath,
                ".config");

            File.WriteAllText(outputPath, string.Format("{0};{1}",
                Field.GetIdMaxNumber(),
                Field.GetBugMaxNumber()));
        }

        /// <summary>
        /// Generates .txt file with tex code with bugs and ideas listing. Tex file have two sections for bugs and ideas separetly
        /// </summary>
        /// <param name="fileName">output file name</param>
        /// <param name="fields">list of Field objects</param>
        public static void GenerateTexFile(string fileName, List<Field> fields)
        {
            string header = "\\documentclass{article}\\usepackage[english]{babel}\\usepackage[a4paper, top = 2cm, bottom = 2cm, left = 2cm, " +
                "right = 2cm, marginparwidth = 1.75cm]{geometry}\\begin{document}\\centerline{\\textbf{\\huge BugTracker listing}}\\centerline{\\large{(\\today)}}";
            string bugsHeader = "\\section{Bugs}";
            string ideasHeader = "\\section{Ideas}";
            string end = "\\vfill\\tiny{Generated by BugTracker}\\end{document}";
            StringBuilder bugs = new StringBuilder();
            StringBuilder ideas = new StringBuilder();
            string type = "";
            StringBuilder tex = new StringBuilder();

            foreach (Field field in fields)
            {
                type = field.GetType() == Field.Type.Idea ? "ID" : "IS";

                if (string.Equals("IS", type))
                {
                    bugs.Append(string.Format("\\textbf{{{0}$\\_${1}\\hfill {2}, {3}}}\\textbf{{\\\\{4}}}\\\\{5}\\vspace{{20pt}}\n",
                        type,
                        field.GetNumber(),
                        field.GetPriority(),
                        field.GetStatus(),
                        ReplaceForbriddenChars(field.GetTitle()),
                        ReplaceForbriddenChars(field.GetDescription())));
                } else
                {
                    ideas.Append(string.Format("\\textbf{{{0}$\\_${1}\\hfill {2}, {3}}}\\textbf{{\\\\{4}}}\\\\{5}\\vspace{{20pt}}\\\\\n",
                        type,
                        field.GetNumber(),
                        field.GetPriority(),
                        field.GetStatus(),
                        ReplaceForbriddenChars(field.GetTitle()),
                        ReplaceForbriddenChars(field.GetDescription())));
                }
            }
            tex.Append(header);
            tex.Append(bugsHeader);
            tex.Append(bugs);
            tex.Append(ideasHeader);
            tex.Append(ideas);
            tex.Append(end);

            File.WriteAllText(string.Format("{0}\\{1}.txt", Directory.GetCurrentDirectory(), fileName), tex.ToString());

            Console.WriteLine(string.Format("\nTex file saved in {0}.txt file, press enter to continue", fileName));
            _ = Console.ReadLine();
        }

        /// <summary>
        /// Replaces all chars which have special functions in tex code
        /// </summary>
        /// <param name="text">text with forbidden chars</param>
        /// <returns>text with escaped characters</returns>
        private static string ReplaceForbriddenChars(string text)
        {
            text = text.Replace("<", "\\textless");
            text = text.Replace(">", "\\textgreater");
            text = text.Replace("|", "\\textbar");
            text = text.Replace("~", "\\textasciitilde");
            text = text.Replace("{", "\\{");
            text = text.Replace("}", "\\}");
            text = text.Replace("%", "\\%");
            text = text.Replace("#", "\\#");
            text = text.Replace("_", "\\_");
            text = text.Replace("$", "\\$");
            text = text.Replace("&", "\\&");
            text = text.Replace("^", "\\^{}");
            text = text.Replace("[", "{[}");
            text = text.Replace("]", "{]}");
            return text;
        }
    }
}
