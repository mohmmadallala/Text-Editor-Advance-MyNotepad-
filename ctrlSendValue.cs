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
    public partial class ctrlSendValue : UserControl
    {

        public event Action<string,string> OnSendValuel;
        protected virtual void SendValue(string str,string Newstr)
        {
            Action<string,string> handler = OnSendValuel;
            if (handler != null)
            {
                handler(str,Newstr);
            }
        }
        bool ShowToRepalce;
        public ctrlSendValue()
        {
            InitializeComponent();
            label3.Visible = false;
            textBox2.Visible = false;
            ShowToRepalce = false;
            if (ShowToRepalce)
            {
                label3.Visible = true;
                textBox2.Visible = true;
            }
        }

        public ctrlSendValue(bool show)
        {
            InitializeComponent();
            label3.Visible = false;
            textBox2.Visible = false;
            ShowToRepalce = show;
            if (show)
            {
                label3.Visible = true;
                textBox2.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                if (ShowToRepalce)
                {
                    if (!string.IsNullOrEmpty(textBox2.Text))
                    {
                        SendValue(textBox1.Text, textBox2.Text);
                    }
                    else
                    {
                        label2.Text = "Please Enter Value In TextBox";
                    }
                }
                else
                {
                    SendValue(textBox1.Text, "");
                }
            }
            else
            {
                label2.Text = "Please Enter Value In TextBox";
            }
        }

        private void ctrlSendValue_Load(object sender, EventArgs e)
        {

        }
    }
}
