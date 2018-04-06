using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Velib_Service_Subscrib
{
    public interface ICalSubscriberService
    {
        [OperationContract(IsOneWay = true)]
        void Subscribe(string contract, string station, int bikes);

        [OperationContract(IsOneWay = true)]
        void SubscribeFinished();
    }
}
