namespace Eatontoollife
{
    partial class Toolingnumberinput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Toolingnumberinput));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPN = new System.Windows.Forms.Label();
            this.Cbpartnumber = new System.Windows.Forms.ComboBox();
            this.Lblnotif = new System.Windows.Forms.Label();
            this.Texttoolingnumber = new System.Windows.Forms.TextBox();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPN);
            this.groupBox1.Controls.Add(this.Cbpartnumber);
            this.groupBox1.Controls.Add(this.Lblnotif);
            this.groupBox1.Controls.Add(this.Texttoolingnumber);
            this.groupBox1.Location = new System.Drawing.Point(12, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 46);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // lblPN
            // 
            this.lblPN.AutoSize = true;
            this.lblPN.Location = new System.Drawing.Point(6, 78);
            this.lblPN.Name = "lblPN";
            this.lblPN.Size = new System.Drawing.Size(72, 13);
            this.lblPN.TabIndex = 3;
            this.lblPN.Text = "Part Number :";
            this.lblPN.Visible = false;
            // 
            // Cbpartnumber
            // 
            this.Cbpartnumber.FormattingEnabled = true;
            this.Cbpartnumber.Location = new System.Drawing.Point(84, 75);
            this.Cbpartnumber.Name = "Cbpartnumber";
            this.Cbpartnumber.Size = new System.Drawing.Size(166, 21);
            this.Cbpartnumber.TabIndex = 2;
            this.Cbpartnumber.Visible = false;
            // 
            // Lblnotif
            // 
            this.Lblnotif.AutoSize = true;
            this.Lblnotif.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lblnotif.ForeColor = System.Drawing.Color.Black;
            this.Lblnotif.Location = new System.Drawing.Point(6, 48);
            this.Lblnotif.Name = "Lblnotif";
            this.Lblnotif.Size = new System.Drawing.Size(244, 13);
            this.Lblnotif.TabIndex = 1;
            this.Lblnotif.Text = "Please select part number before press button OK.";
            this.Lblnotif.Visible = false;
            // 
            // Texttoolingnumber
            // 
            this.Texttoolingnumber.Location = new System.Drawing.Point(6, 16);
            this.Texttoolingnumber.Name = "Texttoolingnumber";
            this.Texttoolingnumber.Size = new System.Drawing.Size(411, 20);
            this.Texttoolingnumber.TabIndex = 0;
            this.Texttoolingnumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Texttoolingnumber_KeyDown);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(360, 86);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 8;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(279, 86);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 7;
            this.ButtonOk.Text = "OK";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(423, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Please key in tooling number and ensure that tooling number is already exist in d" +
    "atabase.";
            // 
            // Toolingnumberinput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 120);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Toolingnumberinput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Tooling Number";
            this.Load += new System.EventHandler(this.Toolingnumberinput_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.TextBox Texttoolingnumber;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPN;
        private System.Windows.Forms.Label Lblnotif;
        public System.Windows.Forms.ComboBox Cbpartnumber;
    }
}