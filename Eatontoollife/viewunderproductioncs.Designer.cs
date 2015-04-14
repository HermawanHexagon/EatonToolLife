namespace Eatontoollife
{
    partial class viewunderproductioncs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(viewunderproductioncs));
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.DGview = new System.Windows.Forms.DataGridView();
            this.label32 = new System.Windows.Forms.Label();
            this.ProgressStatusUBP = new System.Windows.Forms.Label();
            this.E1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn26 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn22 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn25 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.E4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BGworker = new System.ComponentModel.BackgroundWorker();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox5.Controls.Add(this.DGview);
            this.groupBox5.Controls.Add(this.ProgressStatusUBP);
            this.groupBox5.Location = new System.Drawing.Point(12, 64);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(743, 417);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            // 
            // DGview
            // 
            this.DGview.AllowUserToAddRows = false;
            this.DGview.AllowUserToDeleteRows = false;
            this.DGview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.E1,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn26,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn22,
            this.dataGridViewTextBoxColumn24,
            this.dataGridViewTextBoxColumn25,
            this.E2,
            this.E3,
            this.E4});
            this.DGview.Location = new System.Drawing.Point(7, 19);
            this.DGview.Name = "DGview";
            this.DGview.ReadOnly = true;
            this.DGview.Size = new System.Drawing.Size(730, 383);
            this.DGview.TabIndex = 17;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(526, 41);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(229, 20);
            this.label32.TabIndex = 16;
            this.label32.Text = "Under Usage by Production";
            // 
            // ProgressStatusUBP
            // 
            this.ProgressStatusUBP.AutoSize = true;
            this.ProgressStatusUBP.Location = new System.Drawing.Point(845, 355);
            this.ProgressStatusUBP.Name = "ProgressStatusUBP";
            this.ProgressStatusUBP.Size = new System.Drawing.Size(21, 13);
            this.ProgressStatusUBP.TabIndex = 6;
            this.ProgressStatusUBP.Text = "0%";
            // 
            // E1
            // 
            this.E1.HeaderText = "Date Time Out";
            this.E1.Name = "E1";
            this.E1.ReadOnly = true;
            this.E1.Width = 150;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Part Number";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 150;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "Tooling Number";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Width = 150;
            // 
            // dataGridViewTextBoxColumn26
            // 
            this.dataGridViewTextBoxColumn26.HeaderText = "Status";
            this.dataGridViewTextBoxColumn26.Name = "dataGridViewTextBoxColumn26";
            this.dataGridViewTextBoxColumn26.ReadOnly = true;
            this.dataGridViewTextBoxColumn26.Width = 150;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Descriptions";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Width = 150;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "Customer";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Width = 150;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Storage Location";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Width = 150;
            // 
            // dataGridViewTextBoxColumn22
            // 
            this.dataGridViewTextBoxColumn22.HeaderText = "Actual Qty";
            this.dataGridViewTextBoxColumn22.Name = "dataGridViewTextBoxColumn22";
            this.dataGridViewTextBoxColumn22.ReadOnly = true;
            this.dataGridViewTextBoxColumn22.Width = 150;
            // 
            // dataGridViewTextBoxColumn24
            // 
            this.dataGridViewTextBoxColumn24.HeaderText = "Tooling Out Qty";
            this.dataGridViewTextBoxColumn24.Name = "dataGridViewTextBoxColumn24";
            this.dataGridViewTextBoxColumn24.ReadOnly = true;
            this.dataGridViewTextBoxColumn24.Width = 150;
            // 
            // dataGridViewTextBoxColumn25
            // 
            this.dataGridViewTextBoxColumn25.HeaderText = "WO Number";
            this.dataGridViewTextBoxColumn25.Name = "dataGridViewTextBoxColumn25";
            this.dataGridViewTextBoxColumn25.ReadOnly = true;
            this.dataGridViewTextBoxColumn25.Width = 150;
            // 
            // E2
            // 
            this.E2.HeaderText = "WO (Qty Diaphragm)";
            this.E2.Name = "E2";
            this.E2.ReadOnly = true;
            this.E2.Width = 150;
            // 
            // E3
            // 
            this.E3.HeaderText = "Note";
            this.E3.Name = "E3";
            this.E3.ReadOnly = true;
            this.E3.Width = 200;
            // 
            // E4
            // 
            this.E4.HeaderText = "Taken By";
            this.E4.Name = "E4";
            this.E4.ReadOnly = true;
            this.E4.Width = 150;
            // 
            // BGworker
            // 
            this.BGworker.WorkerReportsProgress = true;
            this.BGworker.WorkerSupportsCancellation = true;
            this.BGworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BGworker_DoWork);
            this.BGworker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BGworker_ProgressChanged);
            this.BGworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BGworker_RunWorkerCompleted);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Image = global::Eatontoollife.Properties.Resources.eatonpicedited;
            this.pictureBox3.Location = new System.Drawing.Point(19, 15);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(146, 43);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 14;
            this.pictureBox3.TabStop = false;
            // 
            // viewunderproductioncs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 493);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.groupBox5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "viewunderproductioncs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Under Usage By Production";
            this.Load += new System.EventHandler(this.viewunderproductioncs_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView DGview;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label ProgressStatusUBP;
        private System.Windows.Forms.DataGridViewTextBoxColumn E1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn26;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn22;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn24;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn25;
        private System.Windows.Forms.DataGridViewTextBoxColumn E2;
        private System.Windows.Forms.DataGridViewTextBoxColumn E3;
        private System.Windows.Forms.DataGridViewTextBoxColumn E4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.ComponentModel.BackgroundWorker BGworker;
    }
}