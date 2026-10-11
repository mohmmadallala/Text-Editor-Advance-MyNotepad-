using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorTextAdvance
{
    public partial class ctrlRewriteFile : UserControl
    {
        public event Action<string> OnSendValue;
        protected virtual void SendPath(string path)
        {
            Action<string> handler = OnSendValue;
            if (handler != null)
            {
                handler(path);
            }
        }
        public ctrlRewriteFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                OnSendValue(textBox1.Text);
                
            }
            else
            {
                label2.Text = "Please Enter Value";
            }

        }

        private void ctrlRewriteFile_Load(object sender, EventArgs e)
        {
            label2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|HTML format *.html|*.html|JSON format *.json|*.json|XML format|*.xml|logs(*.log)|*.log";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;

                
            }

        }
    }
}
