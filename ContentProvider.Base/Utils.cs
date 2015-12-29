using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContentProvider.Lib
{
    public class StrUtils
    {
        public static string ExtractString(string text, string start, string end, int textIndex)
        {
            if (textIndex >= text.Length) return null;
            int startIndex = text.IndexOf(start, textIndex) + start.Length;
            if (startIndex >= text.Length) return null;
            //int endIndex = text.IndexOf(end, textIndex);
            int endIndex = text.IndexOf(end, startIndex);
            if (startIndex == start.Length - 1 || endIndex == -1) return null;
            string result = text.Substring(startIndex, endIndex - startIndex);
            return result;
        }
    }


}
