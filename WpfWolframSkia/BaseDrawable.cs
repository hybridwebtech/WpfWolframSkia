using System;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;

namespace WpfWolframSkia
{
    public class XYPoint<T>
    {
        public T X { get; set; }
        public T Y { get; set; }

        public XYPoint(T x, T y)
        {
            X = x;
            Y = y;
        }
    }
    
    public class BaseDrawable : IDrawable
    {
        public double ScaleX { get; set; } = 1.0;
        public double ScaleY { get; set; } = 1.0;
        public double SizeX { get; set; }
        public double SizeY { get; set; }
        public double MaxWorldX { get; set; }
        public double MaxWorldY { get; set; }
        public double MinWorldX { get; set; }
        public double MinWorldY { get; set; }
        
        public virtual void Draw(SKPaintWrapper wrapper, Dictionary<string, object> drawParams)
        {
            SizeX = wrapper.Canvas.LocalClipBounds.Width;
            SizeY = wrapper.Canvas.LocalClipBounds.Height;
            
            ScaleX = DictUtils.GetValue<double>(drawParams, "scaleX", 1.0);
            ScaleY = DictUtils.GetValue<double>(drawParams, "scaleY", 1.0);
        }

        public double FindArrayMax(double[] Q) => Q.Max();
        public double FindArrayMin(double[] Q) => Q.Min();

        public void FindWorldExtents(double[] X, double[] Y)
        {
            MaxWorldX = FindArrayMax(X);
            MinWorldX = FindArrayMin(X);
            MaxWorldY = FindArrayMax(Y);
            MinWorldY = FindArrayMin(Y);
        }
        
        public void FindWorldExtents(double[] X1, double[] X2, double[] Y1, double[] Y2)
        {
            double temp1 = FindArrayMax(X1);
            double temp2 = FindArrayMax(X2);
            MaxWorldX = temp1 > temp2 ? temp1 : temp2;
            
            temp1 = FindArrayMin(X1);
            temp2 = FindArrayMin(X2);
            MinWorldX = temp1 < temp2 ? temp1 : temp2;
            
            temp1 = FindArrayMax(Y1);
            temp2 = FindArrayMax(Y2);
            MaxWorldY = temp1 > temp2 ? temp1 : temp2;
            
            temp1 = FindArrayMin(Y1);
            temp2 = FindArrayMin(Y2);
            MinWorldY = temp1 < temp2 ? temp1 : temp2;            
        }        

        public double WorldXToViewX(double wx) => (ScaleX*SizeX * (wx - MinWorldX) / (MaxWorldX - MinWorldX));
        public double WorldYToViewY(double wy) => (ScaleY*SizeX * (wy - MinWorldY) / (MaxWorldY - MinWorldY));

        public XYPoint<float> WorldToView(double wx, double wy) => 
            new XYPoint<float>((float)WorldXToViewX(wx), (float)WorldYToViewY(wy));
        
        public void DrawPolyline(SKPaintWrapper wrapper, double[] X, double[] Y, SKPaint paint)
        {
            for (int i = 0; i < X.Length - 1; i++)
            {
                XYPoint<float> p0 = WorldToView(X[i], Y[i]);
                XYPoint<float> p1 = WorldToView(X[i + 1], Y[i + 1]);

                wrapper.Canvas.DrawLine((float) p0.X, (float) p0.Y, (float)
                    p1.X, (float) p1.Y,
                    paint);
            }
        }
    }
}