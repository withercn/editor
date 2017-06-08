using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
    public partial class LockTimer : Form
    {
        public int Num
        {
            get
            {
                return number.Value;
            }
        }
        public LockTimer()
        {
            InitializeComponent();
        }

        private void number_Scroll(object sender, EventArgs e)
        {
            label1.Text = number.Value.ToString() + "分钟（0-600）";
        }
    }
}
