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

        internal const string REGEX_VALIDATE_CEP = @"^\d{2}\.?\d{3}\-?\d{3}$";

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

         public static string RemoveNonAlphaNumeric(this string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;
            return new string(Array.FindAll<char>(source.ToCharArray(), (Predicate<char>)(c =>
            {
                if (!char.IsLetterOrDigit(c) && !char.IsWhiteSpace(c))
                    return (int)c == 45;
                return true;
            })));
        }
        public static string FixCep(this string cep)
        {
            if (cep != null)
            {
                cep = cep.Trim();
                if (cep.IndexOf("-") < 0 && cep.Length == 8)
                    cep = cep.Insert(5, "-");
            }
            return cep;
        }
        public static bool IsValidCep(this string cep)
        {
            if (cep.Length != 8 && cep.Length != 9 && cep.Length != 10)
                return false;
            var match = Regex.Match(cep, REGEX_VALIDATE_CEP);
            return match.Success;
        }

         public static string RemoveWhiteSpaces(this string current)
        {
            return string.Join(" ", current.Split(new char[] { ' ' },
                   StringSplitOptions.RemoveEmptyEntries));
        }

         public static string Truncate(this string text, int maxCharacters)
        {
            return text.Truncate(maxCharacters, "..");
        }

        public static string Truncate(this string text, int maxCharacters, string trailingText)
        {
            if (string.IsNullOrEmpty(text) || maxCharacters <= 0 || text.Length <= maxCharacters)
                return text;
            return text.Substring(0, maxCharacters) + trailingText;
        }

        public static T ConvertJsonStringToObject<T>(this string stringToDeserialize)
        {
            return JsonConvert.DeserializeObject<T>(stringToDeserialize);
        }

        public static long ParseLong(this string current, long defaultValueOnNullOrEmpty = 0, string fieldName = null)
        {
            var res = long.TryParse(current, out long result);

            var isEmpty = string.IsNullOrWhiteSpace(current) && current != null;
            if (isEmpty)
                return defaultValueOnNullOrEmpty;

            if (!res || isEmpty)
                throw new FormatException($"can not parse {fieldName + " " ?? ""}to long with value {current}");

            return result;
        }

        public static int ParseInt(this string current, int defaultValueOnNullOrEmpty = 0, string fieldName = null)
        {
            var res = int.TryParse(current, out int result);

            var isEmpty = string.IsNullOrWhiteSpace(current) && current != null;
            if (isEmpty)
                return defaultValueOnNullOrEmpty;

            if (!res || isEmpty)
                throw new FormatException($"can not parse {fieldName + " " ?? ""}to int with value {current}");

            return result;
        }
    }
}
