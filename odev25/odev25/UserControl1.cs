using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace odev25
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.T_Shert;
            pictureBox2.Image = Properties.Resources.Pantolon;
            pictureBox3.Image = Properties.Resources.Corap;
            pictureBox4.Image = Properties.Resources.Gomlek;

        }
    }
}
