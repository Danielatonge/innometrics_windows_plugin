using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LOGINFORM
{
    public partial class Form3 : Form
    {
        private ErrorProvider errField;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void BtnSubmit_Click(object sender, EventArgs e)
        {
            // The user accepted the values.
            // Validate all fields.
            ValidateRequiredField(errField, txtEmail);
            ValidateRequiredField(errField, txtName);
            ValidateRequiredField(errField, txtSurname);
            ValidateRequiredField(errField, txtPassword);

            // See if any field has an error message.
            foreach (Control ctl in Controls)
            {
                if (errField.GetError(ctl) != "")
                {
                    MessageBox.Show(errField.GetError(ctl));
                    return;
                }
            }

            MessageBox.Show("You entered:\n" +
                txtEmail.Text + ' ' + txtName.Text + '\n' +
                txtSurname.Text + '\n' +
                txtPassword.Name + '\t');


            string data = $"email={txtEmail}&name={txtName}&surname={txtSurname}&password={txtPassword}";
            MyWebRequest myRequest = new MyWebRequest("https://innometrics-12856.firebaseapp.com/user", "POST", data);
            MessageBox.Show(myRequest.GetResponse());
            this.Hide();
        }

        private void ValidateRequiredField(object errField, object txtStreet)
        {
            throw new NotImplementedException();
        }

        // Validate fields.
        private void txtRequiredField_Validating(object sender,
            CancelEventArgs e)
        {
            TextBox txt = sender as TextBox;
            e.Cancel = RequiredFieldIsBlank(errField, txt);
        }
        // Split a string in camelCase, removing the prefix.
        private string CamelCaseToWords(string input)
        {
            // Insert a space in front of each capital letter.
            string result = "";
            foreach (char ch in input.ToCharArray())
            {
                if (char.IsUpper(ch)) result += " ";
                result += ch;
            }

            // Find the first space and remove everything before it.
            return result.Substring(result.IndexOf(" ") + 1);
        }

        private bool RequiredFieldIsBlank(ErrorProvider err, TextBox txt)
        {
            if (txt.Text.Length > 0)
            {
                // Clear the error.
                err.SetError(txt, "");
                return false;
            }
            else
            {
                // Set the error.
                err.SetError(txt, CamelCaseToWords(txt.Name) +
                    " must not be blank.");
                return true;
            }
        }
    }
}
