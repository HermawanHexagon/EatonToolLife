namespace Eatontoollife
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.CbAuthorize = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextShowPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CBpassword = new System.Windows.Forms.CheckBox();
            this.ButtonExit = new System.Windows.Forms.Button();
            this.ButtonLogin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextPassword = new System.Windows.Forms.TextBox();
            this.TextUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // CbAuthorize
            // 
            this.CbAuthorize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbAuthorize.FormattingEnabled = true;
            this.CbAuthorize.Items.AddRange(new object[] {
            "Administrator",
            "User"});
            this.CbAuthorize.Location = new System.Drawing.Point(80, 28);
            this.CbAuthorize.Name = "CbAuthorize";
            this.CbAuthorize.Size = new System.Drawing.Size(129, 21);
            this.CbAuthorize.TabIndex = 9;
            this.CbAuthorize.SelectedIndexChanged += new System.EventHandler(this.CbAuthorize_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TextShowPassword);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.CBpassword);
            this.groupBox1.Controls.Add(this.ButtonExit);
            this.groupBox1.Controls.Add(this.ButtonLogin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TextPassword);
            this.groupBox1.Controls.Add(this.TextUsername);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.CbAuthorize);
            this.groupBox1.Location = new System.Drawing.Point(13, 72);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(225, 224);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // TextShowPassword
            // 
            this.TextShowPassword.Location = new System.Drawing.Point(80, 87);
            this.TextShowPassword.Name = "TextShowPassword";
            this.TextShowPassword.Size = new System.Drawing.Size(129, 20);
            this.TextShowPassword.TabIndex = 10;
            this.TextShowPassword.Visible = false;
            this.TextShowPassword.TextChanged += new System.EventHandler(this.TextShowPassword_TextChanged);
            this.TextShowPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextShowPassword_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(211, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "__________________________________";
            // 
            // CBpassword
            // 
            this.CBpassword.AutoSize = true;
            this.CBpassword.Location = new System.Drawing.Point(83, 119);
            this.CBpassword.Name = "CBpassword";
            this.CBpassword.Size = new System.Drawing.Size(102, 17);
            this.CBpassword.TabIndex = 8;
            this.CBpassword.Text = "Show Password";
            this.CBpassword.UseVisualStyleBackColor = true;
            this.CBpassword.CheckedChanged += new System.EventHandler(this.CBpassword_CheckedChanged);
            // 
            // ButtonExit
            // 
            this.ButtonExit.Location = new System.Drawing.Point(122, 169);
            this.ButtonExit.Name = "ButtonExit";
            this.ButtonExit.Size = new System.Drawing.Size(82, 37);
            this.ButtonExit.TabIndex = 7;
            this.ButtonExit.Text = "Exit";
            this.ButtonExit.UseVisualStyleBackColor = true;
            this.ButtonExit.Click += new System.EventHandler(this.ButtonExit_Click);
            // 
            // ButtonLogin
            // 
            this.ButtonLogin.Location = new System.Drawing.Point(18, 169);
            this.ButtonLogin.Name = "ButtonLogin";
            this.ButtonLogin.Size = new System.Drawing.Size(85, 37);
            this.ButtonLogin.TabIndex = 6;
            this.ButtonLogin.Text = "Login";
            this.ButtonLogin.UseVisualStyleBackColor = true;
            this.ButtonLogin.Click += new System.EventHandler(this.ButtonLogin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Password";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "User name";
            // 
            // TextPassword
            // 
            this.TextPassword.Location = new System.Drawing.Point(80, 87);
            this.TextPassword.Name = "TextPassword";
            this.TextPassword.PasswordChar = '●';
            this.TextPassword.Size = new System.Drawing.Size(129, 20);
            this.TextPassword.TabIndex = 3;
            this.TextPassword.TextChanged += new System.EventHandler(this.TextPassword_TextChanged);
            this.TextPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextPassword_KeyDown);
            // 
            // TextUsername
            // 
            this.TextUsername.Location = new System.Drawing.Point(80, 58);
            this.TextUsername.Name = "TextUsername";
            this.TextUsername.Size = new System.Drawing.Size(129, 20);
            this.TextUsername.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Authorize";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Eatontoollife.Properties.Resources.login;
            this.pictureBox1.Location = new System.Drawing.Point(36, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(181, 54);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(250, 308);
            this.ControlBox = false;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Login_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CBpassword;
        private System.Windows.Forms.Button ButtonExit;
        private System.Windows.Forms.Button ButtonLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox TextPassword;
        public System.Windows.Forms.TextBox TextUsername;
        public System.Windows.Forms.TextBox TextShowPassword;
        public System.Windows.Forms.ComboBox CbAuthorize;
    }
}