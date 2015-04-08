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
    public partial class PartNumberinput : Form
    {
        Sqloperation Mydb = new Sqloperation();
        public bool CorrectPartnumber = false;
        public bool Coorecttoolingnumber = false;

        public PartNumberinput()
        {
            InitializeComponent();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Coorecttoolingnumber = false;
            CorrectPartnumber = false;
            this.Close();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (TextPartnumber.Text != "")
            {
                if (RBpartnumber.Checked)
                {
                    if (Mydb.CheckPartNumberexisting(TextPartnumber.Text.Trim().ToUpper()))
                    {
                        CorrectPartnumber = true;
                        this.Close();
                    }
                    else
                    {
                        this.Visible = false;
                        MessageBox.Show("Part Number " + TextPartnumber.Text.Trim() + " is not available in database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Visible = true; TextPartnumber.SelectAll(); TextPartnumber.Focus();
                    }
                }
                else if (RBtoolingnumber.Checked)
                {
                    if (Mydb.Checktoolingnumberexisting(TextPartnumber.Text.Trim().ToUpper()))
                    {
                        //jika tooling sama dengan 1 cek life time
                        if (Mydb.Checkmountoftoolingnumber(TextPartnumber.Text.Trim()) < 2)
                        {
                            if (Mydb.Getvaluetoolinglifetimeusage(TextPartnumber.Text.Trim()) > Mydb.Getvalueperiodlifetime(TextPartnumber.Text.Trim()))
                            {
                                MessageBox.Show("This tooling " + TextPartnumber.Text.Trim() + " has been used more than " + Mydb.Getvalueperiodlifetime(TextPartnumber.Text.Trim()).ToString() + " times. cannot be used anymore", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                Coorecttoolingnumber = true;
                                this.Close();
                            }
                        }
                        //jika tooling number lebih dari 1 ga perlu dicek lagi
                        else
                        {
                            Coorecttoolingnumber = true;
                            this.Close();
                        }
                    }
                    else
                    {
                        if (Mydb.Checkunderproduction(TextPartnumber.Text.Trim().ToUpper()))
                        {
                            this.Visible = false;
                            MessageBox.Show("Tooling Number " + TextPartnumber.Text.Trim() + " is being used by production", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Visible = true; TextPartnumber.SelectAll(); TextPartnumber.Focus();
                        }
                        else
                        {
                            this.Visible = false;
                            MessageBox.Show("Tooling Number " + TextPartnumber.Text.Trim() + " is not available in database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Visible = true; TextPartnumber.SelectAll(); TextPartnumber.Focus();
                        }
                        
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Please key in part number or tooling number that has selected!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TextPartnumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Invoke(new EventHandler(ButtonOk_Click));
            }
        }

        private void RBpartnumber_CheckedChanged(object sender, EventArgs e)
        {
            TextPartnumber.Focus();
        }

        private void RBtoolingnumber_CheckedChanged(object sender, EventArgs e)
        {
            TextPartnumber.Focus();
        }

        private void PartNumberinput_Load(object sender, EventArgs e)
        {

        }
    }
}
