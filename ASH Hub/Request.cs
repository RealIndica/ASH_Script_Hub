using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace ASH_Hub
{
    public partial class Request : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Request()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Request_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Error");
            }
            else if (textBox1.Text == "NAME#0000")
            {
                MessageBox.Show("Error");
            }
            else if (!textBox1.Text.Contains('#'))
            {
                MessageBox.Show("Error");
            }
            else
            {
                var result = new WebClient().DownloadString("-snip-");

                if (result == "GOOD")
                {
                    MessageBox.Show("Your Request has been Submitted!");
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Error");
                }

                try
                {
                    this.Dispose();

                }
                catch
                {

                }
            }
        }
    }
}
                

