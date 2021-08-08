using System;
using System.Collections.Generic;
using SkiaSharp;
using MathNet.Numerics;

namespace WpfWolframSkia
{
    public class Astroid : BaseDrawable
    {
        public override void Draw(SKPaintWrapper wrapper, Dictionary<string, object> drawParams)
        {
            base.Draw(wrapper, drawParams);
            
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
            
            double[] T = Generate.LinearRange(0.0, Math.PI/n, 2*Math.PI);
            
            double[] X = Generate.Map(T, t => (a/4)*(3*Math.Cos(t) + Math.Cos(3*t)));
            double[] Y = Generate.Map(T, t => (a/4)*(3*Math.Sin(t) - Math.Sin(3*t)));
            
            FindWorldExtents(X, Y);
            
            DrawPolyline(wrapper, X, Y, paint);
        }
    }
}