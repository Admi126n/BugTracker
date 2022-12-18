﻿using System;
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

        private readonly Type type;
        private readonly int number;
        private Priority priority;
        private Status status;
        private string title;
        private string description;

        public Field()
        {
            Type type = GetTypeInput();
            int number = GenerateNumber(type);
            Priority priority = GetPriorityInput();
            //Status status = GetStatusinput();
            string title = GetTitleInput(type);
            string description = GetDescriptionInput(type);

            this.type = type;
            this.number = number;
            this.priority = priority;
            this.status = Status.Pending;
            this.title = title;
            this.description = description;
        }

        public override string ToString()
        {
            string typ = this.type == Type.Idea ? "ID" : "IS";

            String output = String.Format("\n{0}_{1}\t\t{2}, {3}\n\t[{4}]\n\t{5}",
                typ,
                this.number,
                this.priority,
                this.status,
                this.title,
                this.description);
            return output;
        }

        private Type GetTypeInput()
        {
            int userInput;

            while (true)
            {
                Console.WriteLine("Choose type:\n1 - Idea\n2 - Issue");
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
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input (not number), try again\n");
                }

            }           
            return (Type)userInput;
        }

        private int GenerateNumber(Type type)
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

        private Priority GetPriorityInput()
        {
            int userInput;
            while (true)
            {
                Console.WriteLine("Choose priority:\n1 - Low\n2 - Medium\n3 - High");
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
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input (not number), try again\n");
                }
            }
            return (Priority)userInput;
        }

        private string GetTitleInput(Type type)
        {
            string userInput;

            Console.Write(string.Format("Type {0:g} title: ", type));
            userInput = Console.ReadLine();
            return userInput;
        }

        private string GetDescriptionInput(Type type)
        {
            string userInput;

            Console.Write(string.Format("Type {0:g} description: ", type));
            userInput = Console.ReadLine();
            return userInput;
        }
    }
}
