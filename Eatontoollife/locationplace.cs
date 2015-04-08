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
    public partial class locationplace : Form
    {
        public string location;
        public string pesan;
        int Animation;

        public locationplace()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Animation++ > 10) Animation = 0;

            if ((Animation % 2) == 0)
            {
                lbllocation.ForeColor = Color.Blue;
            }
            else
            {
                lbllocation.ForeColor = Color.Red;
            }
        }

        private void locationplace_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            lbllocation.Text = location;
            lblmesage.Text = pesan;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            this.Close();
        }
    }
}
