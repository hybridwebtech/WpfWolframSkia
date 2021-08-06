using System;
using System.Collections.Generic;
using SkiaSharp;
using MathNet.Numerics;

namespace WpfWolframSkia
{
    public class Cardioid : IDrawable
    {
        public void Draw(SKPaintWrapper wrapper, Dictionary<string, object> drawParams)
        {
            var paint = new SKPaint
            {
                Color = SKColors.Blue,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = 24
            };

            double a = DictUtils.GetValue<double>(drawParams, "a");
            int n = DictUtils.GetValue<int>(drawParams, "n");
            double scaleX = DictUtils.GetValue<double>(drawParams, "scaleX", 1.0);
            double scaleY = DictUtils.GetValue<double>(drawParams, "scaleY", 1.0);
            
            double[] T = Generate.LinearRange(0.0, Math.PI/n, 2*Math.PI);
            
            double midX = wrapper.Canvas.LocalClipBounds.MidX;
            double midY = wrapper.Canvas.LocalClipBounds.MidY;

            double sizeX = wrapper.Canvas.LocalClipBounds.Width;
            double sizeY = wrapper.Canvas.LocalClipBounds.Height;
            
            // double[] X = Generate.Map(T, t => scaleX*(midX +
            //     midX * (2*a*(1.0 - Math.Cos(t))*Math.Cos(t))));
            // double[] Y = Generate.Map(T, t => scaleY*(midY +
            //     midY * (2*a*(1.0 - Math.Cos(t))*Math.Sin(t))));
            
            double[] X = Generate.Map(T, t => 2*a*(1.0 - Math.Cos(t))*Math.Cos(t));
            double[] Y = Generate.Map(T, t => 2*a*(1.0 - Math.Cos(t))*Math.Sin(t));            
            
            for (int i = 0; i < X.Length - 1; i++)
            {
                float x1 = (float)X[i];
                float y1 = (float)Y[i];
                float x2 = (float)X[i + 1];
                float y2 = (float)Y[i + 1];
                
                wrapper.Canvas.DrawLine(x1, y1, x2, y2, paint);
            }            
        }
    }
}