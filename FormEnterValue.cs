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
    public partial class FormEnterValue : Form
    {
        public delegate void OnsendValue(string str,string str2);
        public event OnsendValue SendValue;
        public delegate void CloseForm();

        public event CloseForm OnCloseForm;

        private bool Show;

        public FormEnterValue(bool show=false)
        {
            Show = show;
            InitializeComponent();
          
        }

       
        private void FormEnterValue_Load(object sender, EventArgs e)
        {

        }

        private void ctrlSendValue1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlSendValue1_Load_1(object sender, EventArgs e)
        {

        }

        private void ctrlSendValue1_OnSendValuel(string arg1, string arg2)
        {
            if (!string.IsNullOrEmpty(arg1)&&!string.IsNullOrEmpty(arg2))
            {
                SendValue?.Invoke(arg1,arg2);
                
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
