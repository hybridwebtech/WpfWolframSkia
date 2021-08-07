﻿using System;
using System.Collections.Generic;
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

            // var spiro = new Spirograph();
            // spiro.Draw(wrapper);
            IDrawable spiro = new Spirograph();
            spiro.Draw(wrapper, new Dictionary<string, object>()
            {
                {"iter", 100.0},
                {"A", 80.0},
                {"B", 14.0},
                {"C", 30.0},
                {"scaleX", 0.9},
                {"scaleY", 0.9},                
            });

            // IDrawable cardio = new Cardioid();
            // cardio.Draw(wrapper, new Dictionary<string, object>()
            // {
            //     {"a", 0.25},
            //     {"n", 100},
            //     {"scaleX", 0.9},
            //     {"scaleY", 0.9},
            // });
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.InvalidateVisual();
        }
    }
}