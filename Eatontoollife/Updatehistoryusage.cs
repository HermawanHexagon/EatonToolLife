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
    public partial class Updatehistoryusage : Form
    {
        Sqloperation Mydb = new Sqloperation();
        public string[] Datareceived = new string[13];

        public Updatehistoryusage()
        {
            InitializeComponent();
        }

        private void Updatehistoryusage_Load(object sender, EventArgs e)
        {
            Txt1.Text = Datareceived[0]; Txt2.Text = Datareceived[1];
            Txt3.Text = Datareceived[2]; Txt4.Text = Datareceived[12];
            Txt5.Text = Datareceived[3]; Txt6.Text = Datareceived[4];
            Txt7.Text = Datareceived[5]; Txt8.Text = Datareceived[6];
            Txt9.Text = Datareceived[7]; Txt10.Text = Datareceived[8];
            Txt11.Text = Datareceived[9]; Txt12.Text = Datareceived[10];
            Txt13.Text = Datareceived[11];
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
                if (Mydb.UpdateMasterhistoryusagebyprod(Getnewupdatedatabase()))
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
    }
}
