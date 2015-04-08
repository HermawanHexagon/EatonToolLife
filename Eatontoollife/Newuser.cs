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
    public partial class Newuser : Form
    {
        Sqloperation Mydb = new Sqloperation();

        public Newuser()
        {
            InitializeComponent();
        }

        private void ButtonCreate_Click(object sender, EventArgs e)
        {
            if (txtusername.Text != "" && txtPasswordvisible.Text != "" && txtconfPasswordvisible.Text != "" && TxtBadgenumber.Text != "")
            {
                if (txtPasswordvisible.Text.Contains(txtconfPasswordvisible.Text))
                {
                    if (!Mydb.Checkexistingnewuser(txtusername.Text))
                    {
                        try
                        {
                            if (Mydb.AddnewUser(txtusername.Text, txtconfPasswordvisible.Text, TxtBadgenumber.Text, Cbauthorize.Text))
                            {
                                MessageBox.Show("New user has been added successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("user name that you typing is already exist", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtusername.Focus(); txtusername.SelectAll();
                    }
                }
                else
                {
                    MessageBox.Show("Password Confirmation Incorrect", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TxtConfrimpass.Focus(); TxtConfrimpass.SelectAll();
                }
            }
            else
            {
                MessageBox.Show("You have to fill up the textbox", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void Newuser_Load(object sender, EventArgs e)
        {
            Cbauthorize.SelectedIndex = 0;
            txtusername.Focus();
        }

        private void Txtpassword_TextChanged(object sender, EventArgs e)
        {
            txtPasswordvisible.Text = Txtpassword.Text;
        }

        private void txtPasswordvisible_TextChanged(object sender, EventArgs e)
        {
            Txtpassword.Text = txtPasswordvisible.Text;
        }

        private void TxtConfrimpass_TextChanged(object sender, EventArgs e)
        {
            txtconfPasswordvisible.Text = TxtConfrimpass.Text;
        }

        private void txtconfPasswordvisible_TextChanged(object sender, EventArgs e)
        {
            TxtConfrimpass.Text = txtconfPasswordvisible.Text;
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
