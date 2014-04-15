using System;
using CoreFramework.Modes;

namespace ConsoleApplication2
{
    class Program
    { 
        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                userInteractionMode();
            }
            else if (args[0] != null && args[0].Equals("configure", StringComparison.OrdinalIgnoreCase))
            {
                obtainUserInput();
            }
            else if (args[0] != null && args[0].Equals("server", StringComparison.OrdinalIgnoreCase))
            {
                setupAndStartServerMode();
            }
            else
            {
                Console.WriteLine("Incorrect option specified");
            }
        }

        private static void userInteractionMode(){
            Console.WriteLine("Please specify what mode you want to run this program in, options are as below,");
            Console.WriteLine("configure");
            Console.WriteLine("server");
            Console.WriteLine("");
            Console.Write(">");
            string input = Console.ReadLine();
            if (input != null && input.Equals("configure", StringComparison.OrdinalIgnoreCase)) {
                obtainUserInput();
            } else if (input != null && input.Equals("server", StringComparison.OrdinalIgnoreCase))
            {
                setupAndStartServerMode();
            } else {
                Console.WriteLine("Incorrect option specified");
            }
        }

        private static void obtainUserInput()
        {
            UserInteraction.doUserInteraction();
        }

        private static void setupAndStartServerMode()
        {
            ServerMode.doConfigurationAndServerStartup();             
        }
    }
}