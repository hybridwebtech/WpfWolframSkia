using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using SkiaSharp;

namespace WpfWolframSkia
{
    public class Spirograph
    {
        private double _scaleFactor = 4.8;
        
        public void Draw(SKPaintWrapper wrapper)
        {
            var paint = new SKPaint
            {
                Color = SKColors.Blue,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = 24,
                StrokeWidth = 2.0f,
            };
            
            double cx = wrapper.Canvas.LocalClipBounds.MidX;
            double cy = wrapper.Canvas.LocalClipBounds.MidY;

            double iter = 100.0;
            
            double A = 80;
            double B = 14;
            double C = 30;
            double t = 0;
            double dt = Math.PI / iter;
            double max_t = 2 * Math.PI * B / GCD((long)A, (long)B);
            double xl = X(t, A, B, C)*_scaleFactor + cx;
            double yl = Y(t, A, B, C)*_scaleFactor + cy;
            
            while (t <= max_t)
            {
                t += dt;
                double x1 = X(t, A, B, C)*_scaleFactor + cx;
                double y1 = Y(t, A, B, C)*_scaleFactor + cy;

                wrapper.Canvas.DrawLine((float)x1, (float)y1, (float)xl, (float)yl,
                    paint);
                
                xl = x1;
                yl = y1;
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