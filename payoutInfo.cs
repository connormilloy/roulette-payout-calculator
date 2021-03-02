using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roulette_Payout_Calculator
{
    public partial class payoutInfo : Form
    {
        public payoutInfo()
        {
            InitializeComponent();
        }

        private void payoutInfo_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
