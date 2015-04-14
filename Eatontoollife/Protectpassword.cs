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
    public partial class Protectpassword : Form
    {
        public bool statuspassword = false;
        
        public Protectpassword()
        {
            InitializeComponent();
        }

        private void Txtfakepassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtpassword.Text == "engineering")
                {
                    statuspassword = true; this.Close();
                }
                else
                {
                    MessageBox.Show("Password wrong!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    statuspassword = false;
                }
            }
        }

        private void Txtfakepassword_TextChanged(object sender, EventArgs e)
        {
            txtpassword.Text = Txtfakepassword.Text;
        }

        private void txtpassword_TextChanged(object sender, EventArgs e)
        {
            Txtfakepassword.Text = txtpassword.Text;
        }
    }
}
