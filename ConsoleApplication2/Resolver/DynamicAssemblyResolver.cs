using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dispatcher;

namespace ConsoleApplication2
{
    public class DynamicAssemblyResolver : DefaultAssembliesResolver
    {
        public override ICollection<Assembly> GetAssemblies()
        {
            ICollection<Assembly> baseAssemblies = base.GetAssemblies();
            List<Assembly> assemblies = new List<Assembly>(baseAssemblies);

            // Add our controller library assembly
            try
            {
                if (ServerMode.generatedAssemblyAtHand != null)
                {
                    //Console.WriteLine("Loading....!!!!");
                    ////assemblies.Add(ServerMode.generatedAssemblyAtHand);
                    assemblies.Add(Assembly.LoadFrom("C:\\Newtonsoft.Json.dll"));
                }
                Console.WriteLine("Loading....!!!!");
                assemblies.Add(Assembly.LoadFrom(""));
                
                    
            }
            catch
            {
                // We ignore errors and just continue
            }

            return assemblies;
        }
    }
}
