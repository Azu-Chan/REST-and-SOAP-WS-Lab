using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Console_Velib_Events
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCallback objsink = new ServiceCallback();
            InstanceContext iCntxt = new InstanceContext(objsink);
            SubService.SubscriberServiceClient objClient = new SubService.SubscriberServiceClient(iCntxt);
            objClient.SubscribeEvent();
            objClient.SubscribeFinishedEvent();
            objClient.Service("Nantes", "00033 - RACINE", 0);
            Console.WriteLine("Press key and exit...");
            Console.ReadKey();
        }
    }
}
