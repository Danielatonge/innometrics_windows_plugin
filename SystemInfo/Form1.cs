using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Required namespaces
using System.Diagnostics;
using System.Management;
using System.Dynamic;
using RestSharp;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.NetworkInformation;
using System.Linq;

namespace SystemInfo
{
    public partial class Form1 : Form
    {
        public static List<DataLine> listData;
        WinEventDelegate dele = null;
        DateTime begin_time;
        public Form1()
        {
            InitializeComponent();
            listData = new List<DataLine>();
            begin_time = DateTime.Now;
            dele = new WinEventDelegate(WinEventProc);
            IntPtr m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, dele, 0, 0, WINEVENT_OUTOFCONTEXT);
        }

        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            //get last active window and update the ending time
            // Log.Text +=  GetActiveWindowTitle() + "\r\n";
            MetricView.Items.Clear();

            uint process_id;
            GetWindowThreadProcessId(GetWindow(GetForegroundWindow(), 3), out process_id);
            foreach (DataLine item in listData)
            {
                if(item.ProcessId == process_id.ToString() && item.ProcessStatus == "App Focus")
                {
                    TimeSpan dt = DateTime.Now.Subtract(begin_time).Subtract(TimeSpan.Parse(item.end_time)); 
                    item.end_time = dt.Add(TimeSpan.Parse(item.end_time)).ToString();   //"previous endtime" + "New end time since application just went idle"
                }

                if(item.ProcessId != process_id.ToString() && item.ProcessStatus == "Idle")
                {
                    TimeSpan dt = DateTime.Now.Subtract(begin_time).Subtract(TimeSpan.Parse(item.end_time));
                    item.end_time = dt.Add(TimeSpan.Parse(item.end_time)).ToString(); //"previous endtime" + "New end time since application just went idle"
                }
            }
            
            updateView();
        }

        private void updateView()
        {
            foreach (DataLine item in listData)
            {
                string[] input =
                {
                    item.ProcessName,
                    item.ProcessId,
                    item.ProcessStatus,
                    item.start_time,
                    item.end_time,
                    item.ip_address,
                    item.mac_address,
                    item.description
                };
                ListViewItem line = new ListViewItem(input);
                MetricView.Items.Add(line);
            }
        }

        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;

        protected delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowTextLength(IntPtr hWnd);
        [DllImport("user32.dll")]
        protected static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        [DllImport("user32.dll")]
        protected static extern bool IsWindowVisible(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        HashSet<Tuple<String, String>> hSet = new HashSet<Tuple<String, String>>();
        protected bool EnumTheWindows(IntPtr hWnd, IntPtr lParam)
        {
            int size = GetWindowTextLength(hWnd);
            if (size++ > 0 && IsWindowVisible(hWnd))
            {
                StringBuilder sb = new StringBuilder(size);
                GetWindowText(hWnd, sb, size);
                uint processID;
                GetWindowThreadProcessId(hWnd, out processID);

                var process = Process.GetProcessById((int)processID);
                // Create an array of string that will store the information to display in our 
                string status;
                if (hWnd == GetForegroundWindow()) {
                    status = "App Focus";
                } else
                {
                    status = "Idle";
                }

                // get IP address of host computer
                string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
                string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

                // get mac address of host machine
                String firstMacAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();


                string[] row = {
                    // 1 Process name
                    process.ProcessName,
                    // 2 Process ID
                    process.Id.ToString(),
                    // 3 Process status
                    status,
                    // 4 start time
                    begin_time.ToString(),
                        // 5 Time associated process was ended
                    (DateTime.Now - begin_time).ToString(),
                    // 6 ip_address
                    myIP,
                    // 7 mac_address
                    firstMacAddress,
                            
                    // 8 Description of the process
                    sb.ToString()
                    };

                // Create a new Item to add into the list view that expects the row of information as first argument
                ListViewItem item = new ListViewItem(row);
                
                if (!hSet.Contains(new Tuple<String, String>(process.Id.ToString(), status)))
                {
                    listData.Add(new DataLine(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]));
                    hSet.Add(new Tuple<String, String>(process.Id.ToString(), status));
                    //MetricView.Items.Add(item);
                }
            }
            return true;
        }


        /// <summary>
        /// This method renders all the processes of Windows on a ListView with some values and icons.
        /// </summary>
        public async Task renderProcessesOnListView()
        {
            while (true)
            {
                // Create an array to store the processes

                EnumWindows(new EnumWindowsProc(EnumTheWindows), IntPtr.Zero);

                // Loop through the array of processes to show information of every process in your console

                

                await Task.Delay(8000);
                
            }
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

        private void Form1_Load_1(object sender, EventArgs e)
        {
            renderProcessesOnListView();
        }

        public static void sendToServer(string token)
        {
            foreach(DataLine item in listData)
            {
                string activity = item.ProcessId;
                string start = item.start_time;
                string end = item.end_time;
                string name = item.ProcessName;
                string ip_address = item.ip_address;
                string mac = item.mac_address;
                //string data = $"activity={activity}&start_time={start}&end_time={end}&executable_name={name}&ip_address={ip_address}&mac_address={mac}";
                //MyWebRequest myRequest = new MyWebRequest("https://innometric.guru:8120/activity", "POST", data);
                //MessageBox.Show(myRequest.GetResponseUnknown());

                var client = new RestClient("https://innometric.guru:8120");
                var login = new RestRequest("https://innometric.guru:8120/activity", Method.POST);
                login.RequestFormat = DataFormat.Json;
                login.AddHeader("content-type", "application/json");
                login.AddHeader("Authorization", token);
                //login.AddHeader("Authorization", "Basic");
                login.AddBody(new
                {
                    activity =
                    new
                    {
                        idle_activity = "false",
                        start_time = start,
                        end_time = end,
                        executable_name = name,
                        ip_address = ip_address,
                        mac_address = mac,
                        activity_type = "idle"
                    }
                });
                var response = client.Execute(login);
                //DEBUG
                //string message = response.StatusCode.ToString() + "\n" + response.Content.ToString();
                //MessageBox.Show(message);
            }
            MessageBox.Show("Transfer Completed");
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Form myForm = new LoginForm();
            myForm.Show();
        }



    }

    public class DataLine
    {
        // key properties
        public String ProcessName { get; set; }
        public String ProcessId { get; set; }
        public String ProcessStatus { get; set; }
        public String start_time;
        public String end_time;
        public String mac_address;
        public String ip_address;
        public String description;

        public DataLine(String processName, String processId, String processStatus)
        {
            ProcessName = processName;
            ProcessId = processId;
            ProcessStatus = processStatus;
        }

        public DataLine(String processName, String processId, String processStatus, String stime, String etime)
            : this(processName, processId, processStatus)
        {
            start_time = stime;
            end_time = etime;
        }

        public DataLine(String processName, String processId, String processStatus, String stime, String etime,
            String Ip_address, String Mac_address, String Description)
            : this(processName, processId, processStatus, stime, etime)
        {
            ip_address = Ip_address;
            mac_address = Mac_address;
            description = Description;
        }
    }
}