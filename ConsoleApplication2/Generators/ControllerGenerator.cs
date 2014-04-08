using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication2.Models;
using System.Reflection;
using System.CodeDom.Compiler;


namespace ConsoleApplication2.Generators
{
    class ControllerGenerator
    {

        public Assembly generateControllersAndDll(DLLModel dllAtHand)
        {            
            IDictionary<string, string> compParams =
                             new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } };
            CodeDomProvider codeProvider = CodeDomProvider.CreateProvider("CSharp", compParams);
            string warningText = "Do Not delete contents in this directory.";
            string filePath = ".\\" + dllAtHand.getDllFileName() + "\\readme.txt";
            System.IO.FileInfo file = new System.IO.FileInfo(filePath);
            file.Directory.Create();
            System.IO.File.WriteAllText(file.FullName, warningText);

            string outputDll = ".\\" + dllAtHand.getDllFileName() + "\\" + dllAtHand.getDllFileName() + "_controllers" + ".dll";
            //string outputDll = "c:\\playdll\\" + dllAtHand.getDllFileName() + "_controllers" + ".dll";
            string codeForDllContents = dllAtHand.generateCodeForDllContents();

            System.IO.File.WriteAllText(".\\" + dllAtHand.getDllFileName() + "\\" + dllAtHand.getDllFileName() + ".txt", codeForDllContents);

            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = false;
            parameters.OutputAssembly = outputDll;
            parameters.ReferencedAssemblies.Add(@"System.Net.Http.dll");
            parameters.ReferencedAssemblies.Add(@"System.Net.Http.WebRequest.dll");
            parameters.ReferencedAssemblies.Add(@"System.Net.Http.Formatting.dll");
            parameters.ReferencedAssemblies.Add(@"System.Web.Http.dll");
            parameters.ReferencedAssemblies.Add(@AppDomain.CurrentDomain.BaseDirectory + "Newtonsoft.Json.dll");

            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, codeForDllContents);
            if (results.Errors.Count > 0)
            {
                Console.WriteLine("Build Failed");
                foreach (CompilerError CompErr in results.Errors)
                {
                    Console.WriteLine(
                                "Line number " + CompErr.Line +
                                ", Error Number: " + CompErr.ErrorNumber +
                                ", '" + CompErr.ErrorText + ";" +
                                Environment.NewLine + Environment.NewLine);
                }
            }
            else
            {
                Console.WriteLine("Build Succeeded");
                return Assembly.LoadFrom(outputDll);
            }
            return null;
        }


    }
}
