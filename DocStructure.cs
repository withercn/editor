using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace Editor
{
    public partial class DocStructure : UserControl
    {
        private string Path = string.Empty;//文件路径
        private bool f_Paint = false;
        
        #region 行距,两端对齐
        public const int WM_USER = 0x0400;
        public const int EM_GETPARAFORMAT = WM_USER + 61;
        public const int EM_SETPARAFORMAT = WM_USER + 71;
        public const long MAX_TAB_STOPS = 32;
        public const uint PFM_LINESPACING = 0x00000100;
        public const int PFM_ALIGNMENT = 8;
        public const int PFA_JUSTIFY = 4;
        public const int CFE_LINK = 0x20;
        public const int CFM_LINK = 0x20;
        public const int EM_SETCHARFORMAT = 0x444;
        public const int SCF_SELECTION = 1;
        [StructLayout(LayoutKind.Sequential)]
        private struct PARAFORMAT2
        {
            public int cbSize;
            public uint dwMask;
            public int dwEffects;
            public short wNumbering;
            public short wReserved;
            public int dxStartIndent;
            public int dxRightIndent;
            public int dxOffset;
            public short wAlignment;
            public short cTabCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] rgxTabs;
            public int dySpaceBefore;
            public int dySpaceAfter;
            public int dyLineSpacing;
            public short sStyle;
            public byte bLineSpacingRule;
            public byte bOutlineLevel;
            public short wShadingWeight;
            public short wShadingStyle;
            public short wNumberingStart;
            public short wNumberingStyle;
            public short wNumberingTab;
            public short wBorderSpace;
            public short wBorderWidth;
            public short wBorders;
        }
        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref PARAFORMAT2 lParam);
        public static void SetLineSpace(RichTextBoxEx box, int dyLineSpacing)
        {
            PARAFORMAT2 fmt = new PARAFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.bLineSpacingRule = 4;// bLineSpacingRule;  
            fmt.dyLineSpacing = dyLineSpacing;
            fmt.dwMask = PFM_LINESPACING;
            try
            {
                SendMessage(new HandleRef(box, box.Handle), EM_SETPARAFORMAT, 0, ref fmt);
            }
            catch
            {

            }
        }
        public void SetJustifyAlignment()
        {
            PARAFORMAT2 fmt = new PARAFORMAT2();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.wAlignment = PFA_JUSTIFY;
            fmt.dwMask = PFM_ALIGNMENT;
            try
            {
                SendMessage(new HandleRef(content, content.Handle), EM_SETPARAFORMAT, 0, ref fmt);
            }
            catch { }
        }
        public void InsertLink(string txt,string lk)
        {
            content.InsertLink(txt, lk);
        }
        /// <summary>
        /// 是否调用OnPaint
        /// </summary>
        /// <param name="paint"></param>
        private void showLineNo(bool paint)
        {
            if (!f_Paint) return;
            //获得当前坐标信息
            Point p = this.content.Location;
            int crntFirstIndex = this.content.GetCharIndexFromPosition(p);
            int crntFirstLine = this.content.GetLineFromCharIndex(crntFirstIndex);
            Point crntFirstPos = this.content.GetPositionFromCharIndex(crntFirstIndex);
            p.Y += this.content.Height;
            int crntLastIndex = this.content.GetCharIndexFromPosition(p);
            int crntLastLine = this.content.GetLineFromCharIndex(crntLastIndex);
            Point crntLastPos = this.content.GetPositionFromCharIndex(crntLastIndex);
            //准备画图
            Graphics g = this.panel.CreateGraphics();
            Font font = new Font(this.content.Font, this.content.Font.Style);
            SolidBrush brush = new SolidBrush(Color.Green);
            //画图开始
            //刷新画布
            if (this.content.Text.Trim().Length > 0)
                this.panel.Width = Convert.ToInt32(font.Size * crntLastLine.ToString().Length) + 5;
            Rectangle rect = this.panel.ClientRectangle;
            brush.Color = this.panel.BackColor;
            g.FillRectangle(brush, 0, 0, this.panel.ClientRectangle.Width, this.panel.ClientRectangle.Height);
            brush.Color = Color.Wheat;
            //绘制行号
            int lineSpace = 0;
            if (crntFirstLine != crntLastLine)
            {
                lineSpace = (crntLastPos.Y - crntFirstPos.Y) / (crntLastLine - crntFirstLine);
            }
            else
            {
                lineSpace = Convert.ToInt32(this.content.Font.Size);
            }
            int brushX = this.panel.ClientRectangle.Width - Convert.ToInt32(font.Size * crntLastLine.ToString().Length);
            int brushY = crntLastPos.Y + Convert.ToInt32(font.Size * 0.21f);//惊人的算法啊！！
            for (int i = crntLastLine; i >= crntFirstLine; i--)
            {
                g.DrawString((i + 1).ToString(), font, brush, brushX, brushY);
                brushY -= lineSpace;
            }
            g.Dispose();
            font.Dispose();
            brush.Dispose();
            //if (paint) this.panel.Invalidate();
        }
        private void content_TextChanged(object sender, EventArgs e)
        {
            if (f_Paint)
            {
                this.showLineNo(true);
            }
        }
        private void content_VScroll(object sender, EventArgs e)
        {
            this.showLineNo(true);
        }
        private void DocStructure_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                this.showLineNo(true);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            f_Paint = true;
            this.showLineNo(false);
            base.OnPaint(e);
        }
        #endregion
        public DocStructure()
        {
            InitializeComponent();
        }
        public DocStructure(string path)
        {
            Path = path;
            InitializeComponent();

            if (File.Exists(path))
                this.content.LoadFile(path);
            //this.content.Text = File.ReadAllText(path, Encoding.Default);// this.content.LoadFile(path, RichTextBoxStreamType.PlainText);
            else
                content.SaveFile(path, RichTextBoxStreamType.RichText);
        }
        private void InitializeDoc()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            content.BackColor = Color.FromArgb(40, 40, 40);
            content.ForeColor = Color.FromArgb(220, 220, 220);
            panel.BackColor = Color.FromArgb(30, 30, 30);
            panel.ForeColor = Color.FromArgb(220, 220, 220);
            content.SelectionFont = new Font("宋体", 12);
            this.content.LanguageOption = RichTextBoxLanguageOptions.DualFont;
            content.SelectAll();
            content.SelectionIndent = 33;
            content.SelectionHangingIndent = -200;
            DocStructure.SetLineSpace(content, 1000);
            content.SelectionRightIndent = 0;
            content.DeselectAll();
            //this.content.SetSelectionAll(); //插入链接
            //this.content.SelectionBullet = true;
            //this.content.SelectionIndent = Convert.ToInt32(this.content.Font.Size * 2);
            //this.content.SelectionHangingIndent = -Convert.ToInt32(this.content.Font.Size * 2);
        }
        public void SaveFile()
        {
            content.SaveFile(Path, RichTextBoxStreamType.RichText);
            content.Modified = false;
        }
        #region 高亮选中行
        private void Content_SelectionChanged(object sender, System.EventArgs e)
        {
            this.LightLine();
        }
        private void LightLine()
        {
            LightLine(Color.FromArgb(40, SystemColors.Control));
        }
        private void LightLine(Color bgColor)
        {
            this.content.Refresh();
            using (Graphics g = this.content.CreateGraphics())
            {
                using (SolidBrush brush = new SolidBrush(bgColor))
                {
                    int index = this.content.GetFirstCharIndexOfCurrentLine();
                    Point pos = this.content.GetPositionFromCharIndex(index);
                    int height = 0;
                    if(this.content.SelectionFont != null)
                    {
                        height = this.content.SelectionFont.Height;
                    }
                    else if(string.IsNullOrEmpty(this.content.SelectedText.Trim()))
                    {
                        int n = this.content.SelectionLength;
                        this.content.DeselectAll();
                        height = this.content.SelectionFont.Height;
                    }
                    Rectangle rect = new Rectangle(0, pos.Y, this.content.Width, height);
                    g.FillRectangle(brush, rect);
                }
            }
        }
        #endregion
        private void DocStructure_Load(object sender, EventArgs e)
        {
            this.InitializeDoc();
            this.content.Initialized = true;
            this.content.Modified = false;
            this.content.Focus();
            //this.testa();
            //content.SelectAll();
            //SetLineSpace(content, 0);
            //content.SelectionCharOffset = -10;
            //content.Select(0, 0);
        }
        private void testa()
        {
            string[] lines = content.Lines;
            int total = lines.Length;
            foreach(string line in lines)
            {
                string a = line;
            }
        }
        public void UnLoad()
        {
            this.SaveFile();
            this.Dispose(true);
            GC.Collect();
        }

        private void content_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (e.LinkText.Trim().IndexOf(' ') != -1)
            {
                if (MessageBox.Show(string.Format("是否弹出未知链接 {0}", e.LinkText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]), "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    System.Diagnostics.Process.Start(e.LinkText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1].Trim());
            }
        }
        private void Content_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.Alt && e.Control && e.KeyCode == Keys.D)
            {
                RtfDebug rtf = new RtfDebug();
                rtf.rtb.Text = content.Rtf;
                rtf.ShowDialog();
            }
        }
    }
}
  