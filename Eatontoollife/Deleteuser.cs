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
    public partial class Deleteuser : Form
    {
        Sqloperation Mydb = new Sqloperation();

        public Deleteuser()
        {
            InitializeComponent();
        }

        private void Deleteuser_Load(object sender, EventArgs e)
        {
            userusagename();
        }

        private void userusagename()
        {
            Cbusername.Items.Clear();
            string[] BufferName = System.Text.RegularExpressions.Regex.Split(Mydb.GetusernameDelete(), ",");

            try
            {
                for (int i = 0; i < BufferName.Length; i++) Cbusername.Items.Add(BufferName[i].ToString());
                Cbusername.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void Cbusername_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] Buffer = System.Text.RegularExpressions.Regex.Split(GetbadgenumberandautohrizeDelete(Cbusername.Text), ",");
                txtautorize.Text = Buffer[0].ToString();
                txtbadgenumber.Text = Buffer[1].ToString();
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public string GetbadgenumberandautohrizeDelete(string user)
        {
            string collectdata = string.Empty;
            SqlConnection connection = new SqlConnection(Mydb.connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT Badgenumber, Authorize FROM Login WHERE Username = '" + user.Trim() + "'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectdata = Reader["Authorize"].ToString() + "," + Reader["Badgenumber"].ToString();
                }

                connection.Close();
            }

            return collectdata.Trim();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
              
                DialogResult result = MessageBox.Show("Are you sure going to delete permanently?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if(result == DialogResult.Yes)
                {
                    if (Mydb.Deleteusername(Cbusername.Text.Trim()))
                    {
                        MessageBox.Show("user " + Cbusername.Text.Trim() + " has been deleted successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        userusagename();
                    }
                }
                else if (result == DialogResult.No)
                { 
                    //nothing                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
    }
}
