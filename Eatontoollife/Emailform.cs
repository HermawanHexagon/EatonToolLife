using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Eatontoollife.Properties;

namespace Eatontoollife
{
    public partial class Emailform : Form
    {
        IniFile Myini = new IniFile(@".\AppConfig.ini");
        Sqloperation Mydb = new Sqloperation();

        public string selectmail;

        public Emailform()
        {
            InitializeComponent();
        }

        private void Emailform_Load(object sender, EventArgs e)
        {
            showsetupemaildata();
        }

        private void showsetupemaildata()
        {
            if (selectmail == "issue")
            {
                this.Text = "Issue Email Setting";
            }
            else if (selectmail == "warning")
            {
                this.Text = "Warning Email Setting";
            }
            else if (selectmail == "stop")
            {
                this.Text = "Stop Email Setting";
            }

            TxtEmailsender.Text = Mydb.Getemailsender(selectmail.ToUpper());
            TxtPassword.Text = Mydb.Getpasswordsender(selectmail.ToUpper());
            Txtpasswordfake.Text = TxtPassword.Text;
            txtlistsendemail.Text = Readnameemail();
            Txtsubject.Text = Mydb.Getemailsubject(selectmail.ToUpper());
            txtmessage.Text = Mydb.Getemailmessage(selectmail.ToUpper());
            txtattach.Text = Mydb.Getemailattachment(selectmail.ToUpper());
        }

        private string Readnameemail()
        {
            string Email = "";
            string[] Buffer = System.Text.RegularExpressions.Regex.Split(Mydb.Getemaillist(selectmail.ToUpper()), ";");

            for (int i = 0; i < (Buffer.Length - 1); i++)
            {
                Email += Buffer[i] + ";\r\n";
            }

            Email = Email.Remove(Email.Length - 1);

            return Email;
        }

        private void Writenameemail(string Emaillist)
        {
            string[] Buffer = System.Text.RegularExpressions.Regex.Split(Emaillist, "\r\n");
            string Collectemail = string.Empty;

            for (int i = 0; i < Buffer.Length; i++)
            {
                Collectemail += Buffer[i];
            }

            //search if there is any mark \r\n
            if (Collectemail.Contains("\r") || Collectemail.Contains("\n"))
            {
                Collectemail = Collectemail.Replace("\r", "");
                Collectemail = Collectemail.Replace("\n", "");
            }

            Mydb.Updateemaillist(Collectemail, selectmail.ToUpper());
        }

        private void Buttonemail_Click(object sender, EventArgs e)
        {
            if (Buttonemail.Text == "Edit Email Address")
            {
                txtlistsendemail.ReadOnly = false;
                txtlistsendemail.Focus();
                Buttonemail.Text = "Save";
            }
            else
            {
                txtlistsendemail.ReadOnly = true;
                Writenameemail(txtlistsendemail.Text);
                Buttonemail.Text = "Edit Email Address";
            }
        }

        private void Buttonsubject_Click(object sender, EventArgs e)
        {
            if (Buttonsubject.Text == "Edit Subject")
            {
                Txtsubject.Enabled = true;
                Txtsubject.Focus();
                Buttonsubject.Text = "Save";
            }
            else
            {
                Txtsubject.Enabled = false;
                Mydb.Updateemailsubject(Txtsubject.Text, selectmail.ToUpper());
                Buttonsubject.Text = "Edit Subject";
            }
        }

        private void Buttonmessage_Click(object sender, EventArgs e)
        {
            if (Buttonmessage.Text == "Edit Message")
            {
                txtmessage.Enabled = true;
                txtmessage.Focus();
                Buttonmessage.Text = "Save";
            }
            else
            {
                txtmessage.Enabled = false;
                Mydb.Updateemailmessage(txtmessage.Text, selectmail.ToUpper());
                Buttonmessage.Text = "Edit Message";
            }
        }

        private void Buttonattached_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog Browse = new FolderBrowserDialog();
            if (Browse.ShowDialog() == DialogResult.OK)
            {
                Mydb.Updateemailattachment(Browse.SelectedPath, selectmail.ToUpper());
                txtattach.Text = Mydb.Getemailattachment(selectmail.ToUpper());
            }
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            Txtpasswordfake.Text = TxtPassword.Text;
        }

        private void Txtpasswordfake_TextChanged(object sender, EventArgs e)
        {
            TxtPassword.Text = Txtpasswordfake.Text;
        }

        private void CheckBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxShow.Checked)
            {
                TxtPassword.Visible = true;
                Txtpasswordfake.Visible = false;
            }
            else
            {
                TxtPassword.Visible = false;
                Txtpasswordfake.Visible = true;
            }
        }

        private void Buttonsavesender_Click(object sender, EventArgs e)
        {
            if (Buttonsavesender.Text == "Edit Sender")
            {
                TxtEmailsender.Enabled = true;
                Txtpasswordfake.Enabled = true;
                TxtPassword.Enabled = true;
                CheckBoxShow.Enabled = true;
                TxtEmailsender.Focus();
                Buttonsavesender.Text = "Save";
            }
            else
            {
                TxtEmailsender.Enabled = false;
                Txtpasswordfake.Enabled = false;
                TxtPassword.Enabled = false;
                CheckBoxShow.Enabled = false;
                Mydb.Updateemailsender(TxtEmailsender.Text, selectmail.ToUpper());
                Mydb.Updatepasswordsender(TxtPassword.Text, selectmail.ToUpper());
                Buttonsavesender.Text = "Edit Sender";
            }
        }
    }
}
