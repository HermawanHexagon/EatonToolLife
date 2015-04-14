namespace Eatontoollife
{
    partial class Protectpassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Protectpassword));
            this.label1 = new System.Windows.Forms.Label();
            this.Txtfakepassword = new System.Windows.Forms.TextBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Key in Password before Import an excel file.";
            // 
            // Txtfakepassword
            // 
            this.Txtfakepassword.Location = new System.Drawing.Point(15, 34);
            this.Txtfakepassword.Name = "Txtfakepassword";
            this.Txtfakepassword.PasswordChar = '●';
            this.Txtfakepassword.Size = new System.Drawing.Size(217, 20);
            this.Txtfakepassword.TabIndex = 0;
            this.Txtfakepassword.TextChanged += new System.EventHandler(this.Txtfakepassword_TextChanged);
            this.Txtfakepassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Txtfakepassword_KeyDown);
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(15, 87);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(217, 20);
            this.txtpassword.TabIndex = 2;
            this.txtpassword.TextChanged += new System.EventHandler(this.txtpassword_TextChanged);
            // 
            // Protectpassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 69);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.Txtfakepassword);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Protectpassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Txtfakepassword;
        private System.Windows.Forms.TextBox txtpassword;
    }
}