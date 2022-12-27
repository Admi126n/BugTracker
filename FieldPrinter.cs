using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker
{
    static class FieldPrinter
    {
        public static void MainFieldPrinter(List<Field> fields)
        {
            int userInput;

            while (true)
            {
                PrintPrinterMenu();
                while (true)
                {
                    try
                    {
                        userInput = Int16.Parse(Console.ReadLine());
                        break;
                    }
                    catch (Exception e) when (e is FormatException || e is OverflowException)
                    {
                        PrintPrinterMenu();
                    }
                }

                switch (userInput)
                {
                    case 1:
                        ShowAllFields(fields);
                        return;
                    case 2:
                        ShowFilteredFields<Field.Type>(fields, "type");
                        return;
                    case 3:
                        ShowFilteredFields<Field.Status>(fields, "status");
                        return;
                    case 4:
                        ShowFilteredFields<Field.Priority>(fields, "priority");
                        return;
                }
            }
        }

        private static void PrintPrinterMenu()
        {
            Console.Clear();
            Console.Write("1 - Show all fields" +
                "\n2 - Show by type" +
                "\n3 - Show by status" +
                "\n4 - Show by priority" +
                "\nOption: ");
        }

        private static void ShowAllFields(List<Field> fields)
        {
            Console.Clear();
            int counter = 0;
            foreach (Field field in fields)
            {
                if (field.GetStatus() != Field.Status.Done)
                {
                    Console.WriteLine(field.ToString());
                    counter++;
                }
            }
            if (counter == 0)
            {
                Console.WriteLine("No fields with status != Done, press enter to continue");
                _ = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nPress enter to continue");
                _ = Console.ReadLine();
            }
        }

        private static void ShowFilteredFields<T>(List<Field> fields, string filterType) where T : Enum
        {
            int userInput;
            int counter = 0;

            while (true)
            {
                PrintEnumValues<T>();
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    userInput--;
                    if (Enum.IsDefined(typeof(T), userInput))
                    {
                        break;
                    }
                }
                catch (Exception e) when (e is FormatException || e is OverflowException) { }
            }

            Console.Clear();
            foreach (Field field in fields)
            {
                if (field.GetEnumField(filterType).Equals((T)(object)userInput))
                {
                    Console.WriteLine(field.ToString());
                    counter++;
                }
            }
            if (counter == 0)
            {
                Console.WriteLine("No fields for this search parameters, press enter to continue");
                _ = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nPress enter to continue");
                _ = Console.ReadLine();
            }
        }

        private static void PrintEnumValues<T>() where T : Enum
        {
            T temp = (T)(object)0;
            string enumType = temp.GetType().ToString().Split('+')[1];

            StringBuilder output = new StringBuilder();

            for (int i = 0; Enum.IsDefined(typeof(T), i); i++)
            {
                output.Append(string.Format("{0} - {1}\n", i + 1, (T)(object)i));
            }

            Console.Clear();
            Console.Write(string.Format("Choose {0}\n{1}{0}: ", enumType, output));
        }
    }
}
