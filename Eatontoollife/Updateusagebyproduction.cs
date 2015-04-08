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
    public partial class Updateusagebyproduction : Form
    {
        Sqloperation Mydb = new Sqloperation();
        public string[] Datareceived = new string[13];

        public Updateusagebyproduction()
        {
            InitializeComponent();
        }
        //GetCustomerMPN
        private void AddItemcustomer(AutoCompleteStringCollection col)
        {
            string colllect = string.Empty;

            string[] BufferName = System.Text.RegularExpressions.Regex.Split(Mydb.GetCustomerMPN(), ",");

            if (BufferName.Length > -1)
            {
                try
                {
                    for (int i = 0; i < BufferName.Length; i++) col.Add(BufferName[i].Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void AddItemuser(AutoCompleteStringCollection col)
        {
            string colllect = string.Empty;

            string[] BufferName = System.Text.RegularExpressions.Regex.Split(Mydb.Getusernameupdate(), ",");

            if (BufferName.Length > -1)
            {
                try
                {
                    for (int i = 0; i < BufferName.Length; i++) col.Add(BufferName[i].Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void AddItemstatus(AutoCompleteStringCollection col)
        {
            string colllect = string.Empty;

            string[] BufferName = System.Text.RegularExpressions.Regex.Split(Mydb.GetStatusproduction(), ",");

            if (BufferName.Length > -1)
            {
                try
                {
                    for (int i = 0; i < BufferName.Length; i++) col.Add(BufferName[i].Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void Updateusagebyproduction_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection DataCollectionstatus = new AutoCompleteStringCollection();
            AddItemstatus(DataCollectionstatus);
            Txt4.AutoCompleteCustomSource = DataCollectionstatus;

            AutoCompleteStringCollection DataCollectionuser = new AutoCompleteStringCollection();
            AddItemuser(DataCollectionuser);
            Txt13.AutoCompleteCustomSource = DataCollectionuser;

            AutoCompleteStringCollection DataCollectioncustomer = new AutoCompleteStringCollection();
            AddItemcustomer(DataCollectioncustomer);
            Txt6.AutoCompleteCustomSource = DataCollectioncustomer;

            Txt1.Text = Datareceived[0]; Txt2.Text = Datareceived[1];
            Txt3.Text = Datareceived[2]; Txt4.Text = Datareceived[3];
            Txt5.Text = Datareceived[4]; Txt6.Text = Datareceived[5];
            Txt7.Text = Datareceived[6]; Txt8.Text = Datareceived[7];
            Txt9.Text = Datareceived[8]; Txt10.Text = Datareceived[9];
            Txt11.Text = Datareceived[10]; Txt12.Text = Datareceived[11];
            Txt13.Text = Datareceived[12];
        }

        private string[] Getnewupdatedatabase()
        {
            string[] Newdataupdate = new string[13];

            Newdataupdate[0] = Txt1.Text; Newdataupdate[5] = Txt6.Text;
            Newdataupdate[1] = Txt2.Text; Newdataupdate[6] = Txt7.Text;
            Newdataupdate[2] = Txt3.Text; Newdataupdate[7] = Txt8.Text;
            Newdataupdate[3] = Txt4.Text; Newdataupdate[8] = Txt9.Text;
            Newdataupdate[4] = Txt5.Text; Newdataupdate[9] = Txt10.Text;
            Newdataupdate[10] = Txt11.Text; Newdataupdate[11] = Txt12.Text;
            Newdataupdate[12] = Txt13.Text;

            return Newdataupdate;
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mydb.UpdateMasterusagebyprod(Getnewupdatedatabase()))
                {
                    if(Mydb.UpdateStatusindatatable(Txt3.Text, Txt2.Text, Txt4.Text.Trim()))
                    {
                        this.Visible = false;
                        MessageBox.Show("Database have been updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
