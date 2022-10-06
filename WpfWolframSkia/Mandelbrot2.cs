using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using WpfWolframSkia;

namespace WpfWolfram
{
    public class Mandelbrot2 : BaseDrawable
    {
        private bool _initialized = false;

        private SKColor[] _palette = new SKColor[256];

        public void CreatePalette()
        {
            for (int x = 0; x < 256; x++) // the loop that creates the pallette
            {
                byte r = 0;
                byte g = 0;
                byte b = 0;

                if (x < 85) // colors 0-84
                {
                    r = (byte)(x * 3);
                    g = 0;
                    b = 0;
                }

                if (x > 84 && x < 171) // colors 85-170
                {
                    r = 0;
                    g = (byte)(3 * (x - 84));
                    b = 0;
                }

                if (x > 170) // colors 170-255
                {
                    r = 0;
                    g = 0;
                    b = (byte)(3 * (x - 170));
                }

                _palette[x] = new SKColor(r, g, b);
            }
        }

        public override void Draw(SKPaintWrapper wrapper, Dictionary<string, object> drawParams)
        {
            if (!_initialized)
            {
                CreatePalette();
                _initialized = true;
            }

            base.Draw(wrapper, drawParams);

            var paint = new SKPaint()
            {
                Color = SKColors.Blue,
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                TextAlign = SKTextAlign.Center,
                TextSize = 24,
                StrokeWidth = 2.0f,
            };

            for (int y = 1; y < wrapper.Canvas.LocalClipBounds.Height; y++)
            {
                for (int x = 1; x < wrapper.Canvas.LocalClipBounds.Width; x++)
                {
                    float dx = (x - 500) / 200.0f;
                    float dy = (y - 500) / 200.0f;

                    //float dx = (x - 500) / 10000.0f - 0.23f;
                    //float dy = (y - 500) / 10000.0f - 0.68f;


                    float a = dx;
                    float b = dy;

                    for (int t = 1; t < 256; t++)
                    {
                        float d = (a * a) - (b * b) + dx;
                        b = 2 * (a * b) + dy;
                        a = d;
                        if (d > 200)
                        {
                            wrapper.Canvas.DrawPoint(x, y, _palette[t]);
                        }
                    }
                }
            }
        }
    }
}
