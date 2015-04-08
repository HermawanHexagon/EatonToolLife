namespace Eatontoollife
{
    partial class Newcustomer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Newcustomer));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Buttondelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.CBfeedback = new System.Windows.Forms.ComboBox();
            this.TxtAddfeedback = new System.Windows.Forms.TextBox();
            this.ButtonAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Buttondelete);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.CBfeedback);
            this.groupBox1.Controls.Add(this.TxtAddfeedback);
            this.groupBox1.Controls.Add(this.ButtonAdd);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(370, 134);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add New Customer";
            // 
            // Buttondelete
            // 
            this.Buttondelete.Location = new System.Drawing.Point(267, 93);
            this.Buttondelete.Name = "Buttondelete";
            this.Buttondelete.Size = new System.Drawing.Size(75, 23);
            this.Buttondelete.TabIndex = 10;
            this.Buttondelete.Text = "Delete";
            this.Buttondelete.UseVisualStyleBackColor = true;
            this.Buttondelete.Click += new System.EventHandler(this.Buttondelete_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "List of customer";
            // 
            // CBfeedback
            // 
            this.CBfeedback.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBfeedback.FormattingEnabled = true;
            this.CBfeedback.Location = new System.Drawing.Point(21, 95);
            this.CBfeedback.Name = "CBfeedback";
            this.CBfeedback.Size = new System.Drawing.Size(229, 21);
            this.CBfeedback.TabIndex = 8;
            // 
            // TxtAddfeedback
            // 
            this.TxtAddfeedback.Location = new System.Drawing.Point(21, 48);
            this.TxtAddfeedback.Name = "TxtAddfeedback";
            this.TxtAddfeedback.Size = new System.Drawing.Size(229, 20);
            this.TxtAddfeedback.TabIndex = 0;
            // 
            // ButtonAdd
            // 
            this.ButtonAdd.Location = new System.Drawing.Point(267, 46);
            this.ButtonAdd.Name = "ButtonAdd";
            this.ButtonAdd.Size = new System.Drawing.Size(75, 23);
            this.ButtonAdd.TabIndex = 1;
            this.ButtonAdd.Text = "Add";
            this.ButtonAdd.UseVisualStyleBackColor = true;
            this.ButtonAdd.Click += new System.EventHandler(this.ButtonAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Add Customer Here";
            // 
            // Newcustomer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 161);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Newcustomer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New customer";
            this.Load += new System.EventHandler(this.Newcustomer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Buttondelete;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CBfeedback;
        private System.Windows.Forms.TextBox TxtAddfeedback;
        private System.Windows.Forms.Button ButtonAdd;
        private System.Windows.Forms.Label label1;
    }
}