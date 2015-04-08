namespace Eatontoollife
{
    partial class Emailform
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Emailform));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Buttonattached = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtattach = new System.Windows.Forms.TextBox();
            this.Buttonemail = new System.Windows.Forms.Button();
            this.Buttonmessage = new System.Windows.Forms.Button();
            this.txtmessage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Buttonsubject = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Txtsubject = new System.Windows.Forms.TextBox();
            this.txtlistsendemail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtEmailsender = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Txtpasswordfake = new System.Windows.Forms.TextBox();
            this.Buttonsavesender = new System.Windows.Forms.Button();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.CheckBoxShow = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CheckBoxShow);
            this.groupBox2.Controls.Add(this.TxtPassword);
            this.groupBox2.Controls.Add(this.Buttonsavesender);
            this.groupBox2.Controls.Add(this.Txtpasswordfake);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.TxtEmailsender);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Buttonattached);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtattach);
            this.groupBox2.Controls.Add(this.Buttonemail);
            this.groupBox2.Controls.Add(this.Buttonmessage);
            this.groupBox2.Controls.Add(this.txtmessage);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.Buttonsubject);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.Txtsubject);
            this.groupBox2.Controls.Add(this.txtlistsendemail);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 444);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Setting Sender, Address, Subject, Message";
            // 
            // Buttonattached
            // 
            this.Buttonattached.Location = new System.Drawing.Point(278, 409);
            this.Buttonattached.Name = "Buttonattached";
            this.Buttonattached.Size = new System.Drawing.Size(82, 23);
            this.Buttonattached.TabIndex = 59;
            this.Buttonattached.Text = "Browse";
            this.Buttonattached.UseVisualStyleBackColor = true;
            this.Buttonattached.Click += new System.EventHandler(this.Buttonattached_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(15, 414);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 58;
            this.label1.Text = "Attachment";
            // 
            // txtattach
            // 
            this.txtattach.Enabled = false;
            this.txtattach.Location = new System.Drawing.Point(80, 411);
            this.txtattach.Name = "txtattach";
            this.txtattach.Size = new System.Drawing.Size(192, 20);
            this.txtattach.TabIndex = 57;
            // 
            // Buttonemail
            // 
            this.Buttonemail.Location = new System.Drawing.Point(80, 219);
            this.Buttonemail.Name = "Buttonemail";
            this.Buttonemail.Size = new System.Drawing.Size(118, 23);
            this.Buttonemail.TabIndex = 1;
            this.Buttonemail.Text = "Edit Email Address";
            this.Buttonemail.UseVisualStyleBackColor = true;
            this.Buttonemail.Click += new System.EventHandler(this.Buttonemail_Click);
            // 
            // Buttonmessage
            // 
            this.Buttonmessage.Location = new System.Drawing.Point(80, 376);
            this.Buttonmessage.Name = "Buttonmessage";
            this.Buttonmessage.Size = new System.Drawing.Size(118, 23);
            this.Buttonmessage.TabIndex = 7;
            this.Buttonmessage.Text = "Edit Message";
            this.Buttonmessage.UseVisualStyleBackColor = true;
            this.Buttonmessage.Click += new System.EventHandler(this.Buttonmessage_Click);
            // 
            // txtmessage
            // 
            this.txtmessage.Enabled = false;
            this.txtmessage.Location = new System.Drawing.Point(80, 281);
            this.txtmessage.Multiline = true;
            this.txtmessage.Name = "txtmessage";
            this.txtmessage.Size = new System.Drawing.Size(280, 89);
            this.txtmessage.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(15, 284);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Message";
            // 
            // Buttonsubject
            // 
            this.Buttonsubject.Location = new System.Drawing.Point(278, 246);
            this.Buttonsubject.Name = "Buttonsubject";
            this.Buttonsubject.Size = new System.Drawing.Size(82, 23);
            this.Buttonsubject.TabIndex = 4;
            this.Buttonsubject.Text = "Edit Subject";
            this.Buttonsubject.UseVisualStyleBackColor = true;
            this.Buttonsubject.Click += new System.EventHandler(this.Buttonsubject_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(15, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Subject";
            // 
            // Txtsubject
            // 
            this.Txtsubject.Enabled = false;
            this.Txtsubject.Location = new System.Drawing.Point(80, 248);
            this.Txtsubject.Name = "Txtsubject";
            this.Txtsubject.Size = new System.Drawing.Size(192, 20);
            this.Txtsubject.TabIndex = 2;
            // 
            // txtlistsendemail
            // 
            this.txtlistsendemail.Location = new System.Drawing.Point(80, 153);
            this.txtlistsendemail.Multiline = true;
            this.txtlistsendemail.Name = "txtlistsendemail";
            this.txtlistsendemail.ReadOnly = true;
            this.txtlistsendemail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtlistsendemail.Size = new System.Drawing.Size(281, 60);
            this.txtlistsendemail.TabIndex = 56;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(16, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Send To";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(15, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 60;
            this.label2.Text = "From";
            // 
            // TxtEmailsender
            // 
            this.TxtEmailsender.Enabled = false;
            this.TxtEmailsender.Location = new System.Drawing.Point(80, 42);
            this.TxtEmailsender.Name = "TxtEmailsender";
            this.TxtEmailsender.Size = new System.Drawing.Size(281, 20);
            this.TxtEmailsender.TabIndex = 61;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(343, 13);
            this.label6.TabIndex = 62;
            this.label6.Text = "________________________________________________________";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(15, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 63;
            this.label7.Text = "Password";
            // 
            // Txtpasswordfake
            // 
            this.Txtpasswordfake.Enabled = false;
            this.Txtpasswordfake.Location = new System.Drawing.Point(80, 72);
            this.Txtpasswordfake.Name = "Txtpasswordfake";
            this.Txtpasswordfake.PasswordChar = '●';
            this.Txtpasswordfake.Size = new System.Drawing.Size(281, 20);
            this.Txtpasswordfake.TabIndex = 64;
            this.Txtpasswordfake.TextChanged += new System.EventHandler(this.Txtpasswordfake_TextChanged);
            // 
            // Buttonsavesender
            // 
            this.Buttonsavesender.Location = new System.Drawing.Point(278, 102);
            this.Buttonsavesender.Name = "Buttonsavesender";
            this.Buttonsavesender.Size = new System.Drawing.Size(82, 23);
            this.Buttonsavesender.TabIndex = 65;
            this.Buttonsavesender.Text = "Edit Sender";
            this.Buttonsavesender.UseVisualStyleBackColor = true;
            this.Buttonsavesender.Click += new System.EventHandler(this.Buttonsavesender_Click);
            // 
            // TxtPassword
            // 
            this.TxtPassword.Enabled = false;
            this.TxtPassword.Location = new System.Drawing.Point(80, 72);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(281, 20);
            this.TxtPassword.TabIndex = 66;
            this.TxtPassword.Visible = false;
            this.TxtPassword.TextChanged += new System.EventHandler(this.TxtPassword_TextChanged);
            // 
            // CheckBoxShow
            // 
            this.CheckBoxShow.AutoSize = true;
            this.CheckBoxShow.Enabled = false;
            this.CheckBoxShow.Location = new System.Drawing.Point(80, 106);
            this.CheckBoxShow.Name = "CheckBoxShow";
            this.CheckBoxShow.Size = new System.Drawing.Size(102, 17);
            this.CheckBoxShow.TabIndex = 67;
            this.CheckBoxShow.Text = "Show Password";
            this.CheckBoxShow.UseVisualStyleBackColor = true;
            this.CheckBoxShow.CheckedChanged += new System.EventHandler(this.CheckBoxShow_CheckedChanged);
            // 
            // Emailform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 468);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Emailform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Setting";
            this.Load += new System.EventHandler(this.Emailform_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button Buttonmessage;
        private System.Windows.Forms.TextBox txtmessage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Buttonsubject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Txtsubject;
        private System.Windows.Forms.TextBox txtlistsendemail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Buttonemail;
        private System.Windows.Forms.Button Buttonattached;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtattach;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.Button Buttonsavesender;
        private System.Windows.Forms.TextBox Txtpasswordfake;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TxtEmailsender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CheckBoxShow;
    }
}