using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SERC_EPOS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
    private void Form1_Load(object sender, EventArgs e)
        {
            txtbxPass.UseSystemPasswordChar = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            {
                // login credentials
                string username = txtbxUser.Text.Trim();
                string password = txtbxPass.Text.Trim();

                // Validate login
                if (username == "admin" && password == "password")
                {
                    // Shows a MessageBox to confirm successful login
                    DialogResult result = MessageBox.Show("Login successful! Click OK to proceed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Checks if the user clicked OK
                    if (result == DialogResult.OK)
                    {
                        // Opens the new form
                        MainForm mainForm = new MainForm();
                        mainForm.Show();

                        //Closes the login form
                        this.Hide();
                    }
                }
                else
                {
                    //Shows error message
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }    
       
    }
}

