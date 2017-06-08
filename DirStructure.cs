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
using System.Collections;
using System.Threading;
using System.Globalization;

namespace Editor
{
    public class TreeViewRemovedArgs : EventArgs
    {
        public TreeNode node;
    }
    public partial class DirStructure : UserControl
    {
        public TreeNode FindNode(string toolTipText)
        {
            foreach(TreeNode node in tree.Nodes)
            {
                foreach(TreeNode n in node.Nodes)
                {
                    if (n.ToolTipText == toolTipText)
                        return n;
                }
            }
            return null;
        }
        public delegate void ArticleRemovedHandle(object sender, TreeViewRemovedArgs e);
        public event ArticleRemovedHandle ArticleRemoved;
        public delegate void TreeNodeDeleteHandle(object sender, TreeViewRemovedArgs e);
        public event TreeNodeDeleteHandle TreeNodeDelete; 
        #region 拖拽
        private int width = 0;//tree保存tree的宽度
        private Point p;//鼠标按下位置
        private bool hasImage;//鼠标按下的位置是否有箭头图片
        #endregion
        #region Tree目录
        internal string currDirectory;
        #endregion
        public DirStructure()
        {
            InitializeComponent();
        }
        #region 拖拽
        private void picture_MouseUp(object sender, MouseEventArgs e)
        {
            if (!this.CheckMouseEnterImage()) return;
            if (tree.Width > picture.Width)
            {
                this.Width = picture.Width;
                picture.Image = global::Editor.Properties.Resources.right1;
            }
            else
            {
                if (Math.Abs(this.Width - width) < 5)
                {
                    this.Width = 250;
                }
                else
                    this.Width = width;
                picture.Image = global::Editor.Properties.Resources.left1;
            }
        }
        private void DirStructure_SizeChanged(object sender, EventArgs e)
        {
            if (this.Width > picture.Width) width = this.Width;
        }
        private void picture_MouseEnter(object sender, EventArgs e)
        {
            picture.BackColor = Color.DarkGray;
        }
        private void picture_MouseLeave(object sender, EventArgs e)
        {
            picture.BackColor = SystemColors.Control;
            this.Cursor = Cursors.Default;
        }
        private void picture_MouseDown(object sender, MouseEventArgs e)
        {
            if (CheckMouseEnterImage())
                hasImage = true;
            else
                hasImage = false;
            p.X = e.X;
            p.Y = e.Y;
        }
        private void picture_MouseMove(object sender, MouseEventArgs e)
        {
            if (CheckMouseEnterImage())
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.SizeWE;
            if (e.Button == MouseButtons.Left && !this.CheckMouseEnterImage() && !hasImage)
            {
                var w = this.Width + e.X - p.X;
                if (w > picture.Width && w < (Screen.PrimaryScreen.WorkingArea.Width - p.X))
                {
                    this.Width = w;
                    picture.Image = global::Editor.Properties.Resources.left1;
                }
                else if (w > (Screen.PrimaryScreen.WorkingArea.Width - p.X))
                    picture.Image = global::Editor.Properties.Resources.left1;
                else
                    picture.Image = global::Editor.Properties.Resources.right1;
            }
        }
        private bool CheckMouseEnterImage()
        {
            int x = 0;
            int h = 20 * 100 / 58;
            int y = (picture.Height / 2) - (h / 2);
            int w = 20;
            Rectangle rect = new Rectangle(x, y, w, h);
            return rect.Contains(picture.PointToClient(MousePosition));
        }
        #endregion
        public class DirComparer : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                //比较字符串大小
                return x.ToString().CompareTo(y.ToString());
            }
        }
        internal void InitializeDirectory()
        {
            currDirectory = System.Windows.Forms.Application.StartupPath;
            var dir = currDirectory + "\\Books";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);//没有目录就创建目录

            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            CultureInfo StrokCi = new CultureInfo(133124);
            Thread.CurrentThread.CurrentCulture = StrokCi;
            ArrayList dirs = new ArrayList(dirInfo.GetDirectories());
            dirs.Sort(new DirComparer());
            foreach (DirectoryInfo d in dirs)
            {
                TreeNode node = new TreeNode(d.Name);
                node.ToolTipText = d.FullName;
                var secDirecory = new DirectoryInfo(d.FullName);
                ArrayList al = new ArrayList(secDirecory.GetFiles());
                al.Sort(new DirComparer());
                foreach (FileInfo file in al)
                {
                    TreeNode secNode = new TreeNode(file.Name);//.TrimEnd(new char[] { '.', 't', 'x', 't' }));
                    secNode.ToolTipText = file.FullName;
                    node.Nodes.Add(secNode);
                }
                tree.Nodes.Add(node);
            }
        }
        private void tree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
            if (e.Button != MouseButtons.Right)
                return;
            tree.SelectedNode = e.Node;
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem add = new ToolStripMenuItem("添加书籍");
            add.Click += Add_Click;
            menu.Items.Add(add);
            if(e.Node == null)
            {
                menu.Items.Add(new ToolStripSeparator());
                ToolStripMenuItem refresh1 = new ToolStripMenuItem("刷新");
                refresh1.Click += Refresh_Click;
                menu.Items.Add(refresh1);
                menu.Show(tree, e.X, e.Y);
                return;
            }
            if (e.Node.Parent == null)
            {
                ToolStripMenuItem rename = new ToolStripMenuItem("修改书籍名称");
                ToolStripMenuItem delete = new ToolStripMenuItem("删除书籍");
                ToolStripMenuItem addArticle = new ToolStripMenuItem("添加章节");
                rename.Click += Rename_Click;
                delete.Click += Delete_Click;
                addArticle.Click += AddArticle_Click;
                menu.Items.Add(new ToolStripSeparator());
                menu.Items.Add(rename);
                menu.Items.Add(delete);
                menu.Items.Add(new ToolStripSeparator());
                menu.Items.Add(addArticle);
            }
            else
            {
                ToolStripMenuItem addArticle = new ToolStripMenuItem("添加章节");
                ToolStripMenuItem renameArticle = new ToolStripMenuItem("修改章节名称");
                ToolStripMenuItem deleteArticle = new ToolStripMenuItem("删除章节");
                addArticle.Click += AddArticle_Click;
                renameArticle.Click += RenameArticle_Click;
                deleteArticle.Click += DeleteArticle_Click;
                menu.Items.Add(new ToolStripSeparator());
                menu.Items.Add(addArticle);
                menu.Items.Add(renameArticle);
                menu.Items.Add(deleteArticle);
            }
            menu.Items.Add(new ToolStripSeparator());
            ToolStripMenuItem refresh = new ToolStripMenuItem("刷新");
            refresh.Click += Refresh_Click;
            menu.Items.Add(refresh);
            menu.Show(tree, e.X, e.Y);
        }
        private void Refresh_Click(object sender, EventArgs e)
        {
            this.tree.Nodes.Clear();
            this.InitializeDirectory();
        }

        private void tree_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            TreeNode node = tree.GetNodeAt(e.X, e.Y);
            TreeNodeMouseClickEventArgs tncea = new TreeNodeMouseClickEventArgs(node, e.Button, e.Clicks, e.X, e.Y);
            tree_NodeMouseClick(sender, tncea);
        }
        private void DeleteArticle_Click(object sender, EventArgs e)
        {
            if (tree.SelectedNode != null)
            {
                if (MessageBox.Show(string.Format("是否删除  [{0}]", tree.SelectedNode.Text), "警告信息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    ArticleRemoved(tree, new TreeViewRemovedArgs() { node = tree.SelectedNode });
                }
            }
        }
        private void RenameArticle_Click(object sender, EventArgs e)
        {
            tree.LabelEdit = true;
            tree.SelectedNode.Tag = "Article";
            tree.SelectedNode.BeginEdit();
        }
        private void AddArticle_Click(object sender, EventArgs e)
        {
            TreeNode node = tree.SelectedNode;
            if (node.Parent != null)
                node = node.Parent;
            TreeNode articleNode = node.Nodes.Add("新的章节.txt");
            node.ExpandAll();
            articleNode.Tag = "Article";
            tree.LabelEdit = true;
            tree.SelectedNode = articleNode;
            articleNode.BeginEdit();
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            if(tree.SelectedNode != null)
            {
                if(MessageBox.Show(string.Format("是否删除  <<{0}>>  书籍,以及里面的所有文章?",tree.SelectedNode.Text),"警告信息", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)== DialogResult.Yes)
                {
                    if (Directory.Exists(tree.SelectedNode.ToolTipText))
                    {
                        Directory.Delete(tree.SelectedNode.ToolTipText, true);
                        TreeNodeDelete(tree, new TreeViewRemovedArgs() { node = tree.SelectedNode });
                        tree.Nodes.Remove(tree.SelectedNode);
                    }
                }
            }
        }
        private void Rename_Click(object sender, EventArgs e)
        {
            tree.LabelEdit = true;
            tree.SelectedNode.Tag = "Book";
            tree.SelectedNode.BeginEdit();
        }
        private void Add_Click(object sender, EventArgs e)
        {
            TreeNode node = tree.Nodes.Add("新的书籍");
            node.Tag = "Book";
            tree.LabelEdit = true;
            node.BeginEdit();
        }
        private void tree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Node.Tag.ToString() == "Book")
            {
                if (!string.IsNullOrEmpty(e.Node.ToolTipText))
                {//重命名书籍
                    if (e.Node.Text == e.Label || e.Label == null) return;
                    DirectoryInfo dir = new DirectoryInfo(e.Node.ToolTipText);
                    e.Node.ToolTipText = e.Node.ToolTipText.Replace(e.Node.Text, e.Label);
                    if (!Directory.Exists(e.Node.ToolTipText))
                    {
                        dir.MoveTo(e.Node.ToolTipText);
                        
                    }
                    tree.LabelEdit = false;
                }
                else
                {//添加书籍
                    if (e.Label == null)
                    {
                        tree.Nodes.Remove(e.Node);
                        tree.LabelEdit = false;
                        return;
                    }
                    if (currDirectory != null)
                    {
                        var path = string.Format("{0}\\Books\\{1}", currDirectory, e.Label);
                        if (Directory.Exists(path))
                        {
                            MessageBox.Show("已存在同名书籍");
                            tree.Nodes.Remove(e.Node);
                            tree.LabelEdit = false;
                            return;
                        }
                        e.Node.ToolTipText = path;
                        Directory.CreateDirectory(path);
                    }
                    tree.LabelEdit = false;
                }
            }
        }
        private void tree_KeyUp(object sender,KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F2)
            {
                if (tree.SelectedNode.Parent == null)
                    Rename_Click(tree, new EventArgs());
                else
                    RenameArticle_Click(tree, new EventArgs());
            }
            else if (e.KeyCode == Keys.Delete)
            {
                if (tree.SelectedNode.Parent == null)
                    Delete_Click(tree, new EventArgs());
                else
                    DeleteArticle_Click(tree, new EventArgs());
            }
        }        
    }
}
