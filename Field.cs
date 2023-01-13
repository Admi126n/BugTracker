using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BugTracker
{
    class Field
    {
        public enum Type
        {
            Idea = 0,
            Bug = 1
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

        private static int _idMaxNumber;
        private static int _bugMaxNumber;

        private readonly int _number;
        private readonly Type _type;
        private Priority _priority;
        private Status _status;
        private string _title;
        private string _description;

        public Field()
        {
            Type type = SetType();
            int number = SetNumber(type);
            SetPriority();
            SetTitle(type);
            SetDescription(type);

            _number = number;
            _type = type;
            _status = Status.Pending;
        }

        public Field(List<string> fieldParams)
        {
            _number = Int16.Parse(fieldParams[0]);
            _type = (Type)Int16.Parse(fieldParams[1]);
            _priority = (Priority)Int16.Parse(fieldParams[2]);
            _status = (Status)Int16.Parse(fieldParams[3]);
            _title = fieldParams[4];
            _description = fieldParams[5];
        }

        public override string ToString()
        {
            string type = _type == Type.Idea ? "ID" : "BUG";

            string output = string.Format("{0}_{1}\t\t{2}, {3}\n\t[{4}]\n\t{5}\n",
                type,
                _number,
                _priority,
                _status,
                _title,
                _description);
            return output;
        }

        public static void SetIdMaxNumber(int idMax)
        {
            _idMaxNumber = idMax;
        }

        public static void SetBugMaxNumber(int bugMax)
        {
            _bugMaxNumber = bugMax;
        }

        public void SetPriority()
        {
            int userInput;
            while (true)
            {
                Console.Write("\nChoose priority:" +
                    "\n1 - Low" +
                    "\n2 - Medium" +
                    "\n3 - High" +
                    "\nOption: ");
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    userInput--;
                    if (Enum.IsDefined(typeof(Priority), userInput))
                    {
                        break;
                    }
                }
                catch (Exception e) when (e is FormatException || e is OverflowException) { }
            }
            _priority = (Priority)userInput;
        }

        public void SetStatus()
        {
            int userInput;
            while (true)
            {
                Console.Write("\nChoose status:" +
                    "\n1 - Pending" +
                    "\n2 - Ongoing" +
                    "\n3 - Done" +
                    "\n4 - Cancelled" +
                    "\nOption: ");
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    userInput--;
                    if (Enum.IsDefined(typeof(Status), userInput))
                    {
                        break;
                    }
                }
                catch (Exception e) when (e is FormatException || e is OverflowException) { }
            }
            _status = (Status)userInput;
        }

        public void SetTitle(Type type, bool printOld=false)
        {
            string userInput;

            if (!printOld)
            {
                Console.Write(string.Format("\nType {0:g} title: ", type));
            }
            do
            {
                if (printOld)
                {
                    Console.WriteLine("Old title:");
                    SendKeys.SendWait(_title);
                }

                userInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("\nTitle cannot be empty!\n");
                    Console.Write(string.Format("\nType {0:g} title: ", type));
                }
            } while (string.IsNullOrWhiteSpace(userInput));

            if (userInput.Length > 1)
            {
                userInput = char.ToUpper(userInput[0]) + userInput.Substring(1);
            } else
            {
                userInput = userInput.ToUpper();
            }
            userInput = userInput.Replace(';', ',');
            _title = userInput;
        }

        public void SetDescription(Type type, bool printOld = false)
        {
            string userInput;

            if (!printOld)
            {
                Console.Write(string.Format("\nType {0:g} description: ", type));
            }

            do
            {
                if (printOld)
                {
                    Console.WriteLine("Old description:");
                    SendKeys.SendWait(_description);
                }

                userInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("\nDescription cannot be empty!\n");
                    Console.Write(string.Format("\nType {0:g} title: ", type));
                }
            } while (string.IsNullOrWhiteSpace(userInput));
            if (userInput.Length > 1)
            {
                userInput = char.ToUpper(userInput[0]) + userInput.Substring(1);
            }
            else
            {
                userInput = userInput.ToUpper();
            }
            userInput = userInput.Replace(';', ',');
            _description = userInput;
        }

        public void SetPrivateField(string setType)
        {
            switch (setType)
            {
                case "priority":
                    SetPriority();
                    break;
                case "status":
                    SetStatus();
                    break;
                case "title":
                    SetTitle(_type, true);
                    break;
                case "description":
                    SetDescription(_type, true);
                    break;
            }
        }

        public static int GetIdMaxNumber()
        {
            return _idMaxNumber;
        }
        
        public static int GetBugMaxNumber()
        {
            return _bugMaxNumber;
        }

        public int GetNumber()
        {
            return _number;
        }

        public new Type GetType()
        {
            return _type;
        }

        public Priority GetPriority()
        {
            return _priority;
        }

        public Status GetStatus()
        {
            return _status;
        }

        public string GetTitle()
        {
            return _title;
        }

        public string GetDescription()
        {
            return _description;
        }

        public Enum GetEnumField(string returnType)
        {
            switch (returnType)
            {
                case "priority":
                    return _priority;
                case "status":
                    return _status;
                case "type":
                    return _type;
                default:
                    return null;
            }
        }

        public bool CheckId(string code)
        {
            string type = _type == Type.Idea ? "ID" : "BUG";
            string fieldCode = string.Format("{0}_{1}", type, _number);

            return string.Equals(code, fieldCode);
        }

        private int SetNumber(Type type)
        {
            int number;

            if (type == Type.Idea)
            {
                number = _idMaxNumber++;
            }
            else
            {
                number = _bugMaxNumber++;
            }

            return number;
        }

        private Type SetType()
        {
            int userInput;

            while (true)
            {
                Console.Write("\nChoose type:" +
                    "\n1 - Idea" +
                    "\n2 - Bug" +
                    "\nOption: ");
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    userInput--;
                    if (Enum.IsDefined(typeof(Type), userInput))
                    {
                        break;
                    }
                }
                catch (Exception e) when (e is FormatException || e is OverflowException) { }
            }
            return (Type)userInput;
        }
    }
}
