using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace PowderPixelArt
{
    public partial class Form1 : Form
    {
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern IntPtr WindowFromPoint(Point pnt);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        private const int SLEEP = 20;
        private Point startPos = new Point(0, 0);
        private KeyboardHook hook = new KeyboardHook();
        private bool cancelDraw = false;

        internal static class PColors
        {
            public static Color empty = Color.FromArgb(0, 0, 0);
            public static Color fanWidget = Color.FromArgb(255, 0, 0);
            public static Color frameBorder = Color.FromArgb(96, 96, 96);
            public static Color powder = Color.FromArgb(242, 189, 107);
            public static Color water = Color.FromArgb(64, 64, 255);
            public static Color fire = Color.FromArgb(255, 64, 64);
            public static Color seed = Color.FromArgb(144, 192, 64);
            public static Color gPowder = Color.FromArgb(176, 128, 80);
            public static Color fan = Color.FromArgb(128, 128, 255);
            public static Color ice = Color.FromArgb(208, 208, 255);
            public static Color sBall = Color.FromArgb(255, 64, 160);
            public static Color clone = Color.FromArgb(144, 112, 16);
            public static Color fWorks = Color.FromArgb(255, 153, 102);
            public static Color oil = Color.FromArgb(128, 48, 32);
            public static Color c4 = Color.FromArgb(255, 255, 204);
            public static Color stone = Color.FromArgb(128, 128, 128);
            public static Color magma = Color.FromArgb(255, 102, 51);
            public static Color virus = Color.FromArgb(128, 0, 128);
            public static Color nitro = Color.FromArgb(68, 119, 0);
            public static Color ant = Color.FromArgb(192, 128, 192);
            public static Color torch = Color.FromArgb(255, 160, 32);
            public static Color gas = Color.FromArgb(204, 153, 153);
            public static Color soapy = Color.FromArgb(224, 224, 224);
            public static Color thunder = Color.FromArgb(255, 255, 32);
            public static Color metal = Color.FromArgb(64, 64, 64);
            public static Color bomb = Color.FromArgb(102, 102, 0);
            public static Color laser = Color.FromArgb(204, 0, 0);
            public static Color acid = Color.FromArgb(204, 255, 0);
            public static Color vine = Color.FromArgb(0, 187, 0);
            public static Color salt = Color.FromArgb(255, 255, 255);
            public static Color glass = Color.FromArgb(1, 1, 1);
            public static Color bird = Color.FromArgb(128, 112, 80);
            public static Color mercury = Color.FromArgb(170, 170, 170);
            public static Color spark = Color.FromArgb(255, 204, 51);
            public static Color fuse = Color.FromArgb(68, 51, 34);
            public static Color cloud = Color.FromArgb(204, 204, 204);
            public static Color pump = Color.FromArgb(0, 51, 51);

            public static Color[] powderColors = new Color[] {  
                                                                PColors.empty,
                                                                PColors.powder, PColors.water, PColors.fire, PColors.seed, PColors.gPowder, PColors.fan, PColors.ice, PColors.sBall, PColors.clone, PColors.fWorks, 
                                                                PColors.oil, PColors.c4, PColors.stone, PColors.magma, PColors.virus, PColors.nitro, PColors.ant, PColors.torch, PColors.gas, PColors.soapy,
                                                                PColors.thunder, PColors.metal, PColors.bomb, PColors.laser, PColors.acid, PColors.vine, PColors.salt, PColors.glass, PColors.bird, PColors.mercury,
                                                                PColors.spark, PColors.fuse, PColors.cloud, PColors.pump
                                                             };
        };

        internal static class PLocations
        {
            public static Size stop = new Size(309, 430);
            public static Size reset = new Size(358, 430);
            public static Size clear = new Size(308, 332);
            public static Size dot = new Size(360, 416);
            public static Size fan = new Size(19, 374);
        };

        public Form1()
        {
            InitializeComponent();

            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            hook.RegisterHotKey(0, Keys.Escape);
        }

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
                applyColorPalette(bmap, PColors.powderColors);
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

        private void bStart_Click(object sender, EventArgs e)
        {
            System.Timers.Timer timer = new System.Timers.Timer(4000);
            timer.Elapsed += new ElapsedEventHandler(onTimedEvent);
            timer.Enabled = true;
        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            cancelDraw = true;
        }

        private void onTimedEvent(object o, ElapsedEventArgs e)
        {
            ((System.Timers.Timer)(o)).Enabled = false;

            String text = getWindowName(WindowFromPoint(Cursor.Position));

            if (text != "SunAwtCanvas")
            {
                MessageBox.Show("Mouse was not placed in Powder Game Window!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Point temp = Cursor.Position;
            alignMouse();
            //Cursor.Position = temp;
            //System.Diagnostics.Debug.WriteLine(new Point(Cursor.Position.X - startPos.X, Cursor.Position.Y - startPos.Y));
            setUpGame();
            drawPic((Bitmap)pictureBox.Image);
        }

        private String getWindowName(IntPtr handle)
        {
            StringBuilder buffer = new StringBuilder(128);
            GetClassName(handle, buffer, buffer.Capacity);
            return buffer.ToString();
        }

        private Bitmap screenPixel = new Bitmap(1, 1);
        private Color getScreenColor(int posX, int posY)
        {
            using (Graphics gdest = Graphics.FromImage(screenPixel))
            {
                using (Graphics gsrc = Graphics.FromHwnd(IntPtr.Zero))
                {
                    IntPtr hSrcDC = gsrc.GetHdc();
                    IntPtr hDC = gdest.GetHdc();
                    int retval = BitBlt(hDC, 0, 0, 1, 1, hSrcDC, posX, posY, (int)CopyPixelOperation.SourceCopy);
                    gdest.ReleaseHdc();
                    gsrc.ReleaseHdc();
                }
            }
            return screenPixel.GetPixel(0, 0);
        }

        private void alignMouse()
        {
            int posX = Cursor.Position.X;
            int posY = Cursor.Position.Y;

            do
            {
                Cursor.Position = new Point(posX, posY);
                posX--;
            } while (getWindowName(WindowFromPoint(Cursor.Position)) == "SunAwtCanvas");
            posX = posX + 6;
            do
            {
                Cursor.Position = new Point(posX, posY);
                posY--;
            } while (getWindowName(WindowFromPoint(Cursor.Position)) == "SunAwtCanvas");
            posY = posY + 6;
            Cursor.Position = new Point(posX, posY);
            startPos = Cursor.Position;
        }

        private void setUpGame()
        {
            keyPress(0x4C);
            keyPress(0x0D);
            keyPress(0x30);
            moveMouse(PLocations.dot);
            rightClick();
            moveMouse(PLocations.clear);
            rightClick();
        }

        private void drawPic(Bitmap image)
        {
            Color curColor = PColors.empty;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    if (cancelDraw)
                    {
                        leftPressUp();
                        cancelDraw = false;
                        return;
                    }

                    Color pixel = image.GetPixel(x, y);
                    if (curColor != pixel)
                    {
                        leftPressUp();
                        rightClick();
                        if (selectColor(pixel))
                        {
                            moveMouse(new Size(x, y));
                            System.Threading.Thread.Sleep(SLEEP);
                            leftPressDown();
                        }
                        curColor = pixel;
                    }
                    moveMouse(new Size(x+1, y));
                }
                leftPressUp();
                curColor = PColors.empty;
            }
        }

        private bool selectColor(Color pixel)
        {
            if (pixel == PColors.fan)
                moveMouse(PLocations.fan);
            else
                return false;
            leftClick();
            return true;
        }

        private void moveMouse(Size loc)
        {
            System.Threading.Thread.Sleep(SLEEP);
            Cursor.Position = Point.Add(startPos, loc);
        }

        private void leftPressDown()
        {
            System.Threading.Thread.Sleep(SLEEP);
            mouse_event((uint)0x00000002, 0, 0, 0, 0);
        }

        private void leftPressUp()
        {
            System.Threading.Thread.Sleep(SLEEP);
            mouse_event((uint)0x00000004, 0, 0, 0, 0);
        }

        private void leftClick()
        {
            System.Threading.Thread.Sleep(SLEEP);
            mouse_event((uint)0x00000002, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(SLEEP);
            mouse_event((uint)0x00000004, 0, 0, 0, 0);
        }

        private void leftClickDrag(Size dist)
        {
            System.Threading.Thread.Sleep(SLEEP);
            mouse_event((uint)0x00000002, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(SLEEP);
            Cursor.Position = Point.Add(Cursor.Position, dist);
            System.Threading.Thread.Sleep(SLEEP);
            mouse_event((uint)0x00000004, 0, 0, 0, 0);
        }

        private void rightClick()
        {
            System.Threading.Thread.Sleep(SLEEP);
            mouse_event((uint)0x00000008, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(SLEEP);
            mouse_event((uint)0x00000010, 0, 0, 0, 0);
        }

        private void keyPress(byte key)
        {
            System.Threading.Thread.Sleep(SLEEP);
            keybd_event(key, 0, 0x0001, 0);
            System.Threading.Thread.Sleep(SLEEP);
            keybd_event(key, 0, 0x0002, 0); 
        }
    }
}
