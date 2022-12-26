using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker
{
    class Field
    {
        public enum Type
        {
            Idea = 0,
            Issue = 1
        }
        public enum Priority
        {
            Low = 0,
            Medium = 1,
            High = 2
        }
        public enum Status
        {
            Pending = 0,
            Ongoing = 1,
            Done = 2,
            Cancelled = 3
        }

        private static int isMaxNumber;
        private static int idMaxNumber;

        private readonly int number;
        private readonly Type type;
        private Priority priority;
        private Status status;
        private string title;
        private string description;

        public Field()
        {
            Type type = SetType();
            int number = SetNumber(type);
            SetPriority();
            SetTitle(type);
            SetDescription(type);

            this.type = type;
            this.number = number;
            status = Status.Pending;
        }

        public Field(List<string> fieldParams)
        {
            number = Int16.Parse(fieldParams[0]);
            type = (Type)Int16.Parse(fieldParams[1]);
            priority = (Priority)Int16.Parse(fieldParams[2]);
            status = (Status)Int16.Parse(fieldParams[3]);
            title = fieldParams[4];
            description = fieldParams[5];
        }

        public override string ToString()
        {
            string typ = this.type == Type.Idea ? "ID" : "IS";

            String output = String.Format("{0}_{1}\t\t{2}, {3}\n\t[{4}]\n\t{5}\n",
                typ,
                this.number,
                this.priority,
                this.status,
                this.title,
                this.description);
            return output;
        }

        private Type SetType()
        {
            int userInput;

            while (true)
            {
                Console.Write("\nChoose type:\n1 - Idea\n2 - Issue\nType: ");
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    userInput--;
                    if (Enum.IsDefined(typeof(Type), userInput))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input (wrong number), try again\n");
                    }
                }
                catch (Exception e) when (e is FormatException || e is OverflowException)
                {
                    Console.WriteLine("Invalid input (not number), try again\n");
                }
            }           
            return (Type)userInput;
        }

        private int SetNumber(Type type)
        {
            int number;

            if (type == Type.Idea)
            {
                number = idMaxNumber;
                idMaxNumber++;
            }
            else
            {
                number = isMaxNumber;
                isMaxNumber++;
            }

            return number;
        }

        public void SetPriority()
        {
            int userInput;
            while (true)
            {
                Console.Write("\nChoose priority:\n1 - Low\n2 - Medium\n3 - High\nPriority: ");
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    userInput--;
                    if (Enum.IsDefined(typeof(Priority), userInput))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input (wrong number), try again\n");
                    }
                }
                catch (Exception e) when (e is FormatException || e is OverflowException)
                {
                    Console.WriteLine("Invalid input (not number), try again\n");
                }
            }
            this.priority = (Priority)userInput;
        }

        // TODO srednik jako znak zakazany (po prostu go usuwac)
        public void SetTitle(Type type)
        {
            string userInput;

            Console.Write(string.Format("\nType {0:g} title: ", type));
            do
            {
                userInput = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("\nTitle cannot be empty!\n");
                    Console.Write(string.Format("\nType {0:g} title: ", type));
                }
            } while (String.IsNullOrWhiteSpace(userInput));

            if (userInput.Length > 1)
            {
                userInput = char.ToUpper(userInput[0]) + userInput.Substring(1);
            } else
            {
                userInput = userInput.ToUpper();
            }
            this.title = userInput;
        }

        // TODO srednik jako znak zakazany (po prostu go usuwac)
        public void SetDescription(Type type)
        {
            string userInput;

            Console.Write(string.Format("\nType {0:g} description: ", type));
            do
            {
                userInput = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("\nDescription cannot be empty!\n");
                    Console.Write(string.Format("\nType {0:g} title: ", type));
                }
            } while (String.IsNullOrWhiteSpace(userInput));
            if (userInput.Length > 1)
            {
                userInput = char.ToUpper(userInput[0]) + userInput.Substring(1);
            }
            else
            {
                userInput = userInput.ToUpper();
            }
            this.description = userInput;
        }

        public void SetStatus()
        {
            int userInput;
            while (true)
            {
                Console.Write("\nChoose status:\n1 - Pending\n2 - Ongoing\n3 - Done\n4 - Cancelled\nStatus: ");
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    userInput--;
                    if (Enum.IsDefined(typeof(Status), userInput))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input (wrong number), try again\n");
                    }
                }
                catch (Exception e) when (e is FormatException || e is OverflowException)
                {
                    Console.WriteLine("Invalid input (not number), try again\n");
                }
            }
            this.status = (Status)userInput;
        }

        public new Type GetType()
        {
            return type;
        }

        public Status GetStatus()
        {
            return status;
        }

        public Priority GetPriority()
        {
            return priority;
        }

        public int GetNumber()
        {
            return number;
        }

        public Enum GetEnumField(string returnType)
        {
            switch (returnType)
            {
                case "priority":
                    return priority;
                case "status":
                    return status;
                case "type":
                    return type;
                default:
                    return null;
            }
        }

        public string GetTitle()
        {
            return title;
        }

        public string GetDescription()
        {
            return description;
        }
        
        public static int GetIsMaxNumber()
        {
            return isMaxNumber;
        }

        public static int GetIdMaxNumber()
        {
            return idMaxNumber;
        }

        public static void SetIdMaxNumber(int idMax)
        {
            idMaxNumber = idMax;
        }

        public static void SetisMaxNumber(int isMax)
        {
            isMaxNumber = isMax;
        }
    }
}
