using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SOAP_LIB_VELIB
{
    [ServiceContract]
    public interface IServiceVelib
    {
        [OperationContract]
        IList<string> getCities();

        [OperationContract]
        IList<string> getStations(string city);

        [OperationContract]
        int getAvailableBikes(string city, string station, int delay);

        //[OperationContract]
        //IDictionary<string, int> getstationsAndBikes(string contract);
    }
}
