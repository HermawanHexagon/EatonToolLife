using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Eatontoollife
{
    public partial class viewunderproductioncs : Form
    {
        Sqloperation Mydb = new Sqloperation();
        public string Querysql = string.Empty;

        public viewunderproductioncs()
        {
            InitializeComponent();
        }

        public class RetriveTableData
        {
            public string ToolingNumber1;
            public string StorageLocation1;
            public string ActualToolsQty1;
            public string ToolsName1;
            public string PartNumber1;
            public string ProductFamily1;
            public string DateTimeout1;
            public string TakenBy1;
            public string ToolingoutQty1;
            public string WONumber1;
            public string WOQty1;
            public string Note1;
            public string Status1;
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
                        Obj.StorageLocation1 = Reader["StorageLocation"].ToString().Trim();
                        Obj.ActualToolsQty1 = Reader["ActualToolsQty"].ToString().Trim();
                        Obj.ToolsName1 = Reader["ToolsName"].ToString().Trim();
                        Obj.PartNumber1 = Reader["PartNumber"].ToString().Trim();
                        Obj.ProductFamily1 = Reader["ProductFamily"].ToString().Trim();
                        Obj.DateTimeout1 = Reader["DateTimeout"].ToString().Trim();
                        Obj.TakenBy1 = Reader["TakenBy"].ToString().Trim();
                        Obj.ToolingoutQty1 = Reader["ToolingoutQty"].ToString().Trim();
                        Obj.WONumber1 = Reader["WONumber"].ToString().Trim();
                        Obj.WOQty1 = Reader["WOQty"].ToString().Trim();
                        Obj.Note1 = Reader["Note"].ToString().Trim();
                        Obj.Status1 = Reader["Status"].ToString().Trim();
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
                DGview.Rows.Add(Obj.DateTimeout1, Obj.PartNumber1, Obj.ToolingNumber1, Obj.Status1,
                        Obj.ToolsName1, Obj.ProductFamily1, Obj.StorageLocation1, Obj.ActualToolsQty1, Obj.ToolingoutQty1,
                        Obj.WONumber1, Obj.WOQty1, Obj.Note1, Obj.TakenBy1);
            }
        }

        private void BGworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                MessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void viewunderproductioncs_Load(object sender, EventArgs e)
        {
            if (!BGworker.IsBusy)
            {
                RetriveTableData TObj = new RetriveTableData();
                DGview.Rows.Clear();
                BGworker.RunWorkerAsync(TObj);
            }
        }
    }
}
