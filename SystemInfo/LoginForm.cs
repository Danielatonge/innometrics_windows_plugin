using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using RestSharp;

namespace SystemInfo
{
    public partial class LoginForm : Form
    {
        public string secret_token;
        public LoginForm()
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

            }
            else//check password is empty 
                if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Enter the passowrd!");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //define local variables from the user inputs 
            string email = txtemail.Text;
            string password = txtpassword.Text;
            
            var client = new RestClient("https://innometric.guru:8120");
            var login = new RestRequest("https://innometric.guru:8120/login", Method.POST);
            login.RequestFormat = DataFormat.Json;
            login.AddHeader("content-type", "application/json");
            //login.AddHeader("Authorization", "Basic");
            login.AddBody(new { email = email, password = password });
            var response = client.Execute(login);
            string message = response.StatusCode.ToString();
            MessageBox.Show(message);
            if (response.StatusCode.ToString().Equals("OK"))
            {
                dynamic obj = JsonConvert.DeserializeObject(response.Content);
                secret_token = obj.token;
                Form1 newForm = new Form1();
                newForm.sendToServer(secret_token);
            }

            
                this.Close();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form nForm = new RegistrationForm();
            nForm.Show();
        }
    }
}
