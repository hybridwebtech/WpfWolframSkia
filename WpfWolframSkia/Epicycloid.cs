using System;
using System.Collections.Generic;
using MathNet.Numerics;
using SkiaSharp;

namespace WpfWolframSkia
{
    public class Epicycloid : BaseDrawable
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
            
            double p = DictUtils.GetValue<double>(drawParams, "p", 1.0);
            double q = DictUtils.GetValue<double>(drawParams, "q", 1.0);

            double[] xs = Generate.LinearRange(0.0, Math.PI/200, 2*Math.PI);

            // double midX = wrapper.Canvas.LocalClipBounds.MidX;
            // double midY = wrapper.Canvas.LocalClipBounds.MidY;
            //
            // double sizeX = 0.8 * midX;
            // double sizeY = 0.8 * midY;
            
            double[] cosxps = Generate.Map(xs, x => Math.Cos(p * x));
            double[] sinxps = Generate.Map(xs, x => Math.Sin(p * x));
            double[] cosxqs = Generate.Map(xs, x => Math.Cos(q * x));
            double[] sinxqs = Generate.Map(xs, x => Math.Sin(q * x));
            
            FindWorldExtents(cosxps, cosxqs, sinxps, sinxqs);

            for (int i = 0; i < cosxps.Length; i++)
            {
                XYPoint<float> p0 = WorldToView(cosxps[i], sinxps[i]);
                XYPoint<float> p1 = WorldToView(cosxqs[i], sinxqs[i]);
                
                wrapper.Canvas.DrawLine((float)p0.X, (float)p0.Y, (float)p1.X, (float)p1.Y, paint);
                
                // wrapper.Canvas.DrawLine((float)cosxps[i], (float)sinxps[i], 
                //     (float)cosxqs[i], (float)sinxqs[i],
                //     paint);
            }
        }
    }
}