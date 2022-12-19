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
            for (int i = 0; i < 3; i++)
            {
                Menu();
            }
        }

        private static void Menu()
        {
            int userInput;
            Console.WriteLine("1 - Add new\n2 - Show by type\n3 - Show by priority\n4 - Show by status");

            while (true)
            {
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    if (userInput > 0 && userInput < 5)
                    {
                        break;
                    } else
                    {
                        Console.WriteLine("Invalid input (wrong number), try again\n");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input (not number), try again\n");
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
                    Console.WriteLine("");
                    break;
                case 3:
                    Console.WriteLine("");
                    break;
                case 4:
                    Console.WriteLine("");
                    break;
            }
        }
    }   
}
