using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Eatontoollife
{
    public partial class Newcustomer : Form
    {
        Sqloperation Mydb = new Sqloperation();
        public string addedby;
        string collect;

        public Newcustomer()
        {
            InitializeComponent();
        }

        private void AddorDeleteissue(string customer, string choice)
        {
            int selectedCB = CBfeedback.SelectedIndex;
            collect = string.Empty;
            try
            {
                
                if (choice == "ADD")
                {
                    if (Mydb.Addnewcustomer(customer.ToUpper(), addedby))
                    {
                        this.Visible = false;
                        MessageBox.Show("New customer has been addded Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (choice == "DELETE")
                {
                    DialogResult result = MessageBox.Show("Are you sure going to delete permanently?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        if (Mydb.Deletecustomer(customer.ToUpper()))
                        {
                            this.Visible = false;
                            MessageBox.Show("customer has been Deleted Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            TxtAddfeedback.Clear();
            TxtAddfeedback.Focus();

            this.Visible = true;
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (TxtAddfeedback.Text != "")
            {
                if (!TxtAddfeedback.Text.Contains(";"))
                {
                    AddorDeleteissue(TxtAddfeedback.Text.Trim(), "ADD");
                    FillUpcomboBox();
                }
                else
                {
                    MessageBox.Show("Please be removed this character semicolon ';'", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtAddfeedback.SelectAll();
                    TxtAddfeedback.Focus();
                }
            }
            else
            {
                MessageBox.Show("Cannot be Add the report feedback. please fill up the report", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtAddfeedback.Focus();
            }
        }

        private void FillUpcomboBox()
        {
            CBfeedback.Items.Clear();

            string[] Buffer = System.Text.RegularExpressions.Regex.Split(Mydb.Getcustomertreeview(), ";");

            try
            {
                for (int i = 0; i < Buffer.Length; i++) { CBfeedback.Items.Add(Buffer[i].Trim()); }
                CBfeedback.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void Newcustomer_Load(object sender, EventArgs e)
        {
            FillUpcomboBox();
        }

        private void Buttondelete_Click(object sender, EventArgs e)
        {
            if (CBfeedback.SelectedIndex > -1)
            {
                TxtAddfeedback.Clear();
                AddorDeleteissue(CBfeedback.Text.Trim(), "DELETE");
                FillUpcomboBox();
            }
        }
    }
}
