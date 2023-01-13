using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker
{
    static class FieldEditor
    {
        public static void MainFieldEditor(List<Field> fields)
        {
            int userInput;

            while (true)
            {
                PrintEditorMenu();
                while (true)
                {
                    try
                    {
                        userInput = Int16.Parse(Console.ReadLine());
                        break;
                    }
                    catch (Exception e) when (e is FormatException || e is OverflowException)
                    {
                        PrintEditorMenu();
                    }
                }

                switch (userInput)
                {
                    case 1:
                        EditField(fields, "priority");
                        return;
                    case 2:
                        EditField(fields, "status");
                        return;
                    case 3:
                        EditField(fields, "title");
                        return;
                    case 4:
                        EditField(fields, "description");
                        return;
                    case 5:
                        DeleteField(fields);
                        return;
                    case 6:
                        return;
                }
            }
        }

        private static void PrintEditorMenu()
        {
            Console.Clear();
            Console.Write("1 - Change priority" +
                "\n2 - Change status" +
                "\n3 - Change title" +
                "\n4 - Change description" +
                "\n5 - Delete field" +
                "\n6 - Exit" +
                "\nOption: ");
        }

        private static void EditField(List<Field> fields, string setType)
        {
            bool exit = false;
            string userInput;

            List<string> fieldsIds = new List<string>();

            foreach (Field field in fields)
            {
                string type = field.GetType() == Field.Type.Idea ? "ID" : "BUG";
                fieldsIds.Add(string.Format("{0}_{1}", type, field.GetNumber()));
            }

            while (!exit)
            {
                Console.Clear();
                Console.Write("Type idea/bug ID ('ex' - exit): ");
                userInput = Console.ReadLine();

                if (string.Equals(userInput, "ex"))
                {
                    return;
                }
                if (!fieldsIds.Contains(userInput))
                {
                    continue;
                }

                for (int i = 0; i < fieldsIds.Count(); i++)
                {
                    if (fields[i].CheckId(userInput))
                    {
                        fields[i].SetPrivateField(setType);

                        exit = true;
                        break;
                    }
                }
            }

            Console.WriteLine("\nField edited, press enter to continue");
            _ = Console.ReadLine();
        }

        private static void DeleteField(List<Field> fields)
        {
            bool exit = false;
            string userInput;

            List<string> fieldsIds = new List<string>();

            foreach (Field field in fields)
            {
                string type = field.GetType() == Field.Type.Idea ? "ID" : "BUG";
                fieldsIds.Add(string.Format("{0}_{1}", type, field.GetNumber()));
            }

            while (!exit)
            {
                Console.Clear();
                Console.Write("Type idea/bug ID ('ex' - exit): ");
                userInput = Console.ReadLine();

                if (string.Equals(userInput, "ex"))
                {
                    return;
                }
                if (!fieldsIds.Contains(userInput))
                {
                    continue;
                }

                for (int i = 0; i < fieldsIds.Count(); i++)
                {
                    if (fields[i].CheckId(userInput))
                    {
                        Console.Write(string.Format("\nAre You sure you want to delete field {0}? (y/n) ", fieldsIds[i]));
                        userInput = Console.ReadLine();

                        if (string.Equals(userInput, "y"))
                        {
                            fields.RemoveAt(i);
                            Console.WriteLine("\nField deleted, press enter to continue");
                            _ = Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("\nAborted, press enter to continue");
                            _ = Console.ReadLine();
                        }

                        exit = true;
                        break;
                    }
                }
            }
        }
    }
}
