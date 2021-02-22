using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Perceptron;

namespace TestApplication
{
    public partial class Form2 : Form
    {
        private MP3Player mp3Player = new MP3Player();

        formEmail showFormEmail = new formEmail();

        PDFViewer pdfForm = new PDFViewer();

        SlimeBallClicker slimeBallClicker = new SlimeBallClicker();

        PerceptronInfo perceptronFormShow = new PerceptronInfo();

       
        private int count = 0;
        public int Count { get => count; set => count = value; }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        public Form2()
        {
            InitializeComponent();
            labelName.Text = "Hello Adminstrator";
            timer1.Start();
            labelTime.Text = DateTime.Now.ToShortDateString();
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labelDay.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void buttonEmail_Click(object sender, EventArgs e)
        {
            showFormEmail.Show();
            Visible = false;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "MP3 Files|* .mp3";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    mp3Player.Open(openFileDialog.FileName);
                    
                }

                labelPlaying.Text = openFileDialog.FileName.ToString();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            mp3Player.Play();
            
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            mp3Player.Stop();
            
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            
            Count++;
            scoreCount.Text = Count.ToString();
            slimeBallClicker.SoundPlay();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pdfForm.Show();
            Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            perceptronFormShow.Show();
            Visible = false;
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ImageEdit imageFileOpen = new ImageEdit();

            imageFileOpen.Show();

            Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            LSystem lSystemForm = new LSystem();
            lSystemForm.Show();
            Visible = false;

        }
    }
}
