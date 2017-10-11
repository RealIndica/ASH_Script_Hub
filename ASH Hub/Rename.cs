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
    public partial class Rename : Form
    {
        private Form1 parent;

        string cat;

        public Rename(Form1 form, string Category)
        {
            InitializeComponent();
            parent = form;
            cat = Category;
        }

        private void Rename_Load(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            Directory.Move(Application.StartupPath + "/Scripts/" + cat + "/", Application.StartupPath + "/Scripts/" + textBox1.Text + "/");
            parent.RefreshAll();
            this.Close();
        }
    }
}
