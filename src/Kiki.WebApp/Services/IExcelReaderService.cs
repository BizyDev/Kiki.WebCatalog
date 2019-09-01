namespace Kiki.WebApp.Services
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Data.Models;

    public interface IExcelReaderService
    {
        IEnumerable<Product> GetLines(Catalog catalog, ImmutableList<DiscountRule> rules);
        IEnumerable<DiscountRule> GetRules(byte[] file);
    }
}