using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TestApplication;
using System.ComponentModel;
using System.Windows.Forms;

namespace Convolutions
{
    public static class Convolution
    {

        private static Color[] nearestNeighbours = new Color[9];

        private static int[] sobelDetectGy = { 1, 2, 1, 0, 0, 0, -1, -2, -1 };
        private static int[] sobelDetectGx = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
        //private static int[] laplacianEdge = {1, 1, -4, 1, 1};

        public static int thresholdAmount = 1;

        public static void NearestNeighbours(Bitmap bitmap, int i, int j, int threshold) //Declares all of the nearestNeighbours in the bitmap as well as have parameters for setting the threshold values and the height and width of the image
        {
            nearestNeighbours[0] = bitmap.GetPixel(i - threshold, j - threshold);
            nearestNeighbours[1] = bitmap.GetPixel(i, j - threshold);
            nearestNeighbours[2] = bitmap.GetPixel(i + threshold, j - threshold);
            nearestNeighbours[3] = bitmap.GetPixel(i - threshold, j);
            nearestNeighbours[4] = bitmap.GetPixel(i, j);
            nearestNeighbours[5] = bitmap.GetPixel(i + threshold, j);
            nearestNeighbours[6] = bitmap.GetPixel(i - threshold, j + threshold);
            nearestNeighbours[7] = bitmap.GetPixel(i, j + threshold);
            nearestNeighbours[8] = bitmap.GetPixel(i + threshold, j + threshold);
        }

        public static Bitmap GrayScale(Bitmap grayBitmap) //This method is used to grayScale an image.
        {
            for (int i = 0; i < grayBitmap.Width; i++)
            {
                for (int j = 0; j < grayBitmap.Height; j++)
                {
                    try
                    {
                        Color color = grayBitmap.GetPixel(i, j);

                        int grayAverage = (color.R + color.G + color.B) / 3; //Calculates the grayscale by adding and averaging all of the rgb values in the image

                        grayBitmap.SetPixel(i, j, Color.FromArgb(color.A, grayAverage, grayAverage, grayAverage)); //sets all of the averaged gray scale values to the bitmap
                    }
                    catch(ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error with Dimensions and Range");
                    }

                }
            }
            return grayBitmap;
        }

        public static Bitmap BoxBlur(Bitmap bitmap) //This method applies the box blur convolution to the bitmap
        {
            for (int i = thresholdAmount; i < bitmap.Width - thresholdAmount; i++)
            {
                for (int j = thresholdAmount; j < bitmap.Height - thresholdAmount; j++)
                {
                    try
                    {
                        Color color = bitmap.GetPixel(i, j); //Used for alpha channel (optional)

                        NearestNeighbours(bitmap, i, j, thresholdAmount); //calls the NearestNeighbours method with the bitmap, i, j and the threshold amount

                        int boxBlurRed = 0;
                        int boxBlurGreen = 0;
                        int boxBlurBlue = 0;

                        for (int k = 0; k < nearestNeighbours.Length; k++) //All of the nearest neighbours in the rgb channels are added. 
                        {
                            boxBlurRed += nearestNeighbours[k].R; //Concatenates the integer values to the red, green and blue channels of the bitmap of nearest neighbours
                            boxBlurGreen += nearestNeighbours[k].G;
                            boxBlurBlue += nearestNeighbours[k].B;
                        }

                        bitmap.SetPixel(i, j, Color.FromArgb(color.A, boxBlurRed / 9, boxBlurGreen / 9, boxBlurBlue / 9)); //The rgb values are divided by 9 to achieve the blur affect
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error with Dimensions and Range");
                    }
                }
            }

            return bitmap;
        }

        public static Bitmap SobelEdge(Bitmap bitmap) //This method applies the Sobel Edge Detection method to the bitmap
        {
            for (int i = 1; i < bitmap.Width - 1; i++)
            {
                for (int j = 1; j < bitmap.Height - 1; j++)
                {
                    Color color = bitmap.GetPixel(i, j);

                    NearestNeighbours(bitmap, i, j, thresholdAmount);

                    int sobelDetectRedGx = 0;
                    int sobelDetectGreenGx = 0;
                    int sobelDetectBlueGx = 0;

                    int sobelDetectRedGy = 0;
                    int sobelDetectGreenGy = 0;
                    int sobelDetectBlueGy = 0;
                    
                    for (int k = 0; k < nearestNeighbours.Length; k++)
                    {
                        sobelDetectRedGx += nearestNeighbours[k].R * sobelDetectGx[k];
                        sobelDetectGreenGx += nearestNeighbours[k].G * sobelDetectGx[k];
                        sobelDetectBlueGx += nearestNeighbours[k].B * sobelDetectGx[k];

                        sobelDetectRedGy += nearestNeighbours[k].R * sobelDetectGy[k];
                        sobelDetectGreenGy += nearestNeighbours[k].G * sobelDetectGy[k];
                        sobelDetectBlueGy += nearestNeighbours[k].B * sobelDetectGy[k];
                    }

                    int averageGx = (sobelDetectRedGx + sobelDetectGreenGx + sobelDetectBlueGx) / 3;
                    int averageGy = (sobelDetectRedGy + sobelDetectGreenGy + sobelDetectBlueGy) / 3;

                    int sobelEdge = (int)Math.Sqrt(averageGx * averageGx + averageGy * averageGy);
                    
                    //Console.WriteLine(sobelEdge);
                    if (sobelEdge < 255)
                    {
                        sobelEdge = 0;
                    }
                    else if (sobelEdge > 50)
                    {
                        sobelEdge = 255;
                    }

                    bitmap.SetPixel(i, j, Color.FromArgb(color.A, sobelEdge, sobelEdge, sobelEdge));
                }
            }

            return bitmap;
        }

        public static Bitmap LaplacianEdge(Bitmap bitmap) //This method applies the Laplacian Edge Detection method to the bitmap
        { 
           for (int i = thresholdAmount; i < bitmap.Width - thresholdAmount; i++)
           {
                for (int j = thresholdAmount; j < bitmap.Height - thresholdAmount; j++)
                {
                    try
                    {
                        Color color = bitmap.GetPixel(i, j);
                        Console.WriteLine(thresholdAmount);
                        NearestNeighbours(bitmap, i, j, thresholdAmount);

                        int laplacianRed = 0;
                        int laplacianGreen = 0;
                        int laplacianBlue = 0;

                        laplacianRed = nearestNeighbours[2].R + nearestNeighbours[4].R + nearestNeighbours[5].R * (-4) + nearestNeighbours[6].R + nearestNeighbours[8].R;
                        laplacianGreen = nearestNeighbours[2].G + nearestNeighbours[4].G + nearestNeighbours[5].G * (-4) + nearestNeighbours[6].G + nearestNeighbours[8].G;
                        laplacianBlue = nearestNeighbours[2].B + nearestNeighbours[4].B + nearestNeighbours[5].B * (-4) + nearestNeighbours[6].B + nearestNeighbours[8].B;
                        

                        int averageLaplacian = (laplacianRed + laplacianGreen + laplacianBlue) / 3;

                        if (averageLaplacian > 255)
                        {
                            averageLaplacian = 255;
                        }
                        else if (averageLaplacian < 0)
                        {
                            averageLaplacian = 0;
                        }

                        bitmap.SetPixel(i, j, Color.FromArgb(color.A, averageLaplacian, averageLaplacian, averageLaplacian));
                    } catch (ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error with Dimensions and Range");
                    }
                }
           }
            
            return bitmap;
        }

        public static Bitmap NegativeImage(Bitmap bitmap) //This method inverses the bitmap
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    try
                    {
                        Color color = bitmap.GetPixel(i, j);

                        int negativeRed = 255 - color.R;
                        int negativeGreen = 255 - color.G;
                        int negativeBlue = 255 - color.B;

                        bitmap.SetPixel(i, j, Color.FromArgb(color.A, negativeRed, negativeGreen, negativeBlue));
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error with Dimensions and Range");
                    }
                }
            }
            return bitmap;
        }

        public static Bitmap BlackWhite(Bitmap bitmap) //This method converts the bitmap into black and white
        {
            for (int i = 0; i < bitmap.Width; i++)
            {
                for (int j = 0; j < bitmap.Height; j++)
                {
                    try
                    {
                        Color color = bitmap.GetPixel(i, j);

                        int grayAverage = (color.R + color.G + color.B) / 3;

                        grayAverage = grayAverage < 128 ? 0 : 255;

                        bitmap.SetPixel(i, j, Color.FromArgb(grayAverage, grayAverage, grayAverage));
                    }

                    catch (ArgumentOutOfRangeException)
                    {
                        MessageBox.Show("Error with Dimensions and Range");
                    }
                }
            }
            return bitmap;
        }


    }

     
    
}
