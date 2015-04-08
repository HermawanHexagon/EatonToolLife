using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Threading;
using System.Windows.Forms;
using Eatontoollife.Properties;

namespace Eatontoollife
{
    public partial class ReturnForm : Form
    {
        Sqloperation Mydb = new Sqloperation();
        Mailclass Myemail = new Mailclass();
        IniFile Myini = new IniFile(@".\AppConfig.ini");

        public string Toolingnumber = string.Empty;
        public string partnumber = string.Empty;
        public string returnuser = string.Empty;
        public bool usingpartnumber = false;
        public bool Administratorstatus = false;
        string[] Getmessage = new string[9];
        string messageissue;
        int WOqty;

        public ReturnForm()
        {
            InitializeComponent();
        }

        private void ReturnForm_Load(object sender, EventArgs e)
        {
            if (usingpartnumber)
            {
                ViewToolingDatabasemorethanonepartnumber(Toolingnumber, partnumber);
            }
            else if (!usingpartnumber)
            {
                ViewToolingDatabaseonepartnumber(Toolingnumber);
            }

            Viewinformationborrowed();
            userusagename();
            FillUpcomboBox();
        }

        private void FillUpcomboBox()
        {
            CbFeedback.Items.Clear();

            string[] BufferIssue = System.Text.RegularExpressions.Regex.Split(Myini.IniReadValue("Report_Feedback", "Issue"), ";");

            try
            {
                for (int i = 0; i < BufferIssue.Length; i++) { CbFeedback.Items.Add(BufferIssue[i].Trim()); }
                CbFeedback.Items.Add("others");
                CbFeedback.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void userusagename()
        {
            string[] BufferName = System.Text.RegularExpressions.Regex.Split(Mydb.Getusernamereturntooling(), ",");

            try
            {
                for (int i = 0; i < BufferName.Length; i++) Cbuser.Items.Add(BufferName[i].ToString());
                Cbuser.SelectedIndex = 0;
                txtreturnby.Text = returnuser;
                txtreturnqty.Text = Getmessage[2];
                txtdatetime.Text = DateTime.Now.ToString("dd-MMM-yyyy  HH:mm tt");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void Viewinformationborrowed()
        {
            Txt1.Text = Getmessage[0];
            Txt2.Text = Getmessage[1];
            Txt3.Text = Getmessage[2];
            Txt4.Text = Getmessage[3];
            Txt5.Text = Getmessage[4];
            Txt6.Text = Getmessage[5];
            Txt7.Text = Getmessage[6];
            Txt8.Text = Getmessage[7];
            WOqty = int.Parse(Getmessage[8]);
        }

        private void ViewToolingDatabasemorethanonepartnumber(string Toolingnumber, string partnumr)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT PartNumber, ToolingNumber, ToolingoutQty, ToolsName, StorageLocation, ProductFamily, DateTimeout, TakenBy, WOQty FROM Toolingusage WHERE ToolingNumber = '" + Toolingnumber + "' AND PartNumber = '" + partnumr + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Getmessage[0] = Reader["PartNumber"].ToString();
                        Getmessage[1] = Reader["ToolingNumber"].ToString();
                        Getmessage[2] = Reader["ToolingoutQty"].ToString();
                        Getmessage[3] = Reader["ToolsName"].ToString();
                        Getmessage[4] = Reader["StorageLocation"].ToString();
                        Getmessage[5] = Reader["ProductFamily"].ToString();
                        Getmessage[6] = Reader["DateTimeout"].ToString();
                        Getmessage[7] = Reader["TakenBy"].ToString();
                        Getmessage[8] = Reader["WOQty"].ToString();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewToolingDatabaseonepartnumber(string Toolingnumber)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT PartNumber, ToolingNumber, ToolingoutQty, ToolsName, StorageLocation, ProductFamily, DateTimeout, TakenBy, WOQty FROM Toolingusage WHERE ToolingNumber = '" + Toolingnumber + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Getmessage[0] = Reader["PartNumber"].ToString();
                        Getmessage[1] = Reader["ToolingNumber"].ToString();
                        Getmessage[2] = Reader["ToolingoutQty"].ToString();
                        Getmessage[3] = Reader["ToolsName"].ToString();
                        Getmessage[4] = Reader["StorageLocation"].ToString();
                        Getmessage[5] = Reader["ProductFamily"].ToString();
                        Getmessage[6] = Reader["DateTimeout"].ToString();
                        Getmessage[7] = Reader["TakenBy"].ToString();
                        Getmessage[8] = Reader["WOQty"].ToString();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CheckNo_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckNo.Checked)
            {
                CheckYes.Checked = false;
                CbFeedback.Enabled = false;
            }

            if ((!CheckNo.Checked) && (!CheckYes.Checked)) CheckNo.Checked = true;
        }

        private void CheckYes_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckYes.Checked)
            {
                CheckNo.Checked = false;
                CbFeedback.Enabled = true;
            }

            if ((!CheckNo.Checked) && (!CheckYes.Checked)) CheckYes.Checked = true;
        }

        private void RBcomplete_CheckedChanged(object sender, EventArgs e)
        {
            if (RBcomplete.Checked)
            {
                Txtissuetools.Clear();
                Txtissuetools.ReadOnly = true;
                txtreturnqty.ReadOnly = true;
            }
        }

        private void RBincomplete_CheckedChanged(object sender, EventArgs e)
        {
            if (RBincomplete.Checked)
            {
                Txtissuetools.Clear();
                Txtissuetools.ReadOnly = false;
                Txtissuetools.Focus();
                txtreturnqty.ReadOnly = false;
            }
        }

        private string[] Getinformation()
        {
            string[] message = new string[13];
            message[0] = Txt2.Text;
            message[1] = Txt4.Text;
            message[2] = Txt1.Text;
            message[3] = Txt6.Text;
            message[4] = txtdatetime.Text;
            message[5] = returnuser;
            message[6] = txtreturnqty.Text;

            if (RBcomplete.Checked) message[7] = RBcomplete.Text;
            else if (RBincomplete.Checked) message[7] = RBincomplete.Text;

            message[8] = Txtissuetools.Text;
            message[9] = Cbuser.Text;

            if (CheckYes.Checked) message[10] = CheckYes.Text;
            else if (CheckNo.Checked) message[10] = CheckNo.Text;

            if (CheckYes.Checked)
            {
                if (CbFeedback.Text != "others")
                {
                    message[11] = CbFeedback.Text;
                }
                else
                {
                    message[11] = Txtissuefeedback.Text;
                }
            }
            else
            {
                message[11] = "";
            }
            

            message[12] = Mydb.Getvaluelifetime(Txt2.Text.Trim(), Txt1.Text.Trim()).ToString();
            
            return message;
        }

        private void SendEmailStop()
        {
            messageissue = string.Empty;

            try
            {
                messageissue = "STOP\r\n";
                messageissue += "--------------------------------------------------------------------\r\n";
                messageissue += "History data that was take out.\r\n\r\n";
                messageissue += "Part number		    : " + Txt1.Text + "\r\n";
                messageissue += "Tooling number              : " + Txt2.Text + "\r\n";
                messageissue += "Tools qty out		    : " + Txt3.Text + "\r\n";
                messageissue += "Tools Name 		    : " + Txt4.Text + "\r\n";
                messageissue += "location storage 	    : " + Txt5.Text + "\r\n";
                messageissue += "Family product		    : " + Txt6.Text + "\r\n";
                messageissue += "Date Time Out		    : " + Txt7.Text + "\r\n";
                messageissue += "Taken By    		    : " + Txt8.Text + "\r\n\r\n";

                messageissue += "History Data that has been returned.\r\n\r\n";

                messageissue += "Part number		    : " + Txt1.Text + "\r\n";
                messageissue += "Tooling number		    : " + Txt2.Text + "\r\n";
                messageissue += "Return Tool qty	            : " + txtreturnqty.Text + "\r\n";
                messageissue += "Tools Name                  : " + Txt4.Text + "\r\n";
                messageissue += "Location storage            : " + Txt5.Text + "\r\n";
                messageissue += "Family product              : " + Txt6.Text + "\r\n";
                messageissue += "Date return                 : " + txtdatetime.Text + "\r\n";
                messageissue += "Return By                   : " + txtreturnby.Text + "\r\n";
                messageissue += "Welder                      : " + Cbuser.Text + "\r\n";


                if (RBcomplete.Checked)
                {
                    messageissue += "Tools Status                : Complete\r\n";
                }
                else if (RBincomplete.Checked)
                {
                    messageissue += "Tools Status                : Incomplete\r\n";
                    messageissue += "Reason                      : " + Txtissuetools.Text + "\r\n";
                }


                if (CheckYes.Checked)
                {
                    messageissue += "Any issue                   : " + "YES" + "\r\n";
                    if (CbFeedback.Text == "others") messageissue += "Technician Feedback         : " + Txtissuefeedback.Text + "\r\n";
                    else messageissue += "Technician Feedback         : " + CbFeedback.Text + "\r\n";
                }
                else
                {
                    messageissue += "Any issue                   : " + "NO" + "\r\n";
                }

                messageissue += "\r\nLIFE TIME TOOLING HAS BEEN USED MORE THAN UNTIL " + Mydb.Getvaluelifetime(Txt2.Text.Trim(), Txt1.Text.Trim()).ToString() + " TIMES FROM " + Mydb.GetvaluelifetimePeriod(Txt2.Text.Trim(), Txt1.Text.Trim()).ToString();

                messageissue += "\r\n\r\n--------------------------------------------------------------------";

                Myemail.usernamestop = "\r\n\r\nLIFE TIME TOOLING HAS BEEN USED MORE THAN UNTIL " + Mydb.Getvaluelifetime(Txt2.Text.Trim(), Txt1.Text.Trim()).ToString() + " TIMES FROM " + Mydb.GetvaluelifetimePeriod(Txt2.Text.Trim(), Txt1.Text.Trim()).ToString();
                Myemail.usernamestop += "\r\n\r\nRegards,\r\n" + txtreturnby.Text;

                Myemail.messagedatestop = DateTime.Now.ToString("dd-MMM-yyyy  HH_mm_ss");

                System.IO.File.WriteAllText(Mydb.Getemailattachment("STOP") + @"\Stop - " + Myemail.messagedatestop + ".txt", messageissue);
                
                Myemail.SendEmail("stop");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void SendEmailwarning()
        {
            messageissue = string.Empty;

            try
            {
                messageissue = "WARNING\r\n";
                messageissue += "--------------------------------------------------------------------\r\n\r\n";
                messageissue += "History data that was take out.\r\n\r\n";
                messageissue += "Part number		    : " + Txt1.Text + "\r\n";
                messageissue += "Tooling number              : " + Txt2.Text + "\r\n";
                messageissue += "Tools qty out		    : " + Txt3.Text + "\r\n";
                messageissue += "Tools Name 		    : " + Txt4.Text + "\r\n";
                messageissue += "location storage 	    : " + Txt5.Text + "\r\n";
                messageissue += "Family product		    : " + Txt6.Text + "\r\n";
                messageissue += "Date Time Out		    : " + Txt7.Text + "\r\n";
                messageissue += "Taken By    		    : " + Txt8.Text + "\r\n\r\n";

                messageissue += "History Data that has been returned.\r\n\r\n";

                messageissue += "Part number		    : " + Txt1.Text + "\r\n";
                messageissue += "Tooling number		    : " + Txt2.Text + "\r\n";
                messageissue += "Return Tool qty	            : " + txtreturnqty.Text + "\r\n";
                messageissue += "Tools Name                  : " + Txt4.Text + "\r\n";
                messageissue += "Location storage            : " + Txt5.Text + "\r\n";
                messageissue += "Family product              : " + Txt6.Text + "\r\n";
                messageissue += "Date return                 : " + txtdatetime.Text + "\r\n";
                messageissue += "Return By                   : " + txtreturnby.Text + "\r\n";
                messageissue += "Welder                      : " + Cbuser.Text + "\r\n";


                if (RBcomplete.Checked)
                {
                    messageissue += "Tools Status                : Complete\r\n";
                }
                else if (RBincomplete.Checked)
                {
                    messageissue += "Tools Status                : Incomplete\r\n";
                    messageissue += "Reason                      : " + Txtissuetools.Text + "\r\n";
                }

                if (CheckYes.Checked)
                {
                    messageissue += "Any issue                   : " + "YES" + "\r\n";
                    if (CbFeedback.Text == "others") messageissue += "Technician Feedback         : " + Txtissuefeedback.Text + "\r\n";
                    else messageissue += "Technician Feedback         : " + CbFeedback.Text + "\r\n";
                }
                else
                {
                    messageissue += "Any issue                   : " + "NO" + "\r\n";
                }

                messageissue += "\r\nLIFE TIME TOOLING HAS BEEN USED UNTIL " + Mydb.Getvaluelifetime(Txt2.Text.Trim(), Txt1.Text.Trim()).ToString() + " TIMES";

                messageissue += "\r\n\r\n--------------------------------------------------------------------";

                Myemail.usernamewarning = "\r\n\r\nLIFE TIME TOOLING HAS BEEN USED UNTIL " + Mydb.Getvaluelifetime(Txt2.Text.Trim(), Txt1.Text.Trim()).ToString() + " TIMES";
                Myemail.usernamewarning += "\r\n\r\nRegards,\r\n" + txtreturnby.Text;

                Myemail.messagedatewarning = DateTime.Now.ToString("dd-MMM-yyyy  HH_mm_ss");

                System.IO.File.WriteAllText(Mydb.Getemailattachment("WARNING") + @"\Warning - " + Myemail.messagedatewarning + ".txt", messageissue);
                Myemail.SendEmail("warning");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void SendEmailissue()
        {
            messageissue = string.Empty;

            try
            {
                messageissue = "\r\nHistory data that was take out.\r\n\r\n";
                messageissue += "Part number		    : " + Txt1.Text + "\r\n";
                messageissue += "Tooling number              : " + Txt2.Text + "\r\n";
                messageissue += "Tools qty out		    : " + Txt3.Text + "\r\n";
                messageissue += "Tools Name 		    : " + Txt4.Text + "\r\n";
                messageissue += "location storage 	    : " + Txt5.Text + "\r\n";
                messageissue += "Family product		    : " + Txt6.Text + "\r\n";
                messageissue += "Date Time Out		    : " + Txt7.Text + "\r\n";
                messageissue += "Taken By    		    : " + Txt8.Text + "\r\n\r\n";

                messageissue += "History Data that has been returned.\r\n\r\n";

                messageissue += "Part number		    : " + Txt1.Text + "\r\n";
                messageissue += "Tooling number		    : " + Txt2.Text + "\r\n";
                messageissue += "Return Tool qty	            : " + txtreturnqty.Text + "\r\n";
                messageissue += "Tools Name                  : " + Txt4.Text + "\r\n";
                messageissue += "Location storage            : " + Txt5.Text + "\r\n";
                messageissue += "Family product              : " + Txt6.Text + "\r\n";
                messageissue += "Date return                 : " + txtdatetime.Text + "\r\n";
                messageissue += "Return By                   : " + txtreturnby.Text + "\r\n";
                messageissue += "Welder                      : " + Cbuser.Text + "\r\n";


                if (RBcomplete.Checked)
                {
                    messageissue += "Tools Status                : Complete\r\n";
                }
                else if (RBincomplete.Checked)
                {
                    messageissue += "Tools Status                : Incomplete\r\n";
                    messageissue += "Reason                      : " + Txtissuetools.Text + "\r\n";
                }

                if (CheckYes.Checked)
                {
                    messageissue += "Any issue                   : " + "YES" + "\r\n";
                    if (CbFeedback.Text == "others")
                        messageissue += "Technician Feedback         : " + Txtissuefeedback.Text + "\r\n";
                    else
                        messageissue += "Technician Feedback         : " + CbFeedback.Text + "\r\n";
                }
                else
                {
                    messageissue += "Any issue                   : " + "NO" + "\r\n";
                }

                Myemail.usernameissue = "\r\n\r\nRegards,\r\n" + txtreturnby.Text;

                Myemail.messagedateissue = DateTime.Now.ToString("dd-MMM-yyyy  HH_mm_ss");

                System.IO.File.WriteAllText(Mydb.Getemailattachment("ISSUE") + @"\Issue - " + Myemail.messagedateissue + ".txt", messageissue);

                Myemail.SendEmail("issue");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonProcced_Click(object sender, EventArgs e)
        {
            int Lifetimecheck = 0;

            if (Cbuser.SelectedIndex > 0)
            {
                if (RBcomplete.Checked)
                {
                    if (CheckYes.Checked)
                    {   //PENTING!!
                        //jumlah Wo Qty dikali dengan nominal dari part number database ditambah dengan Plates at each end dr part number database
                        //process without a reason and save into database and send an email
                        Lifetimecheck = Mydb.UpdateLifetime(Txt2.Text.Trim(), Txt1.Text.Trim(), Mydb.Getvaluelifetime(Txt2.Text.Trim(), Txt1.Text.Trim()) + ((WOqty * Mydb.GetvalueNominal(Txt1.Text.Trim())) +  Mydb.GetvaluePlatesateachend(Txt1.Text.Trim())));

                        if (Mydb.Savedatareturntooling(Getinformation()))
                        {
                            SendEmailissue(); MessageBox.Show("This tooling '" + Txt2.Text + "' must be pass to engineering for verifying.\r\nSilahkan Serahkan tooling '" + Txt2.Text + "' ini ke engineering untuk diverifikasi", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            if (Mydb.Deletetoolingusage(Txt2.Text.Trim(), Txt1.Text.Trim()) && Mydb.UpdateStatusindatatable(Txt2.Text, Txt1.Text, ""))
                            {
                                //update life time dan bandingkan apakah sudah mencapai target                             
                                if (Lifetimecheck >= Mydb.GetWarningvalue(Txt2.Text.Trim(), Txt1.Text.Trim(), "NO"))
                                {
                                    //send email dengan status warning
                                    SendEmailwarning();
                                    MessageBox.Show("This tooling has been used until " + Lifetimecheck.ToString() + " Times", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                                if (Lifetimecheck >= Mydb.GetWarningvalue(Txt2.Text.Trim(), Txt1.Text.Trim(), "YES"))
                                {
                                    //send email dengan status sudah meleibihi batas
                                    SendEmailStop();
                                    MessageBox.Show("This tooling has been used more than " + Lifetimecheck.ToString() + " Times", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }
                                
                                this.Close();
                            }
                        }    
                    }
                    else //check no
                    {
                        //process without a reason and save into database
                        Lifetimecheck = Mydb.UpdateLifetime(Txt2.Text.Trim(), Txt1.Text.Trim(), Mydb.Getvaluelifetime(Txt2.Text.Trim(), Txt1.Text.Trim()) + ((WOqty * Mydb.GetvalueNominal(Txt1.Text.Trim())) + Mydb.GetvaluePlatesateachend(Txt1.Text.Trim())));

                        if (Mydb.Savedatareturntooling(Getinformation()))
                        {
                            if (Mydb.Deletetoolingusage(Txt2.Text.Trim(), Txt1.Text.Trim()) && Mydb.UpdateStatusindatatable(Txt2.Text, Txt1.Text, ""))
                            {
                                //update life time dan bandingkan apakah sudah mencapai target                             
                                if (Lifetimecheck >= Mydb.GetWarningvalue(Txt2.Text.Trim(), Txt1.Text.Trim(), "NO"))
                                {
                                    //send email dengan status warning
                                    SendEmailwarning();
                                    MessageBox.Show("This tooling has been used until " + Lifetimecheck.ToString() + " Times", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                                if (Lifetimecheck >= Mydb.GetWarningvalue(Txt2.Text.Trim(), Txt1.Text.Trim(), "YES"))
                                {
                                    //send email dengan status sudah meleibihi batas
                                    SendEmailStop();
                                    MessageBox.Show("This tooling has been used more than " + Lifetimecheck.ToString() + " Times", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }

                                showmapping(Txt5.Text.Trim());
                                this.Close();
                            }
                        }
                    }
                }
                else if (RBincomplete.Checked)
                {
                    if (Txtissuetools.Text != "")
                    {
                        //process with a reason and save intodatabase
                        if (CheckYes.Checked)
                        {
                            Lifetimecheck = Mydb.UpdateLifetime(Txt2.Text.Trim(), Txt1.Text.Trim(), Mydb.Getvaluelifetime(Txt2.Text.Trim(), Txt1.Text.Trim()) + ((WOqty * Mydb.GetvalueNominal(Txt1.Text.Trim())) + Mydb.GetvaluePlatesateachend(Txt1.Text.Trim())));
                            //process without a reason and save into database and send an email
                            if (Mydb.Savedatareturntooling(Getinformation()) && Mydb.UpdatereturnQTY(Txt2.Text, Txt1.Text, txtreturnqty.Text))
                            {
                                SendEmailissue(); MessageBox.Show("This tooling '" + Txt2.Text + "' must be pass to engineering for verifying.\r\nSilahkan Serahkan tooling '" + Txt2.Text + "' ini ke engineering untuk diverifikasi", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                if (Mydb.Deletetoolingusage(Txt2.Text.Trim(), Txt1.Text.Trim()) && Mydb.UpdateStatusindatatable(Txt2.Text, Txt1.Text, ""))
                                {
                                    //update life time dan bandingkan apakah sudah mencapai target                             
                                    if (Lifetimecheck >= Mydb.GetWarningvalue(Txt2.Text.Trim(), Txt1.Text.Trim(), "NO"))
                                    {
                                        //send email dengan status warning
                                        SendEmailwarning();
                                        MessageBox.Show("This tooling has been used until " + Lifetimecheck.ToString() + " Times", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }

                                    if (Lifetimecheck >= Mydb.GetWarningvalue(Txt2.Text.Trim(), Txt1.Text.Trim(), "YES"))
                                    {
                                        //send email dengan status sudah meleibihi batas
                                        SendEmailStop();
                                        MessageBox.Show("This tooling has been used more than " + Lifetimecheck.ToString() + " Times", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    }

                                    this.Close();
                                }
                            }    
                        }
                        else //check no
                        {
                            Lifetimecheck = Mydb.UpdateLifetime(Txt2.Text.Trim(), Txt1.Text.Trim(), Mydb.Getvaluelifetime(Txt2.Text.Trim(), Txt1.Text.Trim()) + ((WOqty * Mydb.GetvalueNominal(Txt1.Text.Trim())) + Mydb.GetvaluePlatesateachend(Txt1.Text.Trim())));
                            //process without a reason and save into database
                            if (Mydb.Savedatareturntooling(Getinformation()) && Mydb.UpdatereturnQTY(Txt2.Text, Txt1.Text, txtreturnqty.Text))
                            {
                                if (Mydb.Deletetoolingusage(Txt2.Text.Trim(), Txt1.Text.Trim()) && Mydb.UpdateStatusindatatable(Txt2.Text, Txt1.Text, ""))
                                {
                                    //update life time dan bandingkan apakah sudah mencapai target                             
                                    if (Lifetimecheck >= Mydb.GetWarningvalue(Txt2.Text.Trim(), Txt1.Text.Trim(), "NO"))
                                    {
                                        //send email dengan status warning
                                        SendEmailwarning();
                                        MessageBox.Show("This tooling has been used until " + Lifetimecheck.ToString() + " Times", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }

                                    if (Lifetimecheck >= Mydb.GetWarningvalue(Txt2.Text.Trim(), Txt1.Text.Trim(), "YES"))
                                    {
                                        //send email dengan status sudah meleibihi batas
                                        SendEmailStop();
                                        MessageBox.Show("This tooling has been used more than " + Lifetimecheck.ToString() + " Times", "STOP", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                    }

                                    showmapping(Txt5.Text.Trim());
                                    this.Close();
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must be filling up a reason if not completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select the name though at 'used by'", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CbFeedback_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbFeedback.Text == "others")
            {
                Txtissuefeedback.ReadOnly = false;
                Txtissuefeedback.Clear();
                Txtissuefeedback.Focus();
            }
            else
            {
                Txtissuefeedback.ReadOnly = true;
                Txtissuefeedback.Clear();
            }
        }

        private void showmapping(string location)
        {
            this.Visible = false;
            locationplace place = new locationplace();
            place.location = location;
            place.pesan = "Please return the tooling at this location";
            place.ShowDialog();
            place.Dispose();
            this.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SendEmailissue();
            //SendEmailwarning();
            //SendEmailStop();
        }

    }
}
