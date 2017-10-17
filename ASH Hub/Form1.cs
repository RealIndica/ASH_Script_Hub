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
using WebSocketSharp.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        [DllImport("USER32.DLL", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, int wParam, string lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

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

        public void RefreshAll()
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();

            string path = Application.StartupPath + "/Scripts";

            foreach (string s in Directory.GetDirectories(path))
            {
                listBox1.Items.Add(s.Remove(0, path.Length + 1));
            }

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

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
            catch
            {
                RefreshAll();
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
            if (comboBox1.Text == "ProtoSmasher") //slappy made his execution weird, so this is it now lol
            {
                try
                {
                    Dictionary<string, string> value = new Dictionary<string, string>
            {
                {
                    "Action",
                    "ExecuteSource"
                },
                {
                    "Value",
                    fastColoredTextBox1.Text
                }
            };
                    GClass3.gclass3_0.Context.WebSocket.Send(JsonConvert.SerializeObject(value));
                }
                catch
                {
                }
            }

            if (comboBox1.Text == "Stella")
            {
                ExecuteScript("execution", fastColoredTextBox1.Text);
            }

            if (comboBox1.Text == "Seraph") 
            {
                ExecuteScript("SeraphPipe", fastColoredTextBox1.Text);
            }

            if (comboBox1.Text == "Veil")
            {
                try
                {
                    IntPtr hwndParent = Form1.FindWindow("kLBi2xP0o4czr7ckuzyF", null);
                    IntPtr hWnd = Form1.FindWindowEx(hwndParent, IntPtr.Zero, "Edit", null);
                    IntPtr hWnd2 = Form1.FindWindowEx(hwndParent, IntPtr.Zero, "Button", null);
                    SendMessage(hWnd, 12u, 0, fastColoredTextBox1.Text);
                    PostMessage(hWnd2, 513u, IntPtr.Zero, IntPtr.Zero);
                    PostMessage(hWnd2, 514u, IntPtr.Zero, IntPtr.Zero);
                }
                catch
                {
                    MessageBox.Show("An error has occured! Sorry . . .");
                }
            }

            if (comboBox1.Text == "R1")
            {
                R1exec(fastColoredTextBox1.Text);
            }

            if (comboBox1.Text == "RC7")
            {

            }
        }

        void R1exec(string code)
        {
            try
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\r1comm", "lua " + code);
            }
            catch
            {
                R1exec(code);
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

            if (comboBox1.Text == "R1")
            {
                MessageBox.Show("If you haven't already, put the R1 files in with the ASH executable & vice versa due to R1s crappy method of execution");
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

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CategoryClick.Show(MousePosition);
            }
        }

        private void addCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDirectory dir = new NewDirectory(this);
            dir.StartPosition = FormStartPosition.CenterParent;
            dir.ShowDialog(this);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshAll();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Directory.Delete(Application.StartupPath + "/Scripts/" + listBox1.SelectedItem + "/");
            RefreshAll();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rename rename = new Rename(this, listBox1.SelectedItem.ToString());
            rename.StartPosition = FormStartPosition.CenterParent;
            rename.ShowDialog(this);
        }
    }
}
