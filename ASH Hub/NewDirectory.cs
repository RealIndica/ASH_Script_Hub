using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ASH_Hub
{
    public partial class NewDirectory : Form
    {
        private Form1 parent;

        public NewDirectory(Form1 form)
        {
            InitializeComponent();
            parent = form;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void NewDirectory_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 16;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(Application.StartupPath + "/Scripts/" + textBox1.Text + "/");
            parent.RefreshAll();
            this.Close();
        }
    }
}
