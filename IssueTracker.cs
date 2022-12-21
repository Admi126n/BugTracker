using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker
{
    class IssueTracker
    {
        static readonly List<Field> fields = new List<Field>();
        public static void Main()
        {
            Menu();
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
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input (not number), try again\n");
                        _ = Console.ReadLine();
                        Console.Clear();
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
                        ShowAllFields(fields);
                        break;
                    case 3:
                        ShowFilteredFields<Field.Type>(fields, "type");
                        break;
                    case 4:
                        ShowFilteredFields<Field.Priority>(fields, "priority");
                        break;
                    case 5:
                        ShowFilteredFields<Field.Status>(fields, "status");
                        break;
                    case 6:
                        exit = true;
                        break;
                }
                Console.Clear();
            }
        }

        private static void PrintMenu()
        {
            Console.Write("1 - Add new" +
                "\n2 - Show all" +
                "\n3 - Show by type" +
                "\n4 - Show by priority" +
                "\n5 - Show by status" +
                "\n6 - Exit" +
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
                Console.WriteLine("\nNo fields with status != Done, press enter to continue");
                _ = Console.ReadLine();
            } else
            {
                Console.WriteLine("\nPress enter to continue");
                _ = Console.ReadLine();
            }
        }

        private static void ShowFilteredFields<T>(List<Field> fields, string filterType) where T: Enum
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
                    else
                    {
                        Console.WriteLine("\nInvalid input (wrong number), try again");
                        _ = Console.ReadLine();
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nInvalid input (not number), try again");
                    _ = Console.ReadLine();
                }
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
                Console.WriteLine("\nNo fields for this search parameters, press enter to continue");
                _ = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nPress enter to continue");
                _ = Console.ReadLine();
            } 
                
        }
        
        public static void PrintEnumValues<T>() where T : Enum
        {
            T temp = (T)(object)0;
            string enumType = temp.GetType().ToString().Split('+')[1];
            string output = "";            

            for (int i = 0; ; i++)
            {
                if (Enum.IsDefined(typeof(T), i))
                {
                    output += string.Format("{0} - {1}\n", i + 1, (T)(object)i);
                }
                else
                {
                    break;
                }
            }
            Console.Clear();
            Console.WriteLine(string.Format("Choose {0}", enumType));
            Console.Write(output);
            Console.Write(string.Format("{0}: ", enumType));
        }
    }   
}
