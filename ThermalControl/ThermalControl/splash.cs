using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ThermalControl
{
    public partial class splash : Form
    {
        public splash()
        {
            InitializeComponent();

            timer1.Enabled = true;
        }

        private void splash_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Main mainForm = new Main();

            mainForm.Show();
            timer1.Enabled = false;
            this.Dispose(false);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
