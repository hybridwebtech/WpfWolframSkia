using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfWolframSkia
{
    public class XYPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public XYPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
    
    public class BaseDrawable : IDrawable
    {
        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
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

        public double WorldXToViewX(double wx) => ScaleX*SizeX * (wx - MinWorldX) / (MaxWorldX - MinWorldX);
        public double WorldYToViewY(double wy) => ScaleY*SizeX * (wy - MinWorldY) / (MaxWorldY - MinWorldY);

        public XYPoint WorldToView(double wx, double wy) => 
            new XYPoint(WorldXToViewX(wx), WorldYToViewY(wy));
    }
}