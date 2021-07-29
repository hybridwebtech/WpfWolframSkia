using System;
using MathNet.Numerics;
using SkiaSharp;

namespace WpfWolframSkia
{
    public class Epicycloid
    {
        public void Draw(SKPaintWrapper wrapper, double p, double q)
        {
            var paint = new SKPaint
            {
                Color = SKColors.Blue,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = 24
            };

            double[] xs = Generate.LinearRange(0.0, Math.PI/200, 2*Math.PI);

            double midX = wrapper.Canvas.LocalClipBounds.MidX;
            double midY = wrapper.Canvas.LocalClipBounds.MidY;

            double sizeX = 0.8 * midX;
            double sizeY = 0.8 * midY;
            
            double[] cosxps = Generate.Map(xs, x => sizeX * Math.Cos(p * x) + midX);
            double[] sinxps = Generate.Map(xs, x => sizeY * Math.Sin(p * x) + midY);
            double[] cosxqs = Generate.Map(xs, x => sizeX * Math.Cos(q * x) + midX);
            double[] sinxqs = Generate.Map(xs, x => sizeY * Math.Sin(q * x) + midY);

            for (int i = 0; i < xs.Length; i++)
            {
                wrapper.Canvas.DrawLine((float)cosxps[i], (float)sinxps[i], 
                    (float)cosxqs[i], (float)sinxqs[i],
                    paint);
            }
        }
    }
}