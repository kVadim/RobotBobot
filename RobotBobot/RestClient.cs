using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace RobotBobot
{
    public static class RestClient
    {
        public static void GetData()
        {
            HttpWebRequest request;
            HttpWebResponse response;
            StreamReader reader;
            StringBuilder output;

            for (int i = 0; i < 10; i++)
            {
                request = (HttpWebRequest)WebRequest.Create("https://data.btcchina.com/data/orderbook?limit=10");
                request.Method = "GET";
                request.Accept = "application/json";
                request.KeepAlive = false;
                response = (HttpWebResponse)request.GetResponse();

                reader = new StreamReader(response.GetResponseStream());
                output = new StringBuilder();
                output.Append(reader.ReadToEnd());

                string data = output.ToString();
                Logger.Log.Info(data);

                RootObject rootObject = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(data);
                double asks = rootObject.asks.Average(innerList => innerList[0]);
                double bids = rootObject.bids.Average(innerList => innerList[0]);
                Logger.Log.Info("Avarage asks- " + asks);
                Logger.Log.Info("Avarage bids- " + bids);

            }
        }

        public class RootObject
        {
            public List<List<double>> asks { get; set; }
            public List<List<double>> bids { get; set; }
            public int date { get; set; }
        }

    }
}
