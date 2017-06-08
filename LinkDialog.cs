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
    public partial class LinkDialog : Form
    {
        public string Link
        {
            get { return this.link.Text.Trim(); }
        }
        public string Txt
        {
            get { return txt.Text.Trim(); }
            set { txt.Text = value; }
        }
        public LinkDialog()
        {
            InitializeComponent();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.link.Text.Length == 0)               // 只有在文本框里没有文字的情况下才绘制水印。
            {
                Brush brush = SystemBrushes.GrayText;  // 使用灰色的文本色，即表示禁用状态的文字颜色。
                Font font = this.Font;                 // 使用当前控件的字体。
                Rectangle rect = this.link.ClientRectangle; // 在当前控件的客户区域绘制。

                using (Graphics g = this.link.CreateGraphics())
                {
                    g.DrawString("[http://|https://|ftp://]", font, brush, rect);
                }
            }
            base.OnPaint(e);
        }
    }
}
