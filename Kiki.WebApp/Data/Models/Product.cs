namespace Kiki.WebApp.Data.Models
{
    using System;
    using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public decimal FinalPrice { get; set; }
        public int Size { get; set; }
        public string Reference { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public string Info3 { get; set; }
        public int CatalogId { get; set; }
        public Catalog Catalog { get; set; }

        public static int StringToSize(string text, SizeFormatEnum sizeFormat)
        {
            text = text.ToLower().TrimEnd();
            switch (sizeFormat)
            {
                case SizeFormatEnum.Last2AlphaNumeric:
                    text = text.Substring(text.Length - 2, 2);
                    break;
                case SizeFormatEnum.Rxx:
                    text = text.Substring(text.IndexOf('r'), 2);
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
