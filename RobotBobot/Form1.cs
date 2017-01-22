using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            if (backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            button_STOP.Enabled = true;
            button_RUN.Enabled = false;
        }

        private void button_STOP_Click(object sender, EventArgs e)
        {
            
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                backgroundWorker1.CancelAsync();
            }
            button_RUN.Enabled = true;
            button_STOP.Enabled = false;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
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
                    Thread.Sleep(500);
                    //worker.ReportProgress(i * 10);
                }
            }
        }

        // This event handler updates the progress.
        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
        //}

        // This event handler deals with the results of the background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
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
    }
}
