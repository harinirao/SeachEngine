using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ZendeskSearch.Helpers
{
    public static class TitleCaseString
    {
        public static string ToTitleCase(string val)
        {
            TextInfo textInfo = new CultureInfo("en-us", false).TextInfo;
            return textInfo.ToTitleCase(val);
        }
    }
}
