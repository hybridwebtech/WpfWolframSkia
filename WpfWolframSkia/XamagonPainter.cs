using System;
using System.Windows;
using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace WpfWolframSkia
{
    public class XamagonPainter
    {
        private static XamagonPainter _instance = null;
        public static XamagonPainter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new XamagonPainter();
                }

                return _instance;
            }
        }

        private XamagonPainter() { }

        public void OnPaint(SKPaintWrapper wrapper)
        {
            Paint(wrapper.Sender, wrapper.HostWindow, wrapper.EventArgs);
        }
        
        public void Paint(object sender, MainWindow hostWindow, SKPaintSurfaceEventArgs e)
        {
            try
            {
                // the the canvas and properties
                var canvas = e.Surface.Canvas;

                // get the screen density for scaling
                var scale = (float) PresentationSource.FromVisual(hostWindow).CompositionTarget.TransformToDevice.M11;
                var scaledSize = new SKSize(e.Info.Width / scale, e.Info.Height / scale);

                // handle the device screen density
                canvas.Scale(scale);

                /*************/
                // make sure the canvas is blank
                //canvas.Clear(SKColors.White);
                
                // draw some text
                var paint = new SKPaint
                {
                    Color = SKColors.Black,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    TextAlign = SKTextAlign.Center,
                    TextSize = 24
                };

                var coord = new SKPoint(scaledSize.Width / 2, (scaledSize.Height + paint.TextSize) / 2);
                canvas.DrawText("SkiaSharp", coord, paint);

                paint = new SKPaint {
                    IsAntialias = true,
                    Color = new SKColor(0x2c, 0x3e, 0x50),
                    StrokeCap = SKStrokeCap.Round
                };

                // create the Xamagon path
                var path = new SKPath();
                path.MoveTo(71.4311121f, 56f);
                path.CubicTo(68.6763107f, 56.0058575f, 65.9796704f, 57.5737917f, 64.5928855f, 59.965729f);
                path.LineTo(43.0238921f, 97.5342563f);
                path.CubicTo(41.6587026f, 99.9325978f, 41.6587026f, 103.067402f, 43.0238921f, 105.465744f);
                path.LineTo(64.5928855f, 143.034271f);
                path.CubicTo(65.9798162f, 145.426228f, 68.6763107f, 146.994582f, 71.4311121f, 147f);
                path.LineTo(114.568946f, 147f);
                path.CubicTo(117.323748f, 146.994143f, 120.020241f, 145.426228f, 121.407172f, 143.034271f);
                path.LineTo(142.976161f, 105.465744f);
                path.CubicTo(144.34135f, 103.067402f, 144.341209f, 99.9325978f, 142.976161f, 97.5342563f);
                path.LineTo(121.407172f, 59.965729f);
                path.CubicTo(120.020241f, 57.5737917f, 117.323748f, 56.0054182f, 114.568946f, 56f);
                path.LineTo(71.4311121f, 56f);
                path.Close();

                // draw the Xamagon path
                canvas.DrawPath(path, paint);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}