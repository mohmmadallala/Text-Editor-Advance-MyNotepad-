namespace EditorTextAdvance
{
    partial class FormMain
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
            this.fontDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aligmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightToLiftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerlizationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.liftToRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hiddenContainToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.MenuContextToMenuStip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.changeFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deletePageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.MenuContextToMenuStip.SuspendLayout();
            this.SuspendLayout();
            // 
            // fontDisplayToolStripMenuItem
            // 
            this.fontDisplayToolStripMenuItem.Name = "fontDisplayToolStripMenuItem";
            this.fontDisplayToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.fontDisplayToolStripMenuItem.Text = "Font Display";
            // 
            // fontColorToolStripMenuItem
            // 
            this.fontColorToolStripMenuItem.Name = "fontColorToolStripMenuItem";
            this.fontColorToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.fontColorToolStripMenuItem.Text = "Font Color";
            // 
            // backColorToolStripMenuItem
            // 
            this.backColorToolStripMenuItem.Name = "backColorToolStripMenuItem";
            this.backColorToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.backColorToolStripMenuItem.Text = "Back Color";
            // 
            // aligmentToolStripMenuItem
            // 
            this.aligmentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rightToLiftToolStripMenuItem,
            this.centerlizationToolStripMenuItem,
            this.liftToRightToolStripMenuItem});
            this.aligmentToolStripMenuItem.Name = "aligmentToolStripMenuItem";
            this.aligmentToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.aligmentToolStripMenuItem.Text = "Aligment";
            // 
            // rightToLiftToolStripMenuItem
            // 
            this.rightToLiftToolStripMenuItem.Name = "rightToLiftToolStripMenuItem";
            this.rightToLiftToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.rightToLiftToolStripMenuItem.Text = "Right To Lift";
            // 
            // centerlizationToolStripMenuItem
            // 
            this.centerlizationToolStripMenuItem.Name = "centerlizationToolStripMenuItem";
            this.centerlizationToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.centerlizationToolStripMenuItem.Text = "Centerlization";
            // 
            // liftToRightToolStripMenuItem
            // 
            this.liftToRightToolStripMenuItem.Name = "liftToRightToolStripMenuItem";
            this.liftToRightToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.liftToRightToolStripMenuItem.Text = "Lift To Right";
            // 
            // hiddenContainToolStripMenuItem
            // 
            this.hiddenContainToolStripMenuItem.CheckOnClick = true;
            this.hiddenContainToolStripMenuItem.Name = "hiddenContainToolStripMenuItem";
            this.hiddenContainToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.hiddenContainToolStripMenuItem.Text = "Hidden Contain";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.CheckOnClick = true;
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontDisplayToolStripMenuItem,
            this.fontColorToolStripMenuItem,
            this.backColorToolStripMenuItem,
            this.aligmentToolStripMenuItem,
            this.hiddenContainToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(55, 24);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // tabControl1
            // 
            this.tabControl1.ContextMenuStrip = this.MenuContextToMenuStip;
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1329, 785);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControl1_Selecting);
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // MenuContextToMenuStip
            // 
            this.MenuContextToMenuStip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuContextToMenuStip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPageToolStripMenuItem,
            this.toolStripMenuItem2,
            this.changeFileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.deletePageToolStripMenuItem,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.exitToolStripMenuItem});
            this.MenuContextToMenuStip.Name = "MenuContextToMenuStip";
            this.MenuContextToMenuStip.Size = new System.Drawing.Size(181, 148);
            this.MenuContextToMenuStip.Opening += new System.ComponentModel.CancelEventHandler(this.MenuContextToMenuStip_Opening);
            // 
            // newPageToolStripMenuItem
            // 
            this.newPageToolStripMenuItem.Name = "newPageToolStripMenuItem";
            this.newPageToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.newPageToolStripMenuItem.Text = "New Page";
            this.newPageToolStripMenuItem.Click += new System.EventHandler(this.newPageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(207, 6);
            // 
            // changeFileToolStripMenuItem
            // 
            this.changeFileToolStripMenuItem.Name = "changeFileToolStripMenuItem";
            this.changeFileToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.changeFileToolStripMenuItem.Text = "Select File";
            this.changeFileToolStripMenuItem.Click += new System.EventHandler(this.changeFileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(207, 6);
            // 
            // deletePageToolStripMenuItem
            // 
            this.deletePageToolStripMenuItem.Name = "deletePageToolStripMenuItem";
            this.deletePageToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.deletePageToolStripMenuItem.Text = "Close Page";
            this.deletePageToolStripMenuItem.Click += new System.EventHandler(this.deletePageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(207, 6);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(210, 24);
            this.toolStripMenuItem4.Text = "Clear All Pages ";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(207, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1321, 747);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Tag = "+";
            this.tabPage2.Text = "    +";
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1329, 785);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.Name = "FormMain";
            this.Text = " ";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.MenuContextToMenuStip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem fontDisplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fontColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aligmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightToLiftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerlizationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem liftToRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hiddenContainToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ContextMenuStrip MenuContextToMenuStip;
        private System.Windows.Forms.ToolStripMenuItem newPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deletePageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

