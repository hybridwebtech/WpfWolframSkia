using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using WpfWolfram;

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

        private bool _windowLoaded = false;
        
        private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            if (!_windowLoaded) return;

            var wrapper = new SKPaintWrapper(sender, this, e);
            
            wrapper.Canvas.Clear();

            IDrawable mandelbrot = new Mandelbrot2();
            mandelbrot.Draw(wrapper, new Dictionary<string, object>());

            // IDrawable epicycloid = new Epicycloid();
            // epicycloid.Draw(wrapper, new Dictionary<string, object>()
            // {
            //     {"p", 9.0},
            //     {"q", 4.0},
            //     {"scaleX", 0.9},
            //     {"scaleY", 0.9},
            // });

            // var draw3D = new Draw3D();
            // draw3D.Draw(wrapper);

            //IDrawable spiro = new Spirograph();
            //spiro.Draw(wrapper, new Dictionary<string, object>()
            //{
            //    {"iter", 100.0},
            //    {"A", 80.0},
            //    {"B", 14.0},
            //    {"C", 30.0},
            //    {"scaleX", 0.9},
            //    {"scaleY", 0.9},                
            //});

            // IDrawable cardio = new Cardioid();
            // cardio.Draw(wrapper, new Dictionary<string, object>()
            // {
            //     {"a", 0.25},
            //     {"n", 100},
            //     {"scaleX", 0.9},
            //     {"scaleY", 0.9},
            // });

            // IDrawable astroid = new Astroid();
            // astroid.Draw(wrapper, new Dictionary<string, object>()
            // {
            //     {"a", 1.0},
            //     {"n", 100},
            //     {"scaleX", 0.9},
            //     {"scaleY", 0.9},
            // });
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.InvalidateVisual();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _windowLoaded = true;

            Canvas.InvalidateVisual();
        }
    }
}