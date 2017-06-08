using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Editor
{
    public partial class Main : Form
    {
        private Timer timer = new Timer();
        private Timer saveTimer = new Timer();
        const int CLOSE_SIZE = 15;
        private Color bgColor = Color.FromArgb(100, 203, 141, 37);
        private Color foreColor = Color.DarkBlue;
        private class FontSize
        {
            private static List<Main.FontSize> allFontSize;
            private int displaySize;
            private int valueSize;

            private FontSize(int display, int value)
            {
                this.displaySize = display;
                this.valueSize = value;
            }

            public static Main.FontSize Find(int value)
            {
                if (value < 1)
                {
                    return All[0];
                }
                if (value > 7)
                {
                    return All[6];
                }
                return All[value - 1];
            }

            public override string ToString()
            {
                return this.displaySize.ToString();
            }

            public static List<Main.FontSize> All
            {
                get
                {
                    if (allFontSize == null)
                    {
                        allFontSize = new List<Main.FontSize>();
                        allFontSize.Add(new Main.FontSize(8, 1));
                        allFontSize.Add(new Main.FontSize(10, 2));
                        allFontSize.Add(new Main.FontSize(12, 3));
                        allFontSize.Add(new Main.FontSize(14, 4));
                        allFontSize.Add(new Main.FontSize(0x12, 5));
                        allFontSize.Add(new Main.FontSize(0x18, 6));
                        allFontSize.Add(new Main.FontSize(0x24, 7));
                        allFontSize.Add(new Main.FontSize(0x32, 8));
                        allFontSize.Add(new Main.FontSize(0x48, 9));
                        allFontSize.Add(new Main.FontSize(0x64, 10));
                    }
                    return allFontSize;
                }
            }

            public int Display
            {
                get
                {
                    return this.displaySize;
                }
            }

            public int Value
            {
                get
                {
                    return this.valueSize;
                }
            }
        }
        public Main()
        {
            InitializeComponent();
            #region 初始化字体
            foreach (FontFamily family in FontFamily.Families)
            {
                this.toolStripComboBoxName.Items.Add(family.Name);
            }
            this.toolStripComboBoxName.Text = "宋体";
            this.toolStripComboBoxSize.Items.AddRange(FontSize.All.ToArray());
            this.toolStripComboBoxSize.Text = "12";
            #endregion
            //清空控件
            this.MainTabControl.TabPages.Clear();
            //绘制的方式OwnerDrawFixed表示由窗体绘制大小也一样
            this.MainTabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.MainTabControl.Padding = new System.Drawing.Point(CLOSE_SIZE, 3);
            this.MainTabControl.DrawItem += MainTabControl_DrawItem;
            this.MainTabControl.MouseDown += MainTabControl_MouseDown;
            this.dir.tree.NodeMouseClick += Tree_NodeMouseClick;
            this.dir.tree.AfterLabelEdit += Tree_AfterLabelEdit;
            this.dir.ArticleRemoved += Dir_ArticleRemoved;
            this.dir.TreeNodeDelete += Dir_TreeNodeDelete;
            this.dir.InitializeDirectory();
            this.InitializeApp();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
            saveTimer.Interval = 30000;
            saveTimer.Tick += SaveTimer_Tick;
            saveTimer.Start();
        }
        private void SaveTimer_Tick(object sender, EventArgs e)
        {
            var doc = this.getDocStructure();
            if(doc != null)
            {
                if (doc.content.Modified)
                {
                    doc.SaveFile();
                    doc.content.Modified = false;
                    this.status.Text = "内容自动保存";
                }
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            nowTimer.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
        }
        #region 目录与内容处理
        private void InitializeApp()
        {
            try
            {
                if (EditorSetting.Default.D != null)
                    foreach (string text in EditorSetting.Default.D)
                    {
                        TreeNodeMouseClickEventArgs e = new TreeNodeMouseClickEventArgs(dir.FindNode(text), MouseButtons.Left, 0, 0, 0);
                        Tree_NodeMouseClick(dir, e);
                    }
                MainTabControl.SelectedIndex = EditorSetting.Default.SelectionPage;
                foreach (TreeNode node in dir.tree.Nodes)
                {
                    foreach (TreeNode n in node.Nodes)
                    {
                        if (n.ToolTipText == EditorSetting.Default.SelectedNode)
                            dir.tree.SelectedNode = n;
                    }
                }
                if (EditorSetting.Default.IsExPand != null && EditorSetting.Default.IsExPand.Count == this.dir.tree.Nodes.Count)
                    for (int i = 0; i < EditorSetting.Default.IsExPand.Count; i++)
                    {
                        if ((bool)EditorSetting.Default.IsExPand[i])
                        {
                            this.dir.tree.Nodes[i].Expand();
                        }
                    }
                var content = this.getContent();
                if (content != null)
                    content.Focus();
            }
            catch { }
        }
        private void Dir_ArticleRemoved(object sender, TreeViewRemovedArgs e)
        {
            if (File.Exists(e.node.ToolTipText))
            {
                foreach(TabPage t in MainTabControl.TabPages)
                {
                    if(t.Text == e.node.Text)
                    {
                        this.getDocStructure(t).UnLoad();
                        this.status.Text = "内容已保存";
                        t.Controls.Clear();
                        this.MainTabControl.TabPages.Remove(t);
                    }
                }
                if (e.node != null)
                {
                    File.Delete(e.node.ToolTipText);
                    e.node.Remove();
                }
            }
        }
        private void Dir_TreeNodeDelete(object sender, TreeViewRemovedArgs e)
        {
            foreach(TreeNode n in e.node.Nodes)
            {
                foreach(TabPage tab in MainTabControl.TabPages)
                {
                    if (n.ToolTipText == tab.ToolTipText)
                        MainTabControl.TabPages.Remove(tab);
                }
            }
        }
        private void Tree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Tag.ToString() == "Article")
            {
                if (!string.IsNullOrEmpty(e.Node.ToolTipText))
                {//重命名章节
                    if (e.Node.Text == e.Label || e.Label == null) return;
                    string path = e.Node.ToolTipText.Replace(e.Node.Text, e.Label);
                    if (File.Exists(e.Node.ToolTipText) && !File.Exists(path))
                    {
                        Directory.Move(e.Node.ToolTipText, path);
                        foreach(TabPage t in MainTabControl.TabPages)
                        {
                            if(t.Name == e.Node.ToolTipText)
                            {
                                t.Name = path;
                                t.ToolTipText = path;
                                t.Text = e.Label;
                            }
                        }
                        e.Node.ToolTipText = path;
                    }
                    dir.tree.LabelEdit = false;
                }
                else
                {//添加章节
                    if (e.Label== null)
                    {
                        dir.tree.Nodes.Remove(e.Node);
                        dir.tree.LabelEdit = false;
                        return;
                    }
                    if (dir.currDirectory != null)
                    {
                        var path = string.Format("{0}\\Books\\{1}\\{2}.txt", dir.currDirectory, e.Node.Parent.Text, e.Label);
                        if (File.Exists(path))
                        {
                            MessageBox.Show("已存在同名章节");
                            dir.tree.Nodes.Remove(e.Node);
                            dir.tree.LabelEdit = false;
                            return;
                        }
                        e.Node.ToolTipText = path;
                        e.CancelEdit = false;
                        this.OpenNewTab(e.Label, e.Node.ToolTipText);
                    }
                    dir.tree.LabelEdit = false;
                }
            }
        }
        private void Tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null) return;
            if (e.Button == MouseButtons.Right) return;
            if(e.Node.Parent != null)
            {
                bool is_find = false;
                foreach(TabPage tap in MainTabControl.TabPages)
                {
                    foreach (Control c in tap.Controls)
                    {
                        if (c is DocStructure)
                        {
                            c.Visible = false;
                            (c as DocStructure).content.Modified = false;
                            var doc = (c as DocStructure).content;
                            var modify = doc.Modified;
                        }
                    }
                    if (tap.ToolTipText == e.Node.ToolTipText)
                    {
                        is_find = true;
                        MainTabControl.SelectedTab = tap;
                        this.status.Text = "准备";
                        foreach(Control c in tap.Controls)
                        {
                            if (c is DocStructure)
                                c.Visible = true;
                        }
                    }
                }
                if (is_find) return;
                this.OpenNewTab(e.Node.Text, e.Node.ToolTipText);
            }
        }
        private void OpenNewTab(string text, string toolTipText)
        {
            var t = new TabPage(text);
            DocStructure doc = new DocStructure(toolTipText);
            doc.content.TextChanged += Content_TextChanged;
            doc.content.SelectionChanged += Content_SelectionChanged;
            doc.content.KeyUp += Content_KeyUp;
            doc.Dock = DockStyle.Fill;
            doc.Parent = this;
            t.ToolTipText = toolTipText;
            t.Name = toolTipText;
            FontSet(null, null);
            t.Controls.Add(doc);
            MainTabControl.TabPages.Add(t);
            MainTabControl.SelectedTab = t;
            this.charLine.Text = string.Format("{0}行{1}列", 0, 0);
            this.total.Text = doc.content.Text.Length + "字";
            this.status.Text = "准备";
        }
        private void Content_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.S && e.Control)
            {
                var doc = this.getDocStructure();
                doc.SaveFile();
                doc.content.Modified = false;
                this.status.Text = "内容已保存";
            }
            this.TabKeyProcess(e);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab | Keys.Control:
                    if (msg.HWnd == this.MainTabControl.Handle)
                        return true;
                    return false;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void TabKeyProcess(KeyEventArgs e)
        {
            int index = 0;
            if (e.KeyValue >= 49 && e.KeyValue <= 57 && e.Control)
            {
                index = e.KeyValue - 49;
                if (index >= 0 && index < MainTabControl.TabPages.Count)
                    MainTabControl.SelectedIndex = index;
            }
            index = MainTabControl.SelectedIndex;
            if (e.KeyCode == Keys.Tab && e.Control)
            {
                if (index < MainTabControl.TabPages.Count - 1)
                    MainTabControl.SelectedIndex++;
                else
                    MainTabControl.SelectedIndex = 0;
            }
            if (e.KeyCode == Keys.Tab && e.Control && e.Shift)
            {
                if (index > 0)
                    MainTabControl.SelectedIndex--;
                else
                    MainTabControl.SelectedIndex = MainTabControl.TabPages.Count - 1;
            }
        }
        private void Content_SelectionChanged(object sender, EventArgs e)
        {
            var doc = this.getContent();
            if(doc != null)
            {
                var index = doc.GetFirstCharIndexOfCurrentLine();
                var line = doc.GetLineFromCharIndex(index);
                charLine.Text = string.Format("{0}行{1}列", line, doc.SelectionStart - index + 1);
                if (doc.SelectionFont != null)
                {
                    this.toolStripComboBoxName.Text = doc.SelectionFont.Name;
                    this.toolStripComboBoxSize.Text = doc.SelectionFont.Size.ToString();
                    this.toolStripButtonBold.Checked = doc.SelectionFont.Bold;
                    this.toolStripButtonItalic.Checked = doc.SelectionFont.Italic;
                    this.toolStripButtonUnderline.Checked = doc.SelectionFont.Underline;
                }
            }
        }
        private void Content_TextChanged(object sender, EventArgs e)
        {
            var doc = getContent();
            if(doc != null)
            {
                if (doc.Modified && doc.Initialized)
                    this.status.Text = "内容已修改";
                var index = doc.GetFirstCharIndexOfCurrentLine();
                var line = doc.GetLineFromCharIndex(index);
                this.charLine.Text = string.Format("{0}行{1}列", line + 1, doc.SelectionStart - index + 1);
                this.total.Text = doc.Text.Length + "字";
            }
        }
        private RichTextBoxEx getContent()
        {
            return this.getContent(MainTabControl.SelectedTab);
        }
        public RichTextBoxEx getContent(TabPage t)
        {
            if (t != null)
            {
                foreach (Control c in t.Controls)
                {
                    if (c is DocStructure)
                        return (c as DocStructure).content;
                }
            }
            return null;
        }
        private DocStructure getDocStructure()
        {
            return this.getDocStructure(MainTabControl.SelectedTab);
        }
        private DocStructure getDocStructure(TabPage t)
        {
            if (t != null)
            {
                foreach (Control c in t.Controls)
                {
                    if (c is DocStructure)
                        return (c as DocStructure);
                }
            }
            return null;
        }
        #endregion
        private void nFile_Click(object sender, EventArgs e)
        {
            var t = new TabPage("text");
            var doc = new DocStructure();
            doc.Dock = DockStyle.Fill;
            t.Controls.Add(doc);
            MainTabControl.TabPages.Add(t);
            MainTabControl.SelectedTab = t;
        }
        private void text_Click(object sender, EventArgs e)
        {
            var content = this.getContent();
            content.SelectAll();
            DocStructure.SetLineSpace(content, 1000);
            content.Select(0, 0);
            //content.SelectionIndent = 33;
            //content.SelectionHangingIndent = -200;
            //content.SelectionRightIndent = 0;
        }
        private void MainTabControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X, y = e.Y;
                //计算关闭区域   
                Rectangle myTabRect = this.MainTabControl.GetTabRect(this.MainTabControl.SelectedIndex);
                myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 2);
                myTabRect.Width = CLOSE_SIZE;
                myTabRect.Height = CLOSE_SIZE;
                //如果鼠标在区域内就关闭选项卡   
                bool isClose = x > myTabRect.X && x < myTabRect.Right
 && y > myTabRect.Y && y < myTabRect.Bottom;
                if (isClose == true)
                {
                    this.getDocStructure().UnLoad();
                    this.status.Text = "内容已保存";
                    this.MainTabControl.SelectedTab.Controls.Clear();
                    this.MainTabControl.TabPages.Remove(this.MainTabControl.SelectedTab);
                }
            }
        }
        private void MainTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                Rectangle myTabRect = this.MainTabControl.GetTabRect(e.Index);
                using (SolidBrush brush = new SolidBrush(SystemColors.ControlText))
                {
                    Font font = this.Font;
                    if (e.Index == MainTabControl.SelectedIndex)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(bgColor), myTabRect);
                        brush.Color = Color.DarkSlateBlue;
                    }

                    //先添加TabPage属性   
                    e.Graphics.DrawString(this.MainTabControl.TabPages[e.Index].Text, this.Font, brush, myTabRect.X + 2, myTabRect.Y + 2);

                    //再画一个矩形框
                    using (Pen p = new Pen(SystemBrushes.Control))
                    {
                        myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 2);
                        myTabRect.Width = CLOSE_SIZE;
                        myTabRect.Height = CLOSE_SIZE;
                        e.Graphics.DrawRectangle(p, myTabRect);
                    }
                    //填充矩形框
                    Color recColor = e.State == DrawItemState.Selected ? Color.Transparent : Color.Transparent;
                    using (Brush b = new SolidBrush(recColor))
                    {
                        e.Graphics.FillRectangle(b, myTabRect);
                    }
                    //画关闭符号
                    using (Pen objpen = new Pen(brush))
                    {
                        //"\"线
                        Point p1 = new Point(myTabRect.X + 3, myTabRect.Y + 3);
                        Point p2 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + myTabRect.Height - 3);
                        e.Graphics.DrawLine(objpen, p1, p2);
                        //"/"线
                        Point p3 = new Point(myTabRect.X + 3, myTabRect.Y + myTabRect.Height - 3);
                        Point p4 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + 3);
                        e.Graphics.DrawLine(objpen, p3, p4);
                    }
                }
                e.Graphics.Dispose();
            }
            catch (Exception)
            {
            }
        }
        private void MainTabControl_TabIndexChanged(object sender, EventArgs e)
        {
            if (MainTabControl.TabPages.Count > 0)
            {
                foreach (TabPage tab in MainTabControl.TabPages)
                {
                    foreach (Control c in tab.Controls)
                    {
                        c.Visible = false;
                    }
                }
                foreach (Control c in MainTabControl.SelectedTab.Controls)
                {
                    if (c is DocStructure)
                    {
                        c.Visible = true;
                        (c as DocStructure).content.Focus();
                    }
                }
            }
        }
        #region 工具条按钮
        public void FontSet(object sender, EventArgs e)
        {
            float size = 12;
            if (float.TryParse(toolStripComboBoxSize.Text, out size))
            {
                if (MainTabControl.TabPages.Count > 0)
                {
                    var content = this.getContent();
                    content.SelectionFont = new Font(toolStripComboBoxName.Text, size);
                }
            }
        }
        private void toolStripButtonBold_Click(object sender, EventArgs e)
        {
            var content = this.getContent();
            Font font = content.SelectionFont;
            if (font.Bold)
                content.SelectionFont = new Font(font, font.Style & ~FontStyle.Bold);
            else
                content.SelectionFont = new Font(font, font.Style | FontStyle.Bold);
        }
        private void toolStripButtonItalic_Click(object sender, EventArgs e)
        {
            var content = this.getContent();
            Font font = content.SelectionFont;
            if (font.Italic)
                content.SelectionFont = new Font(font, font.Style & ~FontStyle.Italic);
            else
                content.SelectionFont = new Font(font, font.Style | FontStyle.Italic);
        }
        private void toolStripButtonUnderline_Click(object sender, EventArgs e)
        {
            var content = this.getContent();
            Font font = content.SelectionFont;
            if (font.Underline)
                content.SelectionFont = new Font(font, font.Style & ~FontStyle.Underline);
            else
                content.SelectionFont = new Font(font, font.Style | FontStyle.Underline);
        }
        private void toolStripButtonColor_Click(object sender, EventArgs e)
        {
            var content = this.getContent();
            ColorDialog f = new ColorDialog();
            if (f.ShowDialog() == DialogResult.OK)
            {
                content.SelectionColor = f.Color;
            }
            content.Focus();
        }
        private void toolStripButtonBullets_Click(object sender, EventArgs e)
        {
            var content = this.getContent();
            content.SelectionBullet = !content.SelectionBullet;
        }
        private void toolStripButtonOutdent_Click(object sender, EventArgs e)
        {
            var content = this.getContent();
            content.SelectionIndent -= 20;
        }
        private void toolStripButtonIndent_Click(object sender, EventArgs e)
        {
            var content = this.getContent();
            content.SelectionIndent += 20;
        }
        private void toolStripButtonLeft_Click(object sender, EventArgs e)
        {
            var content = this.getContent();
            content.SelectionAlignment = HorizontalAlignment.Left;
        }
        private void toolStripButtonCenter_Click(object sender, EventArgs e)
        {
            var content = this.getContent();
            content.SelectionAlignment = HorizontalAlignment.Center;
        }
        private void toolStripButtonRight_Click(object sender, EventArgs e)
        {
            var content = this.getContent();
            content.SelectionAlignment = HorizontalAlignment.Right;
        }
        private void toolStripButtonFull_Click(object sender, EventArgs e)
        {
            var doc = this.getDocStructure();
            doc.SetJustifyAlignment();
        }
        private void toolStripButtonLine_Click(object sender, EventArgs e)
        {
            return;
            var doc = this.getDocStructure();
            if (doc == null) return;
            var content = this.getContent();
            if (content == null) return;
            LinkDialog link = new LinkDialog();
            link.Txt = content.SelectedText;
            if (link.ShowDialog() == DialogResult.Yes)
            {
                string txt = link.Txt;
                string lk = link.Link;
                doc.InsertLink(txt, lk);
            }
        }
        private void toolStripButtonPicture_Click(object sender, EventArgs e)
        {

        }
        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            this.getContent().Undo();
        }
        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            this.getContent().Redo();
        }
        private void toolStripButtonCut_Click(object sender, EventArgs e)
        {
            this.getContent().Cut();
        }
        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            this.getContent().Copy();
        }
        private void toolStripButtonPaste_Click(object sender, EventArgs e)
        {
            this.getContent().Paste();
        }
        #endregion
        #region 关闭保存
        private void SaveSetting()
        {
            EditorSetting.Default.D = new System.Collections.ArrayList();
            foreach (TabPage tab in MainTabControl.TabPages)
            {
                EditorSetting.Default.D.Add(tab.ToolTipText);
            }
            EditorSetting.Default.SelectionPage = MainTabControl.SelectedIndex;
            if (this.dir.tree.SelectedNode != null)
                EditorSetting.Default.SelectedNode = dir.tree.SelectedNode.ToolTipText;
            EditorSetting.Default.IsExPand = new System.Collections.ArrayList();
            foreach(TreeNode node in dir.tree.Nodes)
            {
                EditorSetting.Default.IsExPand.Add(node.IsExpanded);
            }
            EditorSetting.Default.Save();
        }
        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveSetting();
            foreach (TabPage tab in MainTabControl.TabPages)
            {
                foreach(Control c in tab.Controls)
                {
                    if(c is DocStructure)
                    {
                        if((c as DocStructure).content.Modified)
                        {
                            DialogResult r = MessageBox.Show("还有文件没保存\r\n是否全部保存后关闭窗口", "警告", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                            if (r == DialogResult.Yes)
                            {
                                this.SaveAllFile();
                            }
                            else if(r == DialogResult.Cancel)
                            {
                                e.Cancel = true;
                            }
                            return;
                        }
                    }
                }
            }
        }
        private void SaveAllFile()
        {
            this.timer.Stop();
            this.timer.Dispose();
            this.saveTimer.Stop();
            this.saveTimer.Dispose();
            foreach (TabPage tab in MainTabControl.TabPages)
            {
                foreach (Control c in tab.Controls)
                {
                    if (c is DocStructure)
                    {
                        if ((c as DocStructure).content.Modified)
                        {
                            (c as DocStructure).SaveFile();
                            (c as DocStructure).content.Modified = false;
                        }
                    }
                }
            }
        }
        #endregion

        
    }
}
