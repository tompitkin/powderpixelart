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
        private static Color empty = Color.FromArgb(0, 0, 0);
        private static Color powder = Color.FromArgb(242, 189, 107);
        private static Color water = Color.FromArgb(64, 64, 255);
        private static Color fire = Color.FromArgb(255, 64, 64);
        private static Color seed = Color.FromArgb(144, 192, 64);
        private static Color gPowder = Color.FromArgb(176, 128, 80);
        private static Color fan = Color.FromArgb(128, 128, 255);
        private static Color ice = Color.FromArgb(208, 208, 255);
        private static Color sBall = Color.FromArgb(255, 64, 160);
        private static Color clone = Color.FromArgb(144, 112, 16);
        private static Color fWorks = Color.FromArgb(255, 153, 102);
        private static Color oil = Color.FromArgb(128, 48, 32);
        private static Color c4 = Color.FromArgb(255, 255, 204);
        private static Color stone = Color.FromArgb(128, 128, 128);
        private static Color magma = Color.FromArgb(255, 102, 51);
        private static Color virus = Color.FromArgb(128, 0, 128);
        private static Color nitro = Color.FromArgb(68, 119, 0);
        private static Color ant = Color.FromArgb(192, 128, 192);
        private static Color torch = Color.FromArgb(255, 160, 32);
        private static Color gas = Color.FromArgb(204, 153, 153);
        private static Color soapy = Color.FromArgb(224, 224, 224);
        private static Color thunder = Color.FromArgb(255, 255, 32);
        private static Color metal = Color.FromArgb(64, 64, 64);
        private static Color bomb = Color.FromArgb(102, 102, 0);
        private static Color laser = Color.FromArgb(204, 0, 0);
        private static Color acid = Color.FromArgb(204, 255, 0);
        private static Color vine = Color.FromArgb(0, 187, 0);
        private static Color salt = Color.FromArgb(255, 255, 255);
        private static Color glass = Color.FromArgb(1, 1, 1);
        private static Color bird = Color.FromArgb(128, 112, 80);
        private static Color mercury = Color.FromArgb(170, 170, 170);
        private static Color spark = Color.FromArgb(255, 204, 51);
        private static Color fuse = Color.FromArgb(68, 51, 34);
        private static Color cloud = Color.FromArgb(204, 204, 204);
        private static Color pump = Color.FromArgb(0, 51, 51);
        private static Color[] powderColors = new Color[] { empty,
                                                            powder, water, fire, seed, gPowder, fan, ice, sBall, clone, fWorks, 
                                                            oil, c4, stone, magma, virus, nitro, ant, torch, gas, soapy,
                                                            thunder, metal, bomb, laser, acid, vine, salt, glass, bird, mercury,
                                                            spark, fuse, cloud, pump};

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
