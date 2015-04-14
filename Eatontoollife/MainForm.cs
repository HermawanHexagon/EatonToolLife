using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SqlClient;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace Eatontoollife
{
    public partial class MainForm : Form
    {
        Sqloperation Mydb = new Sqloperation();
        IniFile Myini = new IniFile(@".\AppConfig.ini");
        public string Usernamenow;
        string Authorize, startlogin;
        public bool Administratorstatus = false;
        string Savepathexcel;
        List<string> collectdataexcel = new List<string>();
        int rowsdatagrid = 0;
        int newrowsdatagrid = 0;

        public MainForm()
        {
            InitializeComponent();
        }


        private void Gettreeviewcustomer()
        {
            string[] BufferName = System.Text.RegularExpressions.Regex.Split(Mydb.Getcustomertreeview(), ";");
            TreeViewDB.Nodes.Clear();
            TreeViewDB.Nodes.Add("User Menu Selections");

            for (int node = 0; node < BufferName.Length; node++)
            {
                TreeViewDB.Nodes[0].Nodes.Add(BufferName[node].ToUpper());

                TreeViewDB.Nodes[0].Nodes[node].Nodes.Add("View Database");
                TreeViewDB.Nodes[0].Nodes[node].Nodes.Add("Under Usage By Production");
                TreeViewDB.Nodes[0].Nodes[node].Nodes.Add("Tools Issue / Welder Feedback");
                //TreeViewDB.Nodes[0].Nodes[node].Nodes.Add("Tools Transaction History");
            }

            TreeViewDB.CollapseAll();
            TreeViewDB.Nodes[0].ExpandAll();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (Mydb.OpenConnection(Mydb.connectionstring) && Userloginform())
                {
                    Gettreeviewcustomer();
                    LblUsername.Text = "User Name : " + Usernamenow;
                    LblAuthorize.Text = "Authorize : " + Authorize;
                    Lblstartlogin.Text = "Start Login : " + startlogin;
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }


        private bool Userloginform()
        {
            bool StatusLogin = false;

            Login loginform = new Login();
            loginform.ShowDialog();
            loginform.Dispose();

            if (loginform.Loginghlobal)
            {

                if (loginform.administratorlogin)
                {
                    //full access
                    importToolStripMenuItem.Enabled = true;
                    settingToolStripMenuItem.Enabled = true;
                    insertToolStripMenuItem.Enabled = true;
                    Admintab.Enabled = true;
                    CbcategoryMPN.SelectedIndex = 0;
                }
                else
                {
                    //all function could not be accessed 
                    importToolStripMenuItem.Enabled = false;
                    settingToolStripMenuItem.Enabled = false;
                    insertToolStripMenuItem.Enabled = false;
                    Admintab.Enabled = false;
                }

                StatusLogin = true;
                Administratorstatus = loginform.administratorlogin;
                Usernamenow = loginform.TextUsername.Text.Trim();
                Authorize = loginform.CbAuthorize.Text;
                startlogin = DateTime.Now.ToString("ddd, HH:mm:ss");
            }

            return StatusLogin;
        }

        private void Timerdatetime_Tick(object sender, EventArgs e)
        {
            lbltime.Text = "Time : " + DateTime.Now.ToString("HH:mm:ss tt");
            lblDate.Text = "Date : " + DateTime.Now.ToString("dd-MMM-yyyy");
        }

        private bool GetNodeselected(string textnode)
        {
            bool nodeselect = false;
            try
            {
                TreeNode node = TreeViewDB.SelectedNode;
                if (node.Text.ToLower() == textnode.ToLower()) nodeselect = true;
            }
            catch (Exception)
            {
                nodeselect = false;
            }
            
            return nodeselect;
        }
 
        private void TreeViewDB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            /* 
             * selectnode[0] = title
             * selectnode[1] = customer
             * selectnode[2] = options
             */ 
            string[] selectnode = System.Text.RegularExpressions.Regex.Split(TreeViewDB.SelectedNode.FullPath, ";");

            if (selectnode.Length > 2)
            {
                if (selectnode[2].ToString() == Mydb.VDB[0]) //database view
                {
                    Databaseingridview("SELECT PartNumber, Descriptions, Remarks FROM ToolingPartnumberList WHERE Customer = '" + selectnode[1].ToString() + "'");
                    Databaseingridviewreturn("SELECT DateTimeout, PartNumber, ToolingNumber, StorageLocation, ToolsName, TakenBy FROM Toolingusage WHERE ProductFamily = '" + selectnode[1].ToString() + "'");
                }
                else if (selectnode[2].ToString() == Mydb.VDB[1]) //Under Usage By Production
                {
                    viewunderproductioncs view = new viewunderproductioncs();
                    view.Querysql = "SELECT * FROM Toolingusage WHERE ProductFamily = '" + selectnode[1].Trim() + "'";
                    view.ShowDialog();
                    view.Dispose();
                }
                else if (selectnode[2].ToString() == Mydb.VDB[2]) //Tools Issue / Welder Feedback
                {
                    Viewtoolingisseu view = new Viewtoolingisseu();
                    view.Querysql = "SELECT * FROM ToolingReturnedhistory WHERE ToolingIssue_Yes = 'Yes' AND ProductFamily = '" + selectnode[1].Trim() + "'";
                    view.ShowDialog();
                    view.Dispose();
                }
                else if (selectnode[2].ToString() == Mydb.VDB[3]) //Tools Transaction History
                {

                }
            }
            /*
            if (GetNodeselected(Mydb.VDB[0]))
            {
                Databaseingridview("SELECT PartNumber, Descriptions, Remarks FROM ToolsIndexWarwick");
                Databaseingridviewreturn("SELECT DateTimeout, PartNumber, ToolingNumber, StorageLocation, ToolsName, TakenBy FROM Toolingusage WHERE ProductFamily = 'warwick'");
            }
            else if (GetNodeselected(Mydb.VDB[1]))
            {
                Databaseingridview("SELECT PartNumber, Descriptions, Remarks FROM ToolsIndexFrance");
                Databaseingridviewreturn("SELECT DateTimeout, PartNumber, ToolingNumber, StorageLocation, ToolsName, TakenBy FROM Toolingusage WHERE ProductFamily = 'france'");
            }
            else if (GetNodeselected(Mydb.VDB[2]))
            {
                Databaseingridview("SELECT PartNumber, Descriptions, Remarks FROM ToolsIndexDaytona");
                Databaseingridviewreturn("SELECT DateTimeout, PartNumber, ToolingNumber, StorageLocation, ToolsName, TakenBy FROM Toolingusage WHERE ProductFamily = 'daytona'");
            }
             */
        }

        private void Databaseingridviewreturn(string Query)
        {
            try
            {
                DGreturn.DataSource = null;
                DGreturn.Rows.Clear();
                DGreturn.Refresh();


                SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                SqlCommand cmd = new SqlCommand(Query, connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {

                    while (Reader.Read())
                    {

                        DGreturn.Rows.Add(Reader["DateTimeout"].ToString(), Reader["PartNumber"].ToString(), Reader["ToolingNumber"].ToString(), Reader["StorageLocation"].ToString(), Reader["ToolsName"].ToString(), Reader["TakenBy"].ToString());
                    }

                    connection.Close();
                    CreateHyperlinkdatagrid(DGreturn, 2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            TreeViewDB.UseWaitCursor = false;
        }

        private void Databaseingridview(string Query)
        {
            TreeViewDB.UseWaitCursor = true;
            try
            {
                DGdatabaseAvailibilty.DataSource = null;
                DGdatabaseAvailibilty.Rows.Clear();
                DGdatabaseAvailibilty.Refresh();

                
                SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                SqlCommand cmd = new SqlCommand(Query, connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {

                    while (Reader.Read())
                    {

                        DGdatabaseAvailibilty.Rows.Add(Reader["PartNumber"].ToString(), Reader["Descriptions"].ToString(), Reader["Remarks"].ToString());
                    }

                    connection.Close();

                    //create hyperlink here

                    CreateHyperlinkdatagrid(DGdatabaseAvailibilty, 0);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void newTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PartNumberinput Partinput = new PartNumberinput();
            Partinput.ShowDialog();
            Partinput.Dispose();

            if (Partinput.CorrectPartnumber || Partinput.Coorecttoolingnumber)
            {
                ToolingView Tooling = new ToolingView();
                Tooling.Partnumber = Partinput.TextPartnumber.Text.Trim();
                Tooling.takenuser = Usernamenow;
                Tooling.Administratorstatus = Administratorstatus;
                Tooling.CorrectPN = Partinput.CorrectPartnumber;
                Tooling.CorrectTL = Partinput.Coorecttoolingnumber;
                Tooling.ShowDialog();
                Tooling.Dispose();
            }
        }

        private void returnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Toolingnumberinput Partinput = new Toolingnumberinput();
            Partinput.ShowDialog();
            Partinput.Dispose();

            if (Partinput.Coorecttoolingnumber)
            {
                ReturnForm Returntool = new ReturnForm(); //Partinput.usingpartnumber;
                Returntool.Toolingnumber = Partinput.Texttoolingnumber.Text;
                Returntool.partnumber = Partinput.Cbpartnumber.Text;
                Returntool.usingpartnumber = Partinput.usingpartnumber;
                Returntool.returnuser = Usernamenow;
                Returntool.Administratorstatus = Administratorstatus;
                Returntool.ShowDialog();
                Returntool.Dispose();
            }
        }

        private void DGdatabaseAvailibilty_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGdatabaseAvailibilty.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
            {
                ToolingView Tooling = new ToolingView();
                Tooling.Partnumber = DGdatabaseAvailibilty.Rows[e.RowIndex].Cells[0].Value.ToString();
                Tooling.takenuser = Usernamenow;
                Tooling.Administratorstatus = Administratorstatus;
                Tooling.CorrectPN = true;
                Tooling.CorrectTL = false;
                Tooling.ShowDialog();
                Tooling.Dispose();
            }
        }

        private void DGreturn_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGreturn.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
            {
                if (Mydb.Rechecktoolingnumberexisting(DGreturn.Rows[e.RowIndex].Cells[2].Value.ToString().Trim()))
                {
                    ReturnForm Returntool = new ReturnForm();
                    Returntool.Toolingnumber = DGreturn.Rows[e.RowIndex].Cells[2].Value.ToString();
                    Returntool.partnumber = DGreturn.Rows[e.RowIndex].Cells[1].Value.ToString();
                    Returntool.returnuser = Usernamenow;
                    Returntool.usingpartnumber = true;
                    Returntool.Administratorstatus = Administratorstatus;
                    Returntool.ShowDialog();
                    Returntool.Dispose();
                }
                else
                {
                    MessageBox.Show("Tooling Number " + DGreturn.Rows[e.RowIndex].Cells[2].Value.ToString().Trim() + " is not available in database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void reportFeedBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReportFeedback report = new ReportFeedback();
            report.ShowDialog();
            report.Dispose();

        }

        private void createNewUserToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Newuser Createuser = new Newuser();
            Createuser.ShowDialog();
            Createuser.Dispose();
        }

        private void deleteUserToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Deleteuser Deleteus = new Deleteuser();
            Deleteus.ShowDialog();
            Deleteus.Dispose();
        }

        private void partNumberListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Protectpassword pass = new Protectpassword();
                pass.ShowDialog(); pass.Dispose();

                if (pass.statuspassword)
                {
                    ImportMaster import = new ImportMaster();
                    import.ShowDialog();
                    import.Dispose();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void toolingNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Protectpassword pass = new Protectpassword();
                pass.ShowDialog(); pass.Dispose();

                if (pass.statuspassword)
                {
                    ImportTooling import = new ImportTooling();
                    import.ShowDialog();
                    import.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void emailWarningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Emailform mail = new Emailform();
            mail.selectmail = "warning";
            mail.ShowDialog();
            mail.Dispose();
        }

        private void emailOutOfLimitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Emailform mail = new Emailform();
            mail.selectmail = "stop";
            mail.ShowDialog();
            mail.Dispose();
        }

        private void emailIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Emailform mail = new Emailform();
            mail.selectmail = "issue";
            mail.ShowDialog();
            mail.Dispose();
        }

        private void addNewFamilyProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Newcustomer custom = new Newcustomer();
            custom.addedby = Usernamenow;
            custom.ShowDialog();
            custom.Dispose();
            Gettreeviewcustomer();
        }

        private void partNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Insertdatabasepartnumber insert = new Insertdatabasepartnumber();
            insert.ShowDialog();
            insert.Dispose();
        }

        private void masterToolingNumberListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertDatabaseTooling insert = new InsertDatabaseTooling();
            insert.ShowDialog();
            insert.Dispose();
        }

        private void CreateHyperlinkdatagrid(DataGridView DataGrid, int location)
        {

            foreach (DataGridViewRow row in DataGrid.Rows)
            {
                DataGridViewLinkCell linkcell = new DataGridViewLinkCell();
                linkcell.Value = row.Cells[location].Value;
                DataGrid[location, row.Index] = linkcell;
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to Logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Mydb.CloseConnection(Mydb.connectionstring);
                Environment.Exit(0);
            }  
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Do you really want to Logout?", "Dialog Title", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Mydb.CloseConnection(Mydb.connectionstring);
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void Admintab_Selected(object sender, TabControlEventArgs e)
        {
            if (Admintab.SelectedIndex == 0)
            {
                CbcategoryMPN.SelectedIndex = 0;
            }
            else if (Admintab.SelectedIndex == 1)
            {
                CbcategoryMTN.SelectedIndex = 0;
            }
            else if (Admintab.SelectedIndex == 2)
            {
                CbcategoryUBP.SelectedIndex = 0;
            }
            else if (Admintab.SelectedIndex == 3)
            {
                CbcategoryTIW.SelectedIndex = 0;
            }
            else if (Admintab.SelectedIndex == 4)
            {
                CbcategoryTTH.SelectedIndex = 0;
            }
        }

        #region Master Part Number

        private void AddItemscustomerMPN(AutoCompleteStringCollection col)
        {
            string colllect = string.Empty;

            if (CbcategoryMPN.SelectedIndex == 0) //part number 
                colllect = Mydb.GetCustomerpartnumberMPN();
            else if (CbcategoryMPN.SelectedIndex == 1) //customer
                colllect = Mydb.GetCustomerMPN();

            string[] BufferName = System.Text.RegularExpressions.Regex.Split(colllect, ",");

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

        private bool DatagridshowMPN(string Query)
        {
            bool Status = false;
            try
            {
                DGMPN.DataSource = null;
                DGMPN.Rows.Clear();
                DGMPN.Refresh();


                SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                SqlCommand cmd = new SqlCommand(Query, connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {

                    while (Reader.Read())
                    {

                        DGMPN.Rows.Add("Edit Data", Reader["PartNumber"].ToString(), Reader["Descriptions"].ToString(), Reader["Type"].ToString(),
                                       Reader["Customer"].ToString(), Reader["Units"].ToString(), Reader["Platesdiaphram"].ToString(), Reader["ID_milimeter"].ToString(),
                                       Reader["ID_inch"].ToString(), Reader["OD_milimeter"].ToString(), Reader["OD_inch"].ToString(), Reader["Thickness_milimeter"].ToString(),
                                       Reader["Thickness_inch"].ToString(), Reader["Material"].ToString(), Reader["Material_spec"].ToString(), Reader["Typeplate"].ToString(),
                                       Reader["Nominal"].ToString(), Reader["Platesateachend"].ToString(), Reader["Remarks"].ToString(), Reader["Ply"].ToString());
                        Status = true;
                    }

                    connection.Close();

                    CreateHyperlinkdatagrid(DGMPN, 0);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Status;
        }

        private void ButtonShowMPN_Click(object sender, EventArgs e)
        {
            try
            {
                if(!DatagridshowMPN("SELECT * FROM ToolingPartnumberList"))
                {
                    MessageBox.Show("Databases are not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                this.Invoke(new EventHandler(CbcategoryMPN_SelectedIndexChanged));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CbcategoryMPN_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtsearchMPN.Clear();
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            AddItemscustomerMPN(DataCollection);
            TxtsearchMPN.AutoCompleteCustomSource = DataCollection;
            TxtsearchMPN.Focus();
        }

        private void ButtonFindMPN_Click(object sender, EventArgs e)
        {
            string Queries = string.Empty;

            try
            {
                if (CbcategoryMPN.SelectedIndex == 0) //Part Number
                {
                    Queries = "SELECT PartNumber, Descriptions, Type, Customer, Units, Platesdiaphram, ID_milimeter, ID_inch, OD_milimeter, OD_inch, Thickness_milimeter, Thickness_inch, Material, Material_spec, Typeplate, Nominal, Platesateachend, Remarks, Ply FROM ToolingPartnumberList WHERE PartNumber = '" + TxtsearchMPN.Text + "'";

                    if (!DatagridshowMPN(Queries))
                    {
                        MessageBox.Show("Part Number '" + TxtsearchMPN.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TxtsearchMPN.SelectAll();
                        TxtsearchMPN.Focus();
                    }
                    else
                    {
                        TxtsearchMPN.Focus();
                    }
                }
                else if (CbcategoryMPN.SelectedIndex == 1) //Customer
                {
                    Queries = "SELECT PartNumber, Descriptions, Type, Customer, Units, Platesdiaphram, ID_milimeter, ID_inch, OD_milimeter, OD_inch, Thickness_milimeter, Thickness_inch, Material, Material_spec, Typeplate, Nominal, Platesateachend, Remarks, Ply FROM ToolingPartnumberList WHERE Customer = '" + TxtsearchMPN.Text.ToUpper() + "'";

                    if (!DatagridshowMPN(Queries))
                    {
                        MessageBox.Show("Customer '" + TxtsearchMPN.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TxtsearchMPN.SelectAll();
                        TxtsearchMPN.Focus();
                    }
                    else
                    {
                        TxtsearchMPN.Focus();
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string[] GetDatagridMPN(int row)
        {
            string[] data = new string[19];

            for (int i = 1; i < DGMPN.ColumnCount; i++)
            {
                data[i - 1] = DGMPN.Rows[row].Cells[i].Value.ToString();
            }
                
            return data;
        }

        private void DeleteDataMasterMPN()
        {
            try
            {
                Int32 selectedCellCount = DGMPN.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    bool StatusDelete = true;
                    //delete 
                    foreach (DataGridViewCell oneCell in DGMPN.SelectedCells)
                    {
                        if (!Mydb.DeleteMasterpartnumber(DGMPN.Rows[oneCell.RowIndex].Cells[1].Value.ToString()))
                        {
                            MessageBox.Show("Something an error when deleted. Please contact the developer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            StatusDelete = false; break;
                        }
                        if (oneCell.Selected)
                            DGMPN.Rows.RemoveAt(oneCell.RowIndex);
                    }

                    if (StatusDelete != false && selectedCellCount == 1)
                    { MessageBox.Show("Serial Number Has been deleted from database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (StatusDelete != false && selectedCellCount > 1)
                    { MessageBox.Show("Serial Number Have been deleted from database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGMPN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure going to delete this information permanently?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        DeleteDataMasterMPN();
                        this.Invoke(new EventHandler(CbcategoryMPN_SelectedIndexChanged));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void DGMPN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGMPN.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
            {
                UpdateDBpartNumber update = new UpdateDBpartNumber();
                update.Datareceived = GetDatagridMPN(DGMPN.SelectedCells[0].RowIndex);
                update.ShowDialog();
                update.Dispose();
            }
        }

        private void BGworkerMPN_DoWork(object sender, DoWorkEventArgs e)
        {
            int coloumlist = 0;
            int Counterprogress = 0;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range formatRange;

            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "Part Number";
            xlWorkSheet.Cells[1, 2] = "Descriptions";
            xlWorkSheet.Cells[1, 3] = "Type";
            xlWorkSheet.Cells[1, 4] = "Customer";
            xlWorkSheet.Cells[1, 5] = "Units";
            xlWorkSheet.Cells[1, 6] = "Plates (Diaphragm)";
            xlWorkSheet.Cells[1, 7] = "ID (mm)";
            xlWorkSheet.Cells[1, 8] = "ID (inch)";
            xlWorkSheet.Cells[1, 9] = "OD (mm)";
            xlWorkSheet.Cells[1, 10] = "OD (inch)";
            xlWorkSheet.Cells[1, 11] = "Thickness (mm)";
            xlWorkSheet.Cells[1, 12] = "Thickness (inch)";
            xlWorkSheet.Cells[1, 13] = "Material";
            xlWorkSheet.Cells[1, 14] = "Material Spec";
            xlWorkSheet.Cells[1, 15] = "Type Plate";
            xlWorkSheet.Cells[1, 16] = "Nominal";
            xlWorkSheet.Cells[1, 17] = "Plates at each end";
            xlWorkSheet.Cells[1, 18] = "Remark";
            xlWorkSheet.Cells[1, 19] = "Ply";

            formatRange = xlWorkSheet.get_Range("a1");
            formatRange.EntireRow.Font.Bold = true;
            formatRange = xlWorkSheet.get_Range("a1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("b1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("c1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("d1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("e1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("f1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("g1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("h1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("i1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("j1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("k1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("l1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("m1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("n1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("o1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("p1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("q1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("r1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("s1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;

            for (int row = 0; row < rowsdatagrid; row++)
            {
                for (int col = 1; col < 20; col++)
                {
                    xlWorkSheet.Cells[row + 2, col] = collectdataexcel[coloumlist++];
                }

                BGworkerMPN.ReportProgress((Int32)Math.Round((double)(Counterprogress++ * 100) / (rowsdatagrid - 1)));
            }

            xlWorkBook.SaveAs(Savepathexcel, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void BGworkerMPN_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!BGworkerMPN.CancellationPending)
            {
                ProgressStatusMPN.Text = e.ProgressPercentage.ToString() + "%";
                ProgressbarMPN.Value = e.ProgressPercentage;
                ProgressbarMPN.Update();
            }
        }

        private void BGworkerMPN_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                MessageBox.Show("Export completed!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ButtonExportMPN.Enabled = true;
        }

        private void collectdatafromgridMPN()
        {
            rowsdatagrid = 0;
            //clear
            if (collectdataexcel.Count > 0) collectdataexcel.Clear();

            //collect data
            for (int row = 0; row < DGMPN.RowCount; row++)
            {
                for (int col = 1; col < 20; col++)
                {
                    collectdataexcel.Add(DGMPN.Rows[row].Cells[col].Value.ToString());
                }
            }

            rowsdatagrid = DGMPN.RowCount;
        }

        private void ButtonExportMPN_Click(object sender, EventArgs e)
        {
            Savepathexcel = string.Empty;

            SaveFileDialog savepath = new SaveFileDialog();
            savepath.Filter = "Excel (*.xls)|*.xls";

            if (DGMPN.Rows.Count > 0)
            {
                if (savepath.ShowDialog() == DialogResult.OK)
                {
                    if (!savepath.FileName.Equals(String.Empty))
                    {
                        FileInfo file = new FileInfo(savepath.FileName);
                        if (file.Extension.Equals(".xls"))
                        {
                            Savepathexcel = savepath.FileName;

                            if (!BGworkerMPN.IsBusy)
                            {
                                ButtonExportMPN.Enabled = false;
                                ProgressbarMPN.Value = 0;
                                collectdatafromgridMPN();
                                BGworkerMPN.RunWorkerAsync();
                            }
                        }
                        else
                        {
                            MessageBox.Show("You did pick a location" + " to save file to");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Cannot export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        #endregion

        #region Master Tooling Number
        private void AddItemscustomerMTN(AutoCompleteStringCollection col)
        {
            string colllect = string.Empty;

            if (CbcategoryMTN.SelectedIndex == 0) //part number 
                colllect = Mydb.GetCustomerpartnumberMTN();
            else if (CbcategoryMTN.SelectedIndex == 1) //Tooling number
                colllect = Mydb.GetCustomertoolnumberMTN();
            else if(CbcategoryMTN.SelectedIndex == 2) //customer
                colllect = Mydb.GetCustomerMTN();

            string[] BufferName = System.Text.RegularExpressions.Regex.Split(colllect, ",");

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

        private bool DatagridshowMTN(string Query)
        {
            bool Status = false;
            try
            {
                DGMTN.DataSource = null;
                DGMTN.Rows.Clear();
                DGMTN.Refresh();


                SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                SqlCommand cmd = new SqlCommand(Query, connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {

                    while (Reader.Read())
                    {
                        if (Reader["ToolsDrawingNumber"].ToString() != "" && Reader["ToolsDrawingNumber"].ToString() != null)
                        {
                            DGMTN.Rows.Add("Edit Data", Reader["PartNumber"].ToString(), Reader["ProductFamily"].ToString(), Reader["ToolingNumber"].ToString(),
                                       Reader["Descriptions"].ToString(), Reader["LocationsStorage"].ToString(), Reader["Qty"].ToString(), "View File",
                                       Reader["Revisions"].ToString(), Reader["Remarks"].ToString(), Reader["Status"].ToString(), Reader["LifeTimetoolingperiode"].ToString(),
                                       Reader["Warning"].ToString(), Reader["Lifetimeusage"].ToString());
                        }
                        else
                        {
                            DGMTN.Rows.Add("Edit Data", Reader["PartNumber"].ToString(), Reader["ProductFamily"].ToString(), Reader["ToolingNumber"].ToString(),
                                       Reader["Descriptions"].ToString(), Reader["LocationsStorage"].ToString(), Reader["Qty"].ToString(), "Upload File",
                                       Reader["Revisions"].ToString(), Reader["Remarks"].ToString(), Reader["Status"].ToString(), Reader["LifeTimetoolingperiode"].ToString(),
                                       Reader["Warning"].ToString(), Reader["Lifetimeusage"].ToString());
                        }
                        
                        Status = true;
                    }

                    connection.Close();

                    CreateHyperlinkdatagrid(DGMTN, 0);
                    CreateHyperlinkdatagrid(DGMTN, 7);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Status;
        }

        private void ButtonShowMTN_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DatagridshowMTN("SELECT * FROM ToolingListnumber"))
                {
                    MessageBox.Show("Databases are not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                this.Invoke(new EventHandler(CbcategoryMTN_SelectedIndexChanged));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CbcategoryMTN_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtsearchMTN.Clear();
            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            AddItemscustomerMTN(DataCollection);
            TxtsearchMTN.AutoCompleteCustomSource = DataCollection;
            TxtsearchMTN.Focus();
        }

        private void ButtonFindMTN_Click(object sender, EventArgs e)
        {
            string Queries = string.Empty;

            try
            {
                if (CbcategoryMTN.SelectedIndex == 0) //Part Number
                {
                    Queries = "SELECT PartNumber, ProductFamily, ToolingNumber, Descriptions, LocationsStorage, Qty, ToolsDrawingNumber, Revisions, Remarks, Status, LifeTimetoolingperiode, Warning, Lifetimeusage FROM ToolingListnumber WHERE PartNumber = '" + TxtsearchMTN.Text + "'";

                    if (!DatagridshowMTN(Queries))
                    {
                        MessageBox.Show("Part Number '" + TxtsearchMTN.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TxtsearchMTN.SelectAll();
                        TxtsearchMTN.Focus();
                    }
                    else
                    {
                        TxtsearchMTN.Focus();
                    }
                }
                else if (CbcategoryMTN.SelectedIndex == 1) //tooling number
                {
                    Queries = "SELECT PartNumber, ProductFamily, ToolingNumber, Descriptions, LocationsStorage, Qty, ToolsDrawingNumber, Revisions, Remarks, Status, LifeTimetoolingperiode, Warning, Lifetimeusage FROM ToolingListnumber WHERE ToolingNumber = '" + TxtsearchMTN.Text + "'";

                    if (!DatagridshowMTN(Queries))
                    {
                        MessageBox.Show("Tooling Number '" + TxtsearchMTN.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TxtsearchMTN.SelectAll();
                        TxtsearchMTN.Focus();
                    }
                    else
                    {
                        TxtsearchMTN.Focus();
                    }
                }
                else if (CbcategoryMTN.SelectedIndex == 2) //customer
                {
                    Queries = "SELECT PartNumber, ProductFamily, ToolingNumber, Descriptions, LocationsStorage, Qty, ToolsDrawingNumber, Revisions, Remarks, Status, LifeTimetoolingperiode, Warning, Lifetimeusage FROM ToolingListnumber WHERE ProductFamily = '" + TxtsearchMTN.Text.ToUpper() + "'";

                    if (!DatagridshowMTN(Queries))
                    {
                        MessageBox.Show("Customer '" + TxtsearchMTN.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TxtsearchMTN.SelectAll();
                        TxtsearchMTN.Focus();
                    }
                    else
                    {
                        TxtsearchMTN.Focus();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string[] GetDatagridMTN(int row)
        {
            string[] data = new string[13];

            for (int i = 1; i < DGMTN.ColumnCount; i++)
            {
                data[i - 1] = DGMTN.Rows[row].Cells[i].Value.ToString();
            }

            return data;
        }

        private void DeleteDataMasterMTN()
        {
            try
            {
                Int32 selectedCellCount = DGMTN.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    bool StatusDelete = true;

                    foreach (DataGridViewCell oneCell in DGMTN.SelectedCells)
                    {
                        if (Mydb.DeleteMastertoolingnumber(DGMTN.Rows[oneCell.RowIndex].Cells[1].Value.ToString(), DGMTN.Rows[oneCell.RowIndex].Cells[3].Value.ToString()))
                        {
                            if (!Mydb.DeleteMasterusagebyproduction(DGMTN.Rows[oneCell.RowIndex].Cells[1].Value.ToString(), DGMTN.Rows[oneCell.RowIndex].Cells[3].Value.ToString()))
                            {
                                MessageBox.Show("Something an error when deleted. Please contact the developer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                StatusDelete = false; break;
                            }
                        }
                        if (oneCell.Selected)
                            DGMTN.Rows.RemoveAt(oneCell.RowIndex);
                    }

                    if (StatusDelete != false && selectedCellCount == 1)
                    { MessageBox.Show("Serial Number Has been deleted from database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (StatusDelete != false && selectedCellCount > 1)
                    { MessageBox.Show("Serial Number Have been deleted from database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGMTN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure going to delete this information permanently?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        DeleteDataMasterMTN();
                        this.Invoke(new EventHandler(CbcategoryMTN_SelectedIndexChanged));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void DGMTN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGMTN.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
            {
                if (e.ColumnIndex == 7)
                {
                    if (DGMTN.Rows[e.RowIndex].Cells[7].Value.ToString() == "Upload File")
                    {
                        OpenFileDialog GetPath = new OpenFileDialog();
                        GetPath.Filter = "PDF File(*.Pdf;)|*.Pdf;";
                        GetPath.Title = "Get File";

                        if (GetPath.ShowDialog() == DialogResult.OK)
                        {
                            //uploadfile into folder server
                            //PENTING perlu disesuaikan tempat server location berada

                            string locationserver = Myini.IniReadValue("Location_fileserver", "File");
                            try
                            {                                                           //123 adalah nama file yang akan dimasukan
                                File.Copy(GetPath.FileName.ToString(), locationserver + DGMTN.Rows[e.RowIndex].Cells[1].Value.ToString() + "_" + DGMTN.Rows[e.RowIndex].Cells[3].Value.ToString() + " " + DateTime.Now.ToString("ddd,dd-MMM-yy HH_mm_ss") + ".Pdf");

                                if (Mydb.Updatelocationfile(DGMTN.Rows[e.RowIndex].Cells[1].Value.ToString(), DGMTN.Rows[e.RowIndex].Cells[3].Value.ToString(), locationserver + DGMTN.Rows[e.RowIndex].Cells[1].Value.ToString() + "_" + DGMTN.Rows[e.RowIndex].Cells[3].Value.ToString() + " " + DateTime.Now.ToString("ddd,dd-MMM-yy HH_mm_ss") + ".Pdf"))
                                {
                                    MessageBox.Show("New File has been uploaded successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            catch (Exception copyError)
                            {
                                MessageBox.Show(copyError.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else //download
                    {
                        if (MessageBox.Show("Do you want to see this file?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                System.Diagnostics.Process.Start(Mydb.GetLocationserverpath(DGMTN.Rows[e.RowIndex].Cells[1].Value.ToString(), DGMTN.Rows[e.RowIndex].Cells[3].Value.ToString()));
                            }
                            catch (Exception copyError)
                            {
                                MessageBox.Show(copyError.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
                            /*
                            SaveFileDialog savepath = new SaveFileDialog();
                            savepath.Filter = "PDF File(*.Pdf;)|*.Pdf;";
                            savepath.Title = "Save File";

                            if (savepath.ShowDialog() == DialogResult.OK)
                            {
                                string locationserver = Mydb.GetLocationserverpath(DGMTN.Rows[e.RowIndex].Cells[1].Value.ToString(), DGMTN.Rows[e.RowIndex].Cells[3].Value.ToString());

                                try
                                {
                                    File.Copy(locationserver, savepath.FileName);
                                    MessageBox.Show("The File has been downloaded successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception copyError)
                                {
                                    MessageBox.Show(copyError.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                
                            }
                            */
                        }
                    }
                }
                else if (e.ColumnIndex == 0)
                {
                    UpdateDBtoolingnumber update = new UpdateDBtoolingnumber();
                    update.Datareceived = GetDatagridMTN(DGMTN.SelectedCells[0].RowIndex);
                    update.ShowDialog();
                    update.Dispose();
                } 
            }
        }

        private void BGworkerMTN_DoWork(object sender, DoWorkEventArgs e)
        {
            int coloumlist = 0;
            int Counterprogress = 0;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range formatRange;

            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "Part Number";
            xlWorkSheet.Cells[1, 2] = "Customer";
            xlWorkSheet.Cells[1, 3] = "Tooling Number";
            xlWorkSheet.Cells[1, 4] = "Descriptions";
            xlWorkSheet.Cells[1, 5] = "Locations Storage";
            xlWorkSheet.Cells[1, 6] = "Quantity";
            xlWorkSheet.Cells[1, 7] = "Tools Drawing Number";
            xlWorkSheet.Cells[1, 8] = "Revisions";
            xlWorkSheet.Cells[1, 9] = "Remarks";
            xlWorkSheet.Cells[1, 10] = "Tooling Status";
            xlWorkSheet.Cells[1, 11] = "Tooling Period";
            xlWorkSheet.Cells[1, 12] = "Warning ";
            xlWorkSheet.Cells[1, 13] = "Life Time Usage";


            formatRange = xlWorkSheet.get_Range("a1");
            formatRange.EntireRow.Font.Bold = true;
            formatRange = xlWorkSheet.get_Range("a1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("b1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("c1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("d1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("e1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("f1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("g1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("h1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("i1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("j1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("k1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("l1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("m1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;


            for (int row = 0; row < rowsdatagrid; row++)
            {
                for (int col = 1; col < 14; col++)
                {
                    xlWorkSheet.Cells[row + 2, col] = collectdataexcel[coloumlist++];
                }

                BGworkerMTN.ReportProgress((Int32)Math.Round((double)(Counterprogress++ * 100) / (rowsdatagrid - 1)));
            }

            xlWorkBook.SaveAs(Savepathexcel, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void BGworkerMTN_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!BGworkerMTN.CancellationPending)
            {
                ProgressStatusMTN.Text = e.ProgressPercentage.ToString() + "%";
                ProgressbarMTN.Value = e.ProgressPercentage;
                ProgressbarMTN.Update();
            }
        }

        private void BGworkerMTN_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                MessageBox.Show("Export completed!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ButtonExportMTN.Enabled = true;
        }

        private void collectdatafromgridMTN()
        {
            rowsdatagrid = 0;
            //clear
            if (collectdataexcel.Count > 0) collectdataexcel.Clear();

            //collect data
            for (int row = 0; row < DGMTN.RowCount; row++)
            {
                for (int col = 1; col < 14; col++)
                {
                    collectdataexcel.Add(DGMTN.Rows[row].Cells[col].Value.ToString());
                }
            }

            rowsdatagrid = DGMTN.RowCount;
        }

        private void ButtonExportMTN_Click(object sender, EventArgs e)
        {
            Savepathexcel = string.Empty;

            SaveFileDialog savepath = new SaveFileDialog();
            savepath.Filter = "Excel (*.xls)|*.xls";

            if (DGMTN.Rows.Count > 0)
            {
                if (savepath.ShowDialog() == DialogResult.OK)
                {
                    if (!savepath.FileName.Equals(String.Empty))
                    {
                        FileInfo file = new FileInfo(savepath.FileName);
                        if (file.Extension.Equals(".xls"))
                        {
                            Savepathexcel = savepath.FileName;

                            if (!BGworkerMPN.IsBusy)
                            {
                                ButtonExportMTN.Enabled = false;
                                ProgressbarMTN.Value = 0;
                                collectdatafromgridMTN();
                                BGworkerMTN.RunWorkerAsync();
                            }
                        }
                        else
                        {
                            MessageBox.Show("You did pick a location" + " to save file to");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Cannot export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region Under By Production

        private void AddItemscustomerUBP(AutoCompleteStringCollection col)
        {
            string colllect = string.Empty;

            if (CbcategoryUBP.SelectedIndex == 0) //part number 
                colllect = Mydb.GetCustomerpartnumberUBP();
            else if (CbcategoryUBP.SelectedIndex == 1) //Tooling number
                colllect = Mydb.GetCustomertoolnumberUBP();
            else if (CbcategoryUBP.SelectedIndex == 2) //customer
                colllect = Mydb.GetCustomerUBP();
            else if (CbcategoryUBP.SelectedIndex == 4) //taken by
                colllect = Mydb.Getusernameupdate();

            string[] BufferName = System.Text.RegularExpressions.Regex.Split(colllect, ",");

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

        private bool DatagridshowUBP(string Query)
        {
            bool Status = false;
            try
            {
                DGUBP.DataSource = null;
                DGUBP.Rows.Clear();
                DGUBP.Refresh();


                SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                SqlCommand cmd = new SqlCommand(Query, connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {

                    while (Reader.Read())
                    {
                        DGUBP.Rows.Add("Edit Data", Reader["DateTimeout"].ToString(), Reader["PartNumber"].ToString(), Reader["ToolingNumber"].ToString(), Reader["Status"].ToString(),
                        Reader["ToolsName"].ToString(), Reader["ProductFamily"].ToString(), Reader["StorageLocation"].ToString(), Reader["ActualToolsQty"].ToString(), Reader["ToolingoutQty"].ToString(),
                        Reader["WONumber"].ToString(), Reader["WOQty"].ToString(), Reader["Note"].ToString(), Reader["TakenBy"].ToString());
                        Status = true;
                    }

                    connection.Close();

                    CreateHyperlinkdatagrid(DGUBP, 0);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Status;
        }

        private void ButtonShowUBP_Click(object sender, EventArgs e)
        {
            try
            {

                if (!DatagridshowUBP("SELECT * FROM Toolingusage WHERE Status = 'under used by prod'"))
                {
                    MessageBox.Show("Databases are not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                this.Invoke(new EventHandler(CbcategoryUBP_SelectedIndexChanged));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CbcategoryUBP_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextsearchUBP.Clear();

            if (CbcategoryUBP.SelectedIndex == 3)
            {
                TextsearchUBP.Text = DatePickUBP.Value.ToString("dd-MMM-yyyy");
                DatePickUBP.Enabled = true;
                TextsearchUBP.ReadOnly = true;
            }
            else
            {
                DatePickUBP.Enabled = false;
                TextsearchUBP.ReadOnly = false;
            }

            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            AddItemscustomerUBP(DataCollection);
            TextsearchUBP.AutoCompleteCustomSource = DataCollection;
            TextsearchUBP.Focus();
        }

        private void ButtonFindUBP_Click(object sender, EventArgs e)
        {
            string Queries = string.Empty;

            try
            {
                if (CbcategoryUBP.SelectedIndex == 0) //Part Number
                {
                    Queries = "SELECT * FROM Toolingusage WHERE Status = 'under used by prod' AND PartNumber = '" + TextsearchUBP.Text + "'";

                    if (!DatagridshowUBP(Queries))
                    {
                        MessageBox.Show("Part Number '" + TextsearchUBP.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TextsearchUBP.SelectAll();
                        TextsearchUBP.Focus();
                    }
                    else
                    {
                        TextsearchUBP.Focus();
                    }
                }
                else if (CbcategoryUBP.SelectedIndex == 1) //tooling number
                {
                    Queries = "SELECT * FROM Toolingusage WHERE Status = 'under used by prod' AND ToolingNumber = '" + TextsearchUBP.Text + "'";

                    if (!DatagridshowUBP(Queries))
                    {
                        MessageBox.Show("Tooling Number '" + TextsearchUBP.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TextsearchUBP.SelectAll();
                        TextsearchUBP.Focus();
                    }
                    else
                    {
                        TextsearchUBP.Focus();
                    }
                }
                else if (CbcategoryUBP.SelectedIndex == 2) //customer
                {
                    Queries = "SELECT * FROM Toolingusage WHERE Status = 'under used by prod' AND ProductFamily = '" + TextsearchUBP.Text + "'";

                    if (!DatagridshowUBP(Queries))
                    {
                        MessageBox.Show("Customer '" + TextsearchUBP.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TextsearchUBP.SelectAll();
                        TextsearchUBP.Focus();
                    }
                    else
                    {
                        TextsearchUBP.Focus();
                    }
                }
                else if (CbcategoryUBP.SelectedIndex == 3) //date time out
                {
                    Queries = "SELECT * FROM Toolingusage WHERE Status = 'under used by prod' AND DateTimeout LIKE '%" + TextsearchUBP.Text + "%' ORDER BY DateTimeout ASC";

                    if (!DatagridshowUBP(Queries))
                    {
                        MessageBox.Show("Date on '" + TextsearchUBP.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TextsearchUBP.SelectAll();
                        TextsearchUBP.Focus();
                    }
                    else
                    {
                        TextsearchUBP.Focus();
                    }
                }
                else if (CbcategoryUBP.SelectedIndex == 4) //taken by
                {
                    Queries = "SELECT * FROM Toolingusage WHERE Status = 'under used by prod' AND TakenBy = '" + TextsearchUBP.Text + "'";

                    if (!DatagridshowUBP(Queries))
                    {
                        MessageBox.Show("The name '" + TextsearchUBP.Text + "' does not exist. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        TextsearchUBP.SelectAll();
                        TextsearchUBP.Focus();
                    }
                    else
                    {
                        TextsearchUBP.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string[] GetDatagridUBP(int row)
        {
            string[] data = new string[13];

            for (int i = 1; i < DGUBP.ColumnCount; i++)
            {
                data[i - 1] = DGUBP.Rows[row].Cells[i].Value.ToString();
            }

            return data;
        }

        private void DeleteDataMasterUBP()
        {
            try
            {
                Int32 selectedCellCount = DGUBP.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    bool StatusDelete = true;

                    foreach (DataGridViewCell oneCell in DGUBP.SelectedCells)
                    {
                        if (!Mydb.DeleteMasterusagebyproduction(DGUBP.Rows[oneCell.RowIndex].Cells[2].Value.ToString(), DGUBP.Rows[oneCell.RowIndex].Cells[3].Value.ToString()))
                        {
                            MessageBox.Show("Something an error when deleted. Please contact the developer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            StatusDelete = false; break;
                        }
                        if (oneCell.Selected)
                            DGUBP.Rows.RemoveAt(oneCell.RowIndex);
                    }

                    if (StatusDelete != false && selectedCellCount == 1)
                    { MessageBox.Show("Serial Number Has been deleted from database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (StatusDelete != false && selectedCellCount > 1)
                    { MessageBox.Show("Serial Number Have been deleted from database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGUBP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure going to delete this information permanently?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        DeleteDataMasterUBP();
                        this.Invoke(new EventHandler(CbcategoryUBP_SelectedIndexChanged));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void DGUBP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGUBP.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
            {
                Updateusagebyproduction update = new Updateusagebyproduction();
                update.Datareceived = GetDatagridUBP(DGUBP.SelectedCells[0].RowIndex);
                update.ShowDialog();
                update.Dispose();
            }
        }

        private void BGworkerUBP_DoWork(object sender, DoWorkEventArgs e)
        {
            int coloumlist = 0;
            int Counterprogress = 0;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range formatRange;

            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "Date Time Out";
            xlWorkSheet.Cells[1, 2] = "Part Number";
            xlWorkSheet.Cells[1, 3] = "Tooling Number";
            xlWorkSheet.Cells[1, 4] = "Tooling Status";
            xlWorkSheet.Cells[1, 5] = "Descriptions";
            xlWorkSheet.Cells[1, 6] = "Customer";
            xlWorkSheet.Cells[1, 7] = "Storage Locations";
            xlWorkSheet.Cells[1, 8] = "Actual Quantity";
            xlWorkSheet.Cells[1, 9] = "Tooling Out Quantity";
            xlWorkSheet.Cells[1, 10] = "WO Number";
            xlWorkSheet.Cells[1, 11] = "WO (Qty Diaphragm)";
            xlWorkSheet.Cells[1, 12] = "Note";
            xlWorkSheet.Cells[1, 13] = "Taken By";

            formatRange = xlWorkSheet.get_Range("a1");
            formatRange.EntireRow.Font.Bold = true;
            formatRange = xlWorkSheet.get_Range("a1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("b1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("c1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("d1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("e1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("f1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("g1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("h1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("i1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("j1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("k1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("l1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("m1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
           
            for (int row = 0; row < rowsdatagrid; row++)
            {
                for (int col = 1; col < 14; col++)
                {
                    xlWorkSheet.Cells[row + 2, col] = collectdataexcel[coloumlist++];
                }

                BGworkerUBP.ReportProgress((Int32)Math.Round((double)(Counterprogress++ * 100) / (rowsdatagrid - 1)));
            }

            xlWorkBook.SaveAs(Savepathexcel, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void BGworkerUBP_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!BGworkerUBP.CancellationPending)
            {
                ProgressStatusUBP.Text = e.ProgressPercentage.ToString() + "%";
                ProgressbarUBP.Value = e.ProgressPercentage;
                ProgressbarUBP.Update();
            }
        }

        private void BGworkerUBP_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                MessageBox.Show("Export completed!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ButtonExportUBP.Enabled = true;
        }

        private void collectdatafromgridUBP()
        {
            rowsdatagrid = 0;
            //clear
            if (collectdataexcel.Count > 0) collectdataexcel.Clear();

            //collect data
            for (int row = 0; row < DGUBP.RowCount; row++)
            {
                for (int col = 1; col < 14; col++)
                {
                    collectdataexcel.Add(DGUBP.Rows[row].Cells[col].Value.ToString());
                }
            }

            rowsdatagrid = DGUBP.RowCount;
        }

        private void ButtonExportUBP_Click(object sender, EventArgs e)
        {
            Savepathexcel = string.Empty;

            SaveFileDialog savepath = new SaveFileDialog();
            savepath.Filter = "Excel (*.xls)|*.xls";

            if (DGUBP.Rows.Count > 0)
            {
                if (savepath.ShowDialog() == DialogResult.OK)
                {
                    if (!savepath.FileName.Equals(String.Empty))
                    {
                        FileInfo file = new FileInfo(savepath.FileName);
                        if (file.Extension.Equals(".xls"))
                        {
                            Savepathexcel = savepath.FileName;

                            if (!BGworkerUBP.IsBusy)
                            {
                                ButtonExportUBP.Enabled = false;
                                ProgressbarUBP.Value = 0;
                                collectdatafromgridUBP();
                                BGworkerUBP.RunWorkerAsync();
                            }
                        }
                        else
                        {
                            MessageBox.Show("You did pick a location" + " to save file to");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Cannot export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void DatePickUBP_ValueChanged(object sender, EventArgs e)
        {
            TextsearchUBP.Text = DatePickUBP.Value.ToString("dd-MMM-yyyy");
        }

        #endregion

        #region Tool Issue / Welder Feedback

        private void AddItemscustomerTIW(AutoCompleteStringCollection col)
        {
            string colllect = string.Empty;

            if (CbcategoryTIW.SelectedIndex == 0) //part number 
                colllect = Mydb.GetCustomerpartnumberTIW();
            else if (CbcategoryTIW.SelectedIndex == 1) //Tooling number
                colllect = Mydb.GetCustomertoolnumberTIW();
            else if (CbcategoryTIW.SelectedIndex == 2) //customer
                colllect = Mydb.GetCustomerTIW();

            string[] BufferName = System.Text.RegularExpressions.Regex.Split(colllect, ",");

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

        private bool DatagridshowTIW(string Query)
        {
            bool Status = false;
            try
            {
                DGTIW.DataSource = null;
                DGTIW.Rows.Clear();
                DGTIW.Refresh();

                SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                SqlCommand cmd = new SqlCommand(Query, connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {

                    while (Reader.Read())
                    {
                        DGTIW.Rows.Add("Edit Data", Reader["dateTimeReturned"].ToString(), Reader["PartNumber"].ToString(), Reader["ToolingNumber"].ToString(), Reader["ToolingIssue_Yes"].ToString(),
                        Reader["ToolsName"].ToString(), Reader["ProductFamily"].ToString(), Reader["ToolsStatus"].ToString(), Reader["ReturnedQTY"].ToString(), Reader["Reasonnotcompleted"].ToString(),
                        Reader["TechnicianansFeedback"].ToString(), Reader["Usedby"].ToString(), Reader["Returnby"].ToString());
                        Status = true;
                    }

                    connection.Close();

                    CreateHyperlinkdatagrid(DGTIW, 0);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Status;
        }

        private void CbcategoryTIW_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextsearchTIW.Clear();

            if (CbcategoryTIW.SelectedIndex == 3)
            {
                TextsearchTIW.Text = DatePickTIW.Value.ToString("dd-MMM-yyyy");
                DatePickTIW.Enabled = true;
                TextsearchTIW.ReadOnly = true;
            }
            else
            {
                DatePickTIW.Enabled = false;
                TextsearchTIW.ReadOnly = false;
            }

            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            AddItemscustomerTIW(DataCollection);
            TextsearchTIW.AutoCompleteCustomSource = DataCollection;
            TextsearchTIW.Focus();
        }

        private void ButtonShowTIW_Click(object sender, EventArgs e)
        {
            try
            {

                if (!DatagridshowTIW("SELECT * FROM ToolingReturnedhistory WHERE ToolingIssue_Yes = 'Yes'"))
                {
                    MessageBox.Show("Databases are not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                this.Invoke(new EventHandler(CbcategoryTIW_SelectedIndexChanged));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DatePickTIW_ValueChanged(object sender, EventArgs e)
        {
            TextsearchTIW.Text = DatePickTIW.Value.ToString("dd-MMM-yyyy");
        }

        private void ButtonFindTIW_Click(object sender, EventArgs e)
        {
            string Queries = string.Empty;

            if (CbcategoryTIW.SelectedIndex == 0) //Part Number
            {
                Queries = "SELECT * FROM ToolingReturnedhistory WHERE ToolingIssue_Yes = 'Yes' AND PartNumber = '" + TextsearchTIW.Text + "'";

                if (!DatagridshowTIW(Queries))
                {
                    MessageBox.Show("Part Number '" + TextsearchTIW.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextsearchTIW.SelectAll();
                    TextsearchTIW.Focus();
                }
                else
                {
                    TextsearchTIW.Focus();
                }
            }
            else if (CbcategoryTIW.SelectedIndex == 1) //tooling number
            {
                Queries = "SELECT * FROM ToolingReturnedhistory WHERE ToolingIssue_Yes = 'Yes' AND ToolingNumber = '" + TextsearchTIW.Text + "'";

                if (!DatagridshowTIW(Queries))
                {
                    MessageBox.Show("Tooling Number '" + TextsearchTIW.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextsearchTIW.SelectAll();
                    TextsearchTIW.Focus();
                }
                else
                {
                    TextsearchTIW.Focus();
                }
            }
            else if (CbcategoryTIW.SelectedIndex == 2) //customer
            {
                Queries = "SELECT * FROM ToolingReturnedhistory WHERE ToolingIssue_Yes = 'Yes' AND ProductFamily = '" + TextsearchTIW.Text + "'";

                if (!DatagridshowTIW(Queries))
                {
                    MessageBox.Show("Customer '" + TextsearchTIW.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextsearchTIW.SelectAll();
                    TextsearchTIW.Focus();
                }
                else
                {
                    TextsearchTIW.Focus();
                }
            }
            else if (CbcategoryTIW.SelectedIndex == 3) //date time out
            {
                Queries = "SELECT * FROM ToolingReturnedhistory WHERE ToolingIssue_Yes = 'Yes' AND dateTimeReturned LIKE '%" + TextsearchTIW.Text + "%' ORDER BY dateTimeReturned ASC";

                if (!DatagridshowTIW(Queries))
                {
                    MessageBox.Show("Date on '" + TextsearchTIW.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextsearchTIW.SelectAll();
                    TextsearchTIW.Focus();
                }
                else
                {
                    TextsearchTIW.Focus();
                }
            }
        }

        private string[] GetDatagridTIW(int row)
        {
            string[] data = new string[12];

            for (int i = 1; i < DGTIW.ColumnCount; i++)
            {
                data[i - 1] = DGTIW.Rows[row].Cells[i].Value.ToString();
            }

            return data;
        }

        private void DeleteDataMasterTIW()
        {
            try
            {
                Int32 selectedCellCount = DGTIW.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    bool StatusDelete = true;

                    foreach (DataGridViewCell oneCell in DGTIW.SelectedCells)
                    {
                        if (!Mydb.DeleteMastertoolissue(DGTIW.Rows[oneCell.RowIndex].Cells[2].Value.ToString(), DGTIW.Rows[oneCell.RowIndex].Cells[3].Value.ToString(), DGTIW.Rows[oneCell.RowIndex].Cells[1].Value.ToString()))
                        {
                            MessageBox.Show("Something an error when deleted. Please contact the developer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            StatusDelete = false; break;
                        }
                        if (oneCell.Selected)
                            DGTIW.Rows.RemoveAt(oneCell.RowIndex);
                    }

                    if (StatusDelete != false && selectedCellCount == 1)
                    { MessageBox.Show("Serial Number Has been deleted from database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (StatusDelete != false && selectedCellCount > 1)
                    { MessageBox.Show("Serial Number Have been deleted from database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DGTIW_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure going to delete this information permanently?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        DeleteDataMasterTIW();
                        this.Invoke(new EventHandler(CbcategoryTIW_SelectedIndexChanged));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void DGTIW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGTIW.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
            {
                UpdateDBtoolissue update = new UpdateDBtoolissue();
                update.Datareceived = GetDatagridTIW(DGTIW.SelectedCells[0].RowIndex);
                update.ShowDialog();
                update.Dispose();
            }
        }

        private void BGworkerTIW_DoWork(object sender, DoWorkEventArgs e)
        {
            int coloumlist = 0;
            int Counterprogress = 0;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range formatRange;

            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkSheet.Cells[1, 1] = "Date Time Returned";
            xlWorkSheet.Cells[1, 2] = "Part Number";
            xlWorkSheet.Cells[1, 3] = "Tooling Number";
            xlWorkSheet.Cells[1, 4] = "Tooling Issue Status";
            xlWorkSheet.Cells[1, 5] = "Descriptions";
            xlWorkSheet.Cells[1, 6] = "Customer";
            xlWorkSheet.Cells[1, 7] = "Tool Status";
            xlWorkSheet.Cells[1, 8] = "Returned Tool Quantity";
            xlWorkSheet.Cells[1, 9] = "Reason not complete";
            xlWorkSheet.Cells[1, 10] = "Technician Feedback";
            xlWorkSheet.Cells[1, 11] = "Welder";
            xlWorkSheet.Cells[1, 12] = "Return By";


            formatRange = xlWorkSheet.get_Range("a1");
            formatRange.EntireRow.Font.Bold = true;
            formatRange = xlWorkSheet.get_Range("a1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("b1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("c1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("d1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("e1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("f1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("g1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("h1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("i1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("j1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("k1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("l1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;

            for (int row = 0; row < rowsdatagrid; row++)
            {
                for (int col = 1; col < 13; col++)
                {
                    xlWorkSheet.Cells[row + 2, col] = collectdataexcel[coloumlist++];
                }

                BGworkerTIW.ReportProgress((Int32)Math.Round((double)(Counterprogress++ * 100) / (rowsdatagrid - 1)));
            }

            xlWorkBook.SaveAs(Savepathexcel, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void BGworkerTIW_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!BGworkerTIW.CancellationPending)
            {
                ProgressStatusTIW.Text = e.ProgressPercentage.ToString() + "%";
                ProgressbarTIW.Value = e.ProgressPercentage;
                ProgressbarTIW.Update();
            }
        }

        private void BGworkerTIW_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                MessageBox.Show("Export completed!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ButtonExportTIW.Enabled = true;
        }

        private void collectdatafromgridTIW()
        {
            rowsdatagrid = 0;
            //clear
            if (collectdataexcel.Count > 0) collectdataexcel.Clear();

            //collect data
            for (int row = 0; row < DGTIW.RowCount; row++)
            {
                for (int col = 1; col < 13; col++)
                {
                    collectdataexcel.Add(DGTIW.Rows[row].Cells[col].Value.ToString());
                }
            }

            rowsdatagrid = DGTIW.RowCount;
        }

        private void ButtonExportTIW_Click(object sender, EventArgs e)
        {
            Savepathexcel = string.Empty;

            SaveFileDialog savepath = new SaveFileDialog();
            savepath.Filter = "Excel (*.xls)|*.xls";

            if (DGTIW.Rows.Count > 0)
            {
                if (savepath.ShowDialog() == DialogResult.OK)
                {
                    if (!savepath.FileName.Equals(String.Empty))
                    {
                        FileInfo file = new FileInfo(savepath.FileName);
                        if (file.Extension.Equals(".xls"))
                        {
                            Savepathexcel = savepath.FileName;

                            if (!BGworkerTIW.IsBusy)
                            {
                                ButtonExportTIW.Enabled = false;
                                ProgressbarTIW.Value = 0;
                                collectdatafromgridTIW();
                                BGworkerTIW.RunWorkerAsync();
                            }
                        }
                        else
                        {
                            MessageBox.Show("You did pick a location" + " to save file to");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Cannot export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        #region Tool Transcation history

        private void AddItemscustomerTTH(AutoCompleteStringCollection col)
        {
            string colllect = string.Empty;
            
            if (CbcategoryTTH.SelectedIndex == 2) //customer
                colllect = Mydb.GetCustomerTTH();

            string[] BufferName = System.Text.RegularExpressions.Regex.Split(colllect, ",");

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

        private bool DatagridshowTTH1(string Query)
        {
            bool Status = false;
            try
            {
                DGTTH1.DataSource = null;
                DGTTH1.Rows.Clear();
                DGTTH1.Refresh();

                SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                SqlCommand cmd = new SqlCommand(Query, connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {

                    while (Reader.Read())
                    {
                        DGTTH1.Rows.Add("Edit Data", Reader["DateTimeout"].ToString(), Reader["PartNumber"].ToString(), Reader["ToolingNumber"].ToString(),
                        Reader["ToolsName"].ToString(), Reader["ProductFamily"].ToString(), Reader["StorageLocation"].ToString(), Reader["ActualToolsQty"].ToString(), Reader["ToolingoutQty"].ToString(),
                        Reader["WONumber"].ToString(), Reader["WOQty"].ToString(), Reader["Note"].ToString(), Reader["TakenBy"].ToString(), Reader["Lifetimeusage"].ToString());
                        Status = true;
                    }

                    connection.Close();

                    CreateHyperlinkdatagrid(DGTTH1, 0);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Status;
        } //Takeout

        private bool DatagridshowTTH2(string Query)
        {
            bool Status = false;
            try
            {
                DGTTH2.DataSource = null;
                DGTTH2.Rows.Clear();
                DGTTH2.Refresh();

                SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                SqlCommand cmd = new SqlCommand(Query, connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {

                    while (Reader.Read())
                    {
                        DGTTH2.Rows.Add("Edit Data", Reader["dateTimeReturned"].ToString(), Reader["PartNumber"].ToString(), Reader["ToolingNumber"].ToString(), Reader["ToolingIssue_Yes"].ToString(),
                        Reader["ToolsName"].ToString(), Reader["ProductFamily"].ToString(), Reader["ToolsStatus"].ToString(), Reader["ReturnedQTY"].ToString(), Reader["Reasonnotcompleted"].ToString(),
                        Reader["TechnicianansFeedback"].ToString(), Reader["Usedby"].ToString(), Reader["Returnby"].ToString(), Reader["Lifetimeusage"].ToString());
                        Status = true;
                    }

                    connection.Close();

                    CreateHyperlinkdatagrid(DGTTH2, 0);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Status;
        }

        private void CbcategoryTTH_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextSearchTTH.Clear();

            if (CbcategoryTTH.SelectedIndex == 3)
            {
                TextSearchTTH.Text = DatePickTTH.Value.ToString("dd-MMM-yyyy");
                DatePickTTH.Enabled = true;
                TextSearchTTH.ReadOnly = true;
            }
            else
            {
                DatePickTTH.Enabled = false;
                TextSearchTTH.ReadOnly = false;
            }

            AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
            AddItemscustomerTTH(DataCollection);
            TextSearchTTH.AutoCompleteCustomSource = DataCollection;
            TextSearchTTH.Focus();
        }

        private void ButtonShowTTH_Click(object sender, EventArgs e)
        {
            try
            {
                if (!DatagridshowTTH1("SELECT * FROM Toolingusehistory"))
                    MessageBox.Show("Databases are not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (!DatagridshowTTH2("SELECT * FROM ToolingReturnedhistory"))
                    MessageBox.Show("Databases are not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DatePickTTH_ValueChanged(object sender, EventArgs e)
        {
            TextSearchTTH.Text = DatePickTTH.Value.ToString("dd-MMM-yyyy");
        }

        private void ButtonFindTTH_Click(object sender, EventArgs e)
        {
            string QueriesTTH1 = string.Empty;
            string QueriesTTH2 = string.Empty;

            if (CbcategoryTTH.SelectedIndex == 0) //Part Number
            {
                QueriesTTH1 = "SELECT * FROM Toolingusehistory WHERE PartNumber = '" + TextSearchTTH.Text + "' ORDER BY DateTimeout ASC";

                if (!DatagridshowTTH1(QueriesTTH1))
                {
                    MessageBox.Show("Part Number '" + TextSearchTTH.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextSearchTTH.SelectAll();
                    TextSearchTTH.Focus();
                }

                QueriesTTH2 = "SELECT * FROM ToolingReturnedhistory WHERE PartNumber = '" + TextSearchTTH.Text + "' ORDER BY dateTimeReturned ASC";

                if (!DatagridshowTTH2(QueriesTTH2))
                {
                    MessageBox.Show("Part Number '" + TextSearchTTH.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextSearchTTH.SelectAll();
                    TextSearchTTH.Focus();
                }
            }
            else if (CbcategoryTTH.SelectedIndex == 1) //tooling number
            {
                QueriesTTH1 = "SELECT * FROM Toolingusehistory WHERE ToolingNumber = '" + TextSearchTTH.Text + "' ORDER BY DateTimeout ASC";

                if (!DatagridshowTTH1(QueriesTTH1))
                {
                    MessageBox.Show("Tooling Number '" + TextSearchTTH.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextSearchTTH.SelectAll();
                    TextSearchTTH.Focus();
                }

                QueriesTTH2 = "SELECT * FROM ToolingReturnedhistory WHERE ToolingNumber = '" + TextSearchTTH.Text + "' ORDER BY dateTimeReturned ASC";

                if (!DatagridshowTTH2(QueriesTTH2))
                {
                    MessageBox.Show("Tooling Number '" + TextSearchTTH.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextSearchTTH.SelectAll();
                    TextSearchTTH.Focus();
                }
            }
            else if (CbcategoryTTH.SelectedIndex == 2) //customer
            {
                QueriesTTH1 = "SELECT * FROM Toolingusehistory WHERE ProductFamily = '" + TextSearchTTH.Text + "' ORDER BY DateTimeout ASC";

                if (!DatagridshowTTH1(QueriesTTH1))
                {
                    MessageBox.Show("Customer '" + TextSearchTTH.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextSearchTTH.SelectAll();
                    TextSearchTTH.Focus();
                }

                QueriesTTH2 = "SELECT * FROM ToolingReturnedhistory WHERE ProductFamily = '" + TextSearchTTH.Text + "' ORDER BY dateTimeReturned ASC";

                if (!DatagridshowTTH2(QueriesTTH2))
                {
                    MessageBox.Show("Customer '" + TextSearchTTH.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextSearchTTH.SelectAll();
                    TextSearchTTH.Focus();
                }
            }
            else if (CbcategoryTTH.SelectedIndex == 3) //date time out
            {
                QueriesTTH1 = "SELECT * FROM Toolingusehistory WHERE DateTimeout LIKE '%" + TextSearchTTH.Text + "%' ORDER BY DateTimeout ASC";

                if (!DatagridshowTTH1(QueriesTTH1))
                {
                    MessageBox.Show("Date on '" + TextSearchTTH.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextSearchTTH.SelectAll();
                    TextSearchTTH.Focus();
                }

                QueriesTTH2 = "SELECT * FROM ToolingReturnedhistory WHERE dateTimeReturned LIKE '%" + TextSearchTTH.Text + "%' ORDER BY dateTimeReturned ASC";

                if (!DatagridshowTTH2(QueriesTTH2))
                {
                    MessageBox.Show("Date on '" + TextSearchTTH.Text + "' is not available. Please check one more time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextSearchTTH.SelectAll();
                    TextSearchTTH.Focus();
                }
            }
        }

        private string[] GetDatagridTTH1(int row)
        {
            string[] data = new string[13];

            for (int i = 1; i < DGTTH1.ColumnCount; i++)
            {
                data[i - 1] = DGTTH1.Rows[row].Cells[i].Value.ToString();
            }

            return data;
        }

        private string[] GetDatagridTTH2(int row)
        {
            string[] data = new string[13];

            for (int i = 1; i < DGTTH2.ColumnCount; i++)
            {
                data[i - 1] = DGTTH2.Rows[row].Cells[i].Value.ToString();
            }

            return data;
        }

        private void DeleteDataMasterTTH1()
        {
            try
            {
                Int32 selectedCellCount = DGTTH1.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    bool StatusDelete = true;

                    foreach (DataGridViewCell oneCell in DGTTH1.SelectedCells)
                    {
                        if (!Mydb.DeleteMastertooltranscationusageprod(DGTTH1.Rows[oneCell.RowIndex].Cells[2].Value.ToString(), DGTTH1.Rows[oneCell.RowIndex].Cells[3].Value.ToString(), DGTTH1.Rows[oneCell.RowIndex].Cells[1].Value.ToString()))
                        {
                            MessageBox.Show("Something an error when deleted. Please contact the developer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            StatusDelete = false; break;
                        }
                        if (oneCell.Selected)
                            DGTTH1.Rows.RemoveAt(oneCell.RowIndex);
                    }

                    if (StatusDelete != false && selectedCellCount == 1)
                    { MessageBox.Show("Serial Number Has been deleted from database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (StatusDelete != false && selectedCellCount > 1)
                    { MessageBox.Show("Serial Number Have been deleted from database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteDataMasterTTH2()
        {
            try
            {
                Int32 selectedCellCount = DGTTH2.GetCellCount(DataGridViewElementStates.Selected);
                if (selectedCellCount > 0)
                {
                    bool StatusDelete = true;

                    foreach (DataGridViewCell oneCell in DGTTH2.SelectedCells)
                    {
                        if (!Mydb.DeleteMastertooltranscationreturn(DGTTH2.Rows[oneCell.RowIndex].Cells[2].Value.ToString(), DGTTH2.Rows[oneCell.RowIndex].Cells[3].Value.ToString(), DGTTH2.Rows[oneCell.RowIndex].Cells[1].Value.ToString()))
                        {
                            MessageBox.Show("Something an error when deleted. Please contact the developer", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            StatusDelete = false; break;
                        }
                        if (oneCell.Selected)
                            DGTTH2.Rows.RemoveAt(oneCell.RowIndex);
                    }

                    if (StatusDelete != false && selectedCellCount == 1)
                    { MessageBox.Show("Serial Number Has been deleted from database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else if (StatusDelete != false && selectedCellCount > 1)
                    { MessageBox.Show("Serial Number Have been deleted from database", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void DGTTH1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGTTH1.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
            {
                Updatehistoryusage update = new Updatehistoryusage();
                update.Datareceived = GetDatagridTTH1(DGTTH1.SelectedCells[0].RowIndex);
                update.ShowDialog();
                update.Dispose();
            }
        }

        private void DGTTH2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGTTH2.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
            {
                Updatereturnhistory update = new Updatereturnhistory();
                update.Datareceived = GetDatagridTTH2(DGTTH2.SelectedCells[0].RowIndex);
                update.ShowDialog();
                update.Dispose();
            }
        }

        private void DGTTH1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure going to delete this information permanently?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        DeleteDataMasterTTH1();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void DGTTH2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Are you sure going to delete this information permanently?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        DeleteDataMasterTTH2();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void BGworkerTTH_DoWork(object sender, DoWorkEventArgs e)
        {
            int coloumlist = 0;
            int Counterprogress = 0;

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range formatRange;

            object misValue = System.Reflection.Missing.Value;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //take out
            xlWorkSheet.Cells[1, 1] = "Date Time Out";
            xlWorkSheet.Cells[1, 2] = "Part Number";
            xlWorkSheet.Cells[1, 3] = "Tooling Number";
            xlWorkSheet.Cells[1, 4] = "Descriptions";
            xlWorkSheet.Cells[1, 5] = "Customer";
            xlWorkSheet.Cells[1, 6] = "Storage Location";
            xlWorkSheet.Cells[1, 7] = "Actual Qty";
            xlWorkSheet.Cells[1, 8] = "Tooling Out Qty";
            xlWorkSheet.Cells[1, 9] = "WO Number";
            xlWorkSheet.Cells[1, 10] = "WO (Qty Diaphragm)";
            xlWorkSheet.Cells[1, 11] = "Note";
            xlWorkSheet.Cells[1, 12] = "Taken By";
            xlWorkSheet.Cells[1, 13] = "Life Time Usage";

            formatRange = xlWorkSheet.get_Range("a1");
            formatRange.EntireRow.Font.Bold = true;
            formatRange = xlWorkSheet.get_Range("a1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("b1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("c1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("d1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("e1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("f1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("g1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("h1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("i1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("j1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("k1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("l1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("m1"); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;

            for (int row = 0; row < rowsdatagrid; row++)
            {
                for (int col = 1; col < 14; col++)
                {
                    xlWorkSheet.Cells[row + 2, col] = collectdataexcel[coloumlist++];
                }

                BGworkerTTH.ReportProgress((Int32)Math.Round((double)(Counterprogress++ * 100) / ((rowsdatagrid + newrowsdatagrid) - 1)));
            }

            //buat 2 space

            for (int row = (rowsdatagrid + 2); row < (rowsdatagrid + 4); row++)
            {
                for (int col = 1; col < 14; col++)
                {
                    xlWorkSheet.Cells[row, col] = "";
                }
            }

            //return
            xlWorkSheet.Cells[rowsdatagrid + 4, 1] = "Date Time Returned";
            xlWorkSheet.Cells[rowsdatagrid + 4, 2] = "Part Number";
            xlWorkSheet.Cells[rowsdatagrid + 4, 3] = "Tooling Number";
            xlWorkSheet.Cells[rowsdatagrid + 4, 4] = "Tooling Issue Status";
            xlWorkSheet.Cells[rowsdatagrid + 4, 5] = "Descriptions";
            xlWorkSheet.Cells[rowsdatagrid + 4, 6] = "Customer";
            xlWorkSheet.Cells[rowsdatagrid + 4, 7] = "Tool Status";
            xlWorkSheet.Cells[rowsdatagrid + 4, 8] = "Returned Tool Quantity";
            xlWorkSheet.Cells[rowsdatagrid + 4, 9] = "Reason not complete";
            xlWorkSheet.Cells[rowsdatagrid + 4, 10] = "Technician Feedback";
            xlWorkSheet.Cells[rowsdatagrid + 4, 11] = "Welder";
            xlWorkSheet.Cells[rowsdatagrid + 4, 12] = "Return By";
            xlWorkSheet.Cells[rowsdatagrid + 4, 13] = "Life Time Usage";  

            formatRange = xlWorkSheet.get_Range("a" + (rowsdatagrid + 4).ToString());
            formatRange.EntireRow.Font.Bold = true;
            formatRange = xlWorkSheet.get_Range("a" + (rowsdatagrid + 4).ToString()); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("b" + (rowsdatagrid + 4).ToString()); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("c" + (rowsdatagrid + 4).ToString()); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("d" + (rowsdatagrid + 4).ToString()); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("e" + (rowsdatagrid + 4).ToString()); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("f" + (rowsdatagrid + 4).ToString()); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("g" + (rowsdatagrid + 4).ToString()); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("h" + (rowsdatagrid + 4).ToString()); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("i" + (rowsdatagrid + 4).ToString()); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("j" + (rowsdatagrid + 4).ToString()); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("k" + (rowsdatagrid + 4).ToString()); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("l" + (rowsdatagrid + 4).ToString()); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;
            formatRange = xlWorkSheet.get_Range("m" + (rowsdatagrid + 4).ToString()); formatRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow); formatRange.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing); formatRange.HorizontalAlignment = 3; formatRange.VerticalAlignment = 3;

            for (int row = (rowsdatagrid + 3); row < ((rowsdatagrid + 3) + newrowsdatagrid); row++)
            {
                for (int col = 1; col < 14; col++)
                {
                    xlWorkSheet.Cells[row + 2, col] = collectdataexcel[coloumlist++];
                }

                BGworkerTTH.ReportProgress((Int32)Math.Round((double)(Counterprogress++ * 100) / ((rowsdatagrid + newrowsdatagrid) - 1)));
            }

            xlWorkBook.SaveAs(Savepathexcel, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void BGworkerTTH_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!BGworkerTTH.CancellationPending)
            {
                ProgressStatusTTH.Text = e.ProgressPercentage.ToString() + "%";
                ProgressbarTTH.Value = e.ProgressPercentage;
                ProgressbarTTH.Update();
            }
        }

        private void BGworkerTTH_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                MessageBox.Show("Export completed!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ButtonExportTTH.Enabled = true;
        }

        private void collectdatafromgridTTH()
        {
            rowsdatagrid = 0;
            newrowsdatagrid = 0;
            //clear
            if (collectdataexcel.Count > 0) collectdataexcel.Clear();

            //collect data
            for (int row = 0; row < DGTTH1.RowCount; row++)
            {
                for (int col = 1; col < 14; col++)
                {
                    collectdataexcel.Add(DGTTH1.Rows[row].Cells[col].Value.ToString());
                }
            }

            for (int row = 0; row < DGTTH2.RowCount; row++)
            {
                for (int col = 1; col < 14; col++)
                {
                    collectdataexcel.Add(DGTTH2.Rows[row].Cells[col].Value.ToString());
                }
            }

            rowsdatagrid = DGTTH1.RowCount;
            newrowsdatagrid = DGTTH2.RowCount;

        }

        private void ButtonExportTTH_Click(object sender, EventArgs e)
        {
            Savepathexcel = string.Empty;

            SaveFileDialog savepath = new SaveFileDialog();
            savepath.Filter = "Excel (*.xls)|*.xls";

            if (DGTTH1.Rows.Count > 0 && DGTTH2.Rows.Count > 0)
            {
                if (savepath.ShowDialog() == DialogResult.OK)
                {
                    if (!savepath.FileName.Equals(String.Empty))
                    {
                        FileInfo file = new FileInfo(savepath.FileName);
                        if (file.Extension.Equals(".xls"))
                        {
                            Savepathexcel = savepath.FileName;

                            if (!BGworkerTTH.IsBusy)
                            {
                                ButtonExportTTH.Enabled = false;
                                ProgressbarTTH.Value = 0;
                                collectdatafromgridTTH();
                                BGworkerTTH.RunWorkerAsync();
                            }
                        }
                        else
                        {
                            MessageBox.Show("You did pick a location" + " to save file to");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Cannot export.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion

        private void flowChartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(@".\Flow\Flow Tool Life Management System.pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
