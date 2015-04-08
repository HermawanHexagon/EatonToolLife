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
    public partial class ReportFeedback : Form
    {
        IniFile Myini = new IniFile(@".\AppConfig.ini");
        string[] BufferIssue;
        string collect;

        public ReportFeedback()
        {
            InitializeComponent();
        }

        private void ReportFeedback_Load(object sender, EventArgs e)
        {
            FillUpcomboBox();
        }

        private void FillUpcomboBox()
        {
            CBfeedback.Items.Clear();

            BufferIssue = System.Text.RegularExpressions.Regex.Split(Myini.IniReadValue("Report_Feedback", "Issue"), ";");

            try
            {
                for (int i = 0; i < BufferIssue.Length; i++) { CBfeedback.Items.Add(BufferIssue[i].Trim()); }
                CBfeedback.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        private void AddorDeleteissue(string newissue, string choice)
        {
            int selectedCB = CBfeedback.SelectedIndex;
            collect = string.Empty;

            for (int select = 0; select < CBfeedback.Items.Count; select++)
            {
                
                if (choice == "ADD")
                {
                    CBfeedback.SelectedIndex = select; collect += CBfeedback.Text.Trim() + ";";
                }
                else if (choice == "DELETE")
                {
                    CBfeedback.SelectedIndex = select;

                    if (CBfeedback.SelectedIndex != selectedCB)
                    {
                         collect += CBfeedback.Text.Trim() + ";";
                    }
                }
            }

            if (choice == "ADD")
            {
                collect += newissue;
            }
            else
            {
                if(collect.Substring(collect.Length - 1) == ";") collect = collect.Remove(collect.Length - 1);
            }

            Myini.IniWriteValue("Report_Feedback", "Issue", collect);
            TxtAddfeedback.Clear();
            TxtAddfeedback.Focus();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {

            if (TxtAddfeedback.Text != "")
            {
                if(!TxtAddfeedback.Text.Contains(";"))
                {
                    AddorDeleteissue(TxtAddfeedback.Text.Trim(), "ADD");
                    FillUpcomboBox();
                }
                else
                {
                    MessageBox.Show("Please be removed this character semicolon ';'", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TxtAddfeedback.SelectAll();
                    TxtAddfeedback.Focus();
                }
            }
            else
            {
                MessageBox.Show("Cannot be Add the report feedback. please fill up the report", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtAddfeedback.Focus();
            }
        }

        private void Buttondelete_Click(object sender, EventArgs e)
        {
            if (CBfeedback.SelectedIndex > -1)
            {
                TxtAddfeedback.Clear();
                AddorDeleteissue("", "DELETE");
                FillUpcomboBox();
            }
        }

        private void TxtAddfeedback_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Invoke(new EventHandler(ButtonAdd_Click));
            }
        }
    }
}
