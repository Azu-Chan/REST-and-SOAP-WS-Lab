using Newtonsoft.Json.Linq;
using SOAP_LIB_VELIB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace Wcf_SOAP_Velib
{
    public class ServiceVelib : IServiceVelib
    {
        public const int DelayMilliseconds = 10000;
        private static string API_KEY = "9e2a4e6b9b6673d458518520fc1931a3220ae1a0";

        private static Dictionary<string, Dictionary<string, StationProperties>> tampon = new Dictionary<string, Dictionary<string, StationProperties>>();

        public IList<string> getCities()
        {
            IList<string> listContracts = new List<string>();

            if (tampon.Count < 1)
            {
                WebRequest req = WebRequest.Create(@"https://api.jcdecaux.com/vls/v1/contracts?apiKey=" + API_KEY);
                JArray jsonArray = null;

                WebResponse response = req.GetResponse();
                Stream dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();

                reader.Close();
                response.Close();

                jsonArray = JArray.Parse(responseFromServer);
                foreach (JObject item in jsonArray)
                {
                    tampon.Add((string)(item["name"]), new Dictionary<string, StationProperties>());
                }
            }
            foreach (KeyValuePair<string, Dictionary<string, StationProperties>> entry in tampon)
            {
                listContracts.Add(entry.Key);
            }

            return listContracts;
        }

        public IList<string> getStations(string contract)
        {
            IList<string> listStations = new List<string>();

            if (!tampon.ContainsKey(contract))
                return null;

            if (tampon[contract].Count < 1)
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
                    tampon[contract].Add((string)(item["name"]), new StationProperties((int)(item["number"]), null, -1));
                }
            }

            foreach (KeyValuePair<string, StationProperties> entry in tampon[contract])
            {
                listStations.Add(entry.Key);
            }

            return listStations;
        }

        public int getAvailableBikes(string contract, string station, int delay)
        {
            JObject jsonObject = null;

            StationProperties p = tampon[contract][station];
            if (checkUpdate(p, delay))
            {
                int station_number = tampon[contract][station].StationNumber;
                WebRequest req = WebRequest.Create(@"https://api.jcdecaux.com/vls/v1/stations/" + station_number + @"?contract="
                    + contract + @"&apiKey=" + API_KEY);

                WebResponse response = req.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                Stream dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);

                string responseFromServer = reader.ReadToEnd();

                reader.Close();
                response.Close();

                jsonObject = JObject.Parse(responseFromServer);

                p.AviableBikes = (int)jsonObject["available_bikes"];
                p.LastUpdate = DateTime.Now;
            }

            return p.AviableBikes;
        }

        private bool checkUpdate(StationProperties p, int delay)
        {
            if (p.AviableBikes == -1) return true;
            if (p.LastUpdate == null) return true;

            if (DateTime.Now.Subtract(p.LastUpdate.Value).TotalSeconds >= delay) return true;

            return false;
        }

        public IAsyncResult BegingetCities(AsyncCallback callback, object state)
        {
            var asyncResult = new SimpleAsyncResult<IList<string>>(state);

            // mimic a long running operation
            var timer = new System.Timers.Timer(DelayMilliseconds);
            timer.Elapsed += (_, args) =>
            {
                asyncResult.Result = getCities();
                asyncResult.IsCompleted = true;
                callback(asyncResult);
                timer.Enabled = false;
                timer.Close();
            };
            timer.Enabled = true;
            return asyncResult;
        }

        public IList<string> EndgetCities(IAsyncResult asyncResult)
        {
            return ((SimpleAsyncResult<IList<string>>)asyncResult).Result;
        }

        public IAsyncResult BegingetStations(string city, AsyncCallback callback, object state)
        {
            var asyncResult = new SimpleAsyncResult<IList<string>>(state);

            // mimic a long running operation
            var timer = new System.Timers.Timer(DelayMilliseconds);
            timer.Elapsed += (_, args) =>
            {
                asyncResult.Result = getStations(city);
                asyncResult.IsCompleted = true;
                callback(asyncResult);
                timer.Enabled = false;
                timer.Close();
            };
            timer.Enabled = true;
            return asyncResult;
        }

        public IList<string> EndgetStations(IAsyncResult asyncResult)
        {
            return ((SimpleAsyncResult<IList<string>>)asyncResult).Result;
        }

        public IAsyncResult BegingetAvailableBikes(string city, string station, int delay, AsyncCallback callback, object state)
        {
            var asyncResult = new SimpleAsyncResult<int>(state);

            // mimic a long running operation
            var timer = new System.Timers.Timer(DelayMilliseconds);
            timer.Elapsed += (_, args) =>
            {
                asyncResult.Result = getAvailableBikes(city, station, delay);
                asyncResult.IsCompleted = true;
                callback(asyncResult);
                timer.Enabled = false;
                timer.Close();
            };
            timer.Enabled = true;
            return asyncResult;
        }

        public int EndgetAvailableBikes(IAsyncResult asyncResult)
        {
            return ((SimpleAsyncResult<int>)asyncResult).Result;
        }

        [Obsolete("Obsolète depuis la suspression du cache client", true)]
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


    public class SimpleAsyncResult<T> : IAsyncResult
    {
        private readonly object accessLock = new object();
        private bool isCompleted = false;
        private T result;

        public SimpleAsyncResult(object asyncState)
        {
            AsyncState = asyncState;
        }

        public T Result
        {
            get
            {
                lock (accessLock)
                {
                    return result;
                }
            }
            set
            {
                lock (accessLock)
                {
                    result = value;
                }
            }
        }

        public bool IsCompleted
        {
            get
            {
                lock (accessLock)
                {
                    return isCompleted;
                }
            }
            set
            {
                lock (accessLock)
                {
                    isCompleted = value;
                }
            }
        }

        // WCF seems to use the async callback rather than checking the wait handle
        // so we can safely return null here.
        public WaitHandle AsyncWaitHandle { get { return null; } }

        // We will always be doing an async operation so completed synchronously should always
        // be false.
        public bool CompletedSynchronously { get { return false; } }

        public object AsyncState { get; private set; }

        WaitHandle IAsyncResult.AsyncWaitHandle => throw new NotImplementedException();
    }
}
