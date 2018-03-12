namespace Kiki.WebApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.IO;
    using System.Linq;
    using Data.Models;
    using OfficeOpenXml;

    public class ExcelReaderService
    {
        public IEnumerable<Product> GetLines(Catalog catalog, ImmutableList<DiscountRule> rules, string basePath)
        {
            var existingFile = new FileInfo(basePath + "\\" +catalog.FilePath);
            using (var package = new ExcelPackage(existingFile))
            {
                // get the first worksheet in the workbook
                var worksheet = package.Workbook.Worksheets[catalog.SheetIndex];

                var start = worksheet.Dimension.Start;
                var end = worksheet.Dimension.End;
                for (var row = start.Row; row <= end.Row; row++)
                {
                    var size = StringToSize(worksheet.Cells[catalog.SizeColumn + row].GetValue<string>(), catalog.SizeFormat);
                    if (size == 0) continue;
                    if (!decimal.TryParse(worksheet.Cells[catalog.PriceColumn + row].GetValue<string>(), out var basePrice)) continue;
                    yield return new Product
                    {
                        BasePrice = basePrice,
                        Size = size,
                        Reference = worksheet.Cells[catalog.ReferenceColumn + row].GetValue<string>(),
                        Info1 = worksheet.Cells[catalog.Info1Column + row].GetValue<string>(),
                        Info2 = worksheet.Cells[catalog.Info2Column + row].GetValue<string>(),
                        Info3 = worksheet.Cells[catalog.Info3Column + row].GetValue<string>(),
                        CatalogId = catalog.Id,
                        FinalPrice = CalculateFinalPrice(rules, size, worksheet.Cells[catalog.PriceColumn + row].GetValue<decimal>(), catalog.DiscountPercentage)
                    };
                }
            }
        }

        private static int CalculateFinalPrice(IEnumerable<DiscountRule> rules, int size, decimal price, decimal discount)
        {
            var finalPrice = price - (price / 100 * discount);
            var margin = rules.FirstOrDefault(r => r.Size == size && finalPrice >= r.FromPrice && finalPrice <= r.ToPrice)?.Margin;
            return margin != null ? Convert.ToInt32(finalPrice + margin.Value) : 0;
        }

        private static int StringToSize(string text, SizeFormatEnum sizeFormat)
        {
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
    }
}

