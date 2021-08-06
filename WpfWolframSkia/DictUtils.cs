using System.Collections.Generic;

namespace WpfWolframSkia
{
    public static class DictUtils
    {
        public static T GetValue<T>(Dictionary<string, object> dictParams, string paramName,
            T defaultValue = default(T))
        {
            if (dictParams.ContainsKey(paramName))
            {
                object temp;
                if (dictParams.TryGetValue(paramName, out temp))
                {
                    return (T)temp;
                }
            }

            return defaultValue;
        }
    }
}