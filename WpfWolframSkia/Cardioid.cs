using System;
using System.Collections.Generic;
using SkiaSharp;
using MathNet.Numerics;

namespace WpfWolframSkia
{
    public class Cardioid : BaseDrawable
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
            
            double[] X = Generate.Map(T, t => 2*a*(1.0 - Math.Cos(t))*Math.Cos(t));
            double[] Y = Generate.Map(T, t => 2*a*(1.0 - Math.Cos(t))*Math.Sin(t));
            
            FindWorldExtents(X, Y);
            
            for (int i = 0; i < X.Length - 1; i++)
            {
                XYPoint<float> p0 = WorldToView(X[i], Y[i]);
                XYPoint<float> p1 = WorldToView(X[i + 1], Y[i + 1]);
                
                wrapper.Canvas.DrawLine((float)p0.X, (float)p0.Y, (float)p1.X, (float)p1.Y, paint);
            }            
        }
    }
}