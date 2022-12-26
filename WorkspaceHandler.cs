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
        //var files = Directory.GetFiles(Directory.GetCurrentDirectory());
        private string currentWorkspacePath;
        private string currentWorkspaceName;
        private static Dictionary<int, string> availableWorkspaces = new Dictionary<int, string>();

        private void SetCurrentWorkspacePath(string path)
        {
            currentWorkspacePath = path;
        }

        private void SetCurrentWorkspaceName(string name)
        {
            currentWorkspaceName = name;
        }

        public static void SetAvailableWorkspaces()
        {
            availableWorkspaces = GetAvailableWorkspaces();
        }

        public static Dictionary<int, string> GetAvailableWorkspaces()
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

        public static void PrintWorkspaces()
        {
            if (availableWorkspaces.Count != 0)
            {
                Console.WriteLine("Available workspaces:");
                foreach (KeyValuePair<int, string> keyValuePair in availableWorkspaces)
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

        public static void AddWorkspace()
        {
            string currentDir = Directory.GetCurrentDirectory();
            string workspaceName;

            while (true)
            {
                Console.Write("Type workspace name (max 20 characters): ");
                workspaceName = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(workspaceName))
                {
                    Console.WriteLine("\nWorkspace name cannot be empty!\n");
                }
                else if (workspaceName.Length > 20)
                {
                    Console.WriteLine("\nToo long workspace name!\n");
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

                    if (!availableWorkspaces.ContainsValue(workspaceName))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nWorkspace already exist\n");
                    }
                }
            }

            Directory.CreateDirectory(workspaceName);

            Console.WriteLine("\nWorkspace created, press enter to continue\n");
            _ = Console.ReadLine();
        }

        public static void ChooseWorkspace()
        {
            int userInput;
            
            while (true)
            {
                PrintWorkspaces();
                Console.Write("Workspace: ");
                try
                {
                    userInput = Int16.Parse(Console.ReadLine());
                    if (availableWorkspaces.ContainsKey(userInput))
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
            
            
            
           
        }
    }
}
