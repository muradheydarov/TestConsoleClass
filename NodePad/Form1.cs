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

namespace NodePad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                var filename = openFileDialog1.FileName;

                using (StreamReader reader = new StreamReader(filename))
                {
                    richTextBox1.Text = reader.ReadToEnd();
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            var result = saveFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                using (StreamWriter streamWriter = new StreamWriter(saveFileDialog1.FileName))
                {
                    streamWriter.WriteLine(richTextBox1.Text);
                }
            }

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.FileName == null)
            {
                saveFileDialog1.ShowDialog();

                using (StreamWriter streamWriter = new StreamWriter(saveFileDialog1.FileName))
                {
                    streamWriter.WriteLine(richTextBox1.Text);
                }
            }
            else
            {
                using (StreamWriter streamWriter = new StreamWriter(openFileDialog1.FileName))
                {
                    streamWriter.WriteLine(richTextBox1.Text);
                }
            }
        }
    }
}
