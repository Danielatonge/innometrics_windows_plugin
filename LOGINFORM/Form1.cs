using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace LOGINFORM
{
    public class MyWebRequest
    {
        private WebRequest request;
        private Stream dataStream;

        private string status;

        public String Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public MyWebRequest(string url)
        {
            // Create a request using a URL that can receive a post.

            request = WebRequest.Create(url);
        }

        public MyWebRequest(string url, string method)
            : this(url)
        {

            if (method.Equals("GET") || method.Equals("POST"))
            {
                // Set the Method property of the request to POST.
                request.Method = method;
            }
            else
            {
                throw new Exception("Invalid Method Type");
            }
        }

        public MyWebRequest(string url, string method, string data)
            : this(url, method)
        {

            // Create POST data and convert it to a byte array.
            string postData = data;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;

            // Get the request stream.
            dataStream = request.GetRequestStream();

   
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);

            // Close the Stream object.
            dataStream.Close();

        }

        public string GetResponse()
        {
            // Get the original response.
            WebResponse response = request.GetResponse();

            status = ((HttpWebResponse)response).StatusDescription;

            // Get the stream containing all content returned by the requested server.
            dataStream = response.GetResponseStream();

            // Open the stream using a StreamReader for easy access.
            StreamReader reader = new StreamReader(dataStream);

            // Read the content fully up to the end.
            string responseFromServer = reader.ReadToEnd();

            // Clean up the streams.
            reader.Close();
            dataStream.Close();
            response.Close();

            return status;
        }

        }
    
    public partial class Form1 : Form
    {
        Form myForm = new Form2();
        public Form1()
        {
            InitializeComponent();
        }

        //clear user inputs 
        private void ClearTexts(string user, string pass)
        {
            user = String.Empty;
            pass = String.Empty;
        }
        bool IsLoggedIn(string user, string pass)
        {
            //check user name empty 
            if (string.IsNullOrEmpty(user))
            {
                MessageBox.Show("Enter the user name!");
                return false;

            } else//check password is empty 
                if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Enter the passowrd!");
                return false;
            } else
            {
                return true;
            }
        }
    
        private void Button1_Click(object sender, EventArgs e)
        {
            //define local variables from the user inputs 
            string user = txtemail.Text;
            string pass = txtpassword.Text;
            //check if eligible to be logged in 
            if (IsLoggedIn(user, pass))
            {
                //create the constructor with post type and few data
                string data = $"a={user}&b={pass}";
                MyWebRequest myRequest = new MyWebRequest("https://innometrics-12856.firebaseapp.com/login", "POST", data);
                //show the response string on the console screen.

                //MessageBox.Show(myRequest.GetResponse());
                //MessageBox.Show(status);

                this.Hide();
                myForm.Show();
            }
        }
        private void linkLabel1_Clicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form nForm = new Form3();
            nForm.Show();
        }
    }
}
