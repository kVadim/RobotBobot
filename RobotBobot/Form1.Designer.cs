namespace RobotBobot
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_RUN = new System.Windows.Forms.Button();
            this.button_STOP = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // button_RUN
            // 
            this.button_RUN.Location = new System.Drawing.Point(41, 30);
            this.button_RUN.Name = "button_RUN";
            this.button_RUN.Size = new System.Drawing.Size(92, 23);
            this.button_RUN.TabIndex = 0;
            this.button_RUN.Text = "RUN";
            this.button_RUN.UseVisualStyleBackColor = true;
            this.button_RUN.Click += new System.EventHandler(this.button_RUN_Click);
            // 
            // button_STOP
            // 
            this.button_STOP.Location = new System.Drawing.Point(153, 30);
            this.button_STOP.Name = "button_STOP";
            this.button_STOP.Size = new System.Drawing.Size(89, 23);
            this.button_STOP.TabIndex = 0;
            this.button_STOP.Text = "STOP";
            this.button_STOP.UseVisualStyleBackColor = true;
            this.button_STOP.Click += new System.EventHandler(this.button_STOP_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork_1);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 85);
            this.Controls.Add(this.button_STOP);
            this.Controls.Add(this.button_RUN);
            this.Name = "Form1";
            this.Text = "RobotBobot";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_RUN;
        private System.Windows.Forms.Button button_STOP;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}

