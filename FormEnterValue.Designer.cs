namespace EditorTextAdvance
{
    partial class FormEnterValue
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
            this.button1 = new System.Windows.Forms.Button();
            this.ctrlSendValue1 = new EditorTextAdvance.ctrlSendValue();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Image = global::EditorTextAdvance.Properties.Resources.closeBlack32;
            this.button1.Location = new System.Drawing.Point(535, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 36);
            this.button1.TabIndex = 4;
            this.button1.Text = "  ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ctrlSendValue1
            // 
            this.ctrlSendValue1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ctrlSendValue1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlSendValue1.Location = new System.Drawing.Point(0, 0);
            this.ctrlSendValue1.Name = "ctrlSendValue1";
            this.ctrlSendValue1.Size = new System.Drawing.Size(576, 189);
            this.ctrlSendValue1.TabIndex = 3;
            this.ctrlSendValue1.OnSendValue += new System.Action<string, string>(this.ctrlSendValue1_OnSendValue);
            this.ctrlSendValue1.OnSendValuelINT += new System.Action<int>(this.ctrlSendValue1_OnSendValuelINT);
            this.ctrlSendValue1.Load += new System.EventHandler(this.ctrlSendValue1_Load_1);
            // 
            // FormEnterValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(576, 189);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ctrlSendValue1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEnterValue";
            this.Text = "FormEnterValue";
            this.Load += new System.EventHandler(this.FormEnterValue_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private ctrlSendValue ctrlSendValue1;
        private System.Windows.Forms.Button button1;
    }
}