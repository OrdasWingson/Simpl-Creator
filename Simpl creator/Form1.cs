using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simpl_creator
{
    public partial class Form1 : Form
    {
        string name;
        string path;
        public Form1()
        {
            InitializeComponent();
            name = "NewCommand";
            this.Text = name;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void собратьРешениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(name != "")
            {
                System.Diagnostics.Process.Start("SML Studio.exe", name);
            }
            else
            {
                MessageBox.Show("фаил не сохранен");
            }

            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                desighn();
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.DefaultExt = "sc";
            OPF.Filter = "Simpl(*.sc)|*.sc";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                name = OPF.FileName;
                using (StreamReader streamReader = new StreamReader(OPF.FileName))
                {
                    richTextBox1.Clear();
                    while (!streamReader.EndOfStream)
                    {
                        richTextBox1.Text += streamReader.ReadLine() + Environment.NewLine;
                    }
                }
                desighn();
                this.Text = name;
                path = OPF.FileName;
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog SPF = new SaveFileDialog();
            SPF.DefaultExt = "sc";
            SPF.Filter = "Simpl(*.sc)|*.sc";
            if (SPF.ShowDialog() == DialogResult.OK)
            {
                this.Text = name = SPF.FileName;
                path = SPF.FileName;
                using (StreamWriter writer = new StreamWriter(SPF.FileName))
                {
                    writer.Write(richTextBox1.Text);
                }

            }
                       

        }

        private void новоеРешениеSimplToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            this.Text = "NewCommand";
        }

        private void desighn()
        {
            var currentSelStart = richTextBox1.SelectionStart; //получаем начальную позицию
            var currentSelLength = richTextBox1.SelectionLength; //получаем конечную позицию

            richTextBox1.SelectAll(); //выделяем весь текст
            richTextBox1.SelectionColor = SystemColors.WindowText;

            var matches = Regex.Matches(richTextBox1.Text, @"let|goto|flag|print|input|if|rem|end|var|text");
            foreach (var match in matches.Cast<Match>())
            {
                if (match.ToString() == "rem")
                {
                    int lineIndex = richTextBox1.GetLineFromCharIndex(match.Index);
                    richTextBox1.Select(match.Index, richTextBox1.Lines[lineIndex].Length);
                    richTextBox1.SelectionColor = Color.Green;
                }
                else
                {
                    richTextBox1.Select(match.Index, match.Length);
                    richTextBox1.SelectionColor = Color.Blue;
                }

            }

            richTextBox1.Select(currentSelStart, currentSelLength);
            richTextBox1.SelectionColor = SystemColors.WindowText;
        }

        private void сохранитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(richTextBox1.Text);
            }
        }
    }
}
