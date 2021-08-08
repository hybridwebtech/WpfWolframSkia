using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using SkiaSharp;

namespace WpfWolframSkia
{
    public class Spirograph : BaseDrawable
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
                TextSize = 24,
                StrokeWidth = 2.0f,
            };
            
            double iter = DictUtils.GetValue<double>(drawParams, "iter");
            double A = DictUtils.GetValue<double>(drawParams, "A");
            double B = DictUtils.GetValue<double>(drawParams, "B");
            double C = DictUtils.GetValue<double>(drawParams, "C");
            
            double t = 0;
            double dt = Math.PI / iter;
            double maxt = 2 * Math.PI * B / GCD((long)A, (long)B);
            double xl = X(t, A, B, C);
            double yl = Y(t, A, B, C);

            var XList = new List<double>();
            var YList = new List<double>();
            
            XList.Add(xl);
            YList.Add(yl);
            
            while (t <= maxt)
            {
                t += dt;
                double x1 = X(t, A, B, C);
                double y1 = Y(t, A, B, C);
                
                XList.Add(x1);
                YList.Add(y1);
            }
            
            FindWorldExtents(XList.ToArray(), YList.ToArray());
            
            for (int i = 0; i < XList.Count - 1; i++)
            {
                XYPoint<float> p0 = WorldToView(XList[i], YList[i]);
                XYPoint<float> p1 = WorldToView(XList[i + 1], YList[i + 1]);
                
                wrapper.Canvas.DrawLine((float)p0.X, (float)p0.Y, (float)p1.X, (float)p1.Y, paint);
            }
        }
        
        // The parametric function X(t).
        private double X(double t, double A, double B, double C)
        {
            return (A - B) * Math.Cos(t) + C * Math.Cos((A - B) / B * t);
        }

        // The parametric function Y(t).
        private double Y(double t, double A, double B, double C)
        {
            return (A - B) * Math.Sin(t) - C * Math.Sin((A - B) / B * t);
        }
        
        // Use Euclid's algorithm to calculate the
        // greatest common divisor (GCD) of two numbers.
        private long GCD(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            // Pull out remainders.
            for (; ; )
            {
                long remainder = a % b;
                if (remainder == 0) return b;
                a = b;
                b = remainder;
            };
        }
        
        private long LCM(long a, long b)
        {
            return a * b / GCD(a, b);
        }               
    }
}