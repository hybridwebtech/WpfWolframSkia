using System;
using MathNet.Numerics;
using SkiaSharp;

namespace WpfWolframSkia
{
    public class Draw3D
    {
        public void Draw(SKPaintWrapper wrapper)
        {
            SKCanvas canvas = wrapper.Canvas;

            canvas.Clear(SKColors.White);

            // center the entire drawing
            canvas.Translate(canvas.LocalClipBounds.MidX, canvas.LocalClipBounds.MidY);
            
            canvas.Scale(3.0f, 3.0f);

            // the "3D camera"
            using (var view = new SK3dView())
            {
                // rotate to a nice 3D view
                view.RotateXDegrees(-25);
                view.RotateYDegrees(45);

                // move the origin of the 3D view
                view.Translate(-50, 50, 50);

                // define the cube face
                var face = SKRect.Create(0, 0, 100, 100);

                // draw the left face
                using (new SKAutoCanvasRestore(canvas, true))
                {
                    // get the face in the correct location
                    view.Save();
                    view.RotateYDegrees(-90);
                    view.ApplyToCanvas(canvas);
                    view.Restore();

                    // draw the face
                    var leftFace = new SKPaint
                    {
                        Color = SKColors.LightGray,
                        IsAntialias = true
                    };
                    canvas.DrawRect(face, leftFace);
                }

                // draw the right face
                using (new SKAutoCanvasRestore(canvas, true))
                {
                    // get the face in the correct location
                    view.Save();
                    view.TranslateZ(-100);
                    view.ApplyToCanvas(canvas);
                    view.Restore();

                    // draw the face
                    var rightFace = new SKPaint
                    {
                        Color = SKColors.Gray,
                        IsAntialias = true
                    };
                    canvas.DrawRect(face, rightFace);
                }

                // draw the top face
                using (new SKAutoCanvasRestore(canvas, true))
                {
                    // get the face in the correct location
                    view.Save();
                    view.RotateXDegrees(90);
                    view.ApplyToCanvas(canvas);
                    view.Restore();

                    // draw the face
                    var topFace = new SKPaint
                    {
                        Color = SKColors.DarkGray,
                        IsAntialias = true
                    };
                    canvas.DrawRect(face, topFace);
                }
            }
        }
    }
}