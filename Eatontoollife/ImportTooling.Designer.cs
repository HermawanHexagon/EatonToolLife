namespace Eatontoollife
{
    partial class ImportTooling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportTooling));
            this.Bgworker = new System.ComponentModel.BackgroundWorker();
            this.Progress_Status = new System.Windows.Forms.ProgressBar();
            this.ButtonImport = new System.Windows.Forms.Button();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.lblcount = new System.Windows.Forms.Label();
            this.DGImport = new System.Windows.Forms.DataGridView();
            this.PN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.plt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idmm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idcc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odmm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.odinc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tkn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tkns = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mtr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mtrlspc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.warh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lift = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGImport)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Bgworker
            // 
            this.Bgworker.WorkerReportsProgress = true;
            this.Bgworker.WorkerSupportsCancellation = true;
            this.Bgworker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Bgworker_DoWork);
            this.Bgworker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.Bgworker_ProgressChanged);
            this.Bgworker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Bgworker_RunWorkerCompleted);
            // 
            // Progress_Status
            // 
            this.Progress_Status.Location = new System.Drawing.Point(13, 391);
            this.Progress_Status.Name = "Progress_Status";
            this.Progress_Status.Size = new System.Drawing.Size(485, 10);
            this.Progress_Status.TabIndex = 8;
            // 
            // ButtonImport
            // 
            this.ButtonImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonImport.Location = new System.Drawing.Point(519, 364);
            this.ButtonImport.Name = "ButtonImport";
            this.ButtonImport.Size = new System.Drawing.Size(120, 41);
            this.ButtonImport.TabIndex = 7;
            this.ButtonImport.Text = "Import Master Data";
            this.ButtonImport.UseVisualStyleBackColor = true;
            this.ButtonImport.Click += new System.EventHandler(this.ButtonImport_Click);
            // 
            // ButtonSave
            // 
            this.ButtonSave.Enabled = false;
            this.ButtonSave.Location = new System.Drawing.Point(645, 364);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(120, 41);
            this.ButtonSave.TabIndex = 6;
            this.ButtonSave.Text = "Save into database";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // lblcount
            // 
            this.lblcount.AutoSize = true;
            this.lblcount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcount.ForeColor = System.Drawing.Color.Black;
            this.lblcount.Location = new System.Drawing.Point(10, 374);
            this.lblcount.Name = "lblcount";
            this.lblcount.Size = new System.Drawing.Size(0, 13);
            this.lblcount.TabIndex = 9;
            // 
            // DGImport
            // 
            this.DGImport.AllowUserToAddRows = false;
            this.DGImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGImport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PN,
            this.cst,
            this.plt,
            this.idmm,
            this.idcc,
            this.odmm,
            this.odinc,
            this.tkn,
            this.tkns,
            this.mtr,
            this.mtrlspc,
            this.warh,
            this.lift});
            this.DGImport.Location = new System.Drawing.Point(6, 19);
            this.DGImport.Name = "DGImport";
            this.DGImport.Size = new System.Drawing.Size(740, 269);
            this.DGImport.TabIndex = 0;
            // 
            // PN
            // 
            this.PN.HeaderText = "Part Number";
            this.PN.Name = "PN";
            this.PN.Width = 180;
            // 
            // cst
            // 
            this.cst.HeaderText = "Customer";
            this.cst.Name = "cst";
            // 
            // plt
            // 
            this.plt.HeaderText = "Tooling Number";
            this.plt.Name = "plt";
            this.plt.Width = 265;
            // 
            // idmm
            // 
            this.idmm.HeaderText = "Descriptions";
            this.idmm.Name = "idmm";
            this.idmm.Width = 200;
            // 
            // idcc
            // 
            this.idcc.HeaderText = "Location Storage";
            this.idcc.Name = "idcc";
            this.idcc.Width = 120;
            // 
            // odmm
            // 
            this.odmm.HeaderText = "Quantity";
            this.odmm.Name = "odmm";
            this.odmm.Width = 70;
            // 
            // odinc
            // 
            this.odinc.HeaderText = "Tools Drawing";
            this.odinc.Name = "odinc";
            this.odinc.Width = 260;
            // 
            // tkn
            // 
            this.tkn.HeaderText = "Revisions";
            this.tkn.Name = "tkn";
            this.tkn.Width = 70;
            // 
            // tkns
            // 
            this.tkns.HeaderText = "Remarks";
            this.tkns.Name = "tkns";
            this.tkns.Width = 150;
            // 
            // mtr
            // 
            this.mtr.HeaderText = "Status";
            this.mtr.Name = "mtr";
            this.mtr.Width = 130;
            // 
            // mtrlspc
            // 
            this.mtrlspc.HeaderText = "Life Time Period";
            this.mtrlspc.Name = "mtrlspc";
            this.mtrlspc.Width = 120;
            // 
            // warh
            // 
            this.warh.HeaderText = "Warning Value";
            this.warh.Name = "warh";
            this.warh.Width = 120;
            // 
            // lift
            // 
            this.lift.HeaderText = "Life Time Usage";
            this.lift.Name = "lift";
            this.lift.Width = 120;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DGImport);
            this.groupBox1.Location = new System.Drawing.Point(12, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(752, 298);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bernard MT Condensed", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(11, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 38);
            this.label1.TabIndex = 10;
            this.label1.Text = "Import List Tooling Number.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::Eatontoollife.Properties.Resources.eatonpicedited;
            this.pictureBox1.Location = new System.Drawing.Point(665, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 33);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // ImportTooling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 417);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Progress_Status);
            this.Controls.Add(this.ButtonImport);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.lblcount);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportTooling";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import From Excel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImportTooling_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.DGImport)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker Bgworker;
        private System.Windows.Forms.ProgressBar Progress_Status;
        private System.Windows.Forms.Button ButtonImport;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Label lblcount;
        private System.Windows.Forms.DataGridView DGImport;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PN;
        private System.Windows.Forms.DataGridViewTextBoxColumn cst;
        private System.Windows.Forms.DataGridViewTextBoxColumn plt;
        private System.Windows.Forms.DataGridViewTextBoxColumn idmm;
        private System.Windows.Forms.DataGridViewTextBoxColumn idcc;
        private System.Windows.Forms.DataGridViewTextBoxColumn odmm;
        private System.Windows.Forms.DataGridViewTextBoxColumn odinc;
        private System.Windows.Forms.DataGridViewTextBoxColumn tkn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tkns;
        private System.Windows.Forms.DataGridViewTextBoxColumn mtr;
        private System.Windows.Forms.DataGridViewTextBoxColumn mtrlspc;
        private System.Windows.Forms.DataGridViewTextBoxColumn warh;
        private System.Windows.Forms.DataGridViewTextBoxColumn lift;

    }
}