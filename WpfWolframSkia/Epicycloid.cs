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
            
            double[] cosxps = Generate.Map(xs, x => 300 * Math.Cos(p * x) + 350);
            double[] sinxps = Generate.Map(xs, x => 300 * Math.Sin(p * x) + 350);
            double[] cosxqs = Generate.Map(xs, x => 300 * Math.Cos(q * x) + 350);
            double[] sinxqs = Generate.Map(xs, x => 300 * Math.Sin(q * x) + 350);

            for (int i = 0; i < xs.Length; i++)
            {
                wrapper.Canvas.DrawLine((float)cosxps[i], (float)sinxps[i], 
                    (float)cosxqs[i], (float)sinxqs[i],
                    paint);
            }
            
            // foreach (double x in xs)
            // {
            //     double xp = p * x;
            //     double xq = q * x;
            //
            //     double x0 = 300*Math.Cos(xp) + 350;
            //     double y0 = 300*Math.Sin(xp) + 350;
            //     double x1 = 300*Math.Cos(xq) + 350;
            //     double y1 = 300*Math.Sin(xq) + 350;
            //     
            //     wrapper.Canvas.DrawLine((float)x0, (float)y0, (float)x1, (float)y1, paint);
            // }
        }
    }
}