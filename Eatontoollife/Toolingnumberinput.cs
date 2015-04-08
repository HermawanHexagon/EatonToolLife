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
    public partial class Toolingnumberinput : Form
    {
        Sqloperation Mydb = new Sqloperation();
        public bool Coorecttoolingnumber = false;
        public bool usingpartnumber = false;
        private bool showonemoretime = false;

        public Toolingnumberinput()
        {
            InitializeComponent();
        }

        private void showcomboboxanddatabase(string toolingnumber)
        {
            Cbpartnumber.Visible = true;
            lblPN.Visible = true;
            Lblnotif.Visible = true;

            this.Height = 222;
            this.groupBox1.Height = 106;
            this.ButtonOk.Location = new Point(279, 148);
            this.ButtonCancel.Location = new Point(360, 148);

            SqlConnection connection = new SqlConnection(Mydb.connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT PartNumber FROM Toolingusage WHERE ToolingNumber = '" + toolingnumber + "'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    Cbpartnumber.Items.Add(Reader["PartNumber"].ToString());
                }

                connection.Close();
                Cbpartnumber.SelectedIndex = 0;
                Texttoolingnumber.ReadOnly = true;
                showonemoretime = true;
            }        
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            if (Texttoolingnumber.Text != "")
            {
                if (Mydb.Rechecktoolingnumberexisting(Texttoolingnumber.Text.Trim().ToUpper()))
                {
                    //check if there is few part number with same part numbers
                    if (Mydb.Checkmountofpartnumber(Texttoolingnumber.Text.Trim()) < 2)
                    {
                        usingpartnumber = false;
                        Coorecttoolingnumber = true;
                        this.Close();
                    }
                    else
                    { //more that 1 part number
                        if (!showonemoretime)
                            showcomboboxanddatabase(Texttoolingnumber.Text.Trim().ToUpper());
                        else
                        {
                            showonemoretime = false;
                            usingpartnumber = true;
                            Coorecttoolingnumber = true;
                            this.Close();
                        }
                    }
                    
                }
                else
                {
                    this.Visible = false;
                    MessageBox.Show("Tooling Number " + Texttoolingnumber.Text.Trim() + " is not available in database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Visible = true; Texttoolingnumber.SelectAll(); Texttoolingnumber.Focus();
                }
            }
            else
            {
                MessageBox.Show("Please key in or type of tooling number!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Coorecttoolingnumber = false;
            this.Close();
        }

        private void Texttoolingnumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Invoke(new EventHandler(ButtonOk_Click));
            }
        }

        private void Toolingnumberinput_Load(object sender, EventArgs e)
        {

        }
    }
}
