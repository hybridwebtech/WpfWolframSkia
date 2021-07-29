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
        }
        
        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var wrapper = new SKPaintWrapper(sender, this, e);
            
            wrapper.Canvas.Clear();

            var epicycloid = new Epicycloid();
            
            epicycloid.Draw(wrapper, 9.0, 4.0);
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
        }
        
        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.InvalidateVisual();
        }
    }
}