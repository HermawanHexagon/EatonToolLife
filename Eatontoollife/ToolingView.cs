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
    public partial class ToolingView : Form
    {
        Sqloperation Mydb = new Sqloperation();

        public string Partnumber = string.Empty;
        public string Toolingnumber = string.Empty;
        public string takenuser = string.Empty;
        public string family = string.Empty;
        public string Remarks = string.Empty;
        public bool Continuetransaction = false;
        public bool Administratorstatus = false;
        public bool CorrectPN = false;
        public bool CorrectTL = false;
        public ToolingView()
        {
            InitializeComponent();
        }

        private void ToolingView_Load(object sender, EventArgs e)
        {
            if (CorrectPN)
            {
                ViewToolingDatabase(Partnumber, "Partnumber");
                Fillcombobox(Partnumber, "Partnumber");
            }
            else if (CorrectTL)
            {
                ViewToolingDatabase(Partnumber, "Toolingnumber");
                Fillcombobox(Partnumber, "Toolingnumber");
            }
           
        }

        private void Fillcombobox(string Partnumber, string Statusmsg)
        {
            string collecttoolingnumber = string.Empty;
            string Query = string.Empty;
            int Countcell = 0;

            try
            {
                if (Statusmsg == "Partnumber")
                    Query = "SELECT ToolingNumber, Status, LifeTimetoolingperiode, Lifetimeusage FROM ToolingListnumber WHERE PartNumber =";
                else if (Statusmsg == "Toolingnumber")
                    Query = "SELECT ToolingNumber, Status, LifeTimetoolingperiode, Lifetimeusage FROM ToolingListnumber WHERE ToolingNumber =";

                SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                SqlCommand cmd = new SqlCommand(Query + "'" + Partnumber + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        if ((Reader["Status"].ToString() != "under used by prod") && (int.Parse(Reader["LifeTimetoolingperiode"].ToString().Trim()) > int.Parse(Reader["Lifetimeusage"].ToString().Trim())))
                        {
                            Cbcount.Items.Add(Countcell);
                            collecttoolingnumber += Reader["ToolingNumber"].ToString() + ",";
                        }
                        Countcell++;    
                    }

                    connection.Close();

                    if (collecttoolingnumber != "")
                    {
                        string[] Buffertooling = System.Text.RegularExpressions.Regex.Split(collecttoolingnumber.Remove(collecttoolingnumber.Length - 1), ",");
                        
                        if (Buffertooling.Length > 0)
                        {
                            for (int i = 0; i < Buffertooling.Length; i++)
                            {
                                CBtooling.Items.Add(Buffertooling[i]);
                            }

                            Cbcount.SelectedIndex = 0;
                            CBtooling.SelectedIndex = 0;
                        }
                    }
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ViewToolingDatabase(string Partnumber, string Statusmsg)
        {
            string Query = string.Empty;
            int rowIndex = 0;

            try
            {
                DGdatabaseTooling.DataSource = null;
                DGdatabaseTooling.Rows.Clear();
                DGdatabaseTooling.Refresh();

                if (Statusmsg == "Partnumber")
                    Query = "SELECT PartNumber, ToolingNumber, Descriptions, ProductFamily, Remarks, Status, LifeTimetoolingperiode, Lifetimeusage FROM ToolingListnumber WHERE PartNumber =";
                else if (Statusmsg == "Toolingnumber")
                    Query = "SELECT PartNumber, ToolingNumber, Descriptions, ProductFamily, Remarks, Status, LifeTimetoolingperiode, Lifetimeusage FROM ToolingListnumber WHERE ToolingNumber =";

                SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                SqlCommand cmd = new SqlCommand(Query + "'" + Partnumber + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        if ((Reader["Status"].ToString() != "under used by prod") && (int.Parse(Reader["LifeTimetoolingperiode"].ToString().Trim()) > int.Parse(Reader["Lifetimeusage"].ToString().Trim())))
                        {
                            DGdatabaseTooling.Rows.Add(Reader["PartNumber"].ToString(), Reader["ToolingNumber"].ToString(), Reader["Descriptions"].ToString(), Reader["ProductFamily"].ToString(), Reader["Status"].ToString(), Reader["LifeTimetoolingperiode"].ToString(), Reader["Lifetimeusage"].ToString(), Reader["Remarks"].ToString());
                            
                            for (int columnIndex = 0; columnIndex < 8; columnIndex++)
                            {
                                DGdatabaseTooling.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.White;
                                DGdatabaseTooling.Rows[rowIndex].Cells[columnIndex].Style.ForeColor = Color.Black;
                                DGdatabaseTooling.Rows[rowIndex].Cells[columnIndex].Style.SelectionBackColor = Color.White;
                                DGdatabaseTooling.Rows[rowIndex].Cells[columnIndex].Style.SelectionForeColor = Color.Black;
                            }
                        }
                        else
                        {
                            DGdatabaseTooling.Rows.Add(Reader["PartNumber"].ToString(), Reader["ToolingNumber"].ToString(), Reader["Descriptions"].ToString(), Reader["ProductFamily"].ToString(), Reader["Status"].ToString(), Reader["LifeTimetoolingperiode"].ToString(), Reader["Lifetimeusage"].ToString(), Reader["Remarks"].ToString());

                            for (int columnIndex = 0; columnIndex < 8; columnIndex++)
                            {
                                DGdatabaseTooling.Rows[rowIndex].Cells[columnIndex].Style.BackColor = Color.Red;
                                DGdatabaseTooling.Rows[rowIndex].Cells[columnIndex].Style.ForeColor = Color.White;
                                DGdatabaseTooling.Rows[rowIndex].Cells[columnIndex].Style.SelectionBackColor = Color.Red;
                                DGdatabaseTooling.Rows[rowIndex].Cells[columnIndex].Style.SelectionForeColor = Color.White;
                            }
                            
                        }
                        rowIndex++;
                        family = Reader["ProductFamily"].ToString();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
            txttakenby.Text = takenuser;
            txttimeout.Text = DateTime.Now.ToString("dd-MMM-yyyy  HH:mm tt");
        }

        private int Getcell(int selectindex)
        {
            int result = 0;
            Cbcount.SelectedIndex = selectindex;
            result = int.Parse(Cbcount.Text);
            return result;
        }

        private void CBtooling_SelectedIndexChanged(object sender, EventArgs e)
        {
            //there must be seperate selection
            //CBtooling.SelectedIndex = 
            int Cell = Getcell(CBtooling.SelectedIndex);
            string Query = string.Empty;

            if (CorrectPN)
                Query = "SELECT PartNumber, LocationsStorage, Qty, Descriptions, Remarks, Status FROM ToolingListnumber WHERE PartNumber = '" + Partnumber + "' AND ToolingNumber = '" + CBtooling.Text.Trim() + "'";
            else if (CorrectTL)
                Query = "SELECT PartNumber, LocationsStorage, Qty, Descriptions, Remarks, Status FROM ToolingListnumber WHERE ToolingNumber = '" + DGdatabaseTooling.Rows[Cell].Cells[1].Value.ToString() + "' AND PartNumber = '" + DGdatabaseTooling.Rows[Cell].Cells[0].Value.ToString() + "'";

                try
                {
                    SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                    SqlCommand cmd = new SqlCommand(Query, connection);
                    connection.Open();

                    using (SqlDataReader Reader = cmd.ExecuteReader())
                    {

                        while (Reader.Read())
                        {
                            if (Reader["Status"].ToString() != "under used by prod")
                            {
                                txttoolsname.Text = Reader["Descriptions"].ToString();
                                txtstoragelocation.Text = Reader["LocationsStorage"].ToString();
                                txtactualtoolqty.Text = Reader["Qty"].ToString();
                                Remarks = Reader["Remarks"].ToString();
                                lblpartnumber.Text = "Part Number : " + Reader["PartNumber"].ToString(); ;
                                Textpartnmb.Text = Reader["PartNumber"].ToString();
                                Txtqtyout.Text = txtactualtoolqty.Text;
                            }
                        }

                        connection.Close();                        
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            Continuetransaction = false;
            this.Close();
        }

        private string informationtransaction()
        {
            string message = string.Empty;

            message = "\t\t\tTRANSACTION SLIP\n\n";
            message += "-------------------------------------------------------------------------------------\n";
            message += "Transaction Date\t: " + txttimeout.Text + "\n";
            message += "Taken By\t\t: " + txttakenby.Text + "\n";
            message += "Tooling Qty Out\t: " + Txtqtyout.Text + "\n";
            message += "WO Number\t: " + TxtWonumber.Text + "\n";
            message += "WO Quantity\t: " + txtWoquantity.Text + "\n";
            message += "-------------------------------------------------------------------------------------\n";
            message += "Used On PN\t: " + Textpartnmb.Text + "\n";
            message += "Tooling Number\t: " + CBtooling.Text + "\n";
            message += "Family Product\t: " + family + "\n";
            message += "Description\t: " + txttoolsname.Text + "\n";
            message += "location Storage\t: " + txtstoragelocation.Text + "\n";
            message += "Tools Qty\t\t: " + txtactualtoolqty.Text + "\n";
            message += "Remarks\t\t: " + Remarks + "\n";
            message += "-------------------------------------------------------------------------------------\n\n";
            message += "\t\tAre you going to print out of this transaction?";

            return message;
        }

        private bool Inserttoolingusage()
        {
            bool status = false;

            string[] message = new string[13];
            message[0] = CBtooling.Text;
            message[1] = txtstoragelocation.Text;
            message[2] = txtactualtoolqty.Text;
            message[3] = txttoolsname.Text;
            message[4] = Textpartnmb.Text;
            message[5] = family.ToLower();
            message[6] = txttimeout.Text;
            message[7] = txttakenby.Text;
            message[8] = Txtqtyout.Text; 
            message[9] = TxtWonumber.Text;
            message[10] = txtWoquantity.Text.Replace(".00", "");
            message[11] = Txtnote.Text;
            message[12] = "under used by prod";

            try
            {
                if (Mydb.AddToolingusage(message) && Mydb.Savedatausetooling(message, Mydb.Getvaluelifetime(CBtooling.Text, Textpartnmb.Text).ToString()))
                {
                    if (Mydb.UpdateStatusindatatable(CBtooling.Text, Textpartnmb.Text, "under used by prod"))
                    {
                        //check life time and send an email
                        status = true;
                    }
                    else
                    {
                        MessageBox.Show("Failed once updated new database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return status;
        }

        private void showmapping(string location)
        {
            this.Visible = false;
            locationplace place = new locationplace();
            place.location = location;
            place.pesan = "Please take out the tooling at this location";
            place.ShowDialog();
            place.Dispose();
            this.Visible = true;
        }

        private void ButtonNext_Click(object sender, EventArgs e)
        {
            int num;

            if (txtWoquantity.Text != "")
            {
                if (int.TryParse(txtWoquantity.Text.Trim().Replace(".00", ""), out num))
                {
                    if (Administratorstatus)
                    {
                        if (Inserttoolingusage())
                        {
                            showmapping(txtstoragelocation.Text.Trim());
                            this.Close();
                        }
                    }
                    else
                    {
                        if (Inserttoolingusage())
                        {
                            showmapping(txtstoragelocation.Text.Trim());
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("WO number must be number value!", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("you must be insert WO quantity", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TxtWonumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }

        private void txtWoquantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectNextControl(ActiveControl, true, true, true, true);
            }
        }    
    }
}
