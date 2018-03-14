using Newtonsoft.Json.Linq;
using SOAP_LIB_VELIB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Wcf_SOAP_Velib
{
    public class ServiceVelib : IServiceVelib
    {
        private static string API_KEY = "9e2a4e6b9b6673d458518520fc1931a3220ae1a0";

        public IList<string> getCities()
        {
            WebRequest req = WebRequest.Create(@"https://api.jcdecaux.com/vls/v1/contracts?apiKey="+API_KEY);
            JArray jsonArray = null;
            IList<string> listContracts = new List<string>();

            WebResponse response = req.GetResponse();
            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            jsonArray = JArray.Parse(responseFromServer);
            foreach (JObject item in jsonArray)
            {
                listContracts.Add((string)(item["name"]));
            }

            return listContracts;
        }

        public IList<string> getStations(string contract)
        {
            JArray jsonArray = null;
            IList<string> listStations = new List<string>();
            WebRequest req = WebRequest.Create(@"https://api.jcdecaux.com/vls/v1/stations?contract=" + contract + @"&apiKey=" + API_KEY);

            WebResponse response = req.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            jsonArray = JArray.Parse(responseFromServer);

            foreach (JObject item in jsonArray)
            {
                listStations.Add((string)(item["name"]));
            }

            return listStations;
        }

        public int getAvailableBikes(string contract, string station)
        {
            JArray jsonArray = null;
            WebRequest req = WebRequest.Create(@"https://api.jcdecaux.com/vls/v1/stations?contract=" + contract + @"&apiKey=" + API_KEY);

            WebResponse response = req.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            jsonArray = JArray.Parse(responseFromServer);

            foreach (JObject item in jsonArray)
            {
                string name = (string)item.SelectToken("name");
                if (name.Contains(station))
                {
                    return (int)item.SelectToken("available_bikes");
                }
            }

            return -1;
        }

        public IDictionary<string, int> getstationsAndBikes(string contract)
        {
            JArray jsonArray = null;
            WebRequest req = WebRequest.Create(@"https://api.jcdecaux.com/vls/v1/stations?contract=" + contract + @"&apiKey=" + API_KEY);

            WebResponse response = req.GetResponse();
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            response.Close();

            IDictionary<string, int> values = new Dictionary<string, int>();

            jsonArray = JArray.Parse(responseFromServer);

            foreach (JObject item in jsonArray)
            {
                values.Add((string)item.SelectToken("name"), (int)item.SelectToken("available_bikes"));
            }

            return values;
        }
    }
}
