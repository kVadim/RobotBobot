using System;
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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://data.btcchina.com/data/orderbook?limit=20");
            request.Method = "GET";
            request.Accept = "application/json";
            request.KeepAlive = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            StringBuilder output = new StringBuilder();
                output.Append(reader.ReadToEnd());

                string data = output.ToString();
                Logger.Log.Info(data);

             RootObject rootObject = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(data);
               // double asks = rootObject.asks.Average(innerList => innerList[0]);
               // double bids = rootObject.bids.Average(innerList => innerList[0]);

                double bids1 = rootObject.bids[0][0];
                double bids_BTC_1 = Math.Round(GetBTC(1, rootObject.bids), 2);
                double bids_BTC_5 = Math.Round(GetBTC(5, rootObject.bids), 2);
                double bids_BTC10 = Math.Round(GetBTC(10, rootObject.bids), 2);
                double bids_BTC20 = Math.Round(GetBTC(20, rootObject.bids), 2);

                rootObject.asks.Reverse();
                
                double asks1 = rootObject.asks[0][0];
                double asks_BTC_1 = Math.Round(GetBTC(1, rootObject.asks), 2);
                double asks_BTC_5 = Math.Round(GetBTC(5, rootObject.asks), 2);
                double asks_BTC10 = Math.Round(GetBTC(10, rootObject.asks), 2);
                double asks_BTC20 = Math.Round(GetBTC(20, rootObject.asks), 2);

                Logger.Log.Info("");
                Logger.Log.Info("______BIDS: " + bids1 + " " + bids_BTC_1 + " " + bids_BTC_5 + " " + bids_BTC10 + " " + bids_BTC20 + " ______ASKS: " + asks1 + " " + asks_BTC_1 + " " + asks_BTC_5 + " " + asks_BTC10 + " " + asks_BTC20);               
        }
        public static double GetBTC(int depth, List<List<double>> spot)
        {
            double diff = depth;
            int nubmer = depth;
            double BTC = 0;
            int i = 0;

            while (diff >= 0)
            {
                diff = diff - spot[i][1];
                i++;
            }
            for (int x = 0; x < i - 1; x++)
            {
                BTC = BTC + spot[x][0] * spot[x][1];
            }

            double left = (spot[i - 1][1] + diff);
            BTC = BTC + spot[i - 1][0] * left;
            BTC = BTC / nubmer;

            return BTC;
        }



        public class RootObject
        {
            public List<List<double>> asks { get; set; }
            public List<List<double>> bids { get; set; }
            public int date { get; set; }
        }

    }
}
