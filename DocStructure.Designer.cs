namespace Editor
{
    partial class DocStructure
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel = new System.Windows.Forms.Panel();
            this.content = new Editor.RichTextBoxEx();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(7, 514);
            this.panel1.TabIndex = 2;
            this.panel1.Visible = false;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(0, 514);
            this.panel.TabIndex = 4;
            // 
            // content
            // 
            this.content.AcceptsTab = true;
            this.content.BulletIndent = 30;
            this.content.EnableAutoDragDrop = true;
            this.content.HideSelection = false;
            this.content.Location = new System.Drawing.Point(7, 0);
            this.content.Margin = new System.Windows.Forms.Padding(0);
            this.content.Name = "content";
            this.content.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.content.Size = new System.Drawing.Size(540, 514);
            this.content.TabIndex = 0;
            this.content.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.content_LinkClicked);
            this.content.SelectionChanged += new System.EventHandler(this.Content_SelectionChanged);
            this.content.VScroll += new System.EventHandler(this.content_VScroll);
            this.content.TextChanged += new System.EventHandler(this.content_TextChanged);
            this.content.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Content_KeyDown);
            // 
            // DocStructure
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.content);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DocStructure";
            this.Size = new System.Drawing.Size(547, 514);
            this.Load += new System.EventHandler(this.DocStructure_Load);
            this.VisibleChanged += new System.EventHandler(this.DocStructure_VisibleChanged);
            this.ResumeLayout(false);

        }

        




        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel;
        //internal System.Windows.Forms.RichTextBox content1;
        internal RichTextBoxEx content;
    }
}
