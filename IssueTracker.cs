using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker
{
    class IssueTracker
    {
        public static void Main()
        {
            //PrintMenu();
            Field x = new Field();
            Console.WriteLine(x.ToString());
            _ = Console.ReadLine();
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1 - show ideas\n2 - show issues\n3 - show both\n4 - add new");
        }
    }

    class Field
    {
        public enum Type
        {
            Issue,
            Idea
        }
        public enum Priority
        {
            Low,
            Medium,
            High
        }
        public enum Status
        {
            Pending,
            Ongoing,
            Done,
            Cancelled
        }

        private readonly Type type;
        private readonly int number;
        private Priority priority;
        private Status status;
        private string title;
        private string description;

        public Field(Type type = Type.Idea, int number = 0, Priority priority = Priority.Low, Status status = Status.Pending,
            string title = "", string description = "")
        {
            this.type = type;
            this.number = number;
            this.priority = priority;
            this.status = status;
            this.title = title;
            this.description = description;
        }

        public override string ToString()
        {
            string typ = this.type == Type.Idea ? "ID" : "IS";

            String output = String.Format("{0}_{1}\t\t{2}, {3}\n\t{4}\n\t{5}",
                typ,
                this.number,
                this.priority,
                this.status,
                this.title,
                this.description);
            return output;
        }
    }
}
