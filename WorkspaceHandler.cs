using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker
{
    class WorkspaceHandler
    {
        public static void PrintWorkspaces(string filepath = "Test")
        {
            string[] directories = Directory.GetDirectories(Directory.GetCurrentDirectory());
            //var files = Directory.GetFiles(Directory.GetCurrentDirectory());
            int dirsNumber = directories.Length;

            List<string> workspaces = new List<string>();

            if (dirsNumber == 0)
            {
                Console.WriteLine("No workspaces");
                _ = Console.ReadLine();
                return;
            }
            else
            {
                for (int i = 0; i < dirsNumber; i++)
                {
                    string[] WorkspaceName = directories[i].Split('\\');
                    workspaces.Add(WorkspaceName[WorkspaceName.Length - 1]);
                }
            }
            for (int i = 0; i < dirsNumber; i++)
            {
                Console.WriteLine(string.Format("{0} - {1}", i + 1, workspaces[i]));
            }
            _ = Console.ReadLine();
        }

        // TODO check if workspace exist
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
                    Console.WriteLine("\nToo long name\n");
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
                    break;
                }
            }


            Directory.CreateDirectory(workspaceName);

            currentWorkspace = currentDir + "\\" + workspaceName;

            Console.WriteLine("\nWorkspace created, press enter to continue\n");
            _ = Console.ReadLine();
        }
    }
}
