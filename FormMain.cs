using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EditorTextAdvance
{
    public partial class FormMain : Form
    {
        private bool preventClose = false;
        private int counter = 0;
        public FormMain()
        {
            InitializeComponent();
            counter = 0;
        }

        private void ctrlEditText1_Load(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null &&
                tabControl1.SelectedTab.Controls.Count > 0 &&
                tabControl1.SelectedTab.Controls[0] is ctrlEditText editText)
            {
                editText.FocusTextBox();
            }

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {


        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (tabControl1.TabCount > 10)
            {
                if (tabControl1.SelectedTab != null && tabControl1.SelectedIndex == tabControl1.TabCount - 1)
                {
                    e.Cancel = true;
                    MessageBox.Show("You cannot add more than 10 tabs.", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (tabControl1.SelectedTab != null && tabControl1.SelectedIndex == tabControl1.TabCount - 1)
            {
                int newIndex = tabControl1.TabCount - 1 + counter;
                TabPage newTab = new TabPage("New Page " + newIndex);

                tabControl1.TabPages.Insert(tabControl1.TabCount - 1, newTab);
                tabControl1.SelectedTab = newTab;
                newTab.Tag = "???";
                newTab.BackColor=Color.FromName(clsGlobalSetting.value);
                ctrlEditText ctrl = new ctrlEditText();
                ctrl.Dock = DockStyle.Fill;
                ctrl.Tag = newTab.Tag;
                newTab.Controls.Add(ctrl);
                ctrl.NameTextFileChanged += ctrl_NameTextFileChanged;
                ctrl.CreateNewTab += EventCreateNewTab;
                ctrl.CloseTabPageAndSave += HandlerDeletePage;
                ctrl.CloseTabPageAndNotSave += HandlerDeletePageNotSave;
                ctrl.Load += LoadControlEditor;

            }
            

        }
        private void LoadControlEditor(object sender,EventArgs e)
        {
            ((ctrlEditText)tabControl1.SelectedTab.Controls[0]).FocusTextBox();
        }
        private void EventCreateNewTab(string path="???")
        {

            if (tabControl1.TabCount > 10)
            {
                if (tabControl1.SelectedTab != null)
                {

                    MessageBox.Show("You cannot add more than 10 tabs.", "Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }
            if (tabControl1.SelectedTab != null)
            {
                
                    int newIndex = tabControl1.TabCount-1+counter;
                    TabPage newTab = new TabPage("New Page " + newIndex);
                    newTab.BackColor = Color.FromName(clsGlobalSetting.value);
                    ctrlEditText ctrl = new ctrlEditText();

                    if (File.Exists(path)) {

                        newTab.Text = Path.GetFileName(path);
                        newTab.Tag = path;
                        ctrl.file = clsFileIO.FindFile(path);
                    }
                    else
                    {
                        newTab.Tag = "???";

                    }
                    ctrl.Parent = this;
                    tabControl1.TabPages.Insert(tabControl1.TabCount - 1, newTab);
                    tabControl1.SelectedTab = newTab;
                   
                    
                    ctrl.Dock = DockStyle.Fill;
                    ctrl.Tag = newTab.Tag;
                    newTab.Controls.Add(ctrl);
                    ctrl.NameTextFileChanged += ctrl_NameTextFileChanged;
                    ctrl.CreateNewTab += EventCreateNewTab;
                    ctrl.CloseTabPageAndSave += HandlerDeletePage;
                    ctrl.CloseTabPageAndNotSave += HandlerDeletePageNotSave;
                    ctrl.Load += LoadControlEditor;
                
            }
        }
        private void ctrl_NameTextFileChanged(string newName)
        {

            if (tabControl1.SelectedTab != null)
            {
                tabControl1.SelectedTab.Text = Path.GetFileName(newName);
                tabControl1.SelectedTab.Tag = newName;
                clsFileIO file = clsFileIO.FindFile(newName);
                if (file != null)
                {
                    ((ctrlEditText)tabControl1.SelectedTab.Controls[0]).file = file;
                    ((ctrlEditText)tabControl1.SelectedTab.Controls[0]).containfile = file.DumpingContain();
                }
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= tabControl1.TabPages.Count)
                return;
            TabPage tabPage = tabControl1.TabPages[e.Index];

            bool isSelected = (e.Index == tabControl1.SelectedIndex);

      
            Brush backBrush;
            Brush textBrush;

            if (isSelected)
            {
                backBrush = new SolidBrush(Color.FromName(clsGlobalSetting.value)); // لون التبويب النشط (المحدد)
                textBrush = Brushes.White;                // لون النص للتاب النشط
            }
            else
            {
                backBrush = new SolidBrush(Color.FromName(clsGlobalSetting.value)); // لون التبويبات العادية
                textBrush = Brushes.White ;                  // لون النص للتبويبات العادية
            }

           
            e.Graphics.FillRectangle(backBrush, e.Bounds);

            
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            e.Graphics.DrawString(tabPage.Text, e.Font, textBrush, e.Bounds, sf);
        }

        private void newPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EventCreateNewTab();
        }

        private void HandlerDeletePageSaveOrNotSave(bool save=true)
        {
            if (((ctrlEditText)tabControl1.SelectedTab.Controls[0]).file == null)
            {
                if (tabControl1.SelectedTab != null)
                {
                    int index = tabControl1.SelectedIndex;
                    tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                    counter++;

                    return;
                }
            }
            else
            {
                if (tabControl1.SelectedTab != null)
                {
                    string filePath = ((ctrlEditText)tabControl1.SelectedTab.Controls[0]).file.FilePath;
                    if (File.Exists(filePath))
                    {
                        try
                        {
                            if (MessageBox.Show($"Do You Close this TabPage {tabControl1.SelectedTab.Text} ?", "Close TabPage", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                
                                if (save)
                                {
                                    File.WriteAllText(filePath, ((ctrlEditText)tabControl1.SelectedTab.Controls[0]).containfile);
                                }
                                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
                                counter++;
                            }
                            else
                            {
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error deleting file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("File does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Tab is Null");
                }
            }
        }
        private void HandlerDeletePage()
        {

            HandlerDeletePageSaveOrNotSave(true);
        }
        private void HandlerDeletePageNotSave()
        {

            HandlerDeletePageSaveOrNotSave(false);
        }


        private void deletePageToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (((ctrlEditText)tabControl1.SelectedTab.Controls[0]).file == null)
            {
                HandlerDeletePage();
                return;
            }
            if (!File.Exists(((ctrlEditText)tabControl1.SelectedTab.Controls[0]).file.FilePath))
            {
                ((ctrlEditText)tabControl1.SelectedTab.Controls[0]).file = null;
            }

            if (MessageBox.Show("Do you Save File ?", "Save file", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {

                HandlerDeletePage();
            }
            else
            {
                HandlerDeletePageNotSave();
            }
        }

        private void changeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|HTML format *.html|*.html|JSON format *.json|*.json|XML format|*.xml|logs(*.log)|*.log";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                
                clsFileIO file = clsFileIO.FindFile(filePath);
                if (file == null)
                {
                    return;
                }
                if (((ctrlEditText)tabControl1.SelectedTab.Controls[0]).file != null) 
                {
                    if (MessageBox.Show("Do you Save File ?", "Save file", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        File.WriteAllText(filePath, ((ctrlEditText)tabControl1.SelectedTab.Controls[0]).containfile);
                    }

                }
                ((ctrlEditText)tabControl1.SelectedTab.Controls[0]).file = file;
                ((ctrlEditText)tabControl1.SelectedTab.Controls[0]).containfile = file.DumpingContain();
                ctrl_NameTextFileChanged(file.FilePath);
                ((ctrlEditText)tabControl1.SelectedTab.Controls[0]).ResetCursorTextinStarted();
                ((ctrlEditText)tabControl1.SelectedTab.Controls[0]).RefreashMetaData();
                ((ctrlEditText)tabControl1.SelectedTab.Controls[0]).FocusTextBox();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clsGlobalSetting.value = Registry.GetValue(clsGlobalSetting.key, clsGlobalSetting.name, null) as string;
            clsGlobalform.MainForm = this;
            this.MaximizeBox = false;
            this.WindowState = FormWindowState.Maximized;
            if (counter==0)
            {
                tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
                int newIndex = tabControl1.TabCount;
                TabPage newTab = new TabPage("New Page " + newIndex);
                newTab.BackColor = Color.FromName(clsGlobalSetting.value);
                tabControl1.TabPages.Insert(tabControl1.TabCount - 1, newTab);
                newTab.Tag = "???";
                tabControl1.SelectedTab = newTab;

                ctrlEditText ctrl = new ctrlEditText();
                ctrl.Dock = DockStyle.Fill;
                ctrl.Parent = this;

                ctrl.Tag = newTab.Tag;
                newTab.Controls.Add(ctrl);
                ctrl.NameTextFileChanged += ctrl_NameTextFileChanged;
                ctrl.CreateNewTab += EventCreateNewTab;
                ctrl.CloseTabPageAndSave += HandlerDeletePage;
                ctrl.CloseTabPageAndNotSave += HandlerDeletePageNotSave;
                ctrl.FocusTextBox();
                counter++;
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            foreach (TabPage i in tabControl1.TabPages)
            {
                if (i.Tag.ToString() != "+")
                {

                    tabControl1.SelectedTab = i;
                    counter=1;
                    HandlerDeletePage();
                }
            }

            counter = 1;
        }

        private void MenuContextToMenuStip_Opening(object sender, CancelEventArgs e)
        {
            if (tabControl1.SelectedTab.Tag.ToString() == "???")
            {
                changeFileToolStripMenuItem.Enabled = true;
            }
            else
            {
                changeFileToolStripMenuItem.Enabled = false;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Do You Exit from Application lead to Not Save Files ?", "Exit From System", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                toolStripMenuItem4_Click(sender, e);
                Application.Exit();
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Do You Closing All Pages (without Save Files)? ","Exist System", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
            }
            else
            {
                e.Cancel = true;
            }

        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Registry.SetValue(clsGlobalSetting.key, clsGlobalSetting.name, "DarkGray");
            this.BackColor = Color.FromName("DarkGray");
            clsGlobalSetting.value = "DarkGray";
            
            foreach (TabPage i in tabControl1.TabPages)
                {
                if (i.Tag.ToString() != "+")
                {
                    i.BackColor = Color.FromName("DarkGray"); ;
                    ((ctrlEditText)i.Controls[0]).changeColor = clsGlobalSetting.value;
                }
                }
        }

        private void lightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registry.SetValue(clsGlobalSetting.key, clsGlobalSetting.name, "LightGray");
           
            this.BackColor = Color.FromName("LightGray");
            clsGlobalSetting.value = "LightGray";
            foreach (TabPage i in tabControl1.TabPages)
            {
                if (i.Tag.ToString() != "+")
                {
                    i.BackColor = Color.FromName("LightGray");

                    ((ctrlEditText)i.Controls[0]).changeColor = clsGlobalSetting.value;
                }
            }
        }

        private void costumColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Registry.SetValue(clsGlobalSetting.key, clsGlobalSetting.name, "LightSteelBlue");
            clsGlobalSetting.value = "LightSteelBlue";
            this.BackColor = Color.FromName(clsGlobalSetting.value);
           
            foreach (TabPage i in tabControl1.TabPages)
            {
                if (i.Tag.ToString() != "+")
                {
                    i.BackColor = Color.FromName(clsGlobalSetting.value);

                    ((ctrlEditText)i.Controls[0]).changeColor = clsGlobalSetting.value;
                }
            }
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Registry.SetValue(clsGlobalSetting.key, clsGlobalSetting.name, "Black");
            clsGlobalSetting.value = "Black";
            this.BackColor = Color.FromName(clsGlobalSetting.value);

            foreach (TabPage i in tabControl1.TabPages)
            {
                if (i.Tag.ToString() != "+")
                {
                    i.BackColor = Color.FromName(clsGlobalSetting.value);

                    ((ctrlEditText)i.Controls[0]).changeColor = clsGlobalSetting.value;
                }
            }
        }
    } 
}
