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
using System.Windows.Media.Imaging;

namespace PowderPixelArt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static Color powder = Color.FromArgb(242, 189, 107);
        private static Color water = Color.FromArgb(64, 64, 255);
        private static Color fire = Color.FromArgb(255, 64, 64);
        private static Color seed = Color.FromArgb(144, 192, 64);
        private static Color[] powderColors = new Color[] {powder, water, fire, seed};

        private void bLoadImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream imageStreamSource = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                Image image = Image.FromStream(imageStreamSource);

                Bitmap bmap = resizeImage(image, 40000);
                    
                const int maxImageWidth = 395;
                const int maxImageHeight = 295;
                if (bmap.Width > maxImageWidth)
                {
                    int newImageHeight = (int)(bmap.Height * ((float)maxImageWidth / (float)bmap.Width));
                    bmap = new Bitmap(maxImageWidth, newImageHeight);
                }
                if (bmap.Height > maxImageHeight)
                {
                    int newImageWidth = (int)(bmap.Width * ((float)maxImageHeight / (float)bmap.Height));
                    bmap = new Bitmap(newImageWidth, maxImageHeight);
                }
                Graphics graph = Graphics.FromImage(bmap);
                graph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                graph.DrawImage(image, 0, 0, bmap.Width, bmap.Height);
                applyColorPalette(bmap, powderColors);
                pictureBox.Image = bmap;
                bStart.Enabled = true;
            }
            else
                bStart.Enabled = false;
        }

        private Bitmap resizeImage(Image image, int maxPixels)
        {
            int pixels = image.Width * image.Height;
            if (pixels > maxPixels)
            {
                float ratio = image.Width / image.Height;
                float scale = (float)Math.Sqrt((float)pixels / maxPixels);
                return new Bitmap((int)(ratio * image.Width / scale), (int)(image.Height / scale));
            }
            return new Bitmap(image.Width, image.Height);
        }

        private void applyColorPalette(Bitmap image, Color[] pallete)
        {
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    Color bestColor = Color.Black;
                    double distance = Double.MaxValue;
                    foreach( Color element in pallete)
                    {
                        double red = Math.Pow(Convert.ToDouble(element.R) - pixelColor.R, 2.0);
                        double green = Math.Pow(Convert.ToDouble(element.G) - pixelColor.G, 2.0);
                        double blue = Math.Pow(Convert.ToDouble(element.B) - pixelColor.B, 2.0);
                        double tempDist = Math.Sqrt(red + green + blue);
                        if (tempDist == 0.0)
                        {
                            bestColor = element;
                            break;
                        }
                        else if (tempDist < distance)
                        {
                            distance = tempDist;
                            bestColor = element;
                        }
                    }
                    image.SetPixel(x, y, bestColor);
                }
            }
        }
    }
}
