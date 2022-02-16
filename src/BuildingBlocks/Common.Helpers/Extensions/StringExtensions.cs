using System.Globalization;

namespace Common.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }

        public static string ToFirstLetterUpperCase(this string value)
        {
            return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1);
        }

        public static string ToFirstLetterUpperCase(this string value, bool query)
        {
            value = value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1);
            if (query)
                value = value.Replace("İ", "I");
            return value;
        }
    }
}
