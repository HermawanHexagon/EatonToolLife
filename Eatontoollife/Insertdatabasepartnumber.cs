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
    public partial class Insertdatabasepartnumber : Form
    {
        Sqloperation Mydb = new Sqloperation();

        public Insertdatabasepartnumber()
        {
            InitializeComponent();
        }

        private string[] Getnewinsertdatabase()
        {
            string[] Newdataupdate = new string[19];

            Newdataupdate[0] = Txt1.Text; Newdataupdate[5] = Txt6.Text;
            Newdataupdate[1] = Txt2.Text; Newdataupdate[6] = Txt7.Text;
            Newdataupdate[2] = Txt3.Text; Newdataupdate[7] = Txt8.Text;
            Newdataupdate[3] = Txt4.Text; Newdataupdate[8] = Txt9.Text;
            Newdataupdate[4] = Txt5.Text; Newdataupdate[9] = Txt10.Text;

            Newdataupdate[10] = Txt11.Text; Newdataupdate[15] = Txt16.Text;
            Newdataupdate[11] = Txt12.Text; Newdataupdate[16] = Txt17.Text;
            Newdataupdate[12] = Txt13.Text; Newdataupdate[17] = Txt18.Text;
            Newdataupdate[13] = Txt14.Text; Newdataupdate[18] = Txt19.Text;
            Newdataupdate[14] = Txt15.Text;

            return Newdataupdate;
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mydb.Savedatabasepartnumber(Getnewinsertdatabase()))
                {
                    this.Visible = false;
                    MessageBox.Show("Database have been inserted!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Close();
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
