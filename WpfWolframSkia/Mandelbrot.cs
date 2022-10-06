using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfWolframSkia;

namespace WpfWolfram
{
    public class Mandelbrot : BaseDrawable
    {
        static int MAXCOLORVALUES = 256;

        private bool _initialized = false;

        private string[] _jetColorMap = new string[MAXCOLORVALUES];

        private byte[] _colorLut16 = new byte[65536];

        const int REDINDEX = 0;
        const int GREENINDEX = 1;
        const int BLUEINDEX = 2;

        private string[] LoadColorMap(string colormapFile)
        {
            string[] colorMap = new String[MAXCOLORVALUES];

            // Load in the Color Table
            string line;


            // Read the file and display it line by line.
            using (System.IO.StreamReader file =
                   new System.IO.StreamReader(colormapFile))
            {

                    int index = 0;
                    while (((line = file.ReadLine()) != null) && (index < MAXCOLORVALUES))
                        colorMap[index++] = line;
            }

            return colorMap;
        }

        private void ComputeLookUpTable16_UsingColormap(ref byte[] lut16, int winMin, int winMax)
        {
            string[] colormap = _jetColorMap;

            // Use the Table to preset the values for the current W/L Settings..
            if (winMax == 0)
                winMax = 65536;
            int range = winMax - winMin;
            if (range < 1) range = 1;
            double factor = 255.0 / range;
            int i;

            for (i = 0; i < 65536 / 3; i = i + 1)
            {
                int valueToSearch = i;
                if (i <= winMin)
                {
                    valueToSearch = winMin;
                }
                else if (i >= winMax)
                {
                    valueToSearch = winMax;
                }

                int colorTableIndex = (int)Math.Round(((valueToSearch - winMin) * factor), 0);
                String hexColor = (System.Convert.ToInt32(colormap[colorTableIndex])).ToString("X6");
                lut16[(i * 3) + REDINDEX] = convertToHex(hexColor.Substring(0, 2));
                lut16[(i * 3) + GREENINDEX] = convertToHex(hexColor.Substring(2, 2));
                lut16[(i * 3) + BLUEINDEX] = convertToHex(hexColor.Substring(4, 2));

            }
        }
        public static byte convertToHex(String hexString)
        {
            return (byte)Int32.Parse(hexString, NumberStyles.HexNumber);
        }

        private void Initialize()
        {
            _jetColorMap = LoadColorMap("jetColorMap.txt");

            ComputeLookUpTable16_UsingColormap(ref _colorLut16, 0, 65535);

            _initialized = true;
        }

        public override void Draw(SKPaintWrapper wrapper, Dictionary<string, object> drawParams)
        {
            if (!_initialized)
            {
                Initialize();
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
                    //float dx = (x - 500) / 200.0f;
                    //float dy = (y - 500) / 200.0f;

                    float dx = (x - 500) / 10000 - 0.23f;
                    float dy = (y - 500) / 10000 - 0.68f;


                    float a = dx;
                    float b = dy;

                    for (int t = 1; t < 200; t++)
                    {
                        float d = (a * a) - (b * b) + dx;
                        b = 2 * (a * b) + dy;
                        a = d;
                        if (d > 200)
                        {
                            if (t > 10)
                            {
                                int xxx = 3;
                            }

                            //int lookupIndex = t * 3;
                            //int lookupIndex = (int)Math.Round(Math.Log10(d));
                            int lookupIndex = (int)(65535 * (t / 200.0f));
                            if (lookupIndex > 65533)
                            {
                                lookupIndex = 65533;
                            }
                            byte red = _colorLut16[lookupIndex + REDINDEX];
                            byte green = _colorLut16[lookupIndex + GREENINDEX];
                            byte blue = _colorLut16[lookupIndex + BLUEINDEX];

                            wrapper.Canvas.DrawPoint(x, y, new SKColor(red, green, blue));
                        }
                    }
                }
            }
        }
    }
}
