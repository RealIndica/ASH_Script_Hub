using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO.Pipes;

namespace ASH_Hub
{
    public partial class Form1 : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
            panel1.BackColor = Color.FromArgb(200, 224, 224, 224);
            panel2.BackColor = Color.FromArgb(200, 224, 224, 224);
            comboBox1.Text = "ProtoSmasher";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            fastColoredTextBox1.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "/Scripts";

            foreach (string s in Directory.GetDirectories(path))
            {
                listBox1.Items.Add(s.Remove(0, path.Length + 1));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();

            DirectoryInfo dinfo = new DirectoryInfo(Application.StartupPath + "/Scripts/" + listBox1.SelectedItem);

            FileInfo[] Files = dinfo.GetFiles("*.txt");
            FileInfo[] Files2 = dinfo.GetFiles("*.lua");

            foreach (FileInfo file in Files)
            {
                listBox2.Items.Add(file.Name);
            }

            foreach (FileInfo file2 in Files2)
            {
                listBox2.Items.Add(file2.Name);
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string script = null;
            using (TextReader tr = File.OpenText(Application.StartupPath + "/Scripts/" + listBox1.SelectedItem + "/" + listBox2.SelectedItem))
            {
                script = tr.ReadToEnd();
            }
            fastColoredTextBox1.Text = script;
        }

        void ExecuteScript(string pipe, string script)
        {
            try
            {
                NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(pipe);
                namedPipeClientStream.Connect(2);
                if (namedPipeClientStream.IsConnected)
                {
                    using (StreamWriter streamWriter = new StreamWriter(namedPipeClientStream))
                    {
                        string text = script;
                        streamWriter.WriteLine(text);
                        streamWriter.Flush();
                    }
                    namedPipeClientStream.Dispose();
                }
                else
                {
                    MessageBox.Show("An error has occured! Sorry . . .");
                }
            }
            catch (Exception)
            {
            }
        }

        private NamedPipeClientStream SeraphPipe;

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "ProtoSmasher")
            {
                ExecuteScript("ProtoSmasherPipe", fastColoredTextBox1.Text);
            }

            if (comboBox1.Text == "Stella")
            {
                ExecuteScript("execution", fastColoredTextBox1.Text);
            }

            if (comboBox1.Text == "Seraph") 
            {
                ExecuteScript("SeraphPipe", fastColoredTextBox1.Text);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Seraph")
            {
                MessageBox.Show("Due to complications with named pipes, in order to execute with Seraph via this UI, Seraph must be closed.");
            }

            if (comboBox1.Text == "Request More . . .")
            {
                Request req = new Request();
                req.Show();
                comboBox1.Text = "ProtoSmasher";
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("- Auto Update\n- Drag n Drop Support\n- Better Support for Seraph\n- More stuff in general\n\nMADE BY CODEX", "To Do List . . .");
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Yet Implemented");
        }
    }
}
