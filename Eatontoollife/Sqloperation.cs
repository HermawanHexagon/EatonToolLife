using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;

namespace Eatontoollife
{
    class Sqloperation
    {
        IniFile Myini = new IniFile(@".\AppConfig.ini");

        public string connectionstring;
        public readonly string[] VDB = { "View Database", "Under Usage By Production", "Tools Issue / Welder Feedback", "Tools Transaction History" };


        public Sqloperation()
        {
            connectionstring = "Data Source=" + Myini.IniReadValue("DatabaseConfig", "Data Source") + ";Initial Catalog=" + Myini.IniReadValue("DatabaseConfig", "Databasename") + ";User ID=" + Myini.IniReadValue("DatabaseConfig", "userID") + ";Password=" + Myini.IniReadValue("DatabaseConfig", "Password");
        }

        public bool OpenConnection(string Connstr)
        {
            bool Statusconnection = false;
            try
            {
                SqlConnection connection = new SqlConnection(Connstr);
                connection.Open();
                CloseConnection(Connstr);
                Statusconnection = true;
            }
            catch (Exception)
            {
                throw;
            }

            return Statusconnection;
        }

        public void CloseConnection(string Connstr)
        {
            try
            {
                SqlConnection connection = new SqlConnection(Connstr);
                connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Login(string username, string password, string authorize)
        {
            bool Loginstatus = false;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT Username, Password, Authorize FROM Login WHERE Username = '" + username + "'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                
                while (Reader.Read())
                {
                    if (username == Reader["Username"].ToString() && password == Reader["Password"].ToString() && authorize.ToLower() == Reader["Authorize"].ToString()) Loginstatus = true;
                }

                connection.Close();
            }   

            return Loginstatus;
        }

        public bool Checkunderproduction(string toolingnumber)
        {
            bool existing = false;
            bool Found = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT Status FROM ToolingListnumber WHERE ToolingNumber = '" + toolingnumber + "'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {

                while (Reader.Read())
                {
                    if (Reader["Status"].ToString() == "under used by prod") { Found = true; break; }
                }

                connection.Close();

                if (Found) existing = true;
            }

            return existing;
        }

        public bool ReChecktoolingnumberexisting(string toolingnumber)
        {
            bool existing = false;
            bool Found = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT ToolingNumber, Status FROM ToolingListnumber WHERE ToolingNumber = '" + toolingnumber + "'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {

                while (Reader.Read())
                {
                    if (Reader["Status"].ToString() != "under used by prod")
                    {
                        if (Reader["ToolingNumber"].ToString() == toolingnumber) { Found = true; break; }
                    }
                }

                connection.Close();

                if (Found) existing = true;
            }

            return existing;
        }

        public int Getvalueperiodlifetime(string toolingnumber)
        {
            int nilai = 0;

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT LifeTimetoolingperiode FROM ToolingListnumber WHERE ToolingNumber = '" + toolingnumber + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        nilai = int.Parse(Reader["LifeTimetoolingperiode"].ToString().Trim());
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return nilai;
        }

        public int Getvaluetoolinglifetimeusage(string toolingnumber)
        {
            int nilai = 0;

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT Lifetimeusage FROM ToolingListnumber WHERE ToolingNumber = '" + toolingnumber + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        nilai = int.Parse(Reader["Lifetimeusage"].ToString().Trim());
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return nilai;
        }

        public int Checkmountoftoolingnumber(string toolingnumber)
        {
            int countselected = 0;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT ToolingNumber, Status FROM ToolingListnumber WHERE ToolingNumber = '" + toolingnumber + "'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {

                while (Reader.Read())
                {
                    countselected++;
                }

                connection.Close();
            }

            return countselected;
        }

        public bool Checktoolingnumberexisting(string toolingnumber)
        {
            bool existing = false;
            bool Found = false;
            int countselected = 0;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT ToolingNumber, Status FROM ToolingListnumber WHERE ToolingNumber = '" + toolingnumber + "'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {

                while (Reader.Read())
                {
                    countselected++;
                    if (Reader["Status"].ToString() != "under used by prod")
                    {
                        if (Reader["ToolingNumber"].ToString() == toolingnumber) { Found = true; break; }
                    }
                }

                connection.Close();

                if (Found || countselected > 1) existing = true;
            }

            return existing;
        }

        public int Checkmountofpartnumber(string toolingnumber)
        {
            int mount = 0;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT ToolingNumber FROM Toolingusage WHERE ToolingNumber = '" + toolingnumber + "'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    mount++;
                }

                connection.Close();

            }

            return mount;

        }

        public bool Rechecktoolingnumberexisting(string toolingnumber)
        {
            bool existing = false;
            bool Found = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT ToolingNumber FROM Toolingusage WHERE ToolingNumber = '" + toolingnumber + "'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                     if (Reader["ToolingNumber"].ToString() == toolingnumber) { Found = true; break; }
                }

                connection.Close();

                if (Found) existing = true;
            }

            return existing;
        }

        public bool CheckPartNumberexisting(string Partnumber)
        {
            bool existing = false;
            bool Found = false;
            string Query = string.Empty;

            Query = "SELECT PartNumber FROM ToolingPartnumberList WHERE PartNumber = ";

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand(Query + "'" + Partnumber + "'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {

                while (Reader.Read())
                {
                    if (Reader["PartNumber"].ToString() == Partnumber)
                    { Found = true; break; }
                }

                connection.Close();

                if (Found) existing = true;
            }

            return existing;
        }

        public bool Checkusername(string username)
        {
            bool status = false;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT Username FROM Login WHERE Username = '" + username + "'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {

                while (Reader.Read())
                {
                    if (username == Reader["Username"].ToString()) status = true;
                }

                connection.Close();
            }

            return status;
        }

        public bool Deletecustomer(string customer)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand delete = new SqlCommand("DELETE FROM Customer WHERE productfamily=@productfamily", connection);
            delete.Parameters.AddWithValue("@productfamily", customer);
            connection.Open();

            try
            {
                delete.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool Addnewcustomer(string customer, string addby)
        {
            bool Status = false;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand Insert = new SqlCommand("INSERT INTO Customer VALUES(@Addby, @productfamily)", connection);

            try
            {
                connection.Open();

                Insert.Parameters.AddWithValue("@Addby", addby);
                Insert.Parameters.AddWithValue("@productfamily", customer);
                Insert.ExecuteNonQuery();

                Status = true;

            }
            catch (Exception)
            {
                throw;
            }
            connection.Close();

            return Status;
        }

        public string Getcustomertreeview()
        {
            string collect = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT productfamily FROM Customer", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collect += Reader["productfamily"].ToString() + ";";
                }

                collect = collect.Remove(collect.Length - 1);

                connection.Close();
            }

            return collect;
        }

        public string GetusernameDelete()
        {
            string collectname = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT Username, Badgenumber FROM Login", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectname += Reader["Username"].ToString() + ",";
                }

                collectname = collectname.Remove(collectname.Length - 1);

                connection.Close();
            }

            return collectname;
        }

        public string Getusernamereturntooling()
        {
            string collectname = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT Username, Badgenumber FROM Login", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectname += Reader["Username"].ToString() + " | " + Reader["Badgenumber"].ToString() + ",";
                }

                collectname = collectname.Remove(collectname.Length - 1);

                connection.Close();
            }

            return collectname;
        }

        public string Getusernameupdate()
        {
            string collectname = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT Username FROM Login", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectname += Reader["Username"].ToString() + ",";
                }

                if (collectname != "")
                    collectname = collectname.Remove(collectname.Length - 1);

                connection.Close();
            }

            return collectname;
        }

        public string GetStatusproduction()
        {
            string collectcustomer = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT Status FROM Toolingusage", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectcustomer += Reader["Status"].ToString() + ",";
                }

                if (collectcustomer != "")
                    collectcustomer = collectcustomer.Remove(collectcustomer.Length - 1);

                connection.Close();
            }

            return collectcustomer;
        }

        public string GetCustomertoolnumberTIW()
        {
            string collectpartnumber = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT ToolingNumber FROM ToolingReturnedhistory WHERE ToolingIssue_Yes = 'Yes'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectpartnumber += Reader["ToolingNumber"].ToString() + ",";
                }

                if (collectpartnumber != "")
                    collectpartnumber = collectpartnumber.Remove(collectpartnumber.Length - 1);

                connection.Close();
            }

            return collectpartnumber;
        }

        public string GetCustomertoolnumberUBP()
        {
            string collectpartnumber = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT ToolingNumber FROM Toolingusage WHERE Status = 'under used by prod'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectpartnumber += Reader["ToolingNumber"].ToString() + ",";
                }

                if (collectpartnumber != "")
                    collectpartnumber = collectpartnumber.Remove(collectpartnumber.Length - 1);

                connection.Close();
            }

            return collectpartnumber;
        }

        public string GetCustomertoolnumberMTN()
        {
            string collectpartnumber = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT ToolingNumber FROM ToolingListnumber", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectpartnumber += Reader["ToolingNumber"].ToString() + ",";
                }

                if (collectpartnumber != "")
                    collectpartnumber = collectpartnumber.Remove(collectpartnumber.Length - 1);

                connection.Close();
            }

            return collectpartnumber;
        }

        public string GetCustomerpartnumberTIW()
        {
            string collectpartnumber = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT PartNumber FROM ToolingReturnedhistory WHERE ToolingIssue_Yes = 'Yes'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectpartnumber += Reader["PartNumber"].ToString() + ",";
                }

                if (collectpartnumber != "")
                    collectpartnumber = collectpartnumber.Remove(collectpartnumber.Length - 1);

                connection.Close();
            }

            return collectpartnumber;
        }

        public string GetCustomerpartnumberUBP()
        {
            string collectpartnumber = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT PartNumber FROM Toolingusage WHERE Status = 'under used by prod'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectpartnumber += Reader["PartNumber"].ToString() + ",";
                }

                if (collectpartnumber != "")
                    collectpartnumber = collectpartnumber.Remove(collectpartnumber.Length - 1);

                connection.Close();
            }

            return collectpartnumber;
        }

        public string GetCustomerpartnumberMTN()
        {
            string collectpartnumber = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT PartNumber FROM ToolingListnumber", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectpartnumber += Reader["PartNumber"].ToString() + ",";
                }

                if (collectpartnumber != "")
                    collectpartnumber = collectpartnumber.Remove(collectpartnumber.Length - 1);

                connection.Close();
            }

            return collectpartnumber;
        }

        public string GetCustomerpartnumberMPN()
        {
            string collectpartnumber = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT PartNumber FROM ToolingPartnumberList", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectpartnumber += Reader["PartNumber"].ToString() + ",";
                }

                if(collectpartnumber != "")
                    collectpartnumber = collectpartnumber.Remove(collectpartnumber.Length - 1);

                connection.Close();
            }

            return collectpartnumber;
        }

        public string GetCustomerTIW()
        {
            string collectcustomer = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT ProductFamily FROM ToolingReturnedhistory WHERE ToolingIssue_Yes = 'Yes'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectcustomer += Reader["ProductFamily"].ToString() + ",";
                }

                if (collectcustomer != "")
                    collectcustomer = collectcustomer.Remove(collectcustomer.Length - 1);

                connection.Close();
            }

            return collectcustomer;
        }

        public string GetCustomerTTH()
        {
            string collectcustomer = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT productfamily FROM Customer", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectcustomer += Reader["ProductFamily"].ToString() + ",";
                }

                if (collectcustomer != "")
                    collectcustomer = collectcustomer.Remove(collectcustomer.Length - 1);

                connection.Close();
            }

            return collectcustomer;
        }

        public string GetCustomerUBP()
        {
            string collectcustomer = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT ProductFamily FROM Toolingusage WHERE Status = 'under used by prod'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectcustomer += Reader["ProductFamily"].ToString() + ",";
                }

                if (collectcustomer != "")
                    collectcustomer = collectcustomer.Remove(collectcustomer.Length - 1);

                connection.Close();
            }

            return collectcustomer;
        }

        public string GetCustomerMTN()
        {
            string collectcustomer = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT ProductFamily FROM ToolingListnumber", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectcustomer += Reader["ProductFamily"].ToString() + ",";
                }

                if (collectcustomer != "")
                    collectcustomer = collectcustomer.Remove(collectcustomer.Length - 1);

                connection.Close();
            }

            return collectcustomer;
        }

        public string GetCustomerMPN()
        {
            string collectcustomer = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT Customer FROM ToolingPartnumberList", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectcustomer += Reader["Customer"].ToString() + ",";
                }

                if(collectcustomer != "")
                    collectcustomer = collectcustomer.Remove(collectcustomer.Length - 1);

                connection.Close();
            }

            return collectcustomer;
        }

        public string Getusername(string Auto)
        {
            string collectname = string.Empty;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT Username FROM Login WHERE Authorize = '" + Auto + "'", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    collectname += Reader["Username"].ToString() + ",";
                }

                collectname = collectname.Remove(collectname.Length - 1);

                connection.Close();
            }

            return collectname;
        }

        public bool Checkexistingnewuser(string Nameuser)
        {
            bool Status = false;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand cmd = new SqlCommand("SELECT Username FROM Login", connection);
            connection.Open();

            using (SqlDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    if(Nameuser.Contains(Reader["Username"].ToString()))   
                    {
                        Status = true; break;
                    }
                }

                connection.Close();
            }
            return Status;
        }

        public bool Savedatabasetoolingnumber(string[] Getinsertdata)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand Insert = new SqlCommand("INSERT INTO ToolingListnumber VALUES(@PartNumber, @ProductFamily, @ToolingNumber, @Descriptions, @LocationsStorage, @Qty, @ToolsDrawingNumber, @Revisions, @Remarks, @Status, @LifeTimetoolingperiode, @Warning, @Lifetimeusage)", connection);

            try
            {
                connection.Open();

                if (Getinsertdata[9].ToLower().Trim() != "under used by prod") Getinsertdata[9] = "";

                Insert.Parameters.AddWithValue("@PartNumber", Getinsertdata[0]);
                Insert.Parameters.AddWithValue("@ProductFamily", Getinsertdata[1].ToUpper().Trim());
                Insert.Parameters.AddWithValue("@ToolingNumber", Getinsertdata[2]);
                Insert.Parameters.AddWithValue("@Descriptions", Getinsertdata[3]);
                Insert.Parameters.AddWithValue("@LocationsStorage", Getinsertdata[4]);
                Insert.Parameters.AddWithValue("@Qty", Getinsertdata[5]);
                Insert.Parameters.AddWithValue("@ToolsDrawingNumber", Getinsertdata[6].Trim());
                Insert.Parameters.AddWithValue("@Revisions", Getinsertdata[7]);
                Insert.Parameters.AddWithValue("@Remarks", Getinsertdata[8]);
                Insert.Parameters.AddWithValue("@Status", Getinsertdata[9].ToLower().Trim());
                Insert.Parameters.AddWithValue("@LifeTimetoolingperiode", int.Parse(Getinsertdata[10].Trim()));
                Insert.Parameters.AddWithValue("@Warning", int.Parse(Getinsertdata[11].Trim()));
                Insert.Parameters.AddWithValue("@Lifetimeusage", int.Parse(Getinsertdata[12].Trim()));
                Insert.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }
            connection.Close();

            return Status;
        }

        public void Savedatabasetoolingnumberlist(string ST1, string ST2, string ST3, string ST4, string ST5, string ST6, string ST7, string ST8, string ST9, string ST10, string ST11, string ST12, string ST13)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand Insert = new SqlCommand("INSERT INTO ToolingListnumber VALUES(@PartNumber, @ProductFamily, @ToolingNumber, @Descriptions, @LocationsStorage, @Qty, @ToolsDrawingNumber, @Revisions, @Remarks, @Status, @LifeTimetoolingperiode, @Warning, @Lifetimeusage)", connection);

            try
            {
                connection.Open();

                if(ST10.ToLower().Trim() != "under used by prod") ST10 = "";

                Insert.Parameters.AddWithValue("@PartNumber", ST1);
                Insert.Parameters.AddWithValue("@ProductFamily", ST2.ToUpper().Trim());
                Insert.Parameters.AddWithValue("@ToolingNumber", ST3);
                Insert.Parameters.AddWithValue("@Descriptions", ST4);
                Insert.Parameters.AddWithValue("@LocationsStorage", ST5);
                Insert.Parameters.AddWithValue("@Qty", ST6);
                Insert.Parameters.AddWithValue("@ToolsDrawingNumber", ST7.Trim());
                Insert.Parameters.AddWithValue("@Revisions", ST8);
                Insert.Parameters.AddWithValue("@Remarks", ST9);
                Insert.Parameters.AddWithValue("@Status", ST10.ToLower().Trim());
                Insert.Parameters.AddWithValue("@LifeTimetoolingperiode", int.Parse(ST11.Trim()));
                Insert.Parameters.AddWithValue("@Warning", int.Parse(ST12.Trim()));
                Insert.Parameters.AddWithValue("@Lifetimeusage", int.Parse(ST13.Trim()));
                Insert.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            connection.Close();

        }

        public bool Savedatabasepartnumber(string[] Getinsertdata)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand Insert = new SqlCommand("INSERT INTO ToolingPartnumberList VALUES(@PartNumber, @Descriptions, @Type, @Customer, @Units, @Platesdiaphram, @ID_milimeter, @ID_inch, @OD_milimeter, @OD_inch, @Thickness_milimeter, @Thickness_inch, @Material, @Material_spec, @Typeplate, @Nominal, @Platesateachend, @Remark, @Ply)", connection);

            try
            {
                connection.Open();

                Insert.Parameters.AddWithValue("@PartNumber", Getinsertdata[0]);
                Insert.Parameters.AddWithValue("@Descriptions", Getinsertdata[1]);
                Insert.Parameters.AddWithValue("@Type", Getinsertdata[2]);
                Insert.Parameters.AddWithValue("@Customer", Getinsertdata[3].ToUpper().Trim());
                Insert.Parameters.AddWithValue("@Units", Getinsertdata[4]);
                Insert.Parameters.AddWithValue("@Platesdiaphram", Getinsertdata[5]);
                Insert.Parameters.AddWithValue("@ID_milimeter", Getinsertdata[6]);
                Insert.Parameters.AddWithValue("@ID_inch", Getinsertdata[7]);
                Insert.Parameters.AddWithValue("@OD_milimeter", Getinsertdata[8]);
                Insert.Parameters.AddWithValue("@OD_inch", Getinsertdata[9]);
                Insert.Parameters.AddWithValue("@Thickness_milimeter", Getinsertdata[10]);
                Insert.Parameters.AddWithValue("@Thickness_inch", Getinsertdata[11]);
                Insert.Parameters.AddWithValue("@Material", Getinsertdata[12]);
                Insert.Parameters.AddWithValue("@Material_spec", Getinsertdata[13]);
                Insert.Parameters.AddWithValue("@Typeplate", Getinsertdata[14]);
                Insert.Parameters.AddWithValue("@Nominal", int.Parse(Getinsertdata[15]));
                Insert.Parameters.AddWithValue("@Platesateachend", int.Parse(Getinsertdata[16]));
                Insert.Parameters.AddWithValue("@Remark", Getinsertdata[17]);
                Insert.Parameters.AddWithValue("@Ply", Getinsertdata[18]);
                Insert.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }
            connection.Close();

            return Status;
        }

        public void Savedatabasepartnumberlist(string ST1, string ST2, string ST3, string ST4, string ST5, string ST6, string ST7, string ST8, string ST9, string ST10, string ST11, string ST12, string ST13, string ST14, string ST15, string ST16, string ST17, string ST18, string ST19)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand Insert = new SqlCommand("INSERT INTO ToolingPartnumberList VALUES(@PartNumber, @Descriptions, @Type, @Customer, @Units, @Platesdiaphram, @ID_milimeter, @ID_inch, @OD_milimeter, @OD_inch, @Thickness_milimeter, @Thickness_inch, @Material, @Material_spec, @Typeplate, @Nominal, @Platesateachend, @Remark, @Ply)", connection);

            try
            {
                connection.Open();

                Insert.Parameters.AddWithValue("@PartNumber", ST1);
                Insert.Parameters.AddWithValue("@Descriptions", ST2);
                Insert.Parameters.AddWithValue("@Type", ST3);
                Insert.Parameters.AddWithValue("@Customer", ST4.ToUpper().Trim());
                Insert.Parameters.AddWithValue("@Units", ST5);
                Insert.Parameters.AddWithValue("@Platesdiaphram", ST6);
                Insert.Parameters.AddWithValue("@ID_milimeter", ST7);
                Insert.Parameters.AddWithValue("@ID_inch", ST8);
                Insert.Parameters.AddWithValue("@OD_milimeter", ST9);
                Insert.Parameters.AddWithValue("@OD_inch", ST10);
                Insert.Parameters.AddWithValue("@Thickness_milimeter", ST11);
                Insert.Parameters.AddWithValue("@Thickness_inch", ST12);
                Insert.Parameters.AddWithValue("@Material", ST13);
                Insert.Parameters.AddWithValue("@Material_spec", ST14);
                Insert.Parameters.AddWithValue("@Typeplate", ST15);
                Insert.Parameters.AddWithValue("@Nominal", int.Parse(ST16));
                Insert.Parameters.AddWithValue("@Platesateachend", int.Parse(ST17));
                Insert.Parameters.AddWithValue("@Remark", ST18);
                Insert.Parameters.AddWithValue("@Ply", ST19);
                Insert.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            connection.Close();

        }

        public bool Savedatareturntooling(string[] Getinformation)
        { //history data return once tooling is return back
            bool Status = false;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand Insert = new SqlCommand("INSERT INTO ToolingReturnedhistory VALUES(@ToolingNumber, @ToolsName, @PartNumber, @ProductFamily, @dateTimeReturned, @Returnby, @ReturnedQTY, @ToolsStatus, @Reasonnotcompleted, @Usedby, @ToolingIssue_Yes, @TechnicianansFeedback, @Lifetimeusage)", connection);
            
            try
            {
                connection.Open();

                Insert.Parameters.AddWithValue("@ToolingNumber", Getinformation[0]);
                Insert.Parameters.AddWithValue("@ToolsName", Getinformation[1]);
                Insert.Parameters.AddWithValue("@PartNumber", Getinformation[2]);
                Insert.Parameters.AddWithValue("@ProductFamily", Getinformation[3].ToUpper().Trim());
                Insert.Parameters.AddWithValue("@dateTimeReturned", Getinformation[4]);
                Insert.Parameters.AddWithValue("@Returnby", Getinformation[5]);
                Insert.Parameters.AddWithValue("@ReturnedQTY", Getinformation[6]);
                Insert.Parameters.AddWithValue("@ToolsStatus", Getinformation[7]);
                Insert.Parameters.AddWithValue("@Reasonnotcompleted", Getinformation[8]);
                Insert.Parameters.AddWithValue("@Usedby", Getinformation[9]);
                Insert.Parameters.AddWithValue("@ToolingIssue_Yes", Getinformation[10]);
                Insert.Parameters.AddWithValue("@TechnicianansFeedback", Getinformation[11]); 
                Insert.Parameters.AddWithValue("@Lifetimeusage", int.Parse(Getinformation[12]));
                Insert.ExecuteNonQuery();

                Status = true;

            }
            catch (Exception)
            {
                throw;
            }
            connection.Close();

            return Status;
        }

        public bool UpdatereturnQTY(string toolingnumber, string partnumber, string returnqty)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE ToolingListnumber SET Qty=@Qty WHERE ToolingNumber=@ToolingNumber AND PartNumber=@PartNumber", connection);
            update.Parameters.AddWithValue("@ToolingNumber", toolingnumber);
            update.Parameters.AddWithValue("@PartNumber", partnumber);
            update.Parameters.AddWithValue("@Qty", returnqty);
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public int GetvaluePlatesateachend(string partnumber)
        {
            int nilai = 0;

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT Platesateachend FROM ToolingPartnumberList WHERE PartNumber = '" + partnumber + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        nilai = int.Parse(Reader["Platesateachend"].ToString().Trim());
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return nilai;
        }

        public int GetvalueNominal(string partnumber)
        {
            int nilai = 0;

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT Nominal FROM ToolingPartnumberList WHERE PartNumber = '" + partnumber + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        nilai = int.Parse(Reader["Nominal"].ToString().Trim());
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return nilai;
        }

        public int GetvaluelifetimePeriod(string toolingnumber, string partnumber)
        {
            int nilai = 0;

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT LifeTimetoolingperiode FROM ToolingListnumber WHERE ToolingNumber = '" + toolingnumber + "' AND PartNumber = '" + partnumber + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        nilai = int.Parse(Reader["LifeTimetoolingperiode"].ToString().Trim());
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return nilai;
        }

        public int Getvaluelifetime(string toolingnumber, string partnumber)
        {
            int nilai = 0;

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT Lifetimeusage FROM ToolingListnumber WHERE ToolingNumber = '" + toolingnumber + "' AND PartNumber = '" + partnumber + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        nilai = int.Parse(Reader["Lifetimeusage"].ToString().Trim());
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return nilai;
        }

        public string GetLocationserverpath(string partnumber, string toolingnumber)
        {
            string path = "";

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT ToolsDrawingNumber FROM ToolingListnumber WHERE ToolingNumber = '" + toolingnumber + "' AND PartNumber = '" + partnumber + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        path = Reader["ToolsDrawingNumber"].ToString().Trim();
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return path;
        }

        public int GetWarningvalue(string toolingnumber, string partnumber, string status)
        {
            int lifetime = 0;
            int warning = 0;
            int total = 0;

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT LifeTimetoolingperiode, Warning FROM ToolingListnumber WHERE ToolingNumber = '" + toolingnumber + "' AND PartNumber = '" + partnumber + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        lifetime = int.Parse(Reader["LifeTimetoolingperiode"].ToString().Trim());
                        warning = int.Parse(Reader["Warning"].ToString().Trim());
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            if(status == "NO") //status untuk menentukan apakah tanda warning dan melebihi batas
                total = lifetime - warning;
            else if(status == "YES")
                total =  lifetime + 1;

            return total;
        }

        public int UpdateLifetime(string toolingnumber, string partnumber, int count)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE ToolingListnumber SET Lifetimeusage=@Lifetimeusage WHERE ToolingNumber=@ToolingNumber AND PartNumber=@PartNumber", connection);
            update.Parameters.AddWithValue("@ToolingNumber", toolingnumber);
            update.Parameters.AddWithValue("@PartNumber", partnumber);
            update.Parameters.AddWithValue("@Lifetimeusage", count);
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return count;
        }

        public string Getemailmessage(string Statusemail)
        {
            string Emailmessage = "";

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT Message FROM Email WHERE Status_email = '" + Statusemail + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Emailmessage = Reader["Message"].ToString().Trim();
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Emailmessage;
        }

        public void Updateemailmessage(string pesan, string Statusemail)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE Email SET Message=@Message WHERE Status_email=@Status_email", connection);
            update.Parameters.AddWithValue("@Status_email", Statusemail);
            update.Parameters.AddWithValue("@Message", pesan);
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();
        }

        public string GetPasswordemailsender(string Statusemail)
        {
            string Passwordemailsender = "";

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT Passwordsender FROM Email WHERE Status_email = '" + Statusemail + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Passwordemailsender = Reader["Passwordsender"].ToString().Trim();
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Passwordemailsender;
        }

        public string Getemailattachment(string Statusemail)
        {
            string EmailAttachment = "";

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT Attachment FROM Email WHERE Status_email = '" + Statusemail + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        EmailAttachment = Reader["Attachment"].ToString().Trim();
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return EmailAttachment;
        }

        public void Updateemailattachment(string attachlocation, string Statusemail)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE Email SET Attachment=@Attachment WHERE Status_email=@Status_email", connection);
            update.Parameters.AddWithValue("@Status_email", Statusemail);
            update.Parameters.AddWithValue("@Attachment", attachlocation);
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();
        }

        public string Getpasswordsender(string Statusemail)
        {
            string password = "";

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT Passwordsender FROM Email WHERE Status_email = '" + Statusemail + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        password = Reader["Passwordsender"].ToString().Trim();
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return password;
        }

        public void Updatepasswordsender(string password, string Statusemail)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE Email SET Passwordsender=@Passwordsender WHERE Status_email=@Status_email", connection);
            update.Parameters.AddWithValue("@Status_email", Statusemail);
            update.Parameters.AddWithValue("@Passwordsender", password);
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();
        }

        public string Getemailsender(string Statusemail)
        {
            string Emailsender = "";

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT Emailsender FROM Email WHERE Status_email = '" + Statusemail + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Emailsender = Reader["Emailsender"].ToString().Trim();
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Emailsender;
        }

        public void Updateemailsender(string emailaddress, string Statusemail)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE Email SET Emailsender=@Emailsender WHERE Status_email=@Status_email", connection);
            update.Parameters.AddWithValue("@Status_email", Statusemail);
            update.Parameters.AddWithValue("@Emailsender", emailaddress);
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();
        }

        public string Getemailsubject(string Statusemail)
        {
            string Emailsubject = "";

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT Subject FROM Email WHERE Status_email = '" + Statusemail + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Emailsubject = Reader["Subject"].ToString().Trim();
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Emailsubject;
        }

        public void Updateemailsubject(string subject, string Statusemail)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE Email SET Subject=@Subject WHERE Status_email=@Status_email", connection);
            update.Parameters.AddWithValue("@Status_email", Statusemail);
            update.Parameters.AddWithValue("@Subject", subject);
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();
        }

        public string Getemaillist(string Statusemail)
        {
            string Emailmember = "";

            try
            {
                SqlConnection connection = new SqlConnection(connectionstring);
                SqlCommand cmd = new SqlCommand("SELECT EmailList FROM Email WHERE Status_email = '" + Statusemail + "'", connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Emailmember = Reader["EmailList"].ToString().Trim();
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Emailmember;
        }

        public void Updateemaillist(string Listmember, string Statusemail)
        {
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE Email SET EmailList=@EmailList WHERE Status_email=@Status_email", connection);
            update.Parameters.AddWithValue("@Status_email", Statusemail);
            update.Parameters.AddWithValue("@EmailList", Listmember);
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();
        }

        public bool Updatelocationfile(string Partnumber, string toolingnumber, string location)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE ToolingListnumber SET ToolsDrawingNumber=@ToolsDrawingNumber WHERE PartNumber=@PartNumber AND ToolingNumber=@ToolingNumber", connection);
            update.Parameters.AddWithValue("@PartNumber", Partnumber);
            update.Parameters.AddWithValue("@ToolingNumber", toolingnumber);
            update.Parameters.AddWithValue("@ToolsDrawingNumber", location);
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool UpdateMastertoolissue(string[] Getdatainfoupdate)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE ToolingReturnedhistory SET ToolingNumber=@ToolingNumber, ToolsName=@ToolsName, PartNumber=@PartNumber, ProductFamily=@ProductFamily, dateTimeReturned=@dateTimeReturned, Returnby=@Returnby, ReturnedQTY=@ReturnedQTY, ToolsStatus=@ToolsStatus, Reasonnotcompleted=@Reasonnotcompleted, Usedby=@Usedby, ToolingIssue_Yes=@ToolingIssue_Yes, TechnicianansFeedback=@TechnicianansFeedback WHERE PartNumber=@PartNumber AND ToolingNumber=@ToolingNumber AND dateTimeReturned=@dateTimeReturned", connection);
            update.Parameters.AddWithValue("@dateTimeReturned", Getdatainfoupdate[0]);
            update.Parameters.AddWithValue("@PartNumber", Getdatainfoupdate[1].Trim());
            update.Parameters.AddWithValue("@ToolingNumber", Getdatainfoupdate[2].Trim());
            update.Parameters.AddWithValue("@ToolingIssue_Yes", Getdatainfoupdate[3].Trim());
            update.Parameters.AddWithValue("@ToolsName", Getdatainfoupdate[4]);
            update.Parameters.AddWithValue("@ProductFamily", Getdatainfoupdate[5].ToUpper().Trim());
            update.Parameters.AddWithValue("@ToolsStatus", Getdatainfoupdate[6]);
            update.Parameters.AddWithValue("@ReturnedQTY", Getdatainfoupdate[7].Trim());
            update.Parameters.AddWithValue("@Reasonnotcompleted", Getdatainfoupdate[8]);
            update.Parameters.AddWithValue("@TechnicianansFeedback", Getdatainfoupdate[9]);
            update.Parameters.AddWithValue("@Usedby", Getdatainfoupdate[10].Trim());
            update.Parameters.AddWithValue("@Returnby", Getdatainfoupdate[11]);
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool UpdateMasterhistoryusagebyprod(string[] Getdatainfoupdate)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE Toolingusehistory SET ToolingNumber=@ToolingNumber, StorageLocation=@StorageLocation, ActualToolsQty=@ActualToolsQty, ToolsName=@ToolsName, PartNumber=@PartNumber, ProductFamily=@ProductFamily, DateTimeout=@DateTimeout, TakenBy=@TakenBy, ToolingoutQty=@ToolingoutQty, WONumber=@WONumber, WOQty=@WOQty, Note=@Note, Lifetimeusage=@Lifetimeusage  WHERE PartNumber=@PartNumber AND ToolingNumber=@ToolingNumber AND DateTimeout=@DateTimeout", connection);
            update.Parameters.AddWithValue("@DateTimeout", Getdatainfoupdate[0]);
            update.Parameters.AddWithValue("@PartNumber", Getdatainfoupdate[1].Trim());
            update.Parameters.AddWithValue("@ToolingNumber", Getdatainfoupdate[2]);
            update.Parameters.AddWithValue("@Lifetimeusage", int.Parse(Getdatainfoupdate[3].Trim()));
            update.Parameters.AddWithValue("@ToolsName", Getdatainfoupdate[4]);
            update.Parameters.AddWithValue("@ProductFamily", Getdatainfoupdate[5].ToUpper().Trim());
            update.Parameters.AddWithValue("@StorageLocation", Getdatainfoupdate[6]);
            update.Parameters.AddWithValue("@ActualToolsQty", Getdatainfoupdate[7].Trim());
            update.Parameters.AddWithValue("@ToolingoutQty", Getdatainfoupdate[8].Trim());
            update.Parameters.AddWithValue("@WONumber", Getdatainfoupdate[9]);
            update.Parameters.AddWithValue("@WOQty", Getdatainfoupdate[10].Trim());
            update.Parameters.AddWithValue("@Note", Getdatainfoupdate[11]);
            update.Parameters.AddWithValue("@TakenBy", Getdatainfoupdate[12].Trim());
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool UpdateMasterusagebyprod(string[] Getdatainfoupdate)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE Toolingusage SET ToolingNumber=@ToolingNumber, StorageLocation=@StorageLocation, ActualToolsQty=@ActualToolsQty, ToolsName=@ToolsName, PartNumber=@PartNumber, ProductFamily=@ProductFamily, DateTimeout=@DateTimeout, TakenBy=@TakenBy, ToolingoutQty=@ToolingoutQty, WONumber=@WONumber, WOQty=@WOQty, Note=@Note, Status=@Status  WHERE PartNumber=@PartNumber AND ToolingNumber=@ToolingNumber", connection);
            update.Parameters.AddWithValue("@DateTimeout", Getdatainfoupdate[0]);
            update.Parameters.AddWithValue("@PartNumber", Getdatainfoupdate[1].Trim());
            update.Parameters.AddWithValue("@ToolingNumber", Getdatainfoupdate[2]);
            update.Parameters.AddWithValue("@Status", Getdatainfoupdate[3].ToLower().Trim());
            update.Parameters.AddWithValue("@ToolsName", Getdatainfoupdate[4]);
            update.Parameters.AddWithValue("@ProductFamily", Getdatainfoupdate[5].ToUpper().Trim());
            update.Parameters.AddWithValue("@StorageLocation", Getdatainfoupdate[6]);
            update.Parameters.AddWithValue("@ActualToolsQty", Getdatainfoupdate[7].Trim());
            update.Parameters.AddWithValue("@ToolingoutQty", Getdatainfoupdate[8].Trim());
            update.Parameters.AddWithValue("@WONumber", Getdatainfoupdate[9]);
            update.Parameters.AddWithValue("@WOQty", Getdatainfoupdate[10].Trim());
            update.Parameters.AddWithValue("@Note", Getdatainfoupdate[11]);
            update.Parameters.AddWithValue("@TakenBy", Getdatainfoupdate[12].Trim());
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool UpdateMastertoolingnumber(string[] Getdatainfoupdate)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE ToolingListnumber SET PartNumber=@PartNumber, ProductFamily=@ProductFamily, ToolingNumber=@ToolingNumber, Descriptions=@Descriptions, LocationsStorage=@LocationsStorage, Qty=@Qty, ToolsDrawingNumber=@ToolsDrawingNumber, Revisions=@Revisions, Remarks=@Remarks, Status=@Status, LifeTimetoolingperiode=@LifeTimetoolingperiode, Warning=@Warning, Lifetimeusage=@Lifetimeusage WHERE PartNumber=@PartNumber AND ToolingNumber=@ToolingNumber", connection);
            update.Parameters.AddWithValue("@PartNumber", Getdatainfoupdate[0]);
            update.Parameters.AddWithValue("@ProductFamily", Getdatainfoupdate[1].ToUpper().Trim());
            update.Parameters.AddWithValue("@ToolingNumber", Getdatainfoupdate[2]);
            update.Parameters.AddWithValue("@Descriptions", Getdatainfoupdate[3]);
            update.Parameters.AddWithValue("@LocationsStorage", Getdatainfoupdate[4]);
            update.Parameters.AddWithValue("@ToolsDrawingNumber", Getdatainfoupdate[5]);
            update.Parameters.AddWithValue("@Qty", Getdatainfoupdate[6]);
            update.Parameters.AddWithValue("@Revisions", Getdatainfoupdate[7]);
            update.Parameters.AddWithValue("@Remarks", Getdatainfoupdate[8]);
            update.Parameters.AddWithValue("@Status", Getdatainfoupdate[9].ToLower().Trim());
            update.Parameters.AddWithValue("@LifeTimetoolingperiode", int.Parse(Getdatainfoupdate[10].Trim()));
            update.Parameters.AddWithValue("@Warning", int.Parse(Getdatainfoupdate[11].Trim()));
            update.Parameters.AddWithValue("@Lifetimeusage", int.Parse(Getdatainfoupdate[12].Trim()));
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool UpdateMasterpartnumber(string[] Getdatainfoupdate)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE ToolingPartnumberList SET PartNumber=@PartNumber, Descriptions=@Descriptions, Type=@Type, Customer=@Customer, Units=@Units, Platesdiaphram=@Platesdiaphram, ID_milimeter=@ID_milimeter, ID_inch=@ID_inch, OD_milimeter=@OD_milimeter, OD_inch=@OD_inch, Thickness_milimeter=@Thickness_milimeter, Thickness_inch=@Thickness_inch, Material=@Material, Material_spec=@Material_spec, Typeplate=@Typeplate, Nominal=@Nominal, Platesateachend=@Platesateachend, Remarks=@Remarks, Ply=@Ply WHERE PartNumber=@PartNumber", connection);
            update.Parameters.AddWithValue("@PartNumber", Getdatainfoupdate[0]);
            update.Parameters.AddWithValue("@Descriptions", Getdatainfoupdate[1]);
            update.Parameters.AddWithValue("@Type", Getdatainfoupdate[2]);
            update.Parameters.AddWithValue("@Customer", Getdatainfoupdate[3].ToUpper());
            update.Parameters.AddWithValue("@Units", Getdatainfoupdate[4]);
            update.Parameters.AddWithValue("@Platesdiaphram", Getdatainfoupdate[5]);
            update.Parameters.AddWithValue("@ID_milimeter", Getdatainfoupdate[6]);
            update.Parameters.AddWithValue("@ID_inch", Getdatainfoupdate[7]);
            update.Parameters.AddWithValue("@OD_milimeter", Getdatainfoupdate[8]);
            update.Parameters.AddWithValue("@OD_inch", Getdatainfoupdate[9]);
            update.Parameters.AddWithValue("@Thickness_milimeter", Getdatainfoupdate[10]);
            update.Parameters.AddWithValue("@Thickness_inch", Getdatainfoupdate[11]);
            update.Parameters.AddWithValue("@Material", Getdatainfoupdate[12]);
            update.Parameters.AddWithValue("@Material_spec", Getdatainfoupdate[13]);
            update.Parameters.AddWithValue("@Typeplate", Getdatainfoupdate[14]);
            update.Parameters.AddWithValue("@Nominal", int.Parse(Getdatainfoupdate[15]));
            update.Parameters.AddWithValue("@Platesateachend", int.Parse(Getdatainfoupdate[16]));
            update.Parameters.AddWithValue("@Remarks", Getdatainfoupdate[17]);
            update.Parameters.AddWithValue("@Ply", Getdatainfoupdate[18]);
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool DeleteMastertooltranscationusageprod(string partnumber, string toolingnumber, string date)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand delete = new SqlCommand("DELETE FROM Toolingusehistory WHERE PartNumber=@PartNumber AND ToolingNumber=@ToolingNumber AND DateTimeout=@DateTimeout", connection);
            delete.Parameters.AddWithValue("@PartNumber", partnumber);
            delete.Parameters.AddWithValue("@ToolingNumber", toolingnumber);
            delete.Parameters.AddWithValue("@DateTimeout", date);
            connection.Open();

            try
            {
                delete.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool DeleteMastertoolissue(string partnumber, string toolingnumber, string date)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand delete = new SqlCommand("DELETE FROM ToolingReturnedhistory WHERE PartNumber=@PartNumber AND ToolingNumber=@ToolingNumber AND dateTimeReturned=@dateTimeReturned", connection);
            delete.Parameters.AddWithValue("@PartNumber", partnumber);
            delete.Parameters.AddWithValue("@ToolingNumber", toolingnumber);
            delete.Parameters.AddWithValue("@dateTimeReturned", date);
            connection.Open();

            try
            {
                delete.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool DeleteMasterusagebyproduction(string partnumber, string toolingnumber)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand delete = new SqlCommand("DELETE FROM Toolingusage WHERE PartNumber=@PartNumber AND ToolingNumber=@ToolingNumber", connection);
            delete.Parameters.AddWithValue("@PartNumber", partnumber);
            delete.Parameters.AddWithValue("@ToolingNumber", toolingnumber);
            connection.Open();

            try
            {
                delete.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool DeleteMastertoolingnumber(string partnumber, string toolingnumber)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand delete = new SqlCommand("DELETE FROM ToolingListnumber WHERE PartNumber=@PartNumber AND ToolingNumber=@ToolingNumber", connection);
            delete.Parameters.AddWithValue("@PartNumber", partnumber);
            delete.Parameters.AddWithValue("@ToolingNumber", toolingnumber);
            connection.Open();

            try
            {
                delete.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool DeleteMasterpartnumber(string partnumber)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand delete = new SqlCommand("DELETE FROM ToolingPartnumberList WHERE PartNumber=@PartNumber", connection);
            delete.Parameters.AddWithValue("@PartNumber", partnumber);
            connection.Open();

            try
            {
                delete.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool UpdateStatusindatatable(string toolingnumber, string partnumber, string Statusmsg)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand update = new SqlCommand("UPDATE ToolingListnumber SET Status=@Status WHERE ToolingNumber=@ToolingNumber AND PartNumber=@PartNumber", connection);
            update.Parameters.AddWithValue("@ToolingNumber", toolingnumber);
            update.Parameters.AddWithValue("@PartNumber", partnumber);
            update.Parameters.AddWithValue("@Status", Statusmsg);
            connection.Open();

            try
            {
                update.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool Deleteusername(string username)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand delete = new SqlCommand("DELETE FROM Login WHERE Username=@Username", connection);
            delete.Parameters.AddWithValue("@Username", username);
            connection.Open();

            try
            {
                delete.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool Deletetoolingusage(string toolingnumber, string partnumber)
        {
            bool Status = false;

            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand delete = new SqlCommand("DELETE FROM Toolingusage WHERE ToolingNumber=@ToolingNumber AND PartNumber=@PartNumber", connection);
            delete.Parameters.AddWithValue("@ToolingNumber", toolingnumber);
            delete.Parameters.AddWithValue("@PartNumber", partnumber);
            connection.Open();

            try
            {
                delete.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception)
            {
                 throw;
            }

            connection.Close();

            return Status;
        }

        public bool Savedatausetooling(string[] Getinformation, string lifetime)
        {
            bool Status = false;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand Insert = new SqlCommand("INSERT INTO Toolingusehistory VALUES(@ToolingNumber, @StorageLocation, @ActualToolsQty, @ToolsName, @PartNumber, @ProductFamily, @DateTimeout, @TakenBy, @ToolingoutQty, @WONumber, @WOQty, @Note, @Lifetimeusage)", connection);

            try
            {
                connection.Open();

                Insert.Parameters.AddWithValue("@ToolingNumber", Getinformation[0]);
                Insert.Parameters.AddWithValue("@StorageLocation", Getinformation[1]);
                Insert.Parameters.AddWithValue("@ActualToolsQty", Getinformation[2]);
                Insert.Parameters.AddWithValue("@ToolsName", Getinformation[3]);
                Insert.Parameters.AddWithValue("@PartNumber", Getinformation[4]);
                Insert.Parameters.AddWithValue("@ProductFamily", Getinformation[5].ToUpper().Trim());
                Insert.Parameters.AddWithValue("@DateTimeout", Getinformation[6]);
                Insert.Parameters.AddWithValue("@TakenBy", Getinformation[7]);
                Insert.Parameters.AddWithValue("@ToolingoutQty", Getinformation[8]);
                Insert.Parameters.AddWithValue("@WONumber", Getinformation[9]);
                Insert.Parameters.AddWithValue("@WOQty", Getinformation[10]);
                Insert.Parameters.AddWithValue("@Note", Getinformation[11]);
                Insert.Parameters.AddWithValue("@Lifetimeusage", int.Parse(lifetime));
                Insert.ExecuteNonQuery();

                Status = true;

            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool AddToolingusage(string[] Getinformation)
        {
            bool Status = false;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand Insert = new SqlCommand("INSERT INTO Toolingusage VALUES(@ToolingNumber, @StorageLocation, @ActualToolsQty, @ToolsName, @PartNumber, @ProductFamily, @DateTimeout, @TakenBy, @ToolingoutQty, @WONumber, @WOQty, @Note, @Status)", connection);

            try
            {
                connection.Open();

                Insert.Parameters.AddWithValue("@ToolingNumber", Getinformation[0]);
                Insert.Parameters.AddWithValue("@StorageLocation", Getinformation[1]);
                Insert.Parameters.AddWithValue("@ActualToolsQty", Getinformation[2]);
                Insert.Parameters.AddWithValue("@ToolsName", Getinformation[3]);
                Insert.Parameters.AddWithValue("@PartNumber", Getinformation[4]);
                Insert.Parameters.AddWithValue("@ProductFamily", Getinformation[5].ToUpper().Trim());
                Insert.Parameters.AddWithValue("@DateTimeout", Getinformation[6]);
                Insert.Parameters.AddWithValue("@TakenBy", Getinformation[7]);
                Insert.Parameters.AddWithValue("@ToolingoutQty", Getinformation[8]);
                Insert.Parameters.AddWithValue("@WONumber", Getinformation[9]);
                Insert.Parameters.AddWithValue("@WOQty", Getinformation[10]);
                Insert.Parameters.AddWithValue("@Note", Getinformation[11]);
                Insert.Parameters.AddWithValue("@Status", Getinformation[12]);
                Insert.ExecuteNonQuery();

                Status = true;

            }
            catch (Exception)
            {
                throw;
            }

            connection.Close();

            return Status;
        }

        public bool AddnewUser(string Username, string password, string badge, string Authorize)
        {
            bool Status = false;
            SqlConnection connection = new SqlConnection(connectionstring);
            SqlCommand Insert = new SqlCommand("INSERT INTO Login VALUES(@Username, @Password, @Authorize, @Badgenumber)", connection);

            try
            {
                connection.Open();

                Insert.Parameters.AddWithValue("@Username", Username);
                Insert.Parameters.AddWithValue("@Password", password);
                Insert.Parameters.AddWithValue("@Authorize", Authorize.ToLower());
                Insert.Parameters.AddWithValue("@Badgenumber", badge);
                Insert.ExecuteNonQuery();

                Status = true;

            }
            catch (Exception)
            {
                throw;
            }
            connection.Close();

            return Status;    
        }
    }
}
