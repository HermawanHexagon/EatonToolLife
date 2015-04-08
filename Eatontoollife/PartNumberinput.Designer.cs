namespace Eatontoollife
{
    partial class PartNumberinput
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartNumberinput));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RBtoolingnumber = new System.Windows.Forms.RadioButton();
            this.TextPartnumber = new System.Windows.Forms.TextBox();
            this.RBpartnumber = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RBtoolingnumber);
            this.groupBox1.Controls.Add(this.TextPartnumber);
            this.groupBox1.Controls.Add(this.RBpartnumber);
            this.groupBox1.Location = new System.Drawing.Point(12, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(392, 78);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // RBtoolingnumber
            // 
            this.RBtoolingnumber.AutoSize = true;
            this.RBtoolingnumber.Location = new System.Drawing.Point(111, 18);
            this.RBtoolingnumber.Name = "RBtoolingnumber";
            this.RBtoolingnumber.Size = new System.Drawing.Size(100, 17);
            this.RBtoolingnumber.TabIndex = 6;
            this.RBtoolingnumber.Text = "Tooling Number";
            this.RBtoolingnumber.UseVisualStyleBackColor = true;
            this.RBtoolingnumber.CheckedChanged += new System.EventHandler(this.RBtoolingnumber_CheckedChanged);
            // 
            // TextPartnumber
            // 
            this.TextPartnumber.Location = new System.Drawing.Point(6, 45);
            this.TextPartnumber.Name = "TextPartnumber";
            this.TextPartnumber.Size = new System.Drawing.Size(380, 20);
            this.TextPartnumber.TabIndex = 0;
            this.TextPartnumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextPartnumber_KeyDown);
            // 
            // RBpartnumber
            // 
            this.RBpartnumber.AutoSize = true;
            this.RBpartnumber.Checked = true;
            this.RBpartnumber.Location = new System.Drawing.Point(9, 18);
            this.RBpartnumber.Name = "RBpartnumber";
            this.RBpartnumber.Size = new System.Drawing.Size(84, 17);
            this.RBpartnumber.TabIndex = 5;
            this.RBpartnumber.TabStop = true;
            this.RBpartnumber.Text = "Part Number";
            this.RBpartnumber.UseVisualStyleBackColor = true;
            this.RBpartnumber.CheckedChanged += new System.EventHandler(this.RBpartnumber_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(397, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please key in part number and ensure that part number is already exist in databas" +
    "e.";
            // 
            // ButtonOk
            // 
            this.ButtonOk.Location = new System.Drawing.Point(247, 118);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ButtonOk.TabIndex = 3;
            this.ButtonOk.Text = "OK";
            this.ButtonOk.UseVisualStyleBackColor = true;
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(329, 118);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 4;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // PartNumberinput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 150);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PartNumberinput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Part Number";
            this.Load += new System.EventHandler(this.PartNumberinput_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        public System.Windows.Forms.TextBox TextPartnumber;
        private System.Windows.Forms.RadioButton RBtoolingnumber;
        private System.Windows.Forms.RadioButton RBpartnumber;

    }
}