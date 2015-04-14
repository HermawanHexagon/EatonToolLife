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
    public partial class Viewtoolingisseu : Form
    {
        Sqloperation Mydb = new Sqloperation();
        public string Querysql = string.Empty;

        public Viewtoolingisseu()
        {
            InitializeComponent();
        }

        public class RetriveTableData
        {
            public string ToolingNumber1;
            public string ToolsName1;
            public string PartNumber1;
            public string ProductFamily1;
            public string dateTimeReturned1;
            public string Returnby1;
            public string ReturnedQTY1;
            public string ToolsStatus1;
            public string Reasonnotcompleted1;
            public string Usedby1;
            public string ToolingIssue_Yes1;
            public string TechnicianansFeedback1;
            public string Lifetimeusage1;
        }

        private void BGworker_DoWork(object sender, DoWorkEventArgs e)
        {
            RetriveTableData Obj = (RetriveTableData)e.Argument;

            try
            {
                SqlConnection connection = new SqlConnection(Mydb.connectionstring);
                SqlCommand cmd = new SqlCommand(Querysql, connection);
                connection.Open();

                using (SqlDataReader Reader = cmd.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        Obj.ToolingNumber1 = Reader["ToolingNumber"].ToString().Trim();
                        Obj.ToolsName1 = Reader["ToolsName"].ToString().Trim();
                        Obj.PartNumber1 = Reader["PartNumber"].ToString().Trim();
                        Obj.ProductFamily1 = Reader["ProductFamily"].ToString().Trim();
                        Obj.dateTimeReturned1 = Reader["dateTimeReturned"].ToString().Trim();
                        Obj.Returnby1 = Reader["Returnby"].ToString().Trim();
                        Obj.ReturnedQTY1 = Reader["ReturnedQTY"].ToString().Trim();
                        Obj.ToolsStatus1 = Reader["ToolsStatus"].ToString().Trim();
                        Obj.Reasonnotcompleted1 = Reader["Reasonnotcompleted"].ToString().Trim();
                        Obj.Usedby1 = Reader["Usedby"].ToString().Trim();
                        Obj.ToolingIssue_Yes1 = Reader["ToolingIssue_Yes"].ToString().Trim();
                        Obj.TechnicianansFeedback1 = Reader["TechnicianansFeedback"].ToString().Trim();
                        Obj.Lifetimeusage1 = Reader["Lifetimeusage"].ToString().Trim();
                        System.Threading.Thread.Sleep(100);
                        BGworker.ReportProgress(0, Obj);
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BGworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!BGworker.CancellationPending)
            {
                RetriveTableData Obj = (RetriveTableData)e.UserState;
                DGview.Rows.Add(Obj.dateTimeReturned1, Obj.PartNumber1, Obj.ToolingNumber1, Obj.ToolingIssue_Yes1,
                        Obj.ToolsName1, Obj.ProductFamily1, Obj.ToolsStatus1, Obj.ReturnedQTY1, Obj.Reasonnotcompleted1,
                        Obj.TechnicianansFeedback1, Obj.Usedby1, Obj.Returnby1);
            }
        }

        private void BGworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                MessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Viewtoolingisseu_Load(object sender, EventArgs e)
        {
            if (!BGworker.IsBusy)
            {
                RetriveTableData TObj = new RetriveTableData();
                //SELECT * FROM ToolingReturnedhistory WHERE ToolingIssue_Yes = 'Yes'
                DGview.Rows.Clear();
                BGworker.RunWorkerAsync(TObj);
            }
        }
    }
}
