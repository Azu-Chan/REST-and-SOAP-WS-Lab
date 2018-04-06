using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Velib_Service_Subscrib;

namespace EventConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ServiceHost host = new ServiceHost(typeof(SubscriberService));
                host.Open();
                Console.WriteLine("Service is Hosted as http://localhost:9011/SubService");
                Console.WriteLine("\nPress a key to stop the service.");
                Console.ReadLine();
                host.Close();
            }
            catch (Exception eX)
            {
                Console.WriteLine("There was en error while Hosting Service [" + eX.Message + "]");
                Console.WriteLine("\nPress a key to close.");
                Console.ReadLine();
            }
        }
    }
}
