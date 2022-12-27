using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker
{
    static class FieldEditor
    {
        public static void MainFieldEditor()
        {

        }

        private static void PrintEditorMenu()
        {
            Console.Clear();
            Console.Write("1 - Change type" +
                "\n2 - Change priority" +
                "\n3 - Change status" +
                "\n4 - Delete field" +
                "\n5 - Exit" +
                "Option: ");
        }


        private static void EditField<T>() where T : Enum
        {

        }

        private static void DeleteField()
        {

        }
    }
}
