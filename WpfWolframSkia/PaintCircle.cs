using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace WpfWolframSkia
{
    public class PaintCircle
    {
        private static PaintCircle _instance = null;
        public static PaintCircle Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PaintCircle();
                }

                return _instance;
            }
        }

        private PaintCircle() { }
        
        public void OnPaint(SKPaintWrapper wrapper)
        {
            Paint(wrapper.Sender, wrapper.HostWindow, wrapper.EventArgs);
        }        

        public void Paint(object sender, MainWindow hostWindow, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            //canvas.Clear();
            
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Red,
                StrokeWidth = 25
            };
            
            canvas.DrawCircle(info.Width / 2, info.Height / 2, 100, paint);
            
            paint.Style = SKPaintStyle.Fill;
            paint.Color = SKColors.Blue;
            canvas.DrawCircle(args.Info.Width / 2, args.Info.Height / 2, 100, paint);            
        }
    }
}