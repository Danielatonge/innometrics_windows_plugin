namespace SystemInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.MetricView = new System.Windows.Forms.ListView();
            this.Process_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.P_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stats = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.EndTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ip_address = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mac_address = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.des = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSendData = new System.Windows.Forms.Button();
            this.Battery = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // MetricView
            // 
            this.MetricView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Process_name,
            this.P_ID,
            this.stats,
            this.StartTime,
            this.EndTime,
            this.ip_address,
            this.mac_address,
            this.des,
            this.Battery});
            this.MetricView.Dock = System.Windows.Forms.DockStyle.Top;
            this.MetricView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MetricView.HideSelection = false;
            this.MetricView.Location = new System.Drawing.Point(0, 0);
            this.MetricView.Name = "MetricView";
            this.MetricView.Size = new System.Drawing.Size(1010, 378);
            this.MetricView.TabIndex = 0;
            this.MetricView.UseCompatibleStateImageBehavior = false;
            this.MetricView.View = System.Windows.Forms.View.Details;
            // 
            // Process_name
            // 
            this.Process_name.Text = "Process name";
            this.Process_name.Width = 145;
            // 
            // P_ID
            // 
            this.P_ID.Text = "PID";
            this.P_ID.Width = 79;
            // 
            // stats
            // 
            this.stats.Text = "Status";
            this.stats.Width = 87;
            // 
            // StartTime
            // 
            this.StartTime.Text = "Start time";
            this.StartTime.Width = 100;
            // 
            // EndTime
            // 
            this.EndTime.Text = "End time";
            this.EndTime.Width = 104;
            // 
            // ip_address
            // 
            this.ip_address.Text = "ip address";
            this.ip_address.Width = 78;
            // 
            // mac_address
            // 
            this.mac_address.Text = "mac address";
            this.mac_address.Width = 134;
            // 
            // des
            // 
            this.des.Text = "Description";
            this.des.Width = 166;
            // 
            // btnSendData
            // 
            this.btnSendData.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendData.Location = new System.Drawing.Point(445, 396);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(121, 27);
            this.btnSendData.TabIndex = 1;
            this.btnSendData.Text = "Send Data";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Battery
            // 
            this.Battery.Text = "Battery Consumed";
            this.Battery.Width = 115;
            // 
            // Form1
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1010, 435);
            this.Controls.Add(this.btnSendData);
            this.Controls.Add(this.MetricView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Collector";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView MetricView;
        private System.Windows.Forms.ColumnHeader Process_name;
        private System.Windows.Forms.ColumnHeader P_ID;
        private System.Windows.Forms.ColumnHeader stats;
        private System.Windows.Forms.ColumnHeader EndTime;
        private System.Windows.Forms.ColumnHeader ip_address;
        private System.Windows.Forms.ColumnHeader des;
        private System.Windows.Forms.ColumnHeader StartTime;
        private System.Windows.Forms.ColumnHeader mac_address;
        private System.Windows.Forms.Button btnSendData;
        private System.Windows.Forms.ColumnHeader Battery;
    }
}

