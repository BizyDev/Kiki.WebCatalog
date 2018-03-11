﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kiki.WebApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Kiki.WebApp.Data
{
    using System.Linq;

    public class ApplicationDbContextSeed
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextSeed(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
                try
                {
                    await _context.Database.MigrateAsync();
                }
                catch (Exception e)
                {
                    throw;
                    //TODO Log
                }
            if (!await _context.Catalogs.AnyAsync()) await SeedCatalogsAsync();
            if (!await _context.DiscountRules.AnyAsync()) await SeedDiscountRulesAsync();
        }

        private async Task SeedCatalogsAsync()
        {
            var catalogs = new List<Catalog>
            {
                new Catalog
                {
                    Name = "BF Goodrich",
                    SheetIndex = 0,
                    PriceColumn = "AA",
                    SizeColumn = "U",
                    ReferenceColumn = "V",
                    Info1Column = "T",
                    Info2Column = "W",
                    Info3Column = "",
                    StartLineNumber = 5,
                    DiscountPercentage = 30,
                    SizeFormat = 0,
                    FilePath = "Prix été BF Goodrich.xlsx"
                },
                new Catalog
                {
                    Name = "Bridgestone",
                    SheetIndex = 0,
                    PriceColumn = "O",
                    SizeColumn = "E",
                    ReferenceColumn = "",
                    Info1Column = "B",
                    Info2Column = "H",
                    Info3Column = "",
                    StartLineNumber = 11,
                    DiscountPercentage = 52,
                    SizeFormat = 0,
                    FilePath = "Prix été Bridgestone.xlsm"
                },
                new Catalog
                {
                    Name = "Continental",
                    SheetIndex = 0,
                    PriceColumn = "K",
                    SizeColumn = "C",
                    ReferenceColumn = "D",
                    Info1Column = "C",
                    Info2Column = "F",
                    Info3Column = "",
                    StartLineNumber = 5,
                    DiscountPercentage = 56,
                    SizeFormat = (SizeFormatEnum) 1,
                    FilePath = "Prix été Conti, Uni, Semp, Barum.xlsx"
                },
                new Catalog
                {
                    Name = "Uniroyal",
                    SheetIndex = 1,
                    PriceColumn = "K",
                    SizeColumn = "C",
                    ReferenceColumn = "D",
                    Info1Column = "C",
                    Info2Column = "F",
                    Info3Column = "",
                    StartLineNumber = 5,
                    DiscountPercentage = 60,
                    SizeFormat = (SizeFormatEnum) 1,
                    FilePath = "Prix été Conti, Uni, Semp, Barum.xlsx"
                },
                new Catalog
                {
                    Name = "Semperit",
                    SheetIndex = 2,
                    PriceColumn = "K",
                    SizeColumn = "C",
                    ReferenceColumn = "D",
                    Info1Column = "C",
                    Info2Column = "F",
                    Info3Column = "",
                    StartLineNumber = 5,
                    DiscountPercentage = 60,
                    SizeFormat = (SizeFormatEnum) 1,
                    FilePath = "Prix été Conti, Uni, Semp, Barum.xlsx"
                },
                new Catalog
                {
                    Name = "Barum",
                    SheetIndex = 3,
                    PriceColumn = "K",
                    SizeColumn = "C",
                    ReferenceColumn = "D",
                    Info1Column = "C",
                    Info2Column = "F",
                    Info3Column = "",
                    StartLineNumber = 5,
                    DiscountPercentage = 60,
                    SizeFormat = (SizeFormatEnum) 1,
                    FilePath = "Prix été Conti, Uni, Semp, Barum.xlsx"
                },
                new Catalog
                {
                    Name = "Cooper",
                    SheetIndex = 3,
                    PriceColumn = "H",
                    SizeColumn = "C",
                    ReferenceColumn = "B",
                    Info1Column = "C",
                    Info2Column = "D",
                    Info3Column = "",
                    StartLineNumber = 18,
                    DiscountPercentage = 52,
                    SizeFormat = (SizeFormatEnum) 1,
                    FilePath = "Prix été Cooper.xlsx"
                },
                new Catalog
                {
                    Name = "Firestone",
                    SheetIndex = 0,
                    PriceColumn = "M",
                    SizeColumn = "D",
                    ReferenceColumn = "",
                    Info1Column = "A",
                    Info2Column = "H",
                    Info3Column = "",
                    StartLineNumber = 11,
                    DiscountPercentage = 52,
                    SizeFormat = 0,
                    FilePath = "Prix été Firestone.xlsx"
                },
                new Catalog
                {
                    Name = "Formula",
                    SheetIndex = 0,
                    PriceColumn = "D",
                    SizeColumn = "C",
                    ReferenceColumn = "F",
                    Info1Column = "",
                    Info2Column = "",
                    Info3Column = "",
                    StartLineNumber = 2,
                    DiscountPercentage = 66,
                    SizeFormat = (SizeFormatEnum) 2,
                    FilePath = "Prix été Formula .xlsx"
                },
                new Catalog
                {
                    Name = "Goodyear, Dunlop, Fulda, Sava",
                    SheetIndex = 0,
                    PriceColumn = "K",
                    SizeColumn = "C",
                    ReferenceColumn = "",
                    Info1Column = "G",
                    Info2Column = "D",
                    Info3Column = "",
                    StartLineNumber = 2,
                    DiscountPercentage = 46,
                    SizeFormat = (SizeFormatEnum) 2,
                    FilePath = "Prix été Goodyear, Dunlop, Sava, Fulda.xlsx"
                },
                new Catalog
                {
                    Name = "Kleber",
                    SheetIndex = 0,
                    PriceColumn = "AA",
                    SizeColumn = "U",
                    ReferenceColumn = "V",
                    Info1Column = "T",
                    Info2Column = "W",
                    Info3Column = "",
                    StartLineNumber = 5,
                    DiscountPercentage = 48,
                    SizeFormat = 0,
                    FilePath = "Prix été Kleber.xlsx"
                },
                new Catalog
                {
                    Name = "Michelin",
                    SheetIndex = 0,
                    PriceColumn = "AA",
                    SizeColumn = "U",
                    ReferenceColumn = "V",
                    Info1Column = "T",
                    Info2Column = "W",
                    Info3Column = "",
                    StartLineNumber = 5,
                    DiscountPercentage = 48,
                    SizeFormat = 0,
                    FilePath = "Prix été Michelin .xlsx"
                },
                new Catalog
                {
                    Name = "Nokian",
                    SheetIndex = 0,
                    PriceColumn = "G",
                    SizeColumn = "B",
                    ReferenceColumn = "H",
                    Info1Column = "B",
                    Info2Column = "C",
                    Info3Column = "",
                    StartLineNumber = 16,
                    DiscountPercentage = 47,
                    SizeFormat = (SizeFormatEnum) 2,
                    FilePath = "Prix été Nokian.xlsx"
                },
                new Catalog
                {
                    Name = "Pirelli",
                    SheetIndex = 0,
                    PriceColumn = "D",
                    SizeColumn = "C",
                    ReferenceColumn = "F",
                    Info1Column = "C",
                    Info2Column = "",
                    Info3Column = "",
                    StartLineNumber = 2,
                    DiscountPercentage = 55,
                    SizeFormat = (SizeFormatEnum) 2,
                    FilePath = "Prix été Pirelli.xlsx"
                },
                new Catalog
                {
                    Name = "Seiberling",
                    SheetIndex = 0,
                    PriceColumn = "I",
                    SizeColumn = "A",
                    ReferenceColumn = "",
                    Info1Column = "A",
                    Info2Column = "D",
                    Info3Column = "",
                    StartLineNumber = 11,
                    DiscountPercentage = 48,
                    SizeFormat = (SizeFormatEnum) 2,
                    FilePath = "Prix été Seiberling.xlsm"
                },
                new Catalog
                {
                    Name = "Tigar",
                    SheetIndex = 0,
                    PriceColumn = "AA",
                    SizeColumn = "U",
                    ReferenceColumn = "V",
                    Info1Column = "T",
                    Info2Column = "W",
                    Info3Column = "",
                    StartLineNumber = 5,
                    DiscountPercentage = 30,
                    SizeFormat = 0,
                    FilePath = "Prix été Tigar TTC..xlsx"
                },
                new Catalog
                {
                    Name = "Yokohama",
                    SheetIndex = 0,
                    PriceColumn = "G",
                    SizeColumn = "B",
                    ReferenceColumn = "E",
                    Info1Column = "B",
                    Info2Column = "F",
                    Info3Column = "C",
                    StartLineNumber = 12,
                    DiscountPercentage = 64,
                    SizeFormat = (SizeFormatEnum) 2,
                    FilePath = "Prix été Yoko.xlsx"
                }
            };
            try
            {
                await _context.Catalogs.AddRangeAsync(catalogs);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;
                //TODO Log
            }
        }

        private async Task SeedDiscountRulesAsync()
        {
            var discountRules = new List<DiscountRule>
            {
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 10, Margin = 30 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 10, Margin = 32 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 10, Margin = 35 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 10, Margin = 37 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 10, Margin = 39 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 10, Margin = 41 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 10, Margin = 43 },
                new DiscountRule { FromPrice = 120, ToPrice = 99999, Size = 10, Margin = 45 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 11, Margin = 30 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 11, Margin = 32 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 11, Margin = 35 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 11, Margin = 37 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 11, Margin = 39 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 11, Margin = 41 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 11, Margin = 43 },
                new DiscountRule { FromPrice = 120, ToPrice = 99999, Size = 11, Margin = 45 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 12, Margin = 30 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 12, Margin = 32 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 12, Margin = 35 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 12, Margin = 37 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 12, Margin = 39 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 12, Margin = 41 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 12, Margin = 43 },
                new DiscountRule { FromPrice = 120, ToPrice = 99999, Size = 12, Margin = 45 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 13, Margin = 30 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 13, Margin = 32 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 13, Margin = 35 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 13, Margin = 37 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 13, Margin = 39 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 13, Margin = 41 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 13, Margin = 43 },
                new DiscountRule { FromPrice = 120, ToPrice = 99999, Size = 13, Margin = 45 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 14, Margin = 30 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 14, Margin = 32 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 14, Margin = 35 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 14, Margin = 37 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 14, Margin = 39 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 14, Margin = 41 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 14, Margin = 43 },
                new DiscountRule { FromPrice = 120, ToPrice = 99999, Size = 14, Margin = 46 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 15, Margin = 40 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 15, Margin = 40 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 15, Margin = 43 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 15, Margin = 47 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 15, Margin = 49 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 15, Margin = 51 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 15, Margin = 53 },
                new DiscountRule { FromPrice = 120, ToPrice = 135, Size = 15, Margin = 56 },
                new DiscountRule { FromPrice = 135, ToPrice = 99999, Size = 15, Margin = 59 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 16, Margin = 40 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 16, Margin = 43 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 16, Margin = 45 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 16, Margin = 50 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 16, Margin = 55 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 16, Margin = 60 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 16, Margin = 60 },
                new DiscountRule { FromPrice = 120, ToPrice = 135, Size = 16, Margin = 65 },
                new DiscountRule { FromPrice = 135, ToPrice = 150, Size = 16, Margin = 65 },
                new DiscountRule { FromPrice = 150, ToPrice = 165, Size = 16, Margin = 70 },
                new DiscountRule { FromPrice = 165, ToPrice = 180, Size = 16, Margin = 70 },
                new DiscountRule { FromPrice = 180, ToPrice = 99999, Size = 16, Margin = 75 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 17, Margin = 45 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 17, Margin = 48 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 17, Margin = 51 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 17, Margin = 54 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 17, Margin = 57 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 17, Margin = 60 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 17, Margin = 63 },
                new DiscountRule { FromPrice = 120, ToPrice = 135, Size = 17, Margin = 66 },
                new DiscountRule { FromPrice = 135, ToPrice = 150, Size = 17, Margin = 69 },
                new DiscountRule { FromPrice = 150, ToPrice = 165, Size = 17, Margin = 71 },
                new DiscountRule { FromPrice = 165, ToPrice = 180, Size = 17, Margin = 73 },
                new DiscountRule { FromPrice = 180, ToPrice = 195, Size = 17, Margin = 75 },
                new DiscountRule { FromPrice = 195, ToPrice = 210, Size = 17, Margin = 77 },
                new DiscountRule { FromPrice = 210, ToPrice = 225, Size = 17, Margin = 78 },
                new DiscountRule { FromPrice = 225, ToPrice = 240, Size = 17, Margin = 79 },
                new DiscountRule { FromPrice = 240, ToPrice = 255, Size = 17, Margin = 80 },
                new DiscountRule { FromPrice = 255, ToPrice = 270, Size = 17, Margin = 81 },
                new DiscountRule { FromPrice = 270, ToPrice = 285, Size = 17, Margin = 83 },
                new DiscountRule { FromPrice = 285, ToPrice = 300, Size = 17, Margin = 85 },
                new DiscountRule { FromPrice = 300, ToPrice = 350, Size = 17, Margin = 88 },
                new DiscountRule { FromPrice = 350, ToPrice = 400, Size = 17, Margin = 90 },
                new DiscountRule { FromPrice = 400, ToPrice = 99999, Size = 17, Margin = 90 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 18, Margin = 55 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 18, Margin = 55 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 18, Margin = 65 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 18, Margin = 65 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 18, Margin = 65 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 18, Margin = 70 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 18, Margin = 70 },
                new DiscountRule { FromPrice = 120, ToPrice = 135, Size = 18, Margin = 75 },
                new DiscountRule { FromPrice = 135, ToPrice = 150, Size = 18, Margin = 75 },
                new DiscountRule { FromPrice = 150, ToPrice = 165, Size = 18, Margin = 80 },
                new DiscountRule { FromPrice = 165, ToPrice = 180, Size = 18, Margin = 85 },
                new DiscountRule { FromPrice = 180, ToPrice = 195, Size = 18, Margin = 90 },
                new DiscountRule { FromPrice = 195, ToPrice = 210, Size = 18, Margin = 90 },
                new DiscountRule { FromPrice = 210, ToPrice = 225, Size = 18, Margin = 95 },
                new DiscountRule { FromPrice = 225, ToPrice = 240, Size = 18, Margin = 95 },
                new DiscountRule { FromPrice = 240, ToPrice = 255, Size = 18, Margin = 95 },
                new DiscountRule { FromPrice = 255, ToPrice = 270, Size = 18, Margin = 95 },
                new DiscountRule { FromPrice = 270, ToPrice = 285, Size = 18, Margin = 100 },
                new DiscountRule { FromPrice = 285, ToPrice = 300, Size = 18, Margin = 100 },
                new DiscountRule { FromPrice = 300, ToPrice = 315, Size = 18, Margin = 105 },
                new DiscountRule { FromPrice = 315, ToPrice = 330, Size = 18, Margin = 105 },
                new DiscountRule { FromPrice = 330, ToPrice = 345, Size = 18, Margin = 105 },
                new DiscountRule { FromPrice = 345, ToPrice = 360, Size = 18, Margin = 105 },
                new DiscountRule { FromPrice = 360, ToPrice = 99999, Size = 18, Margin = 105 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 19, Margin = 55 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 19, Margin = 55 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 19, Margin = 60 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 19, Margin = 60 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 19, Margin = 65 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 19, Margin = 70 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 19, Margin = 70 },
                new DiscountRule { FromPrice = 120, ToPrice = 135, Size = 19, Margin = 75 },
                new DiscountRule { FromPrice = 135, ToPrice = 150, Size = 19, Margin = 80 },
                new DiscountRule { FromPrice = 150, ToPrice = 165, Size = 19, Margin = 80 },
                new DiscountRule { FromPrice = 165, ToPrice = 180, Size = 19, Margin = 80 },
                new DiscountRule { FromPrice = 180, ToPrice = 195, Size = 19, Margin = 85 },
                new DiscountRule { FromPrice = 195, ToPrice = 210, Size = 19, Margin = 85 },
                new DiscountRule { FromPrice = 210, ToPrice = 225, Size = 19, Margin = 95 },
                new DiscountRule { FromPrice = 225, ToPrice = 240, Size = 19, Margin = 95 },
                new DiscountRule { FromPrice = 240, ToPrice = 255, Size = 19, Margin = 100 },
                new DiscountRule { FromPrice = 255, ToPrice = 270, Size = 19, Margin = 100 },
                new DiscountRule { FromPrice = 270, ToPrice = 285, Size = 19, Margin = 105 },
                new DiscountRule { FromPrice = 285, ToPrice = 300, Size = 19, Margin = 105 },
                new DiscountRule { FromPrice = 300, ToPrice = 315, Size = 19, Margin = 105 },
                new DiscountRule { FromPrice = 315, ToPrice = 330, Size = 19, Margin = 105 },
                new DiscountRule { FromPrice = 330, ToPrice = 345, Size = 19, Margin = 105 },
                new DiscountRule { FromPrice = 345, ToPrice = 360, Size = 19, Margin = 105 },
                new DiscountRule { FromPrice = 360, ToPrice = 99999, Size = 19, Margin = 105 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 20, Margin = 55 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 20, Margin = 55 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 20, Margin = 60 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 20, Margin = 60 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 20, Margin = 65 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 20, Margin = 70 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 20, Margin = 70 },
                new DiscountRule { FromPrice = 120, ToPrice = 135, Size = 20, Margin = 75 },
                new DiscountRule { FromPrice = 135, ToPrice = 150, Size = 20, Margin = 80 },
                new DiscountRule { FromPrice = 150, ToPrice = 165, Size = 20, Margin = 80 },
                new DiscountRule { FromPrice = 165, ToPrice = 180, Size = 20, Margin = 80 },
                new DiscountRule { FromPrice = 180, ToPrice = 195, Size = 20, Margin = 85 },
                new DiscountRule { FromPrice = 195, ToPrice = 210, Size = 20, Margin = 90 },
                new DiscountRule { FromPrice = 210, ToPrice = 225, Size = 20, Margin = 90 },
                new DiscountRule { FromPrice = 225, ToPrice = 240, Size = 20, Margin = 95 },
                new DiscountRule { FromPrice = 240, ToPrice = 255, Size = 20, Margin = 95 },
                new DiscountRule { FromPrice = 255, ToPrice = 270, Size = 20, Margin = 95 },
                new DiscountRule { FromPrice = 270, ToPrice = 285, Size = 20, Margin = 100 },
                new DiscountRule { FromPrice = 285, ToPrice = 300, Size = 20, Margin = 100 },
                new DiscountRule { FromPrice = 300, ToPrice = 315, Size = 20, Margin = 105 },
                new DiscountRule { FromPrice = 315, ToPrice = 330, Size = 20, Margin = 105 },
                new DiscountRule { FromPrice = 330, ToPrice = 345, Size = 20, Margin = 105 },
                new DiscountRule { FromPrice = 345, ToPrice = 360, Size = 20, Margin = 110 },
                new DiscountRule { FromPrice = 360, ToPrice = 375, Size = 20, Margin = 110 },
                new DiscountRule { FromPrice = 375, ToPrice = 390, Size = 20, Margin = 110 },
                new DiscountRule { FromPrice = 390, ToPrice = 99999, Size = 20, Margin = 110 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 21, Margin = 55 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 21, Margin = 55 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 21, Margin = 60 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 21, Margin = 60 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 21, Margin = 65 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 21, Margin = 70 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 21, Margin = 70 },
                new DiscountRule { FromPrice = 120, ToPrice = 135, Size = 21, Margin = 75 },
                new DiscountRule { FromPrice = 135, ToPrice = 150, Size = 21, Margin = 80 },
                new DiscountRule { FromPrice = 150, ToPrice = 165, Size = 21, Margin = 80 },
                new DiscountRule { FromPrice = 165, ToPrice = 180, Size = 21, Margin = 80 },
                new DiscountRule { FromPrice = 180, ToPrice = 195, Size = 21, Margin = 85 },
                new DiscountRule { FromPrice = 195, ToPrice = 210, Size = 21, Margin = 90 },
                new DiscountRule { FromPrice = 210, ToPrice = 225, Size = 21, Margin = 90 },
                new DiscountRule { FromPrice = 225, ToPrice = 240, Size = 21, Margin = 95 },
                new DiscountRule { FromPrice = 240, ToPrice = 255, Size = 21, Margin = 95 },
                new DiscountRule { FromPrice = 255, ToPrice = 270, Size = 21, Margin = 95 },
                new DiscountRule { FromPrice = 270, ToPrice = 285, Size = 21, Margin = 100 },
                new DiscountRule { FromPrice = 285, ToPrice = 300, Size = 21, Margin = 100 },
                new DiscountRule { FromPrice = 300, ToPrice = 315, Size = 21, Margin = 105 },
                new DiscountRule { FromPrice = 315, ToPrice = 330, Size = 21, Margin = 105 },
                new DiscountRule { FromPrice = 330, ToPrice = 345, Size = 21, Margin = 105 },
                new DiscountRule { FromPrice = 345, ToPrice = 360, Size = 21, Margin = 110 },
                new DiscountRule { FromPrice = 360, ToPrice = 375, Size = 21, Margin = 110 },
                new DiscountRule { FromPrice = 375, ToPrice = 390, Size = 21, Margin = 110 },
                new DiscountRule { FromPrice = 390, ToPrice = 99999, Size = 21, Margin = 110 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 22, Margin = 55 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 22, Margin = 55 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 22, Margin = 60 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 22, Margin = 60 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 22, Margin = 65 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 22, Margin = 70 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 22, Margin = 70 },
                new DiscountRule { FromPrice = 120, ToPrice = 135, Size = 22, Margin = 75 },
                new DiscountRule { FromPrice = 135, ToPrice = 150, Size = 22, Margin = 80 },
                new DiscountRule { FromPrice = 150, ToPrice = 165, Size = 22, Margin = 80 },
                new DiscountRule { FromPrice = 165, ToPrice = 180, Size = 22, Margin = 80 },
                new DiscountRule { FromPrice = 180, ToPrice = 195, Size = 22, Margin = 85 },
                new DiscountRule { FromPrice = 195, ToPrice = 210, Size = 22, Margin = 90 },
                new DiscountRule { FromPrice = 210, ToPrice = 225, Size = 22, Margin = 90 },
                new DiscountRule { FromPrice = 225, ToPrice = 240, Size = 22, Margin = 95 },
                new DiscountRule { FromPrice = 240, ToPrice = 255, Size = 22, Margin = 95 },
                new DiscountRule { FromPrice = 255, ToPrice = 270, Size = 22, Margin = 95 },
                new DiscountRule { FromPrice = 270, ToPrice = 285, Size = 22, Margin = 100 },
                new DiscountRule { FromPrice = 285, ToPrice = 300, Size = 22, Margin = 100 },
                new DiscountRule { FromPrice = 300, ToPrice = 315, Size = 22, Margin = 105 },
                new DiscountRule { FromPrice = 315, ToPrice = 330, Size = 22, Margin = 105 },
                new DiscountRule { FromPrice = 330, ToPrice = 345, Size = 22, Margin = 105 },
                new DiscountRule { FromPrice = 345, ToPrice = 360, Size = 22, Margin = 110 },
                new DiscountRule { FromPrice = 360, ToPrice = 375, Size = 22, Margin = 110 },
                new DiscountRule { FromPrice = 375, ToPrice = 390, Size = 22, Margin = 110 },
                new DiscountRule { FromPrice = 390, ToPrice = 99999, Size = 22, Margin = 110 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 23, Margin = 55 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 23, Margin = 55 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 23, Margin = 60 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 23, Margin = 60 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 23, Margin = 65 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 23, Margin = 70 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 23, Margin = 70 },
                new DiscountRule { FromPrice = 120, ToPrice = 135, Size = 23, Margin = 75 },
                new DiscountRule { FromPrice = 135, ToPrice = 150, Size = 23, Margin = 80 },
                new DiscountRule { FromPrice = 150, ToPrice = 165, Size = 23, Margin = 80 },
                new DiscountRule { FromPrice = 165, ToPrice = 180, Size = 23, Margin = 80 },
                new DiscountRule { FromPrice = 180, ToPrice = 195, Size = 23, Margin = 85 },
                new DiscountRule { FromPrice = 195, ToPrice = 210, Size = 23, Margin = 90 },
                new DiscountRule { FromPrice = 210, ToPrice = 225, Size = 23, Margin = 90 },
                new DiscountRule { FromPrice = 225, ToPrice = 240, Size = 23, Margin = 95 },
                new DiscountRule { FromPrice = 240, ToPrice = 255, Size = 23, Margin = 95 },
                new DiscountRule { FromPrice = 255, ToPrice = 270, Size = 23, Margin = 95 },
                new DiscountRule { FromPrice = 270, ToPrice = 285, Size = 23, Margin = 100 },
                new DiscountRule { FromPrice = 285, ToPrice = 300, Size = 23, Margin = 100 },
                new DiscountRule { FromPrice = 300, ToPrice = 315, Size = 23, Margin = 105 },
                new DiscountRule { FromPrice = 315, ToPrice = 330, Size = 23, Margin = 105 },
                new DiscountRule { FromPrice = 330, ToPrice = 345, Size = 23, Margin = 105 },
                new DiscountRule { FromPrice = 345, ToPrice = 360, Size = 23, Margin = 110 },
                new DiscountRule { FromPrice = 360, ToPrice = 375, Size = 23, Margin = 110 },
                new DiscountRule { FromPrice = 375, ToPrice = 390, Size = 23, Margin = 110 },
                new DiscountRule { FromPrice = 390, ToPrice = 99999, Size = 23, Margin = 110 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 24, Margin = 55 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 24, Margin = 55 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 24, Margin = 60 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 24, Margin = 60 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 24, Margin = 65 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 24, Margin = 70 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 24, Margin = 70 },
                new DiscountRule { FromPrice = 120, ToPrice = 135, Size = 24, Margin = 75 },
                new DiscountRule { FromPrice = 135, ToPrice = 150, Size = 24, Margin = 80 },
                new DiscountRule { FromPrice = 150, ToPrice = 165, Size = 24, Margin = 80 },
                new DiscountRule { FromPrice = 165, ToPrice = 180, Size = 24, Margin = 80 },
                new DiscountRule { FromPrice = 180, ToPrice = 195, Size = 24, Margin = 85 },
                new DiscountRule { FromPrice = 195, ToPrice = 210, Size = 24, Margin = 90 },
                new DiscountRule { FromPrice = 210, ToPrice = 225, Size = 24, Margin = 90 },
                new DiscountRule { FromPrice = 225, ToPrice = 240, Size = 24, Margin = 95 },
                new DiscountRule { FromPrice = 240, ToPrice = 255, Size = 24, Margin = 95 },
                new DiscountRule { FromPrice = 255, ToPrice = 270, Size = 24, Margin = 95 },
                new DiscountRule { FromPrice = 270, ToPrice = 285, Size = 24, Margin = 100 },
                new DiscountRule { FromPrice = 285, ToPrice = 300, Size = 24, Margin = 100 },
                new DiscountRule { FromPrice = 300, ToPrice = 315, Size = 24, Margin = 105 },
                new DiscountRule { FromPrice = 315, ToPrice = 330, Size = 24, Margin = 105 },
                new DiscountRule { FromPrice = 330, ToPrice = 345, Size = 24, Margin = 105 },
                new DiscountRule { FromPrice = 345, ToPrice = 360, Size = 24, Margin = 110 },
                new DiscountRule { FromPrice = 360, ToPrice = 375, Size = 24, Margin = 110 },
                new DiscountRule { FromPrice = 375, ToPrice = 390, Size = 24, Margin = 110 },
                new DiscountRule { FromPrice = 390, ToPrice = 99999, Size = 24, Margin = 110 },
                new DiscountRule { FromPrice = 0, ToPrice = 30, Size = 25, Margin = 55 },
                new DiscountRule { FromPrice = 30, ToPrice = 45, Size = 25, Margin = 55 },
                new DiscountRule { FromPrice = 45, ToPrice = 60, Size = 25, Margin = 60 },
                new DiscountRule { FromPrice = 60, ToPrice = 75, Size = 25, Margin = 60 },
                new DiscountRule { FromPrice = 75, ToPrice = 90, Size = 25, Margin = 65 },
                new DiscountRule { FromPrice = 90, ToPrice = 105, Size = 25, Margin = 70 },
                new DiscountRule { FromPrice = 105, ToPrice = 120, Size = 25, Margin = 70 },
                new DiscountRule { FromPrice = 120, ToPrice = 135, Size = 25, Margin = 75 },
                new DiscountRule { FromPrice = 135, ToPrice = 150, Size = 25, Margin = 80 },
                new DiscountRule { FromPrice = 150, ToPrice = 165, Size = 25, Margin = 80 },
                new DiscountRule { FromPrice = 165, ToPrice = 180, Size = 25, Margin = 80 },
                new DiscountRule { FromPrice = 180, ToPrice = 195, Size = 25, Margin = 85 },
                new DiscountRule { FromPrice = 195, ToPrice = 210, Size = 25, Margin = 90 },
                new DiscountRule { FromPrice = 210, ToPrice = 225, Size = 25, Margin = 90 },
                new DiscountRule { FromPrice = 225, ToPrice = 240, Size = 25, Margin = 95 },
                new DiscountRule { FromPrice = 240, ToPrice = 255, Size = 25, Margin = 95 },
                new DiscountRule { FromPrice = 255, ToPrice = 270, Size = 25, Margin = 95 },
                new DiscountRule { FromPrice = 270, ToPrice = 285, Size = 25, Margin = 100 },
                new DiscountRule { FromPrice = 285, ToPrice = 300, Size = 25, Margin = 100 },
                new DiscountRule { FromPrice = 300, ToPrice = 315, Size = 25, Margin = 105 },
                new DiscountRule { FromPrice = 315, ToPrice = 330, Size = 25, Margin = 105 },
                new DiscountRule { FromPrice = 330, ToPrice = 345, Size = 25, Margin = 105 },
                new DiscountRule { FromPrice = 345, ToPrice = 360, Size = 25, Margin = 110 },
                new DiscountRule { FromPrice = 360, ToPrice = 375, Size = 25, Margin = 110 },
                new DiscountRule { FromPrice = 375, ToPrice = 390, Size = 25, Margin = 110 },
                new DiscountRule { FromPrice = 390, ToPrice = 99999, Size = 25, Margin = 110 }
            };
            try
            {
                await _context.DiscountRules.AddRangeAsync(discountRules);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
                //Todo log
            }
        }
    }
}
