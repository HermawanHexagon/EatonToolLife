using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Eatontoollife
{
    public partial class Login : Form
    {
        Sqloperation mydb = new Sqloperation();

        public bool administratorlogin = false;
        public bool Loginghlobal = false;

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            CbAuthorize.SelectedIndex = 1;
            CBpassword.Checked = false;
        }

        private void CbAuthorize_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextUsername.Clear();
            TextPassword.Clear();
            Collectnameuser(CbAuthorize.Text.ToLower());
            TextUsername.Focus();
        }

        private void Collectnameuser(string autorize)
        {
            TextUsername.AutoCompleteMode = AutoCompleteMode.Suggest;
            TextUsername.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            addItems(DataCollection, autorize);
            TextUsername.AutoCompleteCustomSource = DataCollection;
        }

        private void addItems(AutoCompleteStringCollection col, string autorize)
        {
            string[] BufferName = System.Text.RegularExpressions.Regex.Split(mydb.Getusername(autorize), ",");
            
            try
            {
                for (int i = 0; i < BufferName.Length; i++) col.Add(BufferName[i].Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void CBpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (CBpassword.Checked)
            {
                TextPassword.Visible = false;
                TextShowPassword.Visible = true;
            }
            else
            {
                TextPassword.Visible = true;
                TextShowPassword.Visible = false;
            }
        }

        private void TextPassword_TextChanged(object sender, EventArgs e)
        {
            TextShowPassword.Text = TextPassword.Text;
        }

        private void TextShowPassword_TextChanged(object sender, EventArgs e)
        {
            TextPassword.Text = TextShowPassword.Text;
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            if(mydb.Checkusername(TextUsername.Text.Trim()))
            {
                if (mydb.Login(TextUsername.Text.Trim(), TextShowPassword.Text.Trim(), CbAuthorize.Text))
                {
                    if (CbAuthorize.SelectedIndex != 1) administratorlogin = true;
                    Loginghlobal = true; this.Close();
                }
                else
                {
                    MessageBox.Show("Password is wrong!!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                    if (CBpassword.Checked)
                    { TextShowPassword.Focus(); TextShowPassword.SelectAll(); }
                    else
                    { TextPassword.Focus(); TextPassword.SelectAll(); }
                }
            }
            else
            {
                MessageBox.Show("User name was not registered in database. Please get Administrator to create new user. Thank you", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }  
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            Loginghlobal = false; this.Close();
        }

        private void TextShowPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Invoke(new EventHandler(ButtonLogin_Click));
            }
        }

        private void TextPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Invoke(new EventHandler(ButtonLogin_Click));
            }
        }

    }
}
