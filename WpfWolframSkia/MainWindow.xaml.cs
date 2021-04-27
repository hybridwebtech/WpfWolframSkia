using System;
using System.IO;
using System.Windows;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace WpfWolframSkia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private SKBitmap _bmp;
        private bool[,] _skinMask;
        
        public MainWindow()
        {
            InitializeComponent();
            
            _bmp = SKBitmap.Decode("color_image.png");
            _skinMask = ReadSingleBoolArrayFromFile("skin_mask.dat");
        }
        
        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var wrapper = new SKPaintWrapper(sender, this, e);
            
            wrapper.Canvas.Clear();

            //var epicycloid = new Epicycloid();
            
            //epicycloid.Draw(wrapper, 9.0, 4.0);

            var paint = new SKPaint
            {
                Color = SKColors.Black,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = 24
            };
            
            wrapper.Canvas.DrawBitmap(_bmp, new SKPoint(0.0f, 0.0f), paint);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            WolframService service = new WolframService();
            
            service.ExecuteBackground();
        }
        
        private bool[,] ReadSingleBoolArrayFromFile(string imageFilename)
        {
            if (!File.Exists(imageFilename))
            {
                return null;
            }

            bool[,] imageData = null;

            try
            {
                using (BinaryReader r = new BinaryReader(File.Open(imageFilename, FileMode.Open)))
                {
                    int maxI = r.ReadInt32();
                    int maxJ = r.ReadInt32();

                    imageData = new bool[maxI, maxJ];

                    for (int i = 0; i < maxI; i++)
                    {
                        for (int j = 0; j < maxJ; j++)
                        {
                            imageData[i, j] = r.ReadBoolean();
                        }
                    }
                }

                return imageData;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return null;
        }

        private void CalculateSkinTone(SKBitmap bmp)
        {
            if (bmp != null)
            {
                for (int row = 0; row < bmp.Height; row++)
                {
                    for (int col = 0; col < bmp.Width; col++)
                    {
                        SKColor pixelColor = bmp.GetPixel(col, row);
                    }
                }
            }
        }
    }
}