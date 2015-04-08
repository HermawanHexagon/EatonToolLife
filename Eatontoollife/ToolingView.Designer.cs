namespace Eatontoollife
{
    partial class ToolingView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolingView));
            this.lblpartnumber = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DGdatabaseTooling = new System.Windows.Forms.DataGridView();
            this.PN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.keluarga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sts = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Keterangan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ButtonNext = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Cbcount = new System.Windows.Forms.ComboBox();
            this.txttimeout = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txttakenby = new System.Windows.Forms.TextBox();
            this.txttoolsname = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtactualtoolqty = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtstoragelocation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Textpartnmb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CBtooling = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Txtqtyout = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Txtnote = new System.Windows.Forms.TextBox();
            this.txtWoquantity = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TxtWonumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGdatabaseTooling)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblpartnumber
            // 
            this.lblpartnumber.AutoSize = true;
            this.lblpartnumber.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpartnumber.ForeColor = System.Drawing.Color.Maroon;
            this.lblpartnumber.Location = new System.Drawing.Point(12, 21);
            this.lblpartnumber.Name = "lblpartnumber";
            this.lblpartnumber.Size = new System.Drawing.Size(112, 19);
            this.lblpartnumber.TabIndex = 0;
            this.lblpartnumber.Text = "Part Number :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DGdatabaseTooling);
            this.groupBox1.Location = new System.Drawing.Point(16, 44);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(650, 242);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // DGdatabaseTooling
            // 
            this.DGdatabaseTooling.AllowUserToAddRows = false;
            this.DGdatabaseTooling.AllowUserToDeleteRows = false;
            this.DGdatabaseTooling.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGdatabaseTooling.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PN,
            this.TN,
            this.desc,
            this.keluarga,
            this.sts,
            this.C1,
            this.C2,
            this.Keterangan});
            this.DGdatabaseTooling.Location = new System.Drawing.Point(10, 19);
            this.DGdatabaseTooling.Name = "DGdatabaseTooling";
            this.DGdatabaseTooling.ReadOnly = true;
            this.DGdatabaseTooling.Size = new System.Drawing.Size(625, 211);
            this.DGdatabaseTooling.TabIndex = 0;
            // 
            // PN
            // 
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.PN.DefaultCellStyle = dataGridViewCellStyle1;
            this.PN.HeaderText = "Part Number";
            this.PN.Name = "PN";
            this.PN.ReadOnly = true;
            this.PN.Width = 150;
            // 
            // TN
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.TN.DefaultCellStyle = dataGridViewCellStyle2;
            this.TN.HeaderText = "Tooling Number";
            this.TN.Name = "TN";
            this.TN.ReadOnly = true;
            this.TN.Width = 190;
            // 
            // desc
            // 
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.desc.DefaultCellStyle = dataGridViewCellStyle3;
            this.desc.HeaderText = "Descriptions";
            this.desc.Name = "desc";
            this.desc.ReadOnly = true;
            this.desc.Width = 260;
            // 
            // keluarga
            // 
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.keluarga.DefaultCellStyle = dataGridViewCellStyle4;
            this.keluarga.HeaderText = "Family";
            this.keluarga.Name = "keluarga";
            this.keluarga.ReadOnly = true;
            this.keluarga.Width = 110;
            // 
            // sts
            // 
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.sts.DefaultCellStyle = dataGridViewCellStyle5;
            this.sts.HeaderText = "Status";
            this.sts.Name = "sts";
            this.sts.ReadOnly = true;
            this.sts.Width = 150;
            // 
            // C1
            // 
            this.C1.HeaderText = "Life Time Period";
            this.C1.Name = "C1";
            this.C1.ReadOnly = true;
            this.C1.Width = 110;
            // 
            // C2
            // 
            this.C2.HeaderText = "Life Time Usage";
            this.C2.Name = "C2";
            this.C2.ReadOnly = true;
            this.C2.Width = 110;
            // 
            // Keterangan
            // 
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.Keterangan.DefaultCellStyle = dataGridViewCellStyle6;
            this.Keterangan.HeaderText = "Remarks";
            this.Keterangan.Name = "Keterangan";
            this.Keterangan.ReadOnly = true;
            this.Keterangan.Width = 200;
            // 
            // ButtonNext
            // 
            this.ButtonNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonNext.Location = new System.Drawing.Point(393, 496);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(113, 38);
            this.ButtonNext.TabIndex = 3;
            this.ButtonNext.Text = "Procced";
            this.ButtonNext.UseVisualStyleBackColor = true;
            this.ButtonNext.Click += new System.EventHandler(this.ButtonNext_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonCancel.Location = new System.Drawing.Point(512, 496);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(113, 38);
            this.ButtonCancel.TabIndex = 4;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Cbcount);
            this.groupBox2.Controls.Add(this.txttimeout);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txttakenby);
            this.groupBox2.Controls.Add(this.txttoolsname);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtactualtoolqty);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtstoragelocation);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.Textpartnmb);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.CBtooling);
            this.groupBox2.Location = new System.Drawing.Point(16, 292);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 242);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Parameter Value";
            // 
            // Cbcount
            // 
            this.Cbcount.FormattingEnabled = true;
            this.Cbcount.Location = new System.Drawing.Point(231, 43);
            this.Cbcount.Name = "Cbcount";
            this.Cbcount.Size = new System.Drawing.Size(30, 21);
            this.Cbcount.TabIndex = 10;
            this.Cbcount.Visible = false;
            // 
            // txttimeout
            // 
            this.txttimeout.Location = new System.Drawing.Point(170, 185);
            this.txttimeout.Name = "txttimeout";
            this.txttimeout.ReadOnly = true;
            this.txttimeout.Size = new System.Drawing.Size(130, 20);
            this.txttimeout.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(167, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Date/Time Out.";
            // 
            // txttakenby
            // 
            this.txttakenby.Location = new System.Drawing.Point(170, 138);
            this.txttakenby.Name = "txttakenby";
            this.txttakenby.ReadOnly = true;
            this.txttakenby.Size = new System.Drawing.Size(130, 20);
            this.txttakenby.TabIndex = 7;
            // 
            // txttoolsname
            // 
            this.txttoolsname.Location = new System.Drawing.Point(170, 90);
            this.txttoolsname.Name = "txttoolsname";
            this.txttoolsname.ReadOnly = true;
            this.txttoolsname.Size = new System.Drawing.Size(130, 20);
            this.txttoolsname.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(167, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Taken by.";
            // 
            // txtactualtoolqty
            // 
            this.txtactualtoolqty.Location = new System.Drawing.Point(23, 185);
            this.txtactualtoolqty.Name = "txtactualtoolqty";
            this.txtactualtoolqty.ReadOnly = true;
            this.txtactualtoolqty.Size = new System.Drawing.Size(130, 20);
            this.txtactualtoolqty.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(167, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Tools Name.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(20, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Actual Tool Quantity.";
            // 
            // txtstoragelocation
            // 
            this.txtstoragelocation.Location = new System.Drawing.Point(23, 137);
            this.txtstoragelocation.Name = "txtstoragelocation";
            this.txtstoragelocation.ReadOnly = true;
            this.txtstoragelocation.Size = new System.Drawing.Size(130, 20);
            this.txtstoragelocation.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(20, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Storage location.";
            // 
            // Textpartnmb
            // 
            this.Textpartnmb.Location = new System.Drawing.Point(23, 90);
            this.Textpartnmb.Name = "Textpartnmb";
            this.Textpartnmb.ReadOnly = true;
            this.Textpartnmb.Size = new System.Drawing.Size(130, 20);
            this.Textpartnmb.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(20, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Part Number.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(20, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please choose tooling number below.";
            // 
            // CBtooling
            // 
            this.CBtooling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBtooling.FormattingEnabled = true;
            this.CBtooling.Location = new System.Drawing.Point(23, 43);
            this.CBtooling.Name = "CBtooling";
            this.CBtooling.Size = new System.Drawing.Size(199, 21);
            this.CBtooling.TabIndex = 0;
            this.CBtooling.SelectedIndexChanged += new System.EventHandler(this.CBtooling_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Txtqtyout);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.Txtnote);
            this.groupBox3.Controls.Add(this.txtWoquantity);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.TxtWonumber);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Location = new System.Drawing.Point(347, 292);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(319, 194);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // Txtqtyout
            // 
            this.Txtqtyout.Location = new System.Drawing.Point(141, 23);
            this.Txtqtyout.Name = "Txtqtyout";
            this.Txtqtyout.ReadOnly = true;
            this.Txtqtyout.Size = new System.Drawing.Size(163, 20);
            this.Txtqtyout.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(43, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "● Note.";
            // 
            // Txtnote
            // 
            this.Txtnote.Location = new System.Drawing.Point(141, 105);
            this.Txtnote.Multiline = true;
            this.Txtnote.Name = "Txtnote";
            this.Txtnote.Size = new System.Drawing.Size(163, 80);
            this.Txtnote.TabIndex = 6;
            // 
            // txtWoquantity
            // 
            this.txtWoquantity.Location = new System.Drawing.Point(141, 79);
            this.txtWoquantity.Name = "txtWoquantity";
            this.txtWoquantity.Size = new System.Drawing.Size(163, 20);
            this.txtWoquantity.TabIndex = 5;
            this.txtWoquantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWoquantity_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "● Quantity Order (WO Qty)";
            // 
            // TxtWonumber
            // 
            this.TxtWonumber.Location = new System.Drawing.Point(141, 53);
            this.TxtWonumber.Name = "TxtWonumber";
            this.TxtWonumber.Size = new System.Drawing.Size(163, 20);
            this.TxtWonumber.TabIndex = 3;
            this.TxtWonumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtWonumber_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "● WO Number.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "● Tooling Quantity Out.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::Eatontoollife.Properties.Resources.eatonpicedited;
            this.pictureBox1.Location = new System.Drawing.Point(566, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 33);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // ToolingView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 549);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonNext);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblpartnumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ToolingView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Transaction";
            this.Load += new System.EventHandler(this.ToolingView_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGdatabaseTooling)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblpartnumber;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DGdatabaseTooling;
        private System.Windows.Forms.Button ButtonNext;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox Textpartnmb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CBtooling;
        private System.Windows.Forms.TextBox txttimeout;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txttakenby;
        private System.Windows.Forms.TextBox txttoolsname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtactualtoolqty;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtstoragelocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Txtnote;
        private System.Windows.Forms.TextBox txtWoquantity;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TxtWonumber;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox Txtqtyout;
        private System.Windows.Forms.ComboBox Cbcount;
        private System.Windows.Forms.DataGridViewTextBoxColumn PN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TN;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn keluarga;
        private System.Windows.Forms.DataGridViewTextBoxColumn sts;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1;
        private System.Windows.Forms.DataGridViewTextBoxColumn C2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Keterangan;
    }
}