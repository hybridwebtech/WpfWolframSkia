using System.Collections.Generic;

namespace WpfWolframSkia
{
    public interface IDrawable
    {
        void Draw(SKPaintWrapper wrapper, Dictionary<string, object> drawParams);
    }
}