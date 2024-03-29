﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BugTracker
{
    /// <summary>
    /// Class with all methods needed for workspace management
    /// </summary>
    class WorkspaceHandler
    {
        private string _currentWorkspacePath;
        private string _currentWorkspaceName;
        private Dictionary<int, string> _availableWorkspacesDict = new Dictionary<int, string>();
        private static readonly List<string> forbiddenChars = new List<string> { "\\", "/", ":", "*", "?", "\"", "<", ">", "|" };

        /// <summary>
        /// Sets dict with available workspaces
        /// </summary>
        public WorkspaceHandler()
        {
            _availableWorkspacesDict = GetAvailableWorkspaces();
        }

        /// <summary>
        /// currentWorkspaceName getter
        /// </summary>
        /// <returns>current workspace name</returns>
        public string GetCurrentWorkspaceName()
        {
            return _currentWorkspaceName;
        }

        /// <summary>
        /// Main WorkspaceHandler func, gets user input and runs choosen funcs
        /// </summary>
        /// <returns>current workspace path</returns>
        public string MainWorkspaceHandler()
        {
            int userInput;

            while (true)
            {
                PrintWorkspaceMenu();
                while (true)
                {
                    try
                    {
                        userInput = Int16.Parse(Console.ReadLine());
                        break;
                    }
                    catch (Exception e) when (e is FormatException || e is OverflowException)
                    {
                        PrintWorkspaceMenu();
                    }
                }

                switch (userInput)
                {
                    case 1:
                        AddWorkspace();
                        break;
                    case 2:
                        if (ChooseWorkspace() == 1)
                        {
                            return _currentWorkspacePath;
                        }
                        break;
                    case 3:
                        DeleteWorkspace();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        /// <summary>
        /// currentWorkspacePath setter
        /// </summary>
        /// <param name="path">current workspace path</param>
        private void SetCurrentWorkspacePath(string path)
        {
            _currentWorkspacePath = path;
        }

        /// <summary>
        /// currentWorkspaceName setter
        /// </summary>
        /// <param name="name">current workspace name</param>
        private void SetCurrentWorkspaceName(string name)
        {
            _currentWorkspaceName = name;
        }

        /// <summary>
        /// avaiableWorkspacesDict setter
        /// </summary>
        private void SetAvailableWorkspacesDict()
        {
            _availableWorkspacesDict = GetAvailableWorkspaces();
        }

        /// <summary>
        /// Gets all avaiable directories in current working directory
        /// </summary>
        /// <returns>dict with avaiable workspaces</returns>
        private Dictionary<int, string> GetAvailableWorkspaces()
        {
            Dictionary<int, string> workspacesDict = new Dictionary<int, string>();

            string[] directories = Directory.GetDirectories(Directory.GetCurrentDirectory());
            int dirsNumber = directories.Length;

            if (dirsNumber == 0)
            {
                return workspacesDict;
            }
            
            for (int i = 0; i < dirsNumber; i++)
            {
                string[] workspaceName = directories[i].Split('\\');
                workspacesDict.Add(i + 1, workspaceName[workspaceName.Length - 1]);
            }

            return workspacesDict;
        }

        /// <summary>
        /// Prints avaiableWorkspacesDict
        /// </summary>
        private void PrintWorkspaces()
        {
            Console.Clear();
            if (_availableWorkspacesDict.Count != 0)
            {
                Console.WriteLine("Available workspaces:");
                foreach (KeyValuePair<int, string> keyValuePair in _availableWorkspacesDict)
                {
                    Console.WriteLine(string.Format("{0} - {1}", keyValuePair.Key, keyValuePair.Value));
                }
            }
            else
            {
                Console.WriteLine("No available workspaces, press enter to continue");
                _ = Console.ReadLine();
            }
        }

        /// <summary>
        /// Gets workspace name from user and creates new workspace
        /// </summary>
        private void AddWorkspace()
        {
            string workspaceName;

            while (true)
            {
                Console.Clear();
                Console.Write("Type workspace name (max 20 characters, without \\ / : * ? \" < > |): ");
                workspaceName = RemoveFofbriddenChars(Console.ReadLine());

                if (String.IsNullOrWhiteSpace(workspaceName))
                {
                    Console.WriteLine("\nWorkspace name cannot be empty, press enter to continue");
                    _ = Console.ReadLine();
                }
                else if (workspaceName.Length > 20)
                {
                    Console.WriteLine("\nToo long workspace name, press enter to continue");
                    _ = Console.ReadLine();
                }
                else
                {
                    if (workspaceName.Length > 1)
                    {
                        workspaceName = char.ToUpper(workspaceName[0]) + workspaceName.Substring(1);
                    }
                    else
                    {
                        workspaceName = workspaceName.ToUpper();
                    }

                    if (!_availableWorkspacesDict.ContainsValue(workspaceName))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nWorkspace already exist, press enter to continue");
                        _ = Console.ReadLine();
                        return;
                    }
                }
            }

            Directory.CreateDirectory(workspaceName);
            SetAvailableWorkspacesDict();

            Console.WriteLine("\nWorkspace created, press enter to continue\n");
            _ = Console.ReadLine();
        }

        /// <summary>
        /// Removes forbidden characters from given text
        /// </summary>
        /// <param name="text">text with forbidden characters</param>
        /// <returns>text witchout forbidden characters</returns>
        private string RemoveFofbriddenChars(string text)
        {
            foreach (string el in forbiddenChars)
            {
                text = text.Replace(el, "");
            }
            return text;
        }

        /// <summary>
        /// Gets user input and sets workspace name and path to the choosen one
        /// </summary>
        /// <returns>-1 if user choose exit option, 0 if no workspaces available, 1 if workspace chooser properly</returns>
        private int ChooseWorkspace()
        {
            int userInput;
            
            if (_availableWorkspacesDict.Count == 0)
            {
                PrintWorkspaces();
                return 0;
            }

            while (true)
            {
                PrintWorkspaces();
                Console.WriteLine(string.Format("\n{0} - Exit", _availableWorkspacesDict.Count() + 1));
                Console.Write("Option: ");
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    if (_availableWorkspacesDict.ContainsKey(userInput) || userInput == _availableWorkspacesDict.Count() + 1)
                    {
                        break;
                    }
                }
                catch (Exception e) when (e is FormatException || e is OverflowException) { }
            }

            if (userInput == _availableWorkspacesDict.Count() + 1)
            {
                return -1;
            }
            else
            {
                SetCurrentWorkspaceName(_availableWorkspacesDict[userInput]);
                SetCurrentWorkspacePath(Directory.GetCurrentDirectory() + "\\" + _availableWorkspacesDict[userInput]);
                return 1;
            }
        }

        /// <summary>
        /// Gets user input and deletes choosen workspace
        /// </summary>
        private void DeleteWorkspace()
        {
            int userInput;
            string userInputStr;

            if (_availableWorkspacesDict.Count == 0)
            {
                PrintWorkspaces();
                return;
            }

            while (true)
            {
                PrintWorkspaces();
                Console.WriteLine(string.Format("\n{0} - Exit", _availableWorkspacesDict.Count() + 1));
                Console.Write("Workspace to delete: ");
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    if (_availableWorkspacesDict.ContainsKey(userInput) || userInput == _availableWorkspacesDict.Count() + 1)
                    {
                        break;
                    }
                }
                catch (Exception e) when (e is FormatException || e is OverflowException) { }
            }

            if (userInput == _availableWorkspacesDict.Count() + 1)
            {
                return;
            }
            else
            {
                Console.Write(string.Format("\nAre You sure you want to delete workspace {0}? (y/n) ", _availableWorkspacesDict[userInput]));
                userInputStr = Console.ReadLine();

                if (string.Equals(userInputStr, "y"))
                {
                    Directory.Delete(Directory.GetCurrentDirectory() + "\\" + _availableWorkspacesDict[userInput], true);
                    SetAvailableWorkspacesDict();
                    
                    Console.WriteLine("\nWorkspace deleted, press enter to continue");
                    _ = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("\nAborted, press enter to continue");
                    _ = Console.ReadLine();
                }
            }
        }

        /// <summary>
        /// Prints WorkspaceHandler menu
        /// </summary>
        private void PrintWorkspaceMenu()
        {
            Console.Clear();
            Console.Write("1 - Add workspace" +
                "\n2 - Choose workspace" +
                "\n3 - Delete workspace" +
                "\n4 - Exit" +
                "\nOption: ");
        }
    }
}
