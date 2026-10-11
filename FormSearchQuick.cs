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
    public partial class FormSearchQuick : Form
    {
        public delegate void OnchangeText(string str,bool Casesenstive);
        public event OnchangeText ChangeText;
        public FormSearchQuick()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length >= 1)
            {
                ChangeText(textBox1.Text, checkBox1.Checked);
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                ChangeText(textBox1.Text,checkBox1.Checked);
            }
        }
    }
}
