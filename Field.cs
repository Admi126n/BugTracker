using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BugTracker
{
    /// <summary>
    /// Field class with static max idea/bug numbers, and needed enums
    /// </summary>
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

        /// <summary>
        /// Creates Field object
        /// </summary>
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

        /// <summary>
        /// Creates Field object with parameters given as a list
        /// </summary>
        /// <param name="fieldParams">List of id, Type, Priority, Status, title, description given as strings.
        /// Type, Priority, Status are given as number</param>
        public Field(List<string> fieldParams)
        {
            _number = Int16.Parse(fieldParams[0]);
            _type = (Type)Int16.Parse(fieldParams[1]);
            _priority = (Priority)Int16.Parse(fieldParams[2]);
            _status = (Status)Int16.Parse(fieldParams[3]);
            _title = fieldParams[4];
            _description = fieldParams[5];
        }

        /// <summary>
        /// Converts Field object to string
        /// </summary>
        /// <returns>Field object converted to string</returns>
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

        /// <summary>
        /// Stes static idMaxNumber
        /// </summary>
        /// <param name="idMax">int value</param>
        public static void SetIdMaxNumber(int idMax)
        {
            _idMaxNumber = idMax;
        }

        /// <summary>
        /// Sets static bugMaxNumber
        /// </summary>
        /// <param name="bugMax">int value</param>
        public static void SetBugMaxNumber(int bugMax)
        {
            _bugMaxNumber = bugMax;
        }

        /// <summary>
        /// Gets user input and sets Priority of Field object
        /// </summary>
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

        /// <summary>
        /// Gets user input and sets Seatus of Field object
        /// </summary>
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

        /// <summary>
        /// Get user input and sets title of Field object. If printOld=true prints old title as editable text
        /// Function replaces '\' with '/' and ';' with ','
        /// </summary>
        /// <param name="type">Type of Field object</param>
        /// <param name="printOld">bool value</param>
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
            userInput = userInput.Replace('\\', '/');
            _title = userInput;
        }

        /// <summary>
        /// Get user input and sets description of Field object. If printOld=true prints old description as editable text.
        /// Function replaces '\' with '/' and ';' with ','
        /// </summary>
        /// <param name="type">Type of Field object</param>
        /// <param name="printOld">bool value</param>
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
            userInput = userInput.Replace('\\', '/');
            _description = userInput;
        }

        /// <summary>
        /// Runs setter given in 'setType'
        /// </summary>
        /// <param name="setType">string with property to set</param>
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

        /// <summary>
        /// idMaxNumber getter
        /// </summary>
        /// <returns>idMaxNumber</returns>
        public static int GetIdMaxNumber()
        {
            return _idMaxNumber;
        }

        /// <summary>
        /// bugMaxNumber getter
        /// </summary>
        /// <returns>bugMaxNumber</returns>
        public static int GetBugMaxNumber()
        {
            return _bugMaxNumber;
        }

        /// <summary>
        /// Number getter
        /// </summary>
        /// <returns>Field number</returns>
        public int GetNumber()
        {
            return _number;
        }

        /// <summary>
        /// Type getter
        /// </summary>
        /// <returns>Field Type</returns>
        public new Type GetType()
        {
            return _type;
        }

        /// <summary>
        /// Priority getter
        /// </summary>
        /// <returns>Field Priority</returns>
        public Priority GetPriority()
        {
            return _priority;
        }

        /// <summary>
        /// Status getter
        /// </summary>
        /// <returns>Field Status</returns>
        public Status GetStatus()
        {
            return _status;
        }

        /// <summary>
        /// Title getter
        /// </summary>
        /// <returns>string with Field Title</returns>
        public string GetTitle()
        {
            return _title;
        }

        /// <summary>
        /// Description getter
        /// </summary>
        /// <returns>string with Field description</returns>
        public string GetDescription()
        {
            return _description;
        }

        /// <summary>
        /// Gets enum property of given type
        /// </summary>
        /// <param name="returnType">wanted enum type</param>
        /// <returns>enum property</returns>
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

        /// <summary>
        /// Checks ID equality
        /// </summary>
        /// <param name="code">string with ID</param>
        /// <returns>true if IDs are equal, falsse otherwise</returns>
        public bool CheckId(string code)
        {
            string type = _type == Type.Idea ? "ID" : "BUG";
            string fieldCode = string.Format("{0}_{1}", type, _number);

            return string.Equals(code, fieldCode);
        }

        /// <summary>
        /// Returns value of wanted static field increased by one
        /// </summary>
        /// <param name="type">value from Type enum</param>
        /// <returns>value of wanted static field increased by one</returns>
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

        /// <summary>
        /// Type setter, gets user input and returns property
        /// </summary>
        /// <returns>value of Type enum</returns>
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
