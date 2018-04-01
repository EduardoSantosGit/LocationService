using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace LocationService.Infrastructure.Common
{
    public static class StringExtensions
    {

        public static string[] SplitString(this string value, string separating)
        {
            string[] array = { separating };
            return value.Split(array, System.StringSplitOptions.RemoveEmptyEntries);
        }

        public static string RemoveDiacritics(this string stIn)
        {
            if (string.IsNullOrWhiteSpace(stIn))
                return string.Empty;

            string stFormD = stIn.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(stFormD[ich]);
            }

            return StringNormalizationExtensions.Normalize(sb.ToString(), NormalizationForm.FormC);
        }

        public static string RemoveNonNumeric(this string source)
        {
            string empty = string.Empty;
            for (int index = 0; index < source.Length; ++index)
            {
                if (char.IsNumber(source[index]))
                    empty += (string)(object)source[index];
            }
            return empty;
        }

        public static string FormatWith(this string source, params object[] parameters)
        {
            return string.Format(CultureInfo.InvariantCulture, source, parameters);
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

    }
}
