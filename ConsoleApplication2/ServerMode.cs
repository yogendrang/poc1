using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using System.Net.Http;
using System.ServiceModel;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;
using System.Configuration;
using CoreFramework.Models;
using CoreFramework.Processor;
using CoreFramework.Generators;
using CoreFramework.Modes;


namespace ConsoleApplication2
{
    class ServerMode
    {
        static readonly Uri _baseAddress = new Uri("http://localhost:60064");

        public static void doConfigurationAndServerStartup(){
            if (ServerModeDelegate.doConfiguration())
            {
                startServer();
            }            
        }                        

        public static void startServer()
        {
            HttpSelfHostServer server = null;
            try
            {
                // Set up server configuration
                HttpSelfHostConfiguration config = new HttpSelfHostConfiguration(_baseAddress);
                config.HostNameComparisonMode = HostNameComparisonMode.Exact;
                config.Routes.MapHttpRoute(
                 name: "Api default",
                 routeTemplate: "api/{controller}/{id}",
                 defaults: new { id = RouteParameter.Optional }
                );

                // Create server
                server = new HttpSelfHostServer(config);
                // Start listening
                server.OpenAsync().Wait();
                Console.WriteLine("Listening on " + _baseAddress);
                while (true)
                {
                    // Run HttpClient issuing requests
                    Console.WriteLine("Press Ctrl+C to exit...");
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not start server: {0}", e.GetBaseException().Message);
                Console.WriteLine("Hit ENTER to exit...");
                Console.ReadLine();
            }
            finally
            {
                if (server != null)
                {
                    // Stop listening
                    server.CloseAsync().Wait();
                }
            }
        }
    }
}
