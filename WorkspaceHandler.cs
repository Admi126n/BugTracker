using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker
{
    class WorkspaceHandler
    {
        private string currentWorkspacePath;
        private string currentWorkspaceName;
        private Dictionary<int, string> availableWorkspacesDict = new Dictionary<int, string>();

        public WorkspaceHandler()
        {
            availableWorkspacesDict = GetAvailableWorkspaces();
        }

        private void SetCurrentWorkspacePath(string path)
        {
            currentWorkspacePath = path;
        }

        private void SetCurrentWorkspaceName(string name)
        {
            currentWorkspaceName = name;
        }

        private void SetAvailableWorkspacesDict()
        {
            availableWorkspacesDict = GetAvailableWorkspaces();
        }

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

        public string GetCurrentWorkspaceName()
        {
            return currentWorkspaceName;
        }

        private void PrintWorkspaces()
        {
            if (availableWorkspacesDict.Count != 0)
            {
                Console.WriteLine("Available workspaces:");
                foreach (KeyValuePair<int, string> keyValuePair in availableWorkspacesDict)
                {
                    Console.WriteLine(string.Format("{0} - {1}", keyValuePair.Key, keyValuePair.Value));
                }
            }
            else
            {
                Console.WriteLine("\nNo available workspaces, press enter to continue");
                _ = Console.ReadLine();
            }
        }

        private void AddWorkspace()
        {
            string workspaceName;

            while (true)
            {
                Console.Clear();
                Console.Write("Type workspace name (max 20 characters): ");
                workspaceName = Console.ReadLine();

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

                    if (!availableWorkspacesDict.ContainsValue(workspaceName))
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

        private int ChooseWorkspace()
        {
            int userInput;
            
            if (availableWorkspacesDict.Count == 0)
            {
                PrintWorkspaces();
                return 0;
            }

            while (true)
            {
                Console.Clear();
                PrintWorkspaces();
                Console.WriteLine(string.Format("\n{0} - Exit", availableWorkspacesDict.Count() + 1));
                Console.Write("Option: ");
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    if (availableWorkspacesDict.ContainsKey(userInput) || userInput == availableWorkspacesDict.Count() + 1)
                    {
                        break;
                    }
                }
                catch (Exception e) when (e is FormatException || e is OverflowException)
                {
                }
            }

            if (userInput == availableWorkspacesDict.Count() + 1)
            {
                return -1;
            }
            else
            {
                SetCurrentWorkspaceName(availableWorkspacesDict[userInput]);
                SetCurrentWorkspacePath(Directory.GetCurrentDirectory() + "\\" + availableWorkspacesDict[userInput]);
                return 1;
            }
        }

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
                            return currentWorkspacePath;
                        }
                        break;
                }
            }
        }

        private void PrintWorkspaceMenu()
        {
            Console.Clear();
            Console.Write("1 - Add workspace" +
                "\n2 - Choose workspace" +
                "\nOption: ");
        }
    }
}
