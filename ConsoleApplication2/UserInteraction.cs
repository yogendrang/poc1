using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ConsoleApplication2.Models;

namespace ConsoleApplication2
{
    class UserInteraction
    {
        public static void doUserInteraction()
        {
            Console.WriteLine("In User input mode");
            string dllPath = ConfigurationManager.AppSettings["dllFilePath"];
            Console.WriteLine("DLL File is : " + dllPath);
            if (dllPath != null && dllPath.EndsWith(".dll"))
            {
                Models.DLLModel dLLAtHand = new DLLModel(dllPath);
                dLLAtHand = dLLAtHand.processDll(dLLAtHand);
                printOutClassesAndMethods(dLLAtHand);
            }
            else
            {
                Console.WriteLine("DLL Path information is not properly configured.");
            }
            
        }

        private static void printOutClassesAndMethods(DLLModel dLLModel)
        {
            Console.WriteLine("Inside Here" + dLLModel.getFullyQualifiedPath());
            Dictionary<string, ClassModel> allClassesInDll =
                dLLModel.getAllClassesInThisDll();
            foreach(KeyValuePair<string, ClassModel> pair in allClassesInDll) {
                ClassModel classAtHand = pair.Value;
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("Class ---> " + classAtHand.getClassName());
                Console.WriteLine("");
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("Methods in this class");
                Console.WriteLine("");
                Console.WriteLine("-------------------------------------------");

                SortedList<string, MethodModel> methodsInClass = classAtHand.getAllMethodsInThisClass();
                foreach (KeyValuePair<string, MethodModel> methodPair in methodsInClass)
                {
                    MethodModel methodAtHand = methodPair.Value;
                    Console.WriteLine("  Returns " + methodAtHand.getMethodReturnType() + "-----" + '\t' + methodAtHand.getShortenedName() );
                }
                Console.WriteLine("");
                Console.WriteLine("");
            }
        }
    }
}
