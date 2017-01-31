using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotBobot
{
    public partial class Form1 : Form
    {
        
       
        public Form1()
        {
            InitializeComponent();
            Logger.InitLogger();
            Logger.Log.Info("Started");
            button_STOP.Enabled = false;
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private void button_RUN_Click(object sender, EventArgs e)
        {
            button_STOP.Enabled = true;
            button_RUN.Enabled = false;
            //GUI.InitializeWebDriver();
            //GUI.Login();
            //Thread.Sleep(5000);
            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void button_STOP_Click(object sender, EventArgs e)
        {
            button_RUN.Enabled = true;
            button_STOP.Enabled = false;

            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                backgroundWorker1.CancelAsync();
            }
           
        }


        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            
            BackgroundWorker worker = sender as BackgroundWorker;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                   RestClient.GetData();
                   //  GUI.GetOrderPrice();
                   // Thread.Sleep(100);
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted_1(object sender, RunWorkerCompletedEventArgs e)
        {
            //GUI.CloseApp();
            if (e.Cancelled == true)
            {
                //resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                //resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                //resultLabel.Text = "Done!";
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

       
    }

    public static class GUI {

        public static IWebDriver driver;
        public static IJavaScriptExecutor js;

        #region read elements from page ---

        static public IWebElement emeil { get { return driver.FindElement(By.Name("email")); } }
        static public IWebElement password { get { return driver.FindElement(By.Name("password")); } } 
        static public IWebElement trade { get { return driver.FindElement(By.CssSelector("div>a[href='https://www.btcchina.com/exc/trade/cnybtc']")); } }

        static public IWebElement Asks_BTCChina { get { return driver.FindElement(By.CssSelector(".asks")); } }
        static public IWebElement Bids_BTCChina { get { return driver.FindElement(By.CssSelector(".bids")); } }

        #endregion

        public static void InitializeWebDriver()
        {

            var chromeDriverService = ChromeDriverService.CreateDefaultService();
            chromeDriverService.HideCommandPromptWindow = true;
            driver = new ChromeDriver(chromeDriverService, new ChromeOptions());
            driver.Navigate().GoToUrl("https://www.btcchina.com");
            driver.Manage().Window.Maximize();

            js = (IJavaScriptExecutor)driver;

        }

         public static void CloseApp()
         {
             driver.Quit();
         }

         public static void Login()
         {

             emeil.SendKeys("BTC960882@gmail.com");
             password.SendKeys("AB288069");
             password.SendKeys(OpenQA.Selenium.Keys.Enter);
             Thread.Sleep(8000);
             trade.Click();

         }
         public static void GetOrderPrice()
         {
             string currentAsks = Asks_BTCChina.Text;
             string currentBids = Bids_BTCChina.Text;

             Logger.Log.Info(String.Format("{0} : {1}", AvarageValues(currentAsks), AvarageValues(currentBids)));
         }

         public static string AvarageValues(string row)
         {
             string[] values = Regex.Split(row, "\r\n");
             string[] a = new string[] { values[0].Substring(0, 5), 
                                         values[3].Substring(0, 5),
                                         values[6].Substring(0, 5), 
                                         values[9].Substring(0, 5), 
                                         values[12].Substring(0, 5) 
                                       };
             string[] b = new string[] { values[1], values[4], values[7], values[10], values[13] };
             string[] c = new string[] { values[2], values[5], values[8], values[11], values[14] };
             double[] d = Array.ConvertAll(a, s => double.Parse(s));
             double[] e = Array.ConvertAll(b, s => double.Parse(s, CultureInfo.InvariantCulture));
             double[] h = Array.ConvertAll(c, s => double.Parse(s, CultureInfo.InvariantCulture));
             double average1 = d.Average();
             double average2 = e.Average();
             double average3 = h.Average();

             return String.Format("{0} : {1} : {2}", average1, average2, average3);
         }
    }
}
