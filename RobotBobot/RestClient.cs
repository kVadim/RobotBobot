using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RobotBobot
{
    public static class RestClient
    {
        public static void GetData()
        {
            for (int i =0; i<20; i++)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://data.btcchina.com/data/orderbook?limit=10");
                request.Method = "GET";
                request.Accept = "application/json";
                request.KeepAlive = false;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                StringBuilder output = new StringBuilder();
                output.Append(reader.ReadToEnd());

                string data = output.ToString();
                Console.WriteLine(data);

                RootObject rootObject = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(data);
                double asks = rootObject.asks.Average(innerList => innerList[0]);
                Console.WriteLine(asks);
                double bids = rootObject.bids.Average(innerList => innerList[0]);
                Console.WriteLine(bids);
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
