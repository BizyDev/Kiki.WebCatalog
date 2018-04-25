namespace Kiki.WebApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Data.Models;
    using Extensions;
    using Microsoft.Extensions.Logging;
    using OfficeOpenXml;

    public class ExcelReaderService
    {
        private readonly ILogger<ExcelReaderService> _logger;

        public ExcelReaderService(ILogger<ExcelReaderService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Product> GetLines(Catalog catalog, ImmutableList<DiscountRule> rules)
        {
            using (var ms = new MemoryStream(catalog.File))
            using (var package = new ExcelPackage(ms))
            {
                // get the first worksheet in the workbook
                var worksheet = package.Workbook.Worksheets[catalog.SheetIndex];

                var end = worksheet.Dimension.End;
                for (var row = catalog.StartLineNumber; row <= end.Row; row++)
                {
                    var lol = worksheet.Cells[catalog.DimensionColumn + row].TryGetValue<string>(catalog.Name, _logger);
                    var size = string.IsNullOrWhiteSpace(catalog.DiameterColumn) ? StringToSize(worksheet.Cells[catalog.DimensionColumn + row].TryGetValue<string>(catalog.Name, _logger), catalog.SizeFormat) : worksheet.Cells[catalog.DiameterColumn + row].TryGetValue<int>(catalog.Name, _logger);
                    if (size == 0) continue;
                    if (!decimal.TryParse(worksheet.Cells[catalog.BasePriceColumn + row].GetValue<string>(), out var basePrice)) continue;
                    var lines = new Product
                    {
                        BasePrice = basePrice,
                        Dimater = size,
                        Brand = catalog.BrandColumn.Contains("x") ? catalog.Name : worksheet.Cells[catalog.BrandColumn + row].TryGetValue<string>(catalog.Name, _logger),
                        EAN = string.IsNullOrWhiteSpace(catalog.EANColumn) ? string.Empty : worksheet.Cells[catalog.EANColumn + row].GetValue<string>(),
                        Reference = string.IsNullOrWhiteSpace(catalog.ReferenceColumn) ? string.Empty : worksheet.Cells[catalog.ReferenceColumn + row].GetValue<string>(),
                        Info1 = string.IsNullOrWhiteSpace(catalog.Info1Column) ? string.Empty : worksheet.Cells[catalog.Info1Column + row].GetValue<string>(),
                        Info2 = string.IsNullOrWhiteSpace(catalog.Info2Column) ? string.Empty : worksheet.Cells[catalog.Info2Column + row].GetValue<string>(),
                        Info3 = string.IsNullOrWhiteSpace(catalog.Info3Column) ? string.Empty : worksheet.Cells[catalog.Info3Column + row].GetValue<string>(),
                        CatalogId = catalog.Id,
                        FinalPrice = CalculateFinalPrice(rules, size, basePrice, catalog.DiscountPercentage),
                        Dimension = string.IsNullOrWhiteSpace(catalog.DimensionColumn) ? string.Empty : worksheet.Cells[catalog.DimensionColumn + row].GetValue<string>(),
                        AspectRatio = string.IsNullOrWhiteSpace(catalog.AspectRatioColumn) ? null : worksheet.Cells[catalog.AspectRatioColumn + row].GetValue<string>().ToNullableInt(),
                        Width = string.IsNullOrWhiteSpace(catalog.WidthColumn) ? null : worksheet.Cells[catalog.WidthColumn + row].GetValue<string>().ToNullableInt(),
                        Profil = string.IsNullOrWhiteSpace(catalog.ProfilColumn) ? string.Empty : worksheet.Cells[catalog.ProfilColumn + row].GetValue<string>(),
                        LoadIndexSpeedRating = string.IsNullOrWhiteSpace(catalog.LoadIndexSpeedRatingColumn)
                            ? string.Empty
                            : ConvertStringArrayToString(catalog.LoadIndexSpeedRatingColumn.Split(':').Select(s => worksheet.Cells[s + row].GetValue<string>()).ToArray())
                    };

                    lines.Width = lines.Width ?? int.Parse(new string(lines.Dimension.Where(char.IsDigit).Take(3).ToArray()));
                    var index = lines.Dimension.IndexOf('/');
                    lines.AspectRatio = lines.AspectRatio ?? (index > 0 ? int.Parse(new string(lines.Dimension.Substring(index).Where(char.IsDigit).Take(2).ToArray())) : default(int?));
                    yield return lines;
                }
            }
        }

        public static int CalculateFinalPrice(IEnumerable<DiscountRule> rules, int size, decimal price, decimal discount)
        {
            var finalPrice = decimal.Ceiling(price - price / 100 * discount);
            var margin = rules.FirstOrDefault(r => r.Size == size && finalPrice >= r.FromPrice && finalPrice <= r.ToPrice)?.Margin;
            return margin != null ? Convert.ToInt32(finalPrice + margin.Value) : 0;
        }

        private static int StringToSize(string text, SizeFormatEnum sizeFormat)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0;
            text = text.ToLower().TrimEnd();
            switch (sizeFormat)
            {
                case SizeFormatEnum.Last2AlphaNumeric:
                    text = text.Substring(text.Length - 2, 2);
                    break;
                case SizeFormatEnum.Rxx:
                    var lastR = text.IndexOf('r');
                    text = lastR >= 0 && text.Length - lastR > 1 ? text.Substring(text.IndexOf('r') + 1, 2) : "0";
                    break;
                case SizeFormatEnum.Simple:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sizeFormat), sizeFormat, null);
            }

            int.TryParse(text, out var size);
            return size;
        }

        private static string ConvertStringArrayToString(string[] array)
        {
            var builder = new StringBuilder();
            foreach (var value in array) builder.Append(value);
            return builder.ToString();
        }
    }
}