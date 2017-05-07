using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ColorPanle
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private BitmapSource Bitmap2BitmapSource(Bitmap bitmap)
        {
            BitmapSource bmpSource;
            try
            {
                Bitmap tmpBmp = bitmap.Clone() as Bitmap;
                bmpSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(tmpBmp.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch
            {
                bmpSource = null;
            }
            return bmpSource;
        }

        private void DrawPanel()
        {
            Bitmap bmp = new Bitmap(Convert.ToInt32(this.PanelImage.Width), Convert.ToInt32(this.PanelImage.Height));
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(System.Drawing.Color.White);
            int red = 0;
            int green = 0;
            int blue = 0;
            float startangle = 0;
            for (int j = 0; j < 16; j++)
            {
                startangle = (float)(j * 22.5);
                SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(red, green, blue));
                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, 1024, 1024);
                g.FillPie(brush, rect, startangle, (float)(22.5));

                new Thread(() =>
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        SetImage(bmp);
                    }));
                }).Start();

                Thread.Sleep(30);
                red = (j + 1) * 16 - 1;
                green = (j + 1) * 16 - 1;
                blue = (j + 1) * 16 - 1;
            }
//             for (int i = 0; i < 16; i++)
//             {
//                 startangle = (float)(22.5 * i);
//                 blue = i * 16 - 1;
//                 if (blue < 0)
//                 {
//                     blue = 0;
//                 }
//                 for (int j = 0; j < 16; j++)
//                 {
//                     green = j * 16 - 1;
//                     if (green < 0)
//                     {
//                         green = 0;
//                     }
//                     double edge = 512;
//                     for (int k = 0; k < 16; k++)
//                     {
//                         red = k * 16 - 1;
//                         if (red < 0)
//                         {
//                             red = 0;
//                         }
//                         SolidBrush brush = new SolidBrush(System.Drawing.Color.FromArgb(red, green, blue));
//                         //edge =  ((k + 1)*512 / 16);
//                         edge = 512 - k * 32;
//                         System.Drawing.Rectangle rect = new System.Drawing.Rectangle(Convert.ToInt32(500 - edge / 2), Convert.ToInt32(500 - edge / 2), Convert.ToInt32(edge), Convert.ToInt32(edge));
//                         g.FillPie(brush, rect, startangle, (float)(22.5 / 16));
// 
//                     }
//                     startangle = startangle + (float)(22.5 / 16);
//                 }
//             }

        }

        private void SetImage(Bitmap bmp)
        {
            BitmapSource source = Bitmap2BitmapSource(bmp);
            PanelImage.Source = source;
        }
        private void DrawBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DrawBtn.IsEnabled = false;
            DrawPanel();
            this.DrawBtn.IsEnabled = true;
        }
    }
}
