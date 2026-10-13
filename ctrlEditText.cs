using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace EditorTextAdvance
{
    public partial class ctrlEditText : UserControl
    {
        public string containfile {

            get
            {
                return richTextBox1.Text;
            }
            set
            {
                if (value != null)
                {
                    richTextBox1.Text = "";
                    string[] list = value.Split('\n');
                    foreach (string line in list)
                    {
                        richTextBox1.AppendText(line + Environment.NewLine);
                    }
                }
            }
        }
        public StringBuilder strb=new StringBuilder("");
        FormSreachReguler formSearch;
        FormEnterValue formReplace;
        FormSreachReguler formSearchAndDetailes;
        FormSearchQuick formSearchqui;

        private string ContainToReturn { get; set; }


        public string changeColor
        {
            get
            {
                return richTextBox1.BackColor.Name;
            }
            set
            {
                // تحويل النص إلى لون مباشرة
                Color selectedColor = Color.FromName(value);

                // تطبيق الخلفية على الأدوات
                richTextBox1.BackColor = selectedColor;
                textBox1.BackColor = selectedColor;
                groupBox1.BackColor = selectedColor;

                // تحديد لون الخط بناءً على ما إذا كان اللون أسود أو داكناً
                if (value.Equals("Black", StringComparison.OrdinalIgnoreCase) || selectedColor.GetBrightness() < 0.2f)
                {
                    groupBox1.ForeColor = Color.White;
                    textBox1.ForeColor = Color.White;
                    richTextBox1.ForeColor = Color.White;
                    checkBox1.ForeColor = Color.White;

                    textBox1.Enabled = true;
                    textBox1.ReadOnly = true;
                }
                else
                {
                    groupBox1.ForeColor = Color.Black;
                    textBox1.ForeColor = Color.Black;
                    richTextBox1.ForeColor = Color.Black;
                    checkBox1.ForeColor = Color.Black;
                }

                // تحديث لون كل النصوص داخل الـ RichTextBox لتتوافق مع الثيم الجديد
                if (richTextBox1.TextLength > 0)
                {
                    int selStart = richTextBox1.SelectionStart;
                    int selLen = richTextBox1.SelectionLength;

                    richTextBox1.SelectAll();
                    richTextBox1.SelectionColor = richTextBox1.ForeColor; // جعل النص أبيض أو أسود حسب الثيم

                    // إعادة المؤشر لمكانه الطبيعي وإلغاء التحديد الأزرق
                    richTextBox1.Select(selStart, selLen);
                }
            }
        }
        public event Action<string> NameTextFileChanged;
        public event Action CloseTabPageAndSave;
        protected virtual void OncloseTabPageAndSave()
        {
            Action handler = CloseTabPageAndSave;
            if (handler != null)
            {
                handler();
            }
        }
        public event Action CloseTabPageAndNotSave;
        protected virtual void onCloseTabPageAndNotSave()
        {
            Action handler = CloseTabPageAndNotSave;
            if (handler != null)
            {
                handler();
            }
        }
        public event Action<string> CreateNewTab;
        public clsFileIO file { get; set; }
        private int counter = 0;
        protected virtual void OnCreateNewTab(string path="???")
        {
            Action<string> handler = CreateNewTab;
            if (handler != null)
            {
                handler(path);
            }
        }
        protected virtual void OnNameTextFileChanged(string newName)
        {
            Action<string> handler = NameTextFileChanged;
            if(handler != null)
            {
                handler(newName);
            }
        }
        public ctrlEditText()
        {
            InitializeComponent();
        }

        private void ctrlEditText_Load(object sender, EventArgs e)
        {

            textBox1.ReadOnly = true;
            capitalLetterFirstLetterToolStripMenuItem.Checked = false;
            capitalLetterToolStripMenuItem.Checked = false;
            smallLetterToolStripMenuItem.Checked = false;
            returnTextOriginalToolStripMenuItem.Checked = false;
            lbLastupdate.Text = DateTime.Now.ToString("yyyy-MMM-dd | hh:mm:ss");
            lbLines.Text = "1";
            lbWordcount.Text = "0";
            lbcharcount.Text = "0";
         
             FocusTextBox();
            formSearch = new FormSreachReguler();

            formSearch.SearchValue += EventSreachReguler;
            formSearch.CloseForm += EventCloseFormToColorText;

            formReplace = new FormEnterValue(true);
            formReplace.SendValue += SendValueToReplceWord;

            formSearchAndDetailes = new FormSreachReguler();
            formSearchAndDetailes.SearchValue += EventSearchAndDetailes;
            formSearchAndDetailes.CloseForm += EventCloseFormToColorText;

            formSearchqui = new FormSearchQuick();
            formSearchqui.ChangeText += EventChangeText;
            // ربط أحداث التمرير والتغيير لتحديث الأرقام لحظياً
            richTextBox1.BackColor = Color.FromName(clsGlobalSetting.value);
            textBox1.BackColor= Color.FromName(clsGlobalSetting.value); 
            groupBox1.BackColor= Color.FromName(clsGlobalSetting.value);

            if (richTextBox1.BackColor == Color.Black)
            {
                textBox1.ReadOnly = true;
                textBox1.ForeColor = Color.WhiteSmoke ;
                groupBox1.ForeColor = Color.White;
                richTextBox1.ForeColor = Color.White;
                checkBox1.ForeColor = Color.White;
              
            }
            else
            {
                groupBox1.ForeColor = Color.Black;
                textBox1.ForeColor = Color.Black;
                richTextBox1.ForeColor = Color.Black;
                checkBox1.ForeColor = Color.Black;
            }

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog1.Title = "Save New File";
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.AddExtension = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog1.FileName;

                File.WriteAllText(filePath, richTextBox1.Text);

                file = clsFileIO.FindFile(filePath);
                OnNameTextFileChanged(filePath);
               
                RefreashMetaData();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnCreateNewTab();
        }
        public void RefreashMetaData()
        {
            int counter = 0;
            int counterword = 0;
            string[] Lines = richTextBox1.Text.Split(new[] {'\n' });
            foreach (string i in Lines)
            {
                if (i.Length!=0)
                {
                    counter += i.Trim().Length;
                    string[] listwords = i.Split(' ');
                    foreach (string word in listwords)
                    {

                        if (word != "")

                        {

                            counterword++;
                        }
                    }
                }
            }
            lbLines.Text = richTextBox1.Text.Split('\n').Length.ToString();
            lbcharcount.Text = counter.ToString();
            lbWordcount.Text = counterword.ToString();
            if (file != null)
            {
                lbLastupdate.Text = file.LastUpdate.ToString("yyyy-MMM-dd | hh:mm:ss");
            }
            else
            {
                lbLastupdate.Text=DateTime.Now.ToString("yyyy-MMM-dd | hh:mm:ss");
            }
        }

        private void Implementforamting(object sender,EventArgs e)
        {
            
          
            
           
          
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void showUnderBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showUnderBarToolStripMenuItem.Checked)
            {
                groupBox1.Visible = true;
            }
            else
            {
                groupBox1.Visible = false;
            }
        }

        private void capitalLetterFirstLetterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            returnTextOriginalToolStripMenuItem.Checked = false;
            capitalLetterToolStripMenuItem.Checked = false;
            smallLetterToolStripMenuItem.Checked = false;
            if (richTextBox1.SelectionLength == 0)
            {
                richTextBox1.SelectAll();
            }
            string Temp = richTextBox1.SelectedText;
                richTextBox1.SelectedText = "";
                string[] lines = Temp.Split(new[] { '\n' }, StringSplitOptions.None);
                foreach (string i in lines)
                {
                if (i.Length > 0)
                {
                    string[] listwords = i.Split(' ');
                    foreach (string word in listwords)
                    {


                        if (word != "")

                        {
                            string firstLetter = word.Substring(0, 1).ToUpper();
                            string restOfString = word.Substring(1).ToLower();
                            Temp = Temp.Replace(word, firstLetter + restOfString);

                        }
                    }
                }
               

            }
            richTextBox1.SelectedText += Temp;


        }


        private void smallLetterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            capitalLetterFirstLetterToolStripMenuItem.Checked = false;
            capitalLetterToolStripMenuItem.Checked = false;
            returnTextOriginalToolStripMenuItem.Checked = false;
         
                if (richTextBox1.SelectionLength == 0)
                {
                    richTextBox1.SelectAll();
                }
                richTextBox1.SelectedText = richTextBox1.SelectedText.ToLower();
            
           
        
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.SelectionLength = 0;

        }


        private void capitalLetterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            capitalLetterFirstLetterToolStripMenuItem.Checked = false;
            smallLetterToolStripMenuItem.Checked = false;
            returnTextOriginalToolStripMenuItem.Checked = false;
            
            
                if (richTextBox1.SelectionLength == 0)
                {
                    richTextBox1.SelectAll();
                }
                richTextBox1.SelectedText = richTextBox1.SelectedText.ToUpper();
               
            
       
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.SelectionLength = 0;

        }

        private void lTRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();

            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void rTLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void formatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (counter == 0 && !string.IsNullOrEmpty(richTextBox1.Text)) 
            {
                ContainToReturn = richTextBox1.Text;
                counter++;
            }
        }

        private void returnTextOriginalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            capitalLetterFirstLetterToolStripMenuItem.Checked = false;
            smallLetterToolStripMenuItem.Checked = false;
            capitalLetterToolStripMenuItem.Checked = false;
            richTextBox1.Text = ContainToReturn;
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength <= 0)
            {
                richTextBox1.SelectAll();
            }
            Clipboard.SetText(richTextBox1.SelectedText);

        }
        public void FocusTextBox()
        {
            richTextBox1.Focus();
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText=Clipboard.GetText();
        }

        private void selectAllTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectedText="";
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo)
            {
                richTextBox1.Undo();
            }
           
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                return;
            }
            string str = richTextBox1.SelectedText;
            if (str.Length <= 0)
            {
                richTextBox1.SelectAll();
            }
            Clipboard.SetText(richTextBox1.SelectedText);
            richTextBox1.SelectedText = "";
        }
        private void EventArrivalePathReWrite(string path)
        {
            file = clsFileIO.FindFile(path);
            if (file != null)
            {
                file.ReWriteFile(richTextBox1.Text);
                MessageBox.Show($"Successfully Rewrite on file {file.FileName}");
            }
        }

        private void EventArrivalePathAppend(string path)
        {
            file = clsFileIO.FindFile(path);
            if (file != null)
            {
                file.AppendFile(richTextBox1.Text);
                MessageBox.Show($"Successfully Rewrite on file {file.FileName}");
            }
        }
        private void reWriteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEnterPath f = new FormEnterPath();
            f.SendPath += EventArrivalePathReWrite;
            f.ShowDialog();
            
        }
        private void numberingLines() {
            
            //if (richTextBox1.Text.Length == 0)
            //{
            //    return;
            //}
            //textBox1.Text = "";
            //for(int i = 0; i < richTextBox1.Lines.Length; i++)
            //{
            //    textBox1.Text += (i + 1).ToString() + Environment.NewLine;
            //}
            int firstIndex = richTextBox1.GetCharIndexFromPosition(Point.Empty);
            int firstLine = richTextBox1.GetLineFromCharIndex(firstIndex);

            // 2. معرفة فهرس آخر حرف ظاهر في أسفل الـ RichTextBox
            Point clientBottomRight = new Point(richTextBox1.ClientSize.Width, richTextBox1.ClientSize.Height);
            int lastIndex = richTextBox1.GetCharIndexFromPosition(clientBottomRight);
            if (lastIndex < 0) lastIndex = richTextBox1.Text.Length;
            int lastLine = richTextBox1.GetLineFromCharIndex(lastIndex);
            int totalLines = richTextBox1.Lines.Length;
            int digits = totalLines.ToString().Length;

            // ضبط العرض تلقائياً بناءً على عدد الخانات (يمكنك تعديل الأرقام حسب حجم الخط لديك)
            if (digits == 1)
            {
                textBox1.Width = 20; // عرض صغير للأرقام من 1 إلى 9
            }
            else if (digits == 2)
            {
                textBox1.Width = 30; // توسيع قليل للأرقام من 10 إلى 99
            }
            else if (digits == 3)
            {
                textBox1.Width = 40; // توسيع أكبر للأرقام من 100 فصاعداً
               
                
            }
            else
            {
                textBox1.Width = 55;
            }

            // 3. بناء أرقام الأسطر الظاهرة فقط ووضعها في الـ TextBox الخاص بالأرقام
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = firstLine; i <= lastLine && i < richTextBox1.Lines.Length; i++)
            {
                sb.AppendLine((i + 1).ToString());
            }

            textBox1.Text = sb.ToString();
        }
        private void lockedHashs(bool val)
        {
            textBox2.Enabled = val;
            textBox3.Enabled = val;
            textBox4.Enabled = val;
        }
        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            Implementforamting(sender, e);
            RefreashMetaData();
            numberingLines();
            lockedHashs(false);
        }

        private void appendTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEnterPath f = new FormEnterPath();
            f.SendPath += EventArrivalePathAppend;
            f.ShowDialog();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            selectAllTextToolStripMenuItem_Click(sender, e);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undoToolStripMenuItem1_Click(sender, e);
       }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem1_Click(sender, e);
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cutToolStripMenuItem1_Click(sender,e);
        }

        private void clearTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text="";
        }
        private void EventArrivaleValue(string str,string str2)
        {
            if (file != null)
            {
                
                ContainToReturn = containfile;
                counter++;
                file.ReplaceWord("", str, false, false);
                richTextBox1.Text = "";
                richTextBox1.Text = file.DumpingContain();
                
            }
        }
        private void EventArrivaleValueAllWords(string str,string str2)
        {
            if (file != null)
            {
                ContainToReturn = containfile;
                counter++;
                file.ReplaceWord("", str, true, false);
                richTextBox1.Text = "";
                richTextBox1.Text = file.DumpingContain();

            }
        }

        private void firstWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(file!= null){
                FormEnterValue f = new FormEnterValue();
                f.SendValue += EventArrivaleValue;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("You Must select File existed in System OR Save in New file");
            }
        }

        private void allWordSemlirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file != null)
            {
                FormEnterValue f = new FormEnterValue();
                f.SendValue += EventArrivaleValueAllWords;
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("You Must select File existed in System OR Save in New file");
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file == null)
            {
                MessageBox.Show("Not used this Feature you must Select file from system");
                return;
            }
            else
            {
                file.ReWriteFile(richTextBox1.Text);
            }
        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file != null&&file.IsExited())
            {
                if(MessageBox.Show("Do You Delete File from System ?","Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    File.Delete(file.FilePath);
                    file = null;
                    OncloseTabPageAndSave();
                }
            
             
            }
            else
            {
                MessageBox.Show("Not used this Feature you must Select file from system");
                return;
            }
        }

        private void createFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Files (*.txt)|*.txt|HTML format *.html|*.html|JSON format *.json|*.json|XML format|*.xml|logs(*.log)|*.log";
            saveFileDialog1.Title = "Save New File";
            saveFileDialog1.DefaultExt = "txt";
            saveFileDialog1.AddExtension = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog1.FileName;
                using (StreamWriter w = new StreamWriter(filePath, false))
                {
                    w.Write("");
                }
                if (MessageBox.Show("Do you Open File ?","Open file", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
                    OnCreateNewTab(filePath);

                }
               
                
                
            }
        }

        private void closeWithoutSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onCloseTabPageAndNotSave();
        }

        private void closeWithSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OncloseTabPageAndSave();
        }

        public void ResetCursorTextinStarted()
        {
            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = 0;
            richTextBox1.Focus();
            richTextBox1.ScrollToCaret();
        }
        public void ResetCursorTextinLasted()
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.SelectionLength = 0;
            richTextBox1.Focus();
            richTextBox1.ScrollToCaret();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file == null||this.Tag.ToString()=="???")
            {
                openFileDialog1.Filter = "Text Files (*.txt)|*.txt|HTML format *.html|*.html|JSON format *.json|*.json|XML format|*.xml|logs(*.log)|*.log";

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    OnNameTextFileChanged((openFileDialog1.FileName));
                    ResetCursorTextinStarted();
                    this.Tag = openFileDialog1.FileName;
                }
                
            }
            else
            {
                MessageBox.Show("Your Already Selected File ");
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void clearWordToolStripMenuItem_Click(object sender, EventArgs e)
        {

            firstWordToolStripMenuItem.Enabled = false;
            allWordSemlirToolStripMenuItem.Enabled = false;
            if (file != null)
            {
                saveToolStripMenuItem_Click(sender, e);
                firstWordToolStripMenuItem.Enabled = true;
                allWordSemlirToolStripMenuItem.Enabled = true;
            }
               
            
        }

        private void clearWordToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            clearWordToolStripMenuItem_Click(sender,e);
        }

        private void SendValueToSearchWord(string str,string str2)
        {
            

        }
        private void EventSreachReguler(string str,bool reverse,bool casesenstive)
        {
            formSearch.Hide();
            richTextBox1.Focus();
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = Color.FromName(clsGlobalSetting.value);
            string namefile = "temp.txt";
            int cursorindex = 0;
            if (file == null)
            {
                namefile = "temp.txt";

                if (File.Exists(namefile))
                {
                    File.WriteAllText(namefile, richTextBox1.Text);
                }
                else
                {
                    File.Create(namefile);
                    File.WriteAllText(namefile, richTextBox1.Text);
                }
            }
            else
            {
                if (counter == 0)
                {
                    counter++;
                    ContainToReturn = file.DumpingContain();
                }
                file.ReWriteFile(richTextBox1.Text);
                namefile = file.FilePath;
            }

            file = clsFileIO.FindFile(namefile);
            if (file != null)
            {

                var location = file.ReturnLocation(str, reverse, casesenstive);
                int line = location.Item1;
                int indexstart = location.Item2;
                int lastindex = location.Item3;
                int countingcharinText = location.Item4;
                if (line == -1 || indexstart == -1 || lastindex == -1 || countingcharinText == -1)
                {
                    if (!formSearch.Visible)
                        formSearch.Show();

                    formSearch.BringToFront();
                    return;
                }
                while (line != -1 && indexstart != -1 && lastindex != -1 && countingcharinText != -1)
                {
                    // استخدام طريقة GetFirstCharIndexFromLine الأضمن لتحديد الموضع بالبكسل والحرف بدقة 100%
                    int targetLineIndex = line - 1; // لأن الأسطر تبدأ من 0

                    if (targetLineIndex >= 0 && targetLineIndex < richTextBox1.Lines.Length)
                    {
                        // حساب الموضع الفعلي بالحروف استناداً إلى مكان السطر في الأداة مباشرة
                        int globalIndex = richTextBox1.GetFirstCharIndexFromLine(targetLineIndex) + indexstart;

                        // التركيز على الأداة لضمان ظهور التلوين والتحديد
                        richTextBox1.Focus();

                        // تطبيق التحديد والتلوين
                        richTextBox1.SelectionStart = globalIndex;
                        richTextBox1.SelectionLength = str.Length;
                        richTextBox1.SelectionBackColor = Color.Yellow;
                        file.ReplaceWord("****", str, false, false);
                        cursorindex = globalIndex;
                        if(MessageBox.Show("Do you Search Next Word ?","Search Next", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes){
                            location = file.ReturnLocation(str, reverse, casesenstive);
                            line = location.Item1;
                            indexstart = location.Item2;
                            lastindex = location.Item3;
                            countingcharinText = location.Item4;
                        }
                        else
                        {
                            line = -1;
                        }
                        richTextBox1.SelectAll();
                        richTextBox1.SelectionBackColor = richTextBox1.BackColor;


                    }


                }
                richTextBox1.Focus();
                richTextBox1.SelectionStart = cursorindex ;
                richTextBox1.SelectionLength = 0;
                richTextBox1.ScrollToCaret();
                richTextBox1.Focus();


                file.ReplaceWord(str, "****", true, false);
                formSearch.Show();
                return;

            }
        }
        private void EventCloseFormToColorText()
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = richTextBox1.BackColor;
        }
        private void searchToWordToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (formSearch == null || formSearch.IsDisposed)
            {
                formSearch = new FormSreachReguler();

                formSearch.SearchValue += EventSreachReguler;
                formSearch.CloseForm += EventCloseFormToColorText;

            }


           // formSearch.MdiParent = clsGlobalform.MainForm;
            formSearch.StartPosition = FormStartPosition.Manual;
            formSearch.Location = new Point(1000, 20);

            if (!formSearch.Visible)
                formSearch.Show();

            formSearch.BringToFront();


        }
        private void EventChangeText(string str,bool CaseSenstive)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionBackColor = richTextBox1.BackColor;
            richTextBox1.SelectionLength = 0;

            string namefile = "temp.txt";
            int cursorindex = 0;
            if (file == null)
            {
                namefile = "temp.txt";

                if (File.Exists(namefile))
                {
                    File.WriteAllText(namefile, richTextBox1.Text);
                }
                else
                {
                    File.Create(namefile);
                    File.WriteAllText(namefile, richTextBox1.Text);
                }
            }
            else
            {
                if (counter == 0)
                {
                    counter++;
                    ContainToReturn = file.DumpingContain();
                }
                file.ReWriteFile(richTextBox1.Text);
                namefile = file.FilePath;
            }

            file = clsFileIO.FindFile(namefile);
            if (file != null)
            {

                var location = file.ReturnLocation(str,false,CaseSenstive);
                int line = location.Item1;
                int indexstart = location.Item2;
                int lastindex = location.Item3;
                int countingcharinText = location.Item4;
                if (line == -1 || indexstart == -1 || lastindex == -1 || countingcharinText == -1)
                {
                    if (!formSearchqui.Visible)
                        formSearchqui.Show();

                    formSearchqui.BringToFront();
                    return;
                    
                }
                while (line != -1 && indexstart != -1 && lastindex != -1 && countingcharinText != -1)
                {
                    // استخدام طريقة GetFirstCharIndexFromLine الأضمن لتحديد الموضع بالبكسل والحرف بدقة 100%
                    int targetLineIndex = line - 1; // لأن الأسطر تبدأ من 0

                    if (targetLineIndex >= 0 && targetLineIndex < richTextBox1.Lines.Length)
                    {
                        // حساب الموضع الفعلي بالحروف استناداً إلى مكان السطر في الأداة مباشرة
                        int globalIndex = richTextBox1.GetFirstCharIndexFromLine(targetLineIndex) + indexstart;

                        // التركيز على الأداة لضمان ظهور التلوين والتحديد
                        richTextBox1.Focus();

                        // تطبيق التحديد والتلوين
                        richTextBox1.SelectionStart = globalIndex;
                        richTextBox1.SelectionLength = str.Length;
                        richTextBox1.SelectionBackColor = Color.Yellow;
                        file.ReplaceWord("****", str, false, false);
                        cursorindex = globalIndex;
                        
                            location = file.ReturnLocation(str, false,CaseSenstive);
                            line = location.Item1;
                            indexstart = location.Item2;
                            lastindex = location.Item3;
                            countingcharinText = location.Item4;
                       
                        


                    }


                }
                richTextBox1.Focus();
                richTextBox1.SelectionStart = cursorindex;
                richTextBox1.SelectionLength = 0;
                richTextBox1.ScrollToCaret();
                richTextBox1.Focus();


                file.ReplaceWord(str, "****", true, false);
                if (!formSearchqui.Visible)
                {
                    formSearchqui.Show();
                }
                formSearchqui.BringToFront();
                

            }
        }
        private void searchQuickToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formSearchqui == null || formSearchqui.IsDisposed)
            {
                FormSearchQuick f = new FormSearchQuick();
                f.ChangeText += EventChangeText;
            }
            formSearchqui.StartPosition = FormStartPosition.Manual;
            formSearchqui.Location = new Point(1000, 20);
            
            if (!formSearchqui.Visible)
            {
                formSearchqui.Show();
            }
            formSearchqui.BringToFront();
         

        }

        private void fontDisplayToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;
            fontDialog1.Color = richTextBox1.ForeColor;
            
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                if (richTextBox1.SelectionLength == 0)
                {
                    richTextBox1.SelectAll();
                }
                richTextBox1.SelectionFont = fontDialog1.Font;
                richTextBox1.SelectionColor = fontDialog1.Color;
                colorDialog1.Color = fontDialog1.Color;
                textBox1.Font = fontDialog1.Font;
                textBox1.ForeColor = fontDialog1.Color;
                    
            }
        }

        private void fontColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;
            colorDialog1.AnyColor = true;
            colorDialog1.AllowFullOpen = true;
           
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (richTextBox1.SelectionLength == 0)
                {
                    richTextBox1.SelectAll();
                }
                richTextBox1.SelectionColor = colorDialog1.Color;
                richTextBox1.SelectedText = richTextBox1.SelectedText;

                fontDialog1.Color = colorDialog1.Color;
            }
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            richTextBox1.Font = fontDialog1.Font;
            richTextBox1.ForeColor = fontDialog1.Color;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
          
        }
        private bool ReadOlny = true;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {

                if (ReadOlny)
                {
                    checkBox1.Enabled = true;
                    richTextBox1.ReadOnly = true;
                    richTextBox1.Enabled = true;
                  
                }
                else
                {
                    ReadOlny = true;
                    richTextBox1.ReadOnly = true;
                    foreach (Control i in this.Controls)
                    {
                        i.Enabled = false;
                    }
                    menuStrip1.Enabled = true;
                    toolStripMenuItem2.Enabled = false;
                    searchToolStripMenuItem.Enabled = false;
                    fileToolStripMenuItem.Enabled = false;
                    editToolStripMenuItem.Enabled = false;
                    formatToolStripMenuItem.Enabled = false;
                    toolsToolStripMenuItem.Enabled = false;
                    
                    toolviewMenuItem1.Enabled = true;
                    hiddenContainToolStripMenuItem1.Enabled = true;
                   
                    richTextBox1.Enabled = true;
                    contextMenuToTextBoxMain.Enabled = false;
                }
            }
            else
            {
                richTextBox1.ReadOnly = false;
                foreach (Control i in this.Controls)
                {
                    i.Enabled = true;
                }
                menuStrip1.Enabled = true;
                toolStripMenuItem2.Enabled = true;
                searchToolStripMenuItem.Enabled = !false;
                fileToolStripMenuItem.Enabled = !false;
                editToolStripMenuItem.Enabled =! false;
                formatToolStripMenuItem.Enabled =! false;
                toolsToolStripMenuItem.Enabled = !false;
                contextMenuToTextBoxMain.Enabled = true;
                toolviewMenuItem1.Enabled = true;
                textBox1.Enabled = false; 
            }
        }

        private void backColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            colorDialog1.FullOpen = true;
            colorDialog1.AnyColor = true;
            colorDialog1.AllowFullOpen = true;

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                if (richTextBox1.SelectedText.Length == 0)
                {
                    richTextBox1.SelectAll();
                }
                richTextBox1.SelectionBackColor = colorDialog1.Color;
                richTextBox1.SelectedText = richTextBox1.SelectedText;

                fontDialog1.Color = colorDialog1.Color;
            }
        }

        private void liftToRightToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void centerlizationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void rightToLiftToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void hiddenContainToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (hiddenContainToolStripMenuItem1.Checked)
            {
                fontColorToolStripMenuItem1.Enabled = false;
                fontDisplayToolStripMenuItem1.Enabled = false;
                backColorToolStripMenuItem1.Enabled = false;
                ContainToReturn = richTextBox1.Text;
                richTextBox1.Text = "";
                richTextBox1.ForeColor = richTextBox1.BackColor;
                checkBox1.Checked = true;
                checkBox1.Enabled = false;
                ReadOlny = false;
                checkBox1_CheckedChanged(sender, e);
            }
            else
            {
                fontDisplayToolStripMenuItem1.Enabled = true;
                fontColorToolStripMenuItem1.Enabled = true;
                backColorToolStripMenuItem1.Enabled = true;
                richTextBox1.ForeColor = colorDialog1.Color;
                richTextBox1.Text = ContainToReturn;

                checkBox1.Enabled = true;
                checkBox1.Checked = false;

            }
        }

        private void inseartDateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = richTextBox1.SelectionStart;

            richTextBox1.Text = richTextBox1.Text.Insert(index,"("+DateTime.Now.ToString("yyyy-MMM-dd | hh:mm")+")");

            richTextBox1.SelectionStart = index + DateTime.Now.ToString().Length;
        }
        private void SendValueToReplceWord(string str,string str2)
        {
           
            if (file != null)
            {
                if (file.CountDuplicationWord(str, false) == 0)
                {

                    formReplace.Close();
                    return;
                }
                if (counter == 0)
                {
                    ContainToReturn = containfile;
                    counter++;
                }
                file.ReplaceWord(str2, str, false, false);
               // richTextBox1.Text = "";
                richTextBox1.Text = file.DumpingContain();
            }

        }

        private void replaceWordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (formReplace == null || formReplace.IsDisposed)
            {


                formReplace = new FormEnterValue(true);
                
                formReplace.SendValue += SendValueToReplceWord;
                
            }
            formReplace.StartPosition = FormStartPosition.Manual;
            formReplace.Location = new Point(900,30);
            if (!formReplace.Visible)
            {
                formReplace.Show();
            }
            formReplace.BringToFront();
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                numberingLines();
            }
        }

        private void richTextBox1_Move(object sender, EventArgs e)
        {
            //textBox1.Cursor = richTextBox1.Cursor;

        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            numberingLines();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void showNumberLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!showNumberLinesToolStripMenuItem.Checked)
            {
                textBox1.Visible = false;
            }
            else
            {
                textBox1.Visible = true;
                richTextBox1.Dock = DockStyle.None;
                numberingLines();
            }
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (this.Tag.ToString()=="???")
            {
                openFileToolStripMenuItem.Enabled = true;
                closeWithoutSaveToolStripMenuItem.Enabled =false;
                saveToolStripMenuItem.Enabled = false;
            }
            else
            {
                openFileToolStripMenuItem.Enabled = false;
                closeWithoutSaveToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void formatToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
       
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo)
            {
                richTextBox1.Redo();
            }
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength <= 0)
            {
                richTextBox1.SelectAll();
            }
            richTextBox1.SelectedText = Clipboard.GetText();

        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            redoToolStripMenuItem.Enabled = richTextBox1.CanRedo;
            undoToolStripMenuItem.Enabled = richTextBox1.CanUndo;
        }
        private void EventSearchAndDetailes(string str,bool reverse,bool casesenstive)
        {
            string namefile = "temp.txt";
           
            if (file == null)
            {
                namefile = "temp.txt";

                if (File.Exists(namefile))
                {
                    File.WriteAllText(namefile, richTextBox1.Text);
                }
                else
                {
                    File.Create(namefile);
                    File.WriteAllText(namefile, richTextBox1.Text);
                }
            }
            else
            {
                if (counter == 0)
                {
                    counter++;
                    ContainToReturn = file.DumpingContain();
                }
                file.ReWriteFile(richTextBox1.Text);
                namefile = file.FilePath;
            }

            file = clsFileIO.FindFile(namefile);
            List<string> list = file.GetListLocationsWord(str,casesenstive);
            if (list.Count == 0)
            {
                formSearchAndDetailes.Close();
                MessageBox.Show("Not Found");
                return;
            }
            string AllWords = "";

            foreach(string i in list)
            {
                AllWords += i + "\n" + "-----------------------------------------------------------\n";
            }
            MessageBox.Show($"Count Word [{file.CountDuplicationWord(str,casesenstive)}] \n"
                +AllWords);
        }

        private void searchAndDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formSearchAndDetailes == null || formSearchAndDetailes.IsDisposed) { 
            formSearchAndDetailes = new FormSreachReguler();
            formSearchAndDetailes.SearchValue += EventSearchAndDetailes;
            formSearchAndDetailes.CloseForm += EventCloseFormToColorText; 
            }
            formSearchAndDetailes.StartPosition = FormStartPosition.Manual;
            formSearchAndDetailes.Location = new Point(1000, 20);

            if (!formSearchAndDetailes.Visible)
            {
                formSearchAndDetailes.Show();
            }
            formSearchAndDetailes.BringToFront();
           
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolsToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            if (file == null)
            {
                replaceWordToolStripMenuItem.Enabled = false;
            }
            else
            {
                replaceWordToolStripMenuItem.Enabled = true;
            }
        }

        private void searchToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //if (file == null)
            //{
            //    searchAndDetailsToolStripMenuItem.Enabled = false;
            //    searchQuickToolStripMenuItem.Enabled = false;
            //  //  searchToWordToolStripMenuItem.Enabled = false;
            //}
            //else
            //{
            //    searchToWordToolStripMenuItem.Enabled = true;
            //    searchQuickToolStripMenuItem.Enabled = true;
            //    searchAndDetailsToolStripMenuItem.Enabled = true;
            //}
        }

        private void dumpingContainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string body = Uri.EscapeDataString(richTextBox1.Text);

            string url = $"mailto:?body={body}";

            System.Diagnostics.Process.Start(url);
        
        }

        private void showBasicInfoFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file != null)
            {
                FileInfo Info = new FileInfo(file.FilePath);

                if (!Info.Exists)
                {
                    MessageBox.Show("File does not exist.");
                    return;
                }

                MessageBox.Show(
                    $"File Name: {Info.Name}\n" +
                    $"Extension: {Info.Extension}\n" +
                    $"File Size: {Info.Length:N0} Bytes\n" +
                    $"Created: {Info.CreationTime.ToString("yyyy-MMM-dd | HH:mm")}\n" +
                    $"Last Modified: {Info.LastWriteTime.ToString("yyyy-MMM-dd | HH:mm:ss")}\n" +
                    $"Location: {Info.DirectoryName}",
                    "Basic File Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void showDetailsFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (file != null)
            {
                FileInfo Info = new FileInfo(file.FilePath);
                
                if (!Info.Exists)
                {
                    MessageBox.Show("File does not exist.");
                    return;
                }

                MessageBox.Show(
                    $"Full Path: {Info.FullName}\n" +
                    $"File Attributes: {Info.Attributes}\n" +
                    $"Is Read Only: {Info.IsReadOnly}\n" +
                    $"Creation Time (UTC): {Info.CreationTimeUtc.ToString("yyyy-MMM-dd | HH:mm")}\n" +
                    $"Last Modified (UTC): {Info.LastWriteTimeUtc.ToString("yyyy-MMM-dd | HH:mm:ss")}\n" +
                    $"Last Access Time (UTC): {Info.LastAccessTimeUtc.ToString("yyyy-MMM-dd | HH:mm:ss")}\n" +
                    $"File Size (Bytes): {Info.Length:N0}\n" +
                    $"Directory: {Info.DirectoryName}\n" +
                    $"Is File Exists: {Info.Exists}\n" +
                    $"Is Directory Exists: {Info.Directory?.Exists}",
                    "Advanced File Details",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }

        private void ctrlEditText_BackColorChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = richTextBox1.BackColor;
            groupBox1.BackColor = richTextBox1.BackColor;
            richTextBox1.SelectionLength = 0;
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
           
            
            richTextBox1.BackColor = Color.FromName(clsGlobalSetting.value);
        }

        private void sha512ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SHA512 sha = SHA512.Create())
            {
                byte[] valuehash = sha.ComputeHash(Encoding.UTF8.GetBytes(richTextBox1.Text));
                textBox3.Text = BitConverter.ToString(valuehash).Replace("-","").ToLower();
                textBox3.Enabled = true;
            }
            
            
        }

        private void shaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(SHA256 sha = SHA256.Create())
            {
                byte[] valuehash = sha.ComputeHash(Encoding.UTF8.GetBytes(richTextBox1.Text));
                textBox2.Text = BitConverter.ToString(valuehash).Replace("-","").ToLower();
                textBox2.Enabled = true;
            }
        }

        private void mD5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (MD5 md = MD5.Create())
            {
                byte[] valuehash = md.ComputeHash(Encoding.UTF8.GetBytes(richTextBox1.Text));
                textBox4.Text = BitConverter.ToString(valuehash).Replace("-", "").ToLower();
                textBox4.Enabled = true;
            }
        }
        private void GetKeyEncry(string str1,string s)
        {
            strb.Append(str1);
            clsCyptography.EncryptFile(clsGlobalSetting.PathInput,clsGlobalSetting.PathDestinationEncryption,str1);
        }

        private void encryptionTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobalSetting.PathInput = "temp.txt";
            if (file == null)
            {


                if (File.Exists(clsGlobalSetting.PathInput))
                {
                    File.WriteAllText(clsGlobalSetting.PathInput, richTextBox1.Text);
                }
                else
                {
                    File.Create(clsGlobalSetting.PathInput);
                    File.WriteAllText(clsGlobalSetting.PathInput, richTextBox1.Text);
                }
            }
            else
            {
                if (counter == 0)
                {
                    counter++;
                    ContainToReturn = file.DumpingContain();
                }
                file.ReWriteFile(richTextBox1.Text);
                clsGlobalSetting.PathInput = file.FilePath;
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
              
                    clsGlobalSetting.PathDestinationEncryption=(folderBrowserDialog1.SelectedPath);
                    clsGlobalSetting.PathDestinationEncryption+=("\\" + Path.GetFileNameWithoutExtension(clsGlobalSetting.PathInput) + "_Encry.txt");
                    if (File.Exists(clsGlobalSetting.PathDestinationEncryption))
                    {
                        FormEnterValue f = new FormEnterValue(false);
                        f.SendValue += GetKeyEncry;
                    f.StartPosition = FormStartPosition.Manual;
                    f.Location = new Point(1000, 20);
                    f.Show();
                    }
                    else
                    {
                        using (File.Create(clsGlobalSetting.PathDestinationEncryption))
                        {
                        FormEnterValue f = new FormEnterValue(false);
                        f.SendValue += GetKeyEncry;
                        f.StartPosition = FormStartPosition.Manual;
                        f.Location = new Point(1000, 20);
                        f.Show();
                    }
                    }
                
            }

        }
        private void EventSendPathToEncryption(string str)
        {
            clsGlobalSetting.PathDestinationEncryption = str;
        }
       
        private void GetKeyDcry(string str1, string s)
        {
            strb.Append(str1);
           

           clsCyptography.DecryptFile(clsGlobalSetting.PathDestinationEncryption, clsGlobalSetting.PathDestinationDecryption, str1);
            

          
        }

        private void decryptionTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEnterPath f = new FormEnterPath();
            f.SendPath += EventSendPathToEncryption;
            f.ShowDialog();
            clsGlobalSetting.PathInput = "temp.txt";
            if (file == null)
            {


                if (File.Exists(clsGlobalSetting.PathInput))
                {
                    File.WriteAllText(clsGlobalSetting.PathInput, richTextBox1.Text);
                }
                else
                {
                    File.Create(clsGlobalSetting.PathInput);
                    File.WriteAllText(clsGlobalSetting.PathInput, richTextBox1.Text);
                }
            }
            else
            {
                if (counter == 0)
                {
                    counter++;
                    ContainToReturn = file.DumpingContain();
                }
                file.ReWriteFile(richTextBox1.Text);
                clsGlobalSetting.PathInput = file.FilePath;
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                clsGlobalSetting.PathDestinationDecryption = (folderBrowserDialog1.SelectedPath);
                clsGlobalSetting.PathDestinationDecryption += ("\\" + Path.GetFileNameWithoutExtension(clsGlobalSetting.PathDestinationEncryption) + "_Decry.txt");

                // تم حذف الـ using و File.Create بالكامل من هنا لئلا يحجز الملف
                // لأن دالة التشفير/فك التشفير ستقوم بإنشائه وقت الحاجة تلقائياً

                FormEnterValue fe = new FormEnterValue();
                fe.SendValue += GetKeyDcry;
                fe.StartPosition = FormStartPosition.Manual;
                fe.Location = new Point(1000,20);
                fe.Show(); // فتح نافذة المفتاح بأمان دون حجز أي ملف في الخلفية
            }



        }
    }
}
