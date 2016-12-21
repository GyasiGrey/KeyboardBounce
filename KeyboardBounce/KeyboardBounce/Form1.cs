using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyboardBounce
{
    public partial class Form1 : Form
    {
        Bounce bouncer;

        public Form1()
        {
            InitializeComponent();

            bouncer = new Bounce();
        }

        private void TickTimer_Tick(object sender, EventArgs e)
        {
            bouncer.Tick();
        }

        private void StartStop_Click(object sender, EventArgs e)
        {
            if(TickTimer.Enabled)
            {
                TickTimer.Enabled = false;
                StartStop.Text = "Start";
            }
            else
            {
                TickTimer.Enabled = true;
                StartStop.Text = "Stop";
            }
        }
    }
}
