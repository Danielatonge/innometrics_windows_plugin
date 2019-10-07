using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using System.Dynamic;
using System.Net.Http;
using static System.Windows.Forms.ListView;

namespace LOGINFORM
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public async Task renderProcessesOnListView()
        {
            HashSet<string> hSet = new HashSet<string>();
            while (true)
            {
                // Create an array to store the processes
                Process[] processList = Process.GetProcesses();

                // Create an Imagelist that will store the icons of every process
                ImageList Imagelist = new ImageList();

                // Loop through the array of processes to show information of every process in your console

                foreach (Process process in processList)
                {

                    if (!hSet.Contains(process.Id.ToString()))
                    {
                        // Define the status from a boolean to a simple string
                        string status = (process.Responding == true ? "Responding" : "Not responding");

                        // Retrieve the object of extra information of the process (to retrieve Username and Description)
                        dynamic extraProcessInfo = GetProcessExtraInformation(process.Id);

                        int condition = (process.ProcessName.CompareTo("Idle"));
                        // Create an array of string that will store the information to display in our 
                        string[] row = {
                        // 1 Process name
                        process.ProcessName,
                        // 2 Process ID
                        process.Id.ToString(),
                        // 3 Process status
                        status,
                        // 4 Username that started the process
                        extraProcessInfo.Username,
                         // 5 Time associated process was started
                        (condition != 0)?process.StartTime.ToString(): "",
                        // 6 Memory usage
                        BytesToReadableValue(process.PrivateMemorySize64),
                   
                        // 7 Total processor time for this process
                        (condition != 0)?process.TotalProcessorTime.ToString(): "",
                        // 8 Description of the process
                        extraProcessInfo.Description
                     };


                        //Console.WriteLine(process.ProcessName);
                        //Console.WriteLine("D_A_N_I_E_L"+process.StartTime.ToString());
                        //Console.WriteLine(process.TotalProcessorTime.ToString());
                        //
                        // As not every process has an icon then, prevent the app from crash
                        try
                        {
                            Imagelist.Images.Add(
                                // Add an unique Key as identifier for the icon (same as the ID of the process)
                                process.Id.ToString(),
                                // Add Icon to the List 
                                Icon.ExtractAssociatedIcon(process.MainModule.FileName).ToBitmap()
                            );
                        }
                        catch { }

                        // Create a new Item to add into the list view that expects the row of information as first argument
                        ListViewItem item = new ListViewItem(row)
                        {
                            // Set the ImageIndex of the item as the same defined in the previous try-catch
                            ImageIndex = Imagelist.Images.IndexOfKey(process.Id.ToString())
                        };



                        // Add the Item

                        hSet.Add(process.Id.ToString());
                        Metrics.Items.Add(item);
                    }
                }

                // Set the imagelist of your list view the previous created list :)
                Metrics.LargeImageList = Imagelist;
                Metrics.SmallImageList = Imagelist;

                await Task.Delay(8000);
            }
        }

        /// <summary>
        /// Method that converts bytes to its human readable value
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public string BytesToReadableValue(long number)
        {
            List<string> suffixes = new List<string> { " B", " KB", " MB", " GB", " TB", " PB" };

            for (int i = 0; i < suffixes.Count; i++)
            {
                long temp = number / (int)Math.Pow(1024, i + 1);

                if (temp == 0)
                {
                    return (number / (int)Math.Pow(1024, i)) + suffixes[i];
                }
            }

            return number.ToString();
        }

        /// <summary>
        /// Returns an Expando object with the description and username of a process from the process ID.
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public ExpandoObject GetProcessExtraInformation(int processId)
        {
            // Query the Win32_Process
            string query = "Select * From Win32_Process Where ProcessID = " + processId;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            ManagementObjectCollection processList = searcher.Get();

            // Create a dynamic object to store some properties on it
            dynamic response = new ExpandoObject();
            response.Description = "";
            response.Username = "Unknown";

            foreach (ManagementObject obj in processList)
            {
                // Retrieve username 
                string[] argList = new string[] { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    // return Username
                    response.Username = argList[0];

                    // You can return the domain too like (PCDesktop-123123\Username using instead
                    //response.Username = argList[1] + "\\" + argList[0];
                }

                // Retrieve process description if exists
                if (obj["ExecutablePath"] != null)
                {
                    try
                    {
                        FileVersionInfo info = FileVersionInfo.GetVersionInfo(obj["ExecutablePath"].ToString());
                        response.Description = info.FileDescription;
                    }
                    catch { }
                }
            }

            return response;
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            renderProcessesOnListView();
        
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            int number_of_items = Metrics.Items.Count;
            ListViewItemCollection items = Metrics.Items;
            for (int i = 0; i < 3; i++)
            {
                string activity = items[i].SubItems[3].ToString();
                string start = items[i].SubItems[4].ToString();
                string end = items[i].SubItems[5].ToString();
                string name = items[i].SubItems[0].ToString();
                string ip_address = items[i].SubItems[1].ToString();
                string mac = items[i].SubItems[6].ToString();
                string data = $"activity={activity}&start_time={start}&end_time={end}&executable_name={name}&ip_address={ip_address}&mac_address={mac}";
                MyWebRequest myRequest = new MyWebRequest("https://innometrics-12856.firebaseapp.com/activity", "POST",data);
                MessageBox.Show(myRequest.GetResponse());
            }
            
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            Metrics.Items.Clear();
        }
    }
}
