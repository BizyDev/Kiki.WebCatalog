﻿namespace Kiki.WebApp.Services
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.IO;
    using System.Linq;
    using Data.Models;
    using Extensions;
    using Microsoft.Extensions.Logging;
    using OfficeOpenXml;

    public class ExcelReaderService : IExcelReaderService
    {
        private readonly IPriceCalculatorService _calculatorService;
        private readonly ILogger<ExcelReaderService> _logger;

        public ExcelReaderService(IPriceCalculatorService calculatorService, ILogger<ExcelReaderService> logger)
        {
            _calculatorService = calculatorService;
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
                    var size = string.IsNullOrWhiteSpace(catalog.DiameterColumn) ? worksheet.Cells[catalog.DimensionColumn + row].TryGetValue<string>(catalog.Name, _logger).StringToSize(catalog.SizeFormat) : worksheet.Cells[catalog.DiameterColumn + row].TryGetValue<int>(catalog.Name, _logger);
                    if (size == 0) continue;
                    if (!decimal.TryParse(worksheet.Cells[catalog.BasePriceColumn + row].GetValue<string>(), out var basePrice)) continue;
                    var line = new Product
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
                        FinalPrice = _calculatorService.CalculateFinalPrice(rules, size, basePrice, catalog.DiscountPercentage),
                        FinalPriceGarage = _calculatorService.CalculateFinalGaragePrice(rules, size, basePrice, catalog.DiscountPercentage),
                        Dimension = string.IsNullOrWhiteSpace(catalog.DimensionColumn) ? string.Empty : worksheet.Cells[catalog.DimensionColumn + row].GetValue<string>(),
                        AspectRatio = string.IsNullOrWhiteSpace(catalog.AspectRatioColumn) ? null : worksheet.Cells[catalog.AspectRatioColumn + row].GetValue<string>().ToNullableInt(),
                        Width = string.IsNullOrWhiteSpace(catalog.WidthColumn) ? null : worksheet.Cells[catalog.WidthColumn + row].GetValue<string>().ToNullableInt(),
                        Profil = string.IsNullOrWhiteSpace(catalog.ProfilColumn) ? string.Empty : worksheet.Cells[catalog.ProfilColumn + row].GetValue<string>(),
                        LoadIndexSpeedRating = string.IsNullOrWhiteSpace(catalog.LoadIndexSpeedRatingColumn)
                            ? string.Empty
                            : catalog.LoadIndexSpeedRatingColumn.Split(':').Select(s => worksheet.Cells[s + row].GetValue<string>()).ConvertStringArrayToString()
                    };

                    line.Width = line.Width ?? int.Parse(new string(line.Dimension.Where(char.IsDigit).Take(3).ToArray()));
                    var index = line.Dimension.IndexOf('/');
                    line.AspectRatio = line.AspectRatio ?? (index > 0 ? int.Parse(new string(line.Dimension.Substring(index).Where(char.IsDigit).Take(2).ToArray())) : default(int?));
                    yield return line;
                }
            }
        }

        public IEnumerable<DiscountRule> GetRules(byte[] file)
        {
            using (var ms = new MemoryStream(file))
            using (var package = new ExcelPackage(ms))
            {
                var worksheet = package.Workbook.Worksheets[0];

                var end = worksheet.Dimension.End;
                for (var row = 1; row <= end.Row; row++)
                {
                    var priceCell = worksheet.Cells["A" + row].GetValue<string>();

                    if (string.IsNullOrWhiteSpace(priceCell)) continue;

                    var dimensionsCell = worksheet.Cells["B" + row].GetValue<string>();

                    if (string.IsNullOrWhiteSpace(dimensionsCell)) continue;

                    var margeCell = worksheet.Cells["C" + row].GetValue<string>();

                    if (!decimal.TryParse(margeCell, out var marge)) continue;

                    var margeGarageCell = worksheet.Cells["D" + row].GetValue<string>();

                    if  (!decimal.TryParse(margeGarageCell, out var margeGarage)) continue;

                    var priceArray = priceCell.Split('-');

                    if (((priceArray.Count() == 2) //case XX-YY
                       && decimal.TryParse(priceArray[0], out var priceFrom)
                       && decimal.TryParse(priceArray[1], out var priceTo))
                     || (priceArray.Count() == 1  //case >XX
                      && decimal.TryParse(priceArray[0].Substring(1, priceArray[0].Length -1), out priceFrom) 
                      && decimal.TryParse("999999", out priceTo)))
                    {
                        foreach (var dimensionCell in dimensionsCell.Split(','))
                        {
                            if (!int.TryParse(dimensionCell, out var dimesnion)) continue;
                            yield return new DiscountRule
                            {
                                FromPrice = priceFrom,
                                ToPrice = priceTo,
                                Size = dimesnion,
                                Margin = marge,
                                MarginGarage = margeGarage
                            };
                        }
                    }
                }
            }
        }
    }
}