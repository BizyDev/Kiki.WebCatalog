namespace Kiki.WebApp.Extensions
{
    using System;
    using Microsoft.Extensions.Logging;
    using OfficeOpenXml;
    using Services;

    public static class UtilsExtensions
    {
        public static int? ToNullableInt(this string s)
        {
            if (int.TryParse(s, out var i)) return i;
            return null;
        }

        public static T TryGetValue<T>(this ExcelRangeBase excelRangeBase, string catalog, ILogger<ExcelReaderService> logger)
        {
            try
            {
                return excelRangeBase.GetValue<T>();
            }
            catch (Exception)
            {
                logger.LogError($"Impossible de caster la case {excelRangeBase.Address} de {catalog} à {typeof(T)}");
                return default(T);
            }
        }
    }
}