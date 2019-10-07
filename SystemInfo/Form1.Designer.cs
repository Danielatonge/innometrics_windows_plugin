namespace Sandbox
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.Process_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.P_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stats = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.uname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StartTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Mem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ProcessTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.des = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Process_name,
            this.P_ID,
            this.stats,
            this.uname,
            this.StartTime,
            this.Mem,
            this.ProcessTime,
            this.des});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(879, 435);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.ListView1_SelectedIndexChanged);
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
            // uname
            // 
            this.uname.Text = "Username";
            this.uname.Width = 104;
            // 
            // StartTime
            // 
            this.StartTime.Text = "Start time";
            this.StartTime.Width = 100;
            // 
            // Mem
            // 
            this.Mem.DisplayIndex = 4;
            this.Mem.Text = "Memory";
            this.Mem.Width = 78;
            // 
            // ProcessTime
            // 
            this.ProcessTime.Text = "Process time";
            this.ProcessTime.Width = 155;
            // 
            // des
            // 
            this.des.DisplayIndex = 5;
            this.des.Text = "Description";
            this.des.Width = 166;
            // 
            // Form1
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(879, 435);
            this.Controls.Add(this.listView1);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Process_name;
        private System.Windows.Forms.ColumnHeader P_ID;
        private System.Windows.Forms.ColumnHeader stats;
        private System.Windows.Forms.ColumnHeader uname;
        private System.Windows.Forms.ColumnHeader Mem;
        private System.Windows.Forms.ColumnHeader des;
        private System.Windows.Forms.ColumnHeader StartTime;
        private System.Windows.Forms.ColumnHeader ProcessTime;
    }
}

