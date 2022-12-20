using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker
{
    class IssueTracker
    {
        static List<Field> fields = new List<Field>();
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
                        PrintMenu();
                    }
                }

                switch (userInput)
                {
                    case 1:
                        Field newField = new Field();
                        fields.Add(newField);
                        break;
                    case 2:
                        foreach (Field field in fields)
                        {
                            Console.WriteLine(field.ToString());
                        }
                        break;
                    case 3:
                        ShowFields<Field.Type>(fields, "type");
                        break;
                    case 4:
                        ShowFields<Field.Priority>(fields, "priority");
                        break;
                    case 5:
                        ShowFields<Field.Status>(fields, "status");
                        break;
                    case 6:
                        exit = true;
                        break;
                }
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("\n1 - Add new" +
                "\n2 - Show all" +
                "\n3 - Show by type" +
                "\n4 - Show by priority" +
                "\n5 - Show by status" +
                "\n6 - Exit");
        }
        
        private static void ShowFields<T>(List<Field> fields, string filterType) where T: Enum
        {
            int userInput;
            int counter = 0;

            while (true)
            {
                // loop for printing enum values
                for (int i = 0; ; i++)
                {
                    if (Enum.IsDefined(typeof(T), i))
                    {
                        Console.WriteLine((T)(object)i);
                    } else
                    {
                        break;
                    }
                }
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
                        Console.WriteLine("Invalid input (wrong number), try again\n");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input (not number), try again\n");
                }
            }
            
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
                Console.WriteLine("No fields for this search parameters!");
            }
        }
        
        private static void ShowFieldsByType(List<Field> fields)
        {
            int userInput;
            int counter = 0;

            while (true)
            {
                Console.WriteLine("Choose type:\n1 - Idea\n2 - Issue");
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    userInput--;
                    if (Enum.IsDefined(typeof(Field.Type), userInput))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input (wrong number), try again\n");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input (not number), try again\n");
                }
            }

            foreach (Field field in fields)
            {
                if (field.GetType() == (Field.Type)userInput)
                {
                    Console.WriteLine(field.ToString());
                    counter++;
                }
            }
            if (counter == 0)
            {
                Console.WriteLine("No fields for this search parameters!");
            }
        }

        private static void ShowFieldsByPriority(List<Field> fields)
        {
            int userInput;
            int counter = 0;

            while (true)
            {
                Console.WriteLine("Choose priority:\n1 - Low\n2 - Medium\n3 - High");
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    userInput--;
                    if (Enum.IsDefined(typeof(Field.Priority), userInput))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input (wrong number), try again\n");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input (not number), try again\n");
                }
            }

            foreach (Field field in fields)
            {
                if (field.GetPriority() == (Field.Priority)userInput)
                {
                    Console.WriteLine(field.ToString());
                    counter++;
                }
            }
            if (counter == 0)
            {
                Console.WriteLine("No fields for this search parameters!");
            }
        }

        private static void ShowFieldsByStatus(List<Field> fields)
        {
            int userInput;
            int counter = 0;

            while (true)
            {
                Console.WriteLine("Choose status:\n1 - Pending\n2 - Ongoing\n3 - Done\n4 - Cancelled");
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    userInput--;
                    if (Enum.IsDefined(typeof(Field.Status), userInput))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input (wrong number), try again\n");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input (not number), try again\n");
                }
            }

            foreach (Field field in fields)
            {
                if (field.GetStatus() == (Field.Status)userInput)
                {
                    Console.WriteLine(field.ToString());
                    counter++;
                }
            }
            if (counter == 0)
            {
                Console.WriteLine("No fields for this search parameters!");
            }
        }
    }   
}
