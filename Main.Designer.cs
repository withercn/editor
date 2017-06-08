namespace Editor
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.menu = new System.Windows.Forms.MenuStrip();
            this.文件FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nFile = new System.Windows.Forms.ToolStripMenuItem();
            this.rMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.statusBar = new System.Windows.Forms.ToolStrip();
            this.status = new System.Windows.Forms.ToolStripLabel();
            this.total = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.charLine = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.nowTimer = new System.Windows.Forms.ToolStripLabel();
            this.toolStripBar = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripComboBoxName = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripComboBoxSize = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonBold = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonItalic = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUnderline = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorFont = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonBullets = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOutdent = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonIndent = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorFormat = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonLeft = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCenter = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonFull = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparatorAlign = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPicture = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCopy = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPaste = new System.Windows.Forms.ToolStripButton();
            this.text = new System.Windows.Forms.ToolStripButton();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.dir = new Editor.DirStructure();
            this.menu.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.toolStripBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // notify
            // 
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            this.notify.Text = "notifyIcon1";
            this.notify.Visible = true;
            // 
            // menu
            // 
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件FToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(857, 28);
            this.menu.TabIndex = 5;
            this.menu.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            this.文件FToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nFile});
            this.文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            this.文件FToolStripMenuItem.Size = new System.Drawing.Size(69, 24);
            this.文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // nFile
            // 
            this.nFile.Name = "nFile";
            this.nFile.Size = new System.Drawing.Size(129, 26);
            this.nFile.Text = "新文件";
            this.nFile.Click += new System.EventHandler(this.nFile_Click);
            // 
            // rMenu
            // 
            this.rMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.rMenu.Name = "rMenu";
            this.rMenu.Size = new System.Drawing.Size(67, 4);
            // 
            // statusBar
            // 
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.total,
            this.toolStripSeparator5,
            this.charLine,
            this.toolStripSeparator6,
            this.nowTimer});
            this.statusBar.Location = new System.Drawing.Point(0, 555);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(857, 25);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "toolStrip1";
            // 
            // status
            // 
            this.status.Margin = new System.Windows.Forms.Padding(0, 1, 500, 2);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(39, 22);
            this.status.Text = "准备";
            // 
            // total
            // 
            this.total.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.total.AutoSize = false;
            this.total.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.total.Margin = new System.Windows.Forms.Padding(1, 1, 0, 2);
            this.total.Name = "total";
            this.total.Size = new System.Drawing.Size(100, 22);
            this.total.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // charLine
            // 
            this.charLine.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.charLine.AutoSize = false;
            this.charLine.Margin = new System.Windows.Forms.Padding(1, 1, 1, 2);
            this.charLine.Name = "charLine";
            this.charLine.Size = new System.Drawing.Size(100, 22);
            this.charLine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // nowTimer
            // 
            this.nowTimer.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.nowTimer.Name = "nowTimer";
            this.nowTimer.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.nowTimer.Size = new System.Drawing.Size(0, 22);
            this.nowTimer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // toolStripBar
            // 
            this.toolStripBar.GripMargin = new System.Windows.Forms.Padding(5);
            this.toolStripBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStripBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator3,
            this.toolStripComboBoxName,
            this.toolStripComboBoxSize,
            this.toolStripSeparator1,
            this.toolStripButtonBold,
            this.toolStripButtonItalic,
            this.toolStripButtonUnderline,
            this.toolStripButtonColor,
            this.toolStripSeparatorFont,
            this.toolStripButtonBullets,
            this.toolStripButtonOutdent,
            this.toolStripButtonIndent,
            this.toolStripSeparatorFormat,
            this.toolStripButtonLeft,
            this.toolStripButtonCenter,
            this.toolStripButtonRight,
            this.toolStripButtonFull,
            this.toolStripSeparatorAlign,
            this.toolStripButtonLine,
            this.toolStripButtonPicture,
            this.toolStripSeparator2,
            this.toolStripButtonUndo,
            this.toolStripButtonRedo,
            this.toolStripSeparator4,
            this.toolStripButtonCut,
            this.toolStripButtonCopy,
            this.toolStripButtonPaste,
            this.text});
            this.toolStripBar.Location = new System.Drawing.Point(0, 28);
            this.toolStripBar.Name = "toolStripBar";
            this.toolStripBar.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.toolStripBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripBar.Size = new System.Drawing.Size(857, 40);
            this.toolStripBar.TabIndex = 3;
            this.toolStripBar.Text = "Tool Bar";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripComboBoxName
            // 
            this.toolStripComboBoxName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxName.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.toolStripComboBoxName.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStripComboBoxName.MaxDropDownItems = 30;
            this.toolStripComboBoxName.Name = "toolStripComboBoxName";
            this.toolStripComboBoxName.Size = new System.Drawing.Size(132, 28);
            this.toolStripComboBoxName.SelectedIndexChanged += new System.EventHandler(this.FontSet);
            // 
            // toolStripComboBoxSize
            // 
            this.toolStripComboBoxSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxSize.DropDownWidth = 55;
            this.toolStripComboBoxSize.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.toolStripComboBoxSize.Name = "toolStripComboBoxSize";
            this.toolStripComboBoxSize.Size = new System.Drawing.Size(75, 28);
            this.toolStripComboBoxSize.SelectedIndexChanged += new System.EventHandler(this.FontSet);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButtonBold
            // 
            this.toolStripButtonBold.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBold.Image = global::Editor.Properties.Resources.toolStripButtonBold_Image;
            this.toolStripButtonBold.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBold.Name = "toolStripButtonBold";
            this.toolStripButtonBold.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonBold.Tag = "Bold";
            this.toolStripButtonBold.Text = "加粗";
            this.toolStripButtonBold.Click += new System.EventHandler(this.toolStripButtonBold_Click);
            // 
            // toolStripButtonItalic
            // 
            this.toolStripButtonItalic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonItalic.Image = global::Editor.Properties.Resources.toolStripButtonItalic_Image;
            this.toolStripButtonItalic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonItalic.Name = "toolStripButtonItalic";
            this.toolStripButtonItalic.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonItalic.Text = "倾斜";
            this.toolStripButtonItalic.Click += new System.EventHandler(this.toolStripButtonItalic_Click);
            // 
            // toolStripButtonUnderline
            // 
            this.toolStripButtonUnderline.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUnderline.Image = global::Editor.Properties.Resources.toolStripButtonUnderline_Image;
            this.toolStripButtonUnderline.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUnderline.Name = "toolStripButtonUnderline";
            this.toolStripButtonUnderline.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonUnderline.Text = "下划线";
            this.toolStripButtonUnderline.Click += new System.EventHandler(this.toolStripButtonUnderline_Click);
            // 
            // toolStripButtonColor
            // 
            this.toolStripButtonColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonColor.Image = global::Editor.Properties.Resources.toolStripButtonColor_Image;
            this.toolStripButtonColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonColor.Name = "toolStripButtonColor";
            this.toolStripButtonColor.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonColor.Text = "字体";
            this.toolStripButtonColor.Click += new System.EventHandler(this.toolStripButtonColor_Click);
            // 
            // toolStripSeparatorFont
            // 
            this.toolStripSeparatorFont.Name = "toolStripSeparatorFont";
            this.toolStripSeparatorFont.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButtonBullets
            // 
            this.toolStripButtonBullets.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonBullets.Image = global::Editor.Properties.Resources.toolStripButtonBullets_Image;
            this.toolStripButtonBullets.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonBullets.Name = "toolStripButtonBullets";
            this.toolStripButtonBullets.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonBullets.Text = "项目编号";
            this.toolStripButtonBullets.Click += new System.EventHandler(this.toolStripButtonBullets_Click);
            // 
            // toolStripButtonOutdent
            // 
            this.toolStripButtonOutdent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOutdent.Image = global::Editor.Properties.Resources.toolStripButtonOutdent_Image;
            this.toolStripButtonOutdent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOutdent.Name = "toolStripButtonOutdent";
            this.toolStripButtonOutdent.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonOutdent.Text = "减少缩进";
            this.toolStripButtonOutdent.Click += new System.EventHandler(this.toolStripButtonOutdent_Click);
            // 
            // toolStripButtonIndent
            // 
            this.toolStripButtonIndent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonIndent.Image = global::Editor.Properties.Resources.toolStripButtonIndent_Image;
            this.toolStripButtonIndent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonIndent.Name = "toolStripButtonIndent";
            this.toolStripButtonIndent.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonIndent.Text = "增加缩进";
            this.toolStripButtonIndent.Click += new System.EventHandler(this.toolStripButtonIndent_Click);
            // 
            // toolStripSeparatorFormat
            // 
            this.toolStripSeparatorFormat.Name = "toolStripSeparatorFormat";
            this.toolStripSeparatorFormat.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButtonLeft
            // 
            this.toolStripButtonLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLeft.Image = global::Editor.Properties.Resources.toolStripButtonLeft_Image;
            this.toolStripButtonLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLeft.Name = "toolStripButtonLeft";
            this.toolStripButtonLeft.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonLeft.Text = "文本左对齐";
            this.toolStripButtonLeft.Click += new System.EventHandler(this.toolStripButtonLeft_Click);
            // 
            // toolStripButtonCenter
            // 
            this.toolStripButtonCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCenter.Image = global::Editor.Properties.Resources.toolStripButtonCenter_Image;
            this.toolStripButtonCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCenter.Name = "toolStripButtonCenter";
            this.toolStripButtonCenter.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonCenter.Text = "居中";
            this.toolStripButtonCenter.Click += new System.EventHandler(this.toolStripButtonCenter_Click);
            // 
            // toolStripButtonRight
            // 
            this.toolStripButtonRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRight.Image = global::Editor.Properties.Resources.toolStripButtonRight_Image;
            this.toolStripButtonRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRight.Name = "toolStripButtonRight";
            this.toolStripButtonRight.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonRight.Text = "文本右对齐";
            this.toolStripButtonRight.Click += new System.EventHandler(this.toolStripButtonRight_Click);
            // 
            // toolStripButtonFull
            // 
            this.toolStripButtonFull.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonFull.Image = global::Editor.Properties.Resources.toolStripButtonFull_Image;
            this.toolStripButtonFull.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonFull.Name = "toolStripButtonFull";
            this.toolStripButtonFull.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonFull.Text = "两端对齐";
            this.toolStripButtonFull.Click += new System.EventHandler(this.toolStripButtonFull_Click);
            // 
            // toolStripSeparatorAlign
            // 
            this.toolStripSeparatorAlign.Name = "toolStripSeparatorAlign";
            this.toolStripSeparatorAlign.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButtonLine
            // 
            this.toolStripButtonLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonLine.Image = global::Editor.Properties.Resources.toolStripButtonLine_Image;
            this.toolStripButtonLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLine.Name = "toolStripButtonLine";
            this.toolStripButtonLine.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonLine.Text = "插入水平线";
            this.toolStripButtonLine.Click += new System.EventHandler(this.toolStripButtonLine_Click);
            // 
            // toolStripButtonPicture
            // 
            this.toolStripButtonPicture.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPicture.Image = global::Editor.Properties.Resources.toolStripButtonPicture_Image;
            this.toolStripButtonPicture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPicture.Name = "toolStripButtonPicture";
            this.toolStripButtonPicture.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonPicture.Text = "插入图片";
            this.toolStripButtonPicture.Click += new System.EventHandler(this.toolStripButtonPicture_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButtonUndo
            // 
            this.toolStripButtonUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUndo.Image = global::Editor.Properties.Resources.toolStripButtonUndo_Image;
            this.toolStripButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUndo.Name = "toolStripButtonUndo";
            this.toolStripButtonUndo.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonUndo.Text = "撤消";
            this.toolStripButtonUndo.Click += new System.EventHandler(this.toolStripButtonUndo_Click);
            // 
            // toolStripButtonRedo
            // 
            this.toolStripButtonRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRedo.Image = global::Editor.Properties.Resources.toolStripButtonRedo_Image;
            this.toolStripButtonRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRedo.Name = "toolStripButtonRedo";
            this.toolStripButtonRedo.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonRedo.Text = "重做";
            this.toolStripButtonRedo.Click += new System.EventHandler(this.toolStripButtonRedo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripButtonCut
            // 
            this.toolStripButtonCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCut.Image = global::Editor.Properties.Resources.toolStripButtonCut_Image;
            this.toolStripButtonCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCut.Name = "toolStripButtonCut";
            this.toolStripButtonCut.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonCut.Text = "剪切";
            this.toolStripButtonCut.Click += new System.EventHandler(this.toolStripButtonCut_Click);
            // 
            // toolStripButtonCopy
            // 
            this.toolStripButtonCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCopy.Image = global::Editor.Properties.Resources.toolStripButtonCopy_Image;
            this.toolStripButtonCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopy.Name = "toolStripButtonCopy";
            this.toolStripButtonCopy.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonCopy.Text = "复制";
            this.toolStripButtonCopy.Click += new System.EventHandler(this.toolStripButtonCopy_Click);
            // 
            // toolStripButtonPaste
            // 
            this.toolStripButtonPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPaste.Image = global::Editor.Properties.Resources.toolStripButtonPaste_Image;
            this.toolStripButtonPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPaste.Name = "toolStripButtonPaste";
            this.toolStripButtonPaste.Size = new System.Drawing.Size(24, 25);
            this.toolStripButtonPaste.Text = "粘贴";
            this.toolStripButtonPaste.Click += new System.EventHandler(this.toolStripButtonPaste_Click);
            // 
            // text
            // 
            this.text.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.text.Image = ((System.Drawing.Image)(resources.GetObject("text.Image")));
            this.text.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.text.Name = "text";
            this.text.Size = new System.Drawing.Size(24, 25);
            this.text.Text = "toolStripButton1";
            this.text.Click += new System.EventHandler(this.text_Click);
            // 
            // MainTabControl
            // 
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(250, 68);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(607, 487);
            this.MainTabControl.TabIndex = 6;
            this.MainTabControl.TabStop = false;
            this.MainTabControl.SelectedIndexChanged += new System.EventHandler(this.MainTabControl_TabIndexChanged);
            // 
            // dir
            // 
            this.dir.Dock = System.Windows.Forms.DockStyle.Left;
            this.dir.Location = new System.Drawing.Point(0, 68);
            this.dir.Margin = new System.Windows.Forms.Padding(0);
            this.dir.Name = "dir";
            this.dir.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.dir.Size = new System.Drawing.Size(250, 487);
            this.dir.TabIndex = 7;
            this.dir.TabStop = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 580);
            this.Controls.Add(this.MainTabControl);
            this.Controls.Add(this.dir);
            this.Controls.Add(this.toolStripBar);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "写作工具";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.toolStripBar.ResumeLayout(false);
            this.toolStripBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem 文件FToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip rMenu;
        private System.Windows.Forms.ToolStrip statusBar;
        private System.Windows.Forms.ToolStripLabel status;
        private System.Windows.Forms.ToolStrip toolStripBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxName;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonBold;
        private System.Windows.Forms.ToolStripButton toolStripButtonItalic;
        private System.Windows.Forms.ToolStripButton toolStripButtonUnderline;
        private System.Windows.Forms.ToolStripButton toolStripButtonColor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorFont;
        private System.Windows.Forms.ToolStripButton toolStripButtonBullets;
        private System.Windows.Forms.ToolStripButton toolStripButtonOutdent;
        private System.Windows.Forms.ToolStripButton toolStripButtonIndent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorFormat;
        private System.Windows.Forms.ToolStripButton toolStripButtonLeft;
        private System.Windows.Forms.ToolStripButton toolStripButtonCenter;
        private System.Windows.Forms.ToolStripButton toolStripButtonRight;
        private System.Windows.Forms.ToolStripButton toolStripButtonFull;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorAlign;
        private System.Windows.Forms.ToolStripButton toolStripButtonLine;
        private System.Windows.Forms.ToolStripButton toolStripButtonPicture;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolStripButtonUndo;
        private System.Windows.Forms.ToolStripButton toolStripButtonRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripButtonCut;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopy;
        private System.Windows.Forms.ToolStripButton toolStripButtonPaste;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxSize;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.ToolStripMenuItem nFile;
        private DirStructure dir;
        private System.Windows.Forms.ToolStripButton text;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel charLine;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripLabel total;
        private System.Windows.Forms.ToolStripLabel nowTimer;
    }
}