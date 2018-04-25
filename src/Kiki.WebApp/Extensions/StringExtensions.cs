namespace Kiki.WebApp.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Data.Models;

    public static class StringExtensions
    {
        public static int StringToSize(this string text, SizeFormatEnum sizeFormat)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0;
            text = text.ToLower().TrimEnd();
            switch (sizeFormat)
            {
                case SizeFormatEnum.Last2AlphaNumeric:
                    text = text.Substring(text.Length - 2, 2);
                    break;
                case SizeFormatEnum.Rxx:
                    var lastR = text.LastIndexOf('r');
                    text = lastR >= 0 && text.Length - lastR > 1 ? text.Substring(text.LastIndexOf('r') + 1, 2) : "0";
                    break;
                case SizeFormatEnum.Simple:
                    break;
                case SizeFormatEnum.Vredestein:
                    var firstRSpace = text.IndexOf("r ", StringComparison.OrdinalIgnoreCase);
                    text = firstRSpace >= 0 && text.Length - firstRSpace > 1 ? text.Substring(firstRSpace + 2, 2) : "0";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sizeFormat), sizeFormat, null);
            }

            int.TryParse(text, out var size);
            return size;
        }

        public static string ConvertStringArrayToString(this IEnumerable<string> array)
        {
            var builder = new StringBuilder();
            foreach (var value in array) builder.Append(value);
            return builder.ToString();
        }
    }
}