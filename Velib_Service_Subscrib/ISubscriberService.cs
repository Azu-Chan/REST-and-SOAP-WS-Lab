using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Velib_Service_Subscrib
{
    [ServiceContract(CallbackContract = typeof(ICalSubscriberService))]
    public interface ISubscriberService
    {
        [OperationContract]
        void Service(string contract, string station, int delay);

        [OperationContract]
        void SubscribeEvent();

        [OperationContract]
        void SubscribeFinishedEvent();
    }
}
