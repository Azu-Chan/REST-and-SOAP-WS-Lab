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

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BegingetCities(AsyncCallback callback, object state);
        IList<string> EndgetCities(IAsyncResult asyncResult);

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BegingetStations(string city, AsyncCallback callback, object state);
        IList<string> EndgetStations(IAsyncResult asyncResult);

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BegingetAvailableBikes(string city, string station, int delay, AsyncCallback callback, object state);
        int EndgetAvailableBikes(IAsyncResult asyncResult);

        //[OperationContract]
        //IDictionary<string, int> getstationsAndBikes(string contract);
    }
}
