using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Editor
{
    public class RichTextBoxEx : RichTextBox
    {
        #region Interop-Defines
        [StructLayout(LayoutKind.Sequential)]
        private struct CHARFORMAT2_STRUCT
        {
            public UInt32 cbSize;
            public UInt32 dwMask;
            public UInt32 dwEffects;
            public Int32 yHeight;
            public Int32 yOffset;
            public Int32 crTextColor;
            public byte bCharSet;
            public byte bPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public char[] szFaceName;
            public UInt16 wWeight;
            public UInt16 sSpacing;
            public int crBackColor; // Color.ToArgb() -> int
            public int lcid;
            public int dwReserved;
            public Int16 sStyle;
            public Int16 wKerning;
            public byte bUnderlineType;
            public byte bAnimation;
            public byte bRevAuthor;
            public byte bReserved1;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        private const int WM_USER = 0x0400;
        private const int EM_GETCHARFORMAT = WM_USER + 58;
        private const int EM_SETCHARFORMAT = WM_USER + 68;

        private const int SCF_SELECTION = 0x0001;
        private const int SCF_WORD = 0x0002;
        private const int SCF_ALL = 0x0004;

        #region CHARFORMAT2 Flags
        private const UInt32 CFE_BOLD = 0x0001;
        private const UInt32 CFE_ITALIC = 0x0002;
        private const UInt32 CFE_UNDERLINE = 0x0004;
        private const UInt32 CFE_STRIKEOUT = 0x0008;
        private const UInt32 CFE_PROTECTED = 0x0010;
        private const UInt32 CFE_LINK = 0x0020;
        private const UInt32 CFE_AUTOCOLOR = 0x40000000;
        private const UInt32 CFE_SUBSCRIPT = 0x00010000;        /* Superscript and subscript are */
        private const UInt32 CFE_SUPERSCRIPT = 0x00020000;      /*  mutually exclusive			 */

        private const int CFM_SMALLCAPS = 0x0040;           /* (*)	*/
        private const int CFM_ALLCAPS = 0x0080;         /* Displayed by 3.0	*/
        private const int CFM_HIDDEN = 0x0100;          /* Hidden by 3.0 */
        private const int CFM_OUTLINE = 0x0200;         /* (*)	*/
        private const int CFM_SHADOW = 0x0400;          /* (*)	*/
        private const int CFM_EMBOSS = 0x0800;          /* (*)	*/
        private const int CFM_IMPRINT = 0x1000;         /* (*)	*/
        private const int CFM_DISABLED = 0x2000;
        private const int CFM_REVISED = 0x4000;

        private const int CFM_BACKCOLOR = 0x04000000;
        private const int CFM_LCID = 0x02000000;
        private const int CFM_UNDERLINETYPE = 0x00800000;       /* Many displayed by 3.0 */
        private const int CFM_WEIGHT = 0x00400000;
        private const int CFM_SPACING = 0x00200000;     /* Displayed by 3.0	*/
        private const int CFM_KERNING = 0x00100000;     /* (*)	*/
        private const int CFM_STYLE = 0x00080000;       /* (*)	*/
        private const int CFM_ANIMATION = 0x00040000;       /* (*)	*/
        private const int CFM_REVAUTHOR = 0x00008000;


        private const UInt32 CFM_BOLD = 0x00000001;
        private const UInt32 CFM_ITALIC = 0x00000002;
        private const UInt32 CFM_UNDERLINE = 0x00000004;
        private const UInt32 CFM_STRIKEOUT = 0x00000008;
        private const UInt32 CFM_PROTECTED = 0x00000010;
        private const UInt32 CFM_LINK = 0x00000020;
        private const UInt32 CFM_SIZE = 0x80000000;
        private const UInt32 CFM_COLOR = 0x40000000;
        private const UInt32 CFM_FACE = 0x20000000;
        private const UInt32 CFM_OFFSET = 0x10000000;
        private const UInt32 CFM_CHARSET = 0x08000000;
        private const UInt32 CFM_SUBSCRIPT = CFE_SUBSCRIPT | CFE_SUPERSCRIPT;
        private const UInt32 CFM_SUPERSCRIPT = CFM_SUBSCRIPT;

        private const byte CFU_UNDERLINENONE = 0x00000000;
        private const byte CFU_UNDERLINE = 0x00000001;
        private const byte CFU_UNDERLINEWORD = 0x00000002; /* (*) displayed as ordinary underline	*/
        private const byte CFU_UNDERLINEDOUBLE = 0x00000003; /* (*) displayed as ordinary underline	*/
        private const byte CFU_UNDERLINEDOTTED = 0x00000004;
        private const byte CFU_UNDERLINEDASH = 0x00000005;
        private const byte CFU_UNDERLINEDASHDOT = 0x00000006;
        private const byte CFU_UNDERLINEDASHDOTDOT = 0x00000007;
        private const byte CFU_UNDERLINEWAVE = 0x00000008;
        private const byte CFU_UNDERLINETHICK = 0x00000009;
        private const byte CFU_UNDERLINEHAIRLINE = 0x0000000A; /* (*) displayed as ordinary underline	*/

        #endregion

        #endregion
        [DefaultValue(false)]
        public new bool DetectUrls
        {
            get { return base.DetectUrls; }
            set { base.DetectUrls = value; }
        }
        [DefaultValue(false)]
        public new bool Modified
        {
            get { return base.Modified; }
            set { base.Modified = value; }
            }
        [DefaultValue(false)]
        public bool Initialized
        {
            get;set;
        }
        private FullScreenHelper fullScreen = new FullScreenHelper();
        private int Number = 0;
        public RichTextBoxEx()
        {
            // Otherwise, non-standard links get lost when user starts typing
            // next to a non-standard link
            this.DetectUrls = false;
            this.TabStop = false;
            this.Dock = DockStyle.Fill;
            //this.Select(0, 0);
            //this.SelectionIndent = 33;
            //this.SelectionHangingIndent = -200;
            //this.SelectionRightIndent = 0;
            this.ContextMenuStrip = GetContextMenu();
        }
        private ContextMenuStrip GetContextMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem undo = new ToolStripMenuItem("撤消");
            menu.Items.Add(undo);
            ToolStripMenuItem redo = new ToolStripMenuItem("重做");
            menu.Items.Add(redo);
            menu.Items.Add(new ToolStripSeparator());
            ToolStripMenuItem cut = new ToolStripMenuItem("剪切");
            menu.Items.Add(cut);
            ToolStripMenuItem copy = new ToolStripMenuItem("复制");
            menu.Items.Add(copy);
            ToolStripMenuItem paste = new ToolStripMenuItem("粘贴");
            menu.Items.Add(paste);
            ToolStripMenuItem delete = new ToolStripMenuItem("删除");
            menu.Items.Add(delete);
            menu.Items.Add(new ToolStripSeparator());
            ToolStripMenuItem selectAll = new ToolStripMenuItem("全选");
            menu.Items.Add(selectAll);
            foreach (var item in menu.Items)
            {
                if(item is ToolStripMenuItem)
                {
                    (item as ToolStripMenuItem).Click += Menu_Click;
                }
            }
            ToolStripMenuItem Indent = new ToolStripMenuItem("段首缩进");
            Indent.Click += Indent_Click;
            menu.Items.Add(Indent);
            ToolStripMenuItem fullScreen = new ToolStripMenuItem("全屏锁定");
            fullScreen.Name = "FullScreen";
            fullScreen.Click += RichTextBoxEx_Click;
            menu.Items.Add(fullScreen);
            return menu;
        }

        private void RichTextBoxEx_Click(object sender, EventArgs e)
        {
            if (!fullScreen.m_bFullScreen)
            {
                using (LockTimer locktimer = new LockTimer())
                {
                    if (locktimer.ShowDialog() == DialogResult.OK)
                    {
                        var menu = this.ContextMenuStrip.Items.Find("FullScreen", true)[0];
                        Number = locktimer.Num * 60;
                        menu.Enabled = false;
                        Timer timer = new Timer();
                        timer.Interval = 1000;
                        timer.Tick += Timer_Tick;
                        timer.Start();
                        this.Focus();
                    }
                    else
                        return;
                }
            }
            this.Invalidate();
            fullScreen.m_control = this.Parent.Parent;
            fullScreen.m_inputControl = this;
            fullScreen.FullScreen();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var menu = this.ContextMenuStrip.Items.Find("FullScreen", true)[0];
            if (Number > 0)
            {
                menu.Text = string.Format("全屏解锁还有{0}秒", Number);
                menu.Invalidate();
            }
            else
            {
                menu.Text = "屏幕锁定";
                menu.Enabled = true;
                (sender as Timer).Stop();
            }
            Number--;
        }
        protected override void OnEnter(EventArgs e)
        {
            this.ImeModeBase = ImeMode.On;
            base.OnEnter(e);
        }
        private void Indent_Click(object sender, EventArgs e)
        {
            int start = this.SelectionStart;
            int length = this.SelectionLength;
            this.SelectAll();
            this.SelectionIndent = 33;
            this.SelectionHangingIndent = -200;
            this.SelectionRightIndent = 0;
            this.DeselectAll();
            this.SelectionStart = start;
            this.SelectionLength = length;
        }

        private void Menu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            switch (item.Text)
            {
                case "撤消":
                    this.Undo();
                    break;
                case "重做":
                    this.Redo();
                    break;
                case "剪切":
                    this.Cut();
                    break;
                case "复制":
                    this.Copy();
                    break;
                case "粘贴":
                    this.Paste();
                    break;
                case "删除":
                    this.SelectedText = "";
                    break;
                case "全选":
                    this.SelectAll();
                    break;
            }
        }
        /// <summary>
        /// Insert a given text as a link into the RichTextBox at the current insert position.
        /// </summary>
        /// <param name="text">Text to be inserted</param>
        public void InsertLink(string text)
        {
            InsertLink(text, this.SelectionStart);
        }

        /// <summary>
        /// Insert a given text at a given position as a link. 
        /// </summary>
        /// <param name="text">Text to be inserted</param>
        /// <param name="position">Insert position</param>
        public void InsertLink(string text, int position)
        {
            if (position < 0 || position > this.Text.Length)
                throw new ArgumentOutOfRangeException("position");

            this.SelectionStart = position;
            this.SelectedText = text;
            this.Select(position, text.Length);
            this.SetSelectionLink(true);
            this.Select(position + text.Length, 0);
        }
        /// <summary>
        /// Insert a given text at at the current input position as a link.
        /// The link text is followed by a hash (#) and the given hyperlink text, both of
        /// them invisible.
        /// When clicked on, the whole link text and hyperlink string are given in the
        /// LinkClickedEventArgs.
        /// </summary>
        /// <param name="text">Text to be inserted</param>
        /// <param name="hyperlink">Invisible hyperlink string to be inserted</param>
        public void InsertLink(string text, string hyperlink)
        {
            InsertLink(text, hyperlink, this.SelectionStart);
        }

        /// <summary>
        /// Insert a given text at a given position as a link. The link text is followed by
        /// a hash (#) and the given hyperlink text, both of them invisible.
        /// When clicked on, the whole link text and hyperlink string are given in the
        /// LinkClickedEventArgs.
        /// </summary>
        /// <param name="text">Text to be inserted</param>
        /// <param name="hyperlink">Invisible hyperlink string to be inserted</param>
        /// <param name="position">Insert position</param>
        public void InsertLink(string text, string hyperlink, int position)
        {
            if (position < 0 || position > this.Text.Length)
                throw new ArgumentOutOfRangeException("position");
            this.SelectionStart = position;
            //MessageBox.Show(position.ToString());
            this.SelectedRtf = @"{\rtf1 \v#\v0 " + TextToRtf(text) + @"\v " + TextToRtf(hyperlink) + @"\v0}";
            this.Select(position + 1, text.Length + hyperlink.Length + 1);
            this.SetSelectionLink(true);
            this.Select(position + text.Length + hyperlink.Length + 1, 0);
        }
        public void SetSelectionAll()
        {
            Regex reg = new Regex(@"\\v #\\v0(?<txt>.*)\\v(?<link>.*)\\v0", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var matches = reg.Matches(this.Rtf);
            foreach (Match m in matches)
            {
                string text = m.Groups[0].Value;
                string txt_rtf = m.Groups["txt"].Value, txt = m.Groups["txt"].Value;
                string link = m.Groups["link"].Value;
                Regex r = new Regex(@"\\\'([0-9a-f]{2})\\\'([0-9a-f]{2})");
                txt = Regex.Replace(txt, @"\\\'([0-9a-f]{2})\\\'([0-9a-f]{2})", delegate (Match m1) {
                    string c1 = string.Format("0x{0}", m1.Groups[1].Value);
                    string c2 = string.Format("0x{0}", m1.Groups[2].Value);
                    byte[] bytes = new byte[] { Convert.ToByte(c1, 16), Convert.ToByte(c2, 16) };
                    return Encoding.Default.GetString(bytes);
                });
                Regex g = new Regex(txt.Trim());
                var ms = g.Matches(this.Text);
                foreach (Match match in ms)
                {
                    int pos = match.Index;
                    this.Select(pos, txt.Length + link.Length);
                    Match mt = new Regex(@".*\\v(?<link>.*)\\v0").Match(this.SelectedRtf);
                    if (mt.Success)
                    {
                        if (mt.Groups["link"].Value == link)
                            this.SetSelectionLink(true);
                    }
                }
            }
            this.Select(0, 0);
        }
        public string TextToRtf(string AText)
        {
            string vReturn = "";
            foreach (char vChar in AText)
            {
                switch (vChar)
                {
                    case '\\':
                        vReturn += @"\\";
                        break;
                    case '{':
                        vReturn += @"\{";
                        break;
                    case '}':
                        vReturn += @"\}";
                        break;
                    default:
                        if (vChar > (char)127)
                            vReturn += @"\u" + ((int)vChar).ToString() + "?";
                        else vReturn += vChar;
                        break;
                }
            }
            return vReturn;
        }
        /// <summary>
        /// Set the current selection's link style
        /// </summary>
        /// <param name="link">true: set link style, false: clear link style</param>
        public void SetSelectionLink(bool link)
        {
            SetSelectionStyle(CFM_LINK, link ? CFE_LINK : 0);
        }
        /// <summary>
        /// Get the link style for the current selection
        /// </summary>
        /// <returns>0: link style not set, 1: link style set, -1: mixed</returns>
        public int GetSelectionLink()
        {
            return GetSelectionStyle(CFM_LINK, CFE_LINK);
        }


        private void SetSelectionStyle(UInt32 mask, UInt32 effect)
        {
            CHARFORMAT2_STRUCT cf = new CHARFORMAT2_STRUCT();
            cf.cbSize = (UInt32)Marshal.SizeOf(cf);
            cf.dwMask = mask;
            cf.dwEffects = effect;

            IntPtr wpar = new IntPtr(SCF_SELECTION);
            IntPtr lpar = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
            Marshal.StructureToPtr(cf, lpar, false);

            IntPtr res = SendMessage(Handle, EM_SETCHARFORMAT, wpar, lpar);

            Marshal.FreeCoTaskMem(lpar);
        }

        private int GetSelectionStyle(UInt32 mask, UInt32 effect)
        {
            CHARFORMAT2_STRUCT cf = new CHARFORMAT2_STRUCT();
            cf.cbSize = (UInt32)Marshal.SizeOf(cf);
            cf.szFaceName = new char[32];

            IntPtr wpar = new IntPtr(SCF_SELECTION);
            IntPtr lpar = Marshal.AllocCoTaskMem(Marshal.SizeOf(cf));
            Marshal.StructureToPtr(cf, lpar, false);

            IntPtr res = SendMessage(Handle, EM_GETCHARFORMAT, wpar, lpar);

            cf = (CHARFORMAT2_STRUCT)Marshal.PtrToStructure(lpar, typeof(CHARFORMAT2_STRUCT));

            int state;
            // dwMask holds the information which properties are consistent throughout the selection:
            if ((cf.dwMask & mask) == mask)
            {
                if ((cf.dwEffects & effect) == effect)
                    state = 1;
                else
                    state = 0;
            }
            else
            {
                state = -1;
            }

            Marshal.FreeCoTaskMem(lpar);
            return state;
        }
    }
}
