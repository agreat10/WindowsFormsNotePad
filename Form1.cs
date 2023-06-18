using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsNotePad
{
    
    public partial class Form1 : Form
    {
       
        Font selectedFont;
        FontStyle newFontStyle;
        float newFontSize;
        Color newForeColor;
        bool selectionChanged = false;
        public Form1()
        {
            InitializeComponent();
            statusStrip1.Text = $"Количество символов: 0";            
        }
      
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            StatusLabel1.Text = $"Количество символов: {richTextBox1.Text.Length}";
        }

     

        private void OpenStripButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;
                toolStripStatusLabel1.Text = fileName;
                richTextBox1.Clear();
                richTextBox1.Text = File.ReadAllText(fileName);
                richTextBox1.Focus();                
            }
        }

        private void CloseStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                File.WriteAllText(fileName,richTextBox1.Text);
                toolStripStatusLabel1.Text = fileName;
                richTextBox1.Focus();

            }
        }

        private void CutStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Cut();
            }
        }

        private void CopyStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void PasteStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
            {
                richTextBox1.Paste();
            }
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void CenterButton_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void AlignTextWidth()
        {
            if (richTextBox1.SelectionLength > 0)
            {
                int start = richTextBox1.SelectionStart;
                int end = start + richTextBox1.SelectionLength;

                // Разбиваем выделенный текст на строки
                string[] lines = richTextBox1.SelectedText.Split('\n');

                // Находим самую длинную строку
                int maxLength = 0;
                foreach (string line in lines)
                {
                    if (line.Length > maxLength)
                    {
                        maxLength = line.Length;
                    }
                }

                // Выравниваем каждую строку по ширине
                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    int spacesToAdd = maxLength - line.Length;

                    // Добавляем пробелы в конец строки
                    if (spacesToAdd > 0)
                    {
                        lines[i] = line + new string(' ', spacesToAdd);
                    }
                }

                // Объединяем строки обратно в один текст
                string newText = string.Join("\n", lines);

                // Заменяем выделенный текст на новый
                richTextBox1.SelectedText = newText;

                // Восстанавливаем выделение
                richTextBox1.Select(start, end - start);
            }
        }

        private void JustButton_Click(object sender, EventArgs e)
        {
            AlignTextWidth();
        }

        private void BoltButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Bold);
            }
            
        }

        private void ItalicButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Italic);
            }
        }

        private void UnderButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont, FontStyle.Underline);
            }

        }

        private void DecButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null&& richTextBox1.SelectionFont.Size>5)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily,
                                                       richTextBox1.SelectionFont.Size - 1,
                                                       richTextBox1.SelectionFont.Style);
            }
        }

        private void IncButton_Click(object sender, EventArgs e)
        {
            
            if (richTextBox1.SelectionFont != null && richTextBox1.SelectionFont.Size <72)
            {
                richTextBox1.SelectionFont = new Font(richTextBox1.SelectionFont.FontFamily,
                                                       richTextBox1.SelectionFont.Size + 1,
                                                       richTextBox1.SelectionFont.Style);
            }
        }

        private void UpperButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
            {
                richTextBox1.SelectedText = richTextBox1.SelectedText.ToUpper();
            }
        }

        private void LowerButton_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
            {
                richTextBox1.SelectedText = richTextBox1.SelectedText.ToLower();
            }

        }
        private void ChangeTextColor(Color color)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                int selectionStart = richTextBox1.SelectionStart;
                int selectionLength = richTextBox1.SelectionLength;

                richTextBox1.SelectionColor = color;
                richTextBox1.Select(selectionStart, selectionLength);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                ChangeTextColor(colorDialog.Color);
            }
        }

        private void fontStripButton3_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                // Изменяем шрифт выделенного текста
                richTextBox1.SelectionFont = fontDialog.Font;
            }
        }

        private void FormatMenuItem_Click(object sender, EventArgs e)
        {
            selectionChanged = true;
            if (richTextBox1.SelectedText != "")
            {
                selectedFont = richTextBox1.SelectionFont;
                newFontStyle = selectedFont.Style;
                newFontSize = selectedFont.Size;

                // Скопировать свойства цвета текста
                newForeColor = richTextBox1.SelectionColor;
            }
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (selectionChanged)
                {
                    // Применить свойства к другому выделенному тексту
                    richTextBox1.SelectionFont = new Font(selectedFont.FontFamily, newFontSize, newFontStyle);
                    richTextBox1.SelectionColor = newForeColor;
                }
                selectionChanged = false;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            TraverseMenuItems(menuStrip1.Items,true);
            //// Установка цветов для тёмной темы
            //this.BackColor = Color.FromArgb(160, 160, 160);
            //this.ForeColor = Color.White;


            menuStrip1.BackColor = Color.FromArgb(160, 160, 160);
            menuStrip1.ForeColor = Color.White;
            toolStrip1.BackColor = Color.FromArgb(160, 160, 160);
            toolStrip1.ForeColor = Color.White;
            statusStrip1.BackColor = Color.FromArgb(160, 160, 160);
            statusStrip1.ForeColor = Color.White;
            CloseStripMenuItem.BackColor = Color.FromArgb(160, 160, 160);
            CloseStripMenuItem.ForeColor = Color.White;
            tableLayoutPanel1.BackColor = Color.FromArgb(160, 160, 160);
            tableLayoutPanel1.ForeColor = Color.White;
            richTextBox1.BackColor = Color.FromArgb(191, 191, 191);
            //richTextBox1.ForeColor = Color.White;


        }
        private void TraverseMenuItems(ToolStripItemCollection items, bool color)
        {
            foreach (ToolStripItem control in items)
            {
                if(color)
                {  
                    // Обработка элемента меню
                    control.BackColor = Color.FromArgb(160, 160, 160);
                    control.ForeColor = Color.White;
                }
                else
                {
                    control.BackColor = SystemColors.Control;
                    control.ForeColor = SystemColors.ControlText;
                }
              

                // Если элемент является подменю, вызываем функцию TraverseMenuItems рекурсивно
                if (control is ToolStripMenuItem subMenu)
                {
                    TraverseMenuItems(subMenu.DropDownItems, color);
                }
            }
        }

        // Пример использования        TraverseMenuItems(menuStrip1.Items);


        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            TraverseMenuItems(menuStrip1.Items, false);
            // Установка цветов для светлой темы           
            menuStrip1.BackColor = SystemColors.Control;
            menuStrip1.ForeColor = SystemColors.ControlText;
            toolStrip1.BackColor = SystemColors.Control;
            toolStrip1.ForeColor = SystemColors.ControlText;
            statusStrip1.BackColor = SystemColors.Control;
            statusStrip1.ForeColor = SystemColors.ControlText;
            CloseStripMenuItem.BackColor = SystemColors.Control;
            CloseStripMenuItem.ForeColor = SystemColors.ControlText;
            tableLayoutPanel1.BackColor = SystemColors.Control;
            tableLayoutPanel1.ForeColor = SystemColors.ControlText;
            richTextBox1.BackColor = SystemColors.Control;
            //richTextBox1.ForeColor = SystemColors.ControlText;          


        }
    }
}
