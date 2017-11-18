using System;
using System.Collections.Generic;
using System.Text;

namespace LocationService.Infrastructure.Common
{
    public class ScrapParser
    {
        public string ScrapBlockPage(string text, string indexOn, string indexLast)
        {
            var fisrtIndex = text.LastIndexOf(indexOn);

            fisrtIndex = fisrtIndex + indexOn.Length;

            var lastIndex = text.IndexOf(indexLast);

            var exit = lastIndex - fisrtIndex;

            var block = text.Substring(fisrtIndex, exit);
            return block;
        }

        public bool ContainsValue(string text, string value, bool flgContains)
        {
            var ret = text.Contains(value);
            return (ret == flgContains) ? true : false;
        }

    }
}
