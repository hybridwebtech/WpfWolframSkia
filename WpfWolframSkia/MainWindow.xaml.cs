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
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            var wrapper = new SKPaintWrapper(sender, this, e);
            
            wrapper.Canvas.Clear();

            //var epicycloid = new Epicycloid();
            //epicycloid.Draw(wrapper, 9.0, 4.0);

            // var draw3D = new Draw3D();
            // draw3D.Draw(wrapper);

            var spiro = new Spirograph();
            spiro.Draw(wrapper);
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.InvalidateVisual();
        }
    }
}