using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker
{
    class IssueTracker
    {
        static readonly WorkspaceHandler wh = new WorkspaceHandler();
        static string workspacePath = wh.MainWorkspaceHandler();

        static List<Field> fields = FileHandler.ReadFieldsFromFile(workspacePath);

        public static void Main()
        {            
            FileHandler.ReadMaxNumbersFromFile(workspacePath);
            
            Menu();
            
            FileHandler.WriteFieldsToFile(fields, workspacePath);
            FileHandler.WriteMaxNumbersToFile(workspacePath);
        }

        private static void Menu()
        {
            int userInput;
            bool exit = false;

            while (!exit)
            {
                PrintMenu();
                while (true)
                {
                    try
                    {
                        userInput = Int16.Parse(Console.ReadLine());
                        break;
                    }
                    catch (Exception e) when (e is FormatException || e is OverflowException)
                    {
                        PrintMenu();
                    }
                }

                switch (userInput)
                {
                    case 1:
                        Field newField = new Field();
                        fields.Add(newField);
                        Console.WriteLine("\nField added, press enter to continue");
                        _ = Console.ReadLine();
                        break;
                    case 2:
                        FieldPrinter.MainFieldPrinter(fields);
                        break;
                    case 3:
                        FieldEditor.MainFieldEditor(fields);
                        break;
                    case 4:
                        ChangeWorkspace();
                        break;
                    case 5:
                        exit = true;
                        break;
                }
            }
        }

        private static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine(string.Format("Current workspace: {0}", wh.GetCurrentWorkspaceName()));
            Console.Write("1 - Add new" +
                "\n2 - Show fields" +
                "\n3 - Edit fields" +
                "\n4 - Change workspace" +
                "\n5 - Exit" +
                "\nOption: ");
        }

        private static void ChangeWorkspace()
        {
            FileHandler.WriteFieldsToFile(fields, workspacePath);
            FileHandler.WriteMaxNumbersToFile(workspacePath);

            workspacePath = wh.MainWorkspaceHandler();

            fields = FileHandler.ReadFieldsFromFile(workspacePath);
            FileHandler.ReadMaxNumbersFromFile(workspacePath);

            Console.WriteLine("\nWorkspace changed, press enter to continue");
            _ = Console.ReadLine();
        }
    }   
}
