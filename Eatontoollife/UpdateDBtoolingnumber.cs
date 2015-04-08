using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Eatontoollife
{
    public partial class UpdateDBtoolingnumber : Form
    {
        Sqloperation Mydb = new Sqloperation();

        public string[] Datareceived = new string[13];

        public UpdateDBtoolingnumber()
        {
            InitializeComponent();
        }

        private string[] Getnewupdatedatabase()
        {
            string[] Newdataupdate = new string[13];

            Newdataupdate[0] = Txt1.Text; Newdataupdate[5] = Txt6.Text;
            Newdataupdate[1] = Txt2.Text; Newdataupdate[6] = Txt7.Text;
            Newdataupdate[2] = Txt3.Text; Newdataupdate[7] = Txt8.Text;
            Newdataupdate[3] = Txt4.Text; Newdataupdate[8] = Txt9.Text;
            Newdataupdate[4] = Txt5.Text; Newdataupdate[9] = Txt10.Text;

            Newdataupdate[10] = Txt11.Text; 
            Newdataupdate[11] = Txt12.Text; 
            Newdataupdate[12] = Txt13.Text; 


            return Newdataupdate;
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

        private void UpdateDBtoolingnumber_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            AddItemstatus(DataCollection);
            Txt10.AutoCompleteCustomSource = DataCollection;

            Txt1.Text = Datareceived[0]; Txt2.Text = Datareceived[1];
            Txt3.Text = Datareceived[2]; Txt4.Text = Datareceived[3];
            Txt5.Text = Datareceived[4]; Txt6.Text = Datareceived[5];
            Txt7.Text = Mydb.GetLocationserverpath(Txt1.Text, Txt3.Text); Txt8.Text = Datareceived[7];
            Txt9.Text = Datareceived[8]; Txt10.Text = Datareceived[9];
            Txt11.Text = Datareceived[10]; Txt12.Text = Datareceived[11];
            Txt13.Text = Datareceived[12];

            if (Txt7.Text != "") ButtonDelete.Enabled = true;
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mydb.UpdateMastertoolingnumber(Getnewupdatedatabase()))
                {
                    this.Visible = false;
                    MessageBox.Show("Database have been updated!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
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

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (Txt7.Text != "")
            {
                try
                {
                    File.SetAttributes(Txt7.Text, FileAttributes.Normal);

                    if (System.IO.File.Exists(Txt7.Text))
                    {
                        System.IO.File.Delete(Txt7.Text);

                        if (Mydb.Updatelocationfile(Txt1.Text.Trim(), Txt3.Text.Trim(), ""))
                        {
                            Txt7.Text = ""; ButtonDelete.Enabled = false;
                            MessageBox.Show("File has been deleted successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }                    
                }
                catch (Exception ex)
                {
                    Txt7.Text = ""; ButtonDelete.Enabled = false;
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Mydb.Updatelocationfile(Txt1.Text.Trim(), Txt3.Text.Trim(), "");
                }
            }
            
        }
    }
}
