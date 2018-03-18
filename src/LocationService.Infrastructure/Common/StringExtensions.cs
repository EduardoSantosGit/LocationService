using System;
using System.Collections.Generic;
using System.Text;

namespace LocationService.Infrastructure.Common
{
    public static class StringExtensions
    {

        public static string[] SplitString(this string value, string separating)
        {
            string[] array = { separating };
            return value.Split(array, System.StringSplitOptions.RemoveEmptyEntries);
        }

    }
}
