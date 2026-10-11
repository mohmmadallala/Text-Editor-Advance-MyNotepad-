using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorTextAdvance
{
    public partial class FormEnterPath : Form
    {
        public delegate void OnSendPath(string path);

        public event OnSendPath SendPath;
        public FormEnterPath()
        {
            InitializeComponent();
        }

        private void ctrlRewriteFile1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlRewriteFile1_OnSendValue(string obj)
        {
            if (File.Exists(obj))
            {
                SendPath?.Invoke(obj);
                this.Close();
            }
            else
            {
                label2.Text = "Path Not found in System";
            }
        }

        private void FormEnterPath_Load(object sender, EventArgs e)
        {

        }
    }
}
