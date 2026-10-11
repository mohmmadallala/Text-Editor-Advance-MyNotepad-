using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EditorTextAdvance
{
    public partial class FormSreachReguler : Form
    {
        public delegate void DeleCloseform();
        public event DeleCloseform CloseForm;
        public delegate void DeleSearchValue(string str,bool Reverse,bool Casesenstive);
        public event DeleSearchValue SearchValue;
        public FormSreachReguler()
        {
            InitializeComponent();
        }

        private void FormSreachReguler_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
          
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {

                    SearchValue(textBox1.Text, false, checkBox1.Checked);
                }
                

            }
        }

        private void FormSreachReguler_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseForm();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
