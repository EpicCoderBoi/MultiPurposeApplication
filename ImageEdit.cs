using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Convolutions;
namespace TestApplication
{
    public partial class ImageEdit : Form
    {
        
        string imageLocation = string.Empty; //The current imageLocation is empty
        
        public ImageEdit()
        {
            InitializeComponent();
        }

        
        private void button6_Click(object sender, EventArgs e)
        {
            
            try
            {
                var openFile = new OpenFileDialog(); //Whenthe button is clicked, the openFileDialog is opened

                openFile.Filter = ("jpg files(*.jpg)|*.jpg| PNG files (*.png)|*.png| All Files(*.*)|*.*"); //The FileDialog prioritizes the important image files after all the files

                if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK) //If the OK button is clicked in the DialogForm
                {
                    imageLocation = openFile.FileName; //The imageLocation changes to that of the file selected
                    imageOpener.ImageLocation = imageLocation; //The imageLocation then is set to the imageOpener's location (The Original Image's PictureBox so that it can be cast to a Bitmap later on)
                }
            }

            catch(Exception) //If any exception occurs then display the message below
            {
                MessageBox.Show("Error Importing File");
            }
        }

        private void button1_Click(object sender, EventArgs e) //When this button is clicked, a bitmap object is created and passed through the GrayScale method in the Convolutions class. After that, the image is then set into the "Convolved" pictureBox.
        {
            var grayScale = new Bitmap((Bitmap)imageOpener.Image);
            Convolution.GrayScale(grayScale);
            imageDisplay.Image = grayScale;
        }

        private void button2_Click(object sender, EventArgs e) //When this button is clicked, a bitmap object is created and passed through the BlurBitmap method in the Convolutions class. After that, the image is then set into the "Convolved" pictureBox.
        {
            var blurBitmap = new Bitmap((Bitmap)imageOpener.Image);
            Convolution.BoxBlur(blurBitmap);
            imageDisplay.Image = blurBitmap;
        }

        private void button3_Click(object sender, EventArgs e) //When this button is clicked, a bitmap object is created and passed through the SobelEdge method in the Convolutions class. After that, the image is then set into the "Convolved" pictureBox.
        {
            var sobelBitmap = new Bitmap((Bitmap)imageOpener.Image);
            Convolution.SobelEdge(sobelBitmap);
            imageDisplay.Image = sobelBitmap;
        }

        private void button4_Click(object sender, EventArgs e) //When this button is clicked, a bitmap object is created and passed through the LaplacianEdge method in the Convolutions class. After that, the image is then set into the "Convolved" pictureBox.
        {
            var laplacianBitmap = new Bitmap((Bitmap)imageOpener.Image);
            Convolution.LaplacianEdge(laplacianBitmap);
            imageDisplay.Image = laplacianBitmap;
        }

        private void button5_Click(object sender, EventArgs e) //When this button is clicked, a bitmap object is created and passed through the BlackWhite method in the Convolutions class. After that, the image is then set into the "Convolved" pictureBox.
        {
            var blackWhiteBitmap = new Bitmap((Bitmap)imageOpener.Image);
            Convolution.BlackWhite(blackWhiteBitmap);
            imageDisplay.Image = blackWhiteBitmap;
        }

        private void negativeImage_Click(object sender, EventArgs e) //When this button is clicked, a bitmap object is created and passed through the NegativeImage method in the Convolutions class. After that, the image is then set into the "Convolved" pictureBox.
        {
            var negativeImageBitmap = new Bitmap((Bitmap)imageOpener.Image);
            Convolution.NegativeImage(negativeImageBitmap);
            imageDisplay.Image = negativeImageBitmap;
        }

        private void button7_Click(object sender, EventArgs e) //This method utilizes the saveFile dialog box to save the bitmap in a desired location
        {
            var image = imageDisplay.Image;
            try
            {
                var saveFile = new SaveFileDialog();

                saveFile.Filter = ("jpg files(*.jpg)|*.jpg| PNG files (*.png)|*.png| All Files(*.*)|*.*");

                if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    image.Save(saveFile.FileName);
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Error Saving File");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e) //This method is used when the exit picture box is clicked so that the application exits
        {
            Application.Exit();
        }

        private void pictureBox2_Click(object sender, EventArgs e) //This method is used when the minimized picture box is clicked so that the application is minimized
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e) //This method is used when the back button is clicked so that an instance of Form 2 (The main Homepage) is created 
        {
            var showForm2 = new Form2();
            showForm2.Show();
            Visible = false;
        }



        private void trackBar1_Scroll(object sender, EventArgs e) //This method is used for setting the value of the threshold bar to adjust the convolution levels
        {
            Convolution.thresholdAmount = int.Parse(trackBar1.Value.ToString());
        }

        private void ImageEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
