using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Velib_Events
{
    class ServiceCallback : SubService.ISubscriberServiceCallback
    {
        public void Subscribe(string contract, string station, int bikes)
        {
            Console.WriteLine(contract + " : " + station + " :\n");
            if (bikes == -1)
            {
                Console.WriteLine("ERROR");
            }
            else
            {
                Console.WriteLine(bikes + " aviable bikes...");
            }
        }

        public void SubscribeFinished()
        {
            Console.WriteLine("END");
        }
    }
}
