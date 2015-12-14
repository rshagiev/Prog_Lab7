using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextDEmo
{
    public partial class MainForm : Form
    {
        private Data data = new Data();

        public MainForm()
        {
            InitializeComponent();
            Console.WriteLine("It's alive!!!");
            data.loadConfig();
            if (data.fileName != null)
            {
                data.readFromFile();
            }
            if (data.txt != null)
            {
                richTextBox1.Text = data.txt;
            }
            showHistory();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void openFile(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult r = dlg.ShowDialog();
            if (r==DialogResult.OK)
            {
                data.fileName = dlg.FileName;
                data.readFromFile();
                richTextBox1.Text = data.txt;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            data.history.Clear();
            showHistory();
        }

        Match m;
        private void button1_Click(object sender, EventArgs e)
        {
            string q = comboBox1.Text;
            Regex r = new Regex(q,
           (checkBox1.Checked ? RegexOptions.Multiline : 0)
          |(checkBox2.Checked ? RegexOptions.IgnoreCase : 0)
                                );
            m = r.Match(data.txt);
            if (m.Success)
            {
                //if (!data.history.Contains(q))
                {
                    data.history.Add(q);
                    showHistory();
                    comboBox1.Text = q;
                }
            }
            else
                MessageBox.Show("Non found");
            showMatch();

        }

        private void showHistory()
        {
            comboBox1.DataSource = null;
            comboBox1.DataSource
                = data.history.ToArray();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (m != null && m.Success)
            {
                m = m.NextMatch();
                showMatch();
            }

        }

        private void showMatch()
        {
            if (m.Success)
            {
                richTextBox2.Text += "Найдено: " + m.Value + "\n";
                for (int i = 0; i < m.Groups.Count; i++) {
                    richTextBox2.Text +=
                        String.Format("Groups[{0}]={1}\n",
                        i, m.Groups[i]);

                }

                richTextBox1.SelectAll();
                richTextBox1.SelectionBackColor = Color.White;
                richTextBox1.SelectionStart = m.Index;
                richTextBox1.SelectionLength = m.Value.Length;
                richTextBox1.SelectionBackColor = Color.Yellow;
                richTextBox1.ScrollToCaret();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            data.saveConfig();
        }

        private void firstLetters(object sender, EventArgs e)
        {
            data.firstLetters();
            new StatisticsForm(data.firstLetterCount).Show();
        }
    }
}
