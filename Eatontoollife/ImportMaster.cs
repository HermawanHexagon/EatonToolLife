using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace Eatontoollife
{
    public partial class ImportMaster : Form
    {
        Sqloperation Mydb = new Sqloperation();
        public string Path;

        public ImportMaster()
        {
            InitializeComponent();
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
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        public class RetriveTableData
        {
            public string[] Collectdata = new string[19];
        }

        private void ButtonImport_Click(object sender, EventArgs e)
        {
            Path = string.Empty;
            OpenFileDialog GetPath = new OpenFileDialog();
            GetPath.Filter = "Excel Files 2007(*.xlsx;)|*.xlsx;";
            if (GetPath.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (GetPath.FileName != null)
                    {
                        Path = GetPath.FileName;

                        if (!Bgworker.IsBusy)
                        {
                            RetriveTableData TObj = new RetriveTableData();
                            DGImport.Rows.Clear();
                            ButtonSave.Enabled = false;
                            ButtonImport.Enabled = false;
                            Bgworker.RunWorkerAsync(TObj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Bgworker_DoWork(object sender, DoWorkEventArgs e)
        {
            RetriveTableData Obj = (RetriveTableData)e.Argument;
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            int RowCnt = 0;  int ColCnt = 0;
            int Counter = 1; int Coltooling = 19;
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(Path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            range = xlWorkSheet.UsedRange;
            
            for (RowCnt = 4; RowCnt <= range.Rows.Count; RowCnt++) //baris
            {
                for (ColCnt = 1; ColCnt <= Coltooling; ColCnt++) //kolom
                {
                    if ((string)(range.Cells[RowCnt, ColCnt] as Excel.Range).Text != null)
                    {
                        Obj.Collectdata[ColCnt - 1] = (string)(range.Cells[RowCnt, ColCnt] as Excel.Range).Text;
                    }
                    else
                    {
                        Obj.Collectdata[ColCnt - 1] = "";
                    }   
                    
                }
                Bgworker.ReportProgress((Int32)Math.Round((double)(Counter++ * 100) / (range.Rows.Count - 3)), Obj);
            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void Bgworker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!Bgworker.CancellationPending)
            {
                RetriveTableData Obj = (RetriveTableData)e.UserState;
                DGImport.Rows.Add(Obj.Collectdata[0].ToString(), Obj.Collectdata[1].ToString(), Obj.Collectdata[2].ToString(), Obj.Collectdata[3].ToString().ToUpper(), Obj.Collectdata[4].ToString(), Obj.Collectdata[5].ToString(), Obj.Collectdata[6].ToString(), Obj.Collectdata[7].ToString(), Obj.Collectdata[8].ToString(), Obj.Collectdata[9].ToString(), Obj.Collectdata[10].ToString(), Obj.Collectdata[11].ToString(), Obj.Collectdata[12].ToString(), Obj.Collectdata[13].ToString(), Obj.Collectdata[14].ToString(), Obj.Collectdata[15].ToString(), Obj.Collectdata[16].ToString(), Obj.Collectdata[17].ToString(), Obj.Collectdata[18].ToString());
                DGImport.FirstDisplayedCell = DGImport.Rows[DGImport.Rows.Count - 1].Cells[0];
                lblcount.Text = "Proccessing..." + e.ProgressPercentage.ToString() + "%";
                Progress_Status.Value = e.ProgressPercentage;
                Progress_Status.Update();
            }
        }

        private void Bgworker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)//Check if the worker has been canceled or if an error occurred
            {
                ButtonImport.Enabled = true;
                ButtonSave.Enabled = true;
                MessageBox.Show("Import completed!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("An error has occurred", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImportMaster_Load(object sender, EventArgs e)
        {
            //empty
        }

        private void showprogress(int percent)
        {
            lblcount.Text = "Proccessing..." + percent.ToString() + "%";
            Progress_Status.Value = percent;
            Progress_Status.Update();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (DGImport.Rows.Count > 0)
            {
                try
                {
                    DialogResult result = MessageBox.Show("Are you sure going to save all of these data into database?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        for (int CounterRow = 0; CounterRow < DGImport.Rows.Count; CounterRow++)
                        {
                            Mydb.Savedatabasepartnumberlist(DGImport.Rows[CounterRow].Cells[0].Value.ToString(), DGImport.Rows[CounterRow].Cells[1].Value.ToString(),
                            DGImport.Rows[CounterRow].Cells[2].Value.ToString(), DGImport.Rows[CounterRow].Cells[3].Value.ToString(),
                            DGImport.Rows[CounterRow].Cells[4].Value.ToString(), DGImport.Rows[CounterRow].Cells[5].Value.ToString(),
                            DGImport.Rows[CounterRow].Cells[6].Value.ToString(), DGImport.Rows[CounterRow].Cells[7].Value.ToString(),
                            DGImport.Rows[CounterRow].Cells[8].Value.ToString(), DGImport.Rows[CounterRow].Cells[9].Value.ToString(),
                            DGImport.Rows[CounterRow].Cells[10].Value.ToString(), DGImport.Rows[CounterRow].Cells[11].Value.ToString(),
                            DGImport.Rows[CounterRow].Cells[12].Value.ToString(), DGImport.Rows[CounterRow].Cells[13].Value.ToString(),
                            DGImport.Rows[CounterRow].Cells[14].Value.ToString(), DGImport.Rows[CounterRow].Cells[15].Value.ToString(),
                            DGImport.Rows[CounterRow].Cells[16].Value.ToString(), DGImport.Rows[CounterRow].Cells[17].Value.ToString(), DGImport.Rows[CounterRow].Cells[18].Value.ToString());

                            if (CounterRow > 0)
                            {
                                showprogress((Int32)Math.Round((double)(CounterRow * 100) / (DGImport.Rows.Count - 1)));
                            }
                            
                        }
                        MessageBox.Show("All of these data have been saved successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    
                }
                catch (Exception ex)
                {
                    
                    showprogress(0);
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }

            }
            else
            {
                MessageBox.Show("You haven't importing the excel file.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ImportMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Bgworker.IsBusy)
            {
                Bgworker.CancelAsync();
            }
        }

    }
}
