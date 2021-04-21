using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace WpfWolframSkia
{
    public class SKPaintWrapper
    {
        public object Sender { get; set; }
        
        public MainWindow HostWindow { get; set; }
        
        public SKPaintSurfaceEventArgs EventArgs { get; set; }
        
        public SKImageInfo ImageInfo => EventArgs.Info;
        
        public SKSurface Surface => EventArgs.Surface;
        
        public SKCanvas Canvas => Surface.Canvas;
        
        public SKPaintWrapper(object sender, MainWindow hostWindow, SKPaintSurfaceEventArgs args)
        {
            Sender = sender;
            HostWindow = hostWindow;
            EventArgs = args;
        }
    }
}