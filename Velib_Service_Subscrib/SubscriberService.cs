using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Velib_Service_Subscrib
{
    public class SubscriberService : ISubscriberService
    {
        private static readonly string API_KEY = "9e2a4e6b9b6673d458518520fc1931a3220ae1a0";
        public const int DelayMilliseconds = 10000;

        static Action<string, string, int> m_Event1 = delegate { };

        static Action m_Event2 = delegate { };
        public void Service(string contract, string station, int time)
        {
            int nb_bikes;
            nb_bikes = getAvailableBikes(contract, station);
            m_Event1(contract, station, time);
            m_Event2();
        }

        public void SubscribeEvent()
        {
            ICalSubscriberService subscriber = OperationContext.Current.GetCallbackChannel<ICalSubscriberService>();
            m_Event1 += subscriber.Subscribe;
        }

        public void SubscribeFinishedEvent()
        {
            ICalSubscriberService subscriber = OperationContext.Current.GetCallbackChannel<ICalSubscriberService>();
            m_Event2 += subscriber.SubscribeFinished;
        }

        private int getAvailableBikes(string contract, string station)
        {

            WebRequest requete = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + contract + "&apiKey=" + API_KEY);
            WebResponse response = requete.GetResponse();
            Stream stream = response.GetResponseStream();

            StreamReader reader = new StreamReader(stream);
            string restr = reader.ReadToEnd();

            response.Close();

            reader.Close();

            JArray jsonArray = JArray.Parse(restr);
            int size = jsonArray.Count;
            foreach (JObject item in jsonArray)
            {
                string name = ((string)item["name"]).ToLower();

                if (((string)(item["name"])).ToLower().Contains(station.ToLower()))
                {
                    int nb = (int)item["available_bikes"];
                    return nb;
                }
            }

            return -1;
        }
    }
}
