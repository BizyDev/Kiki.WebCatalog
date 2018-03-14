namespace Kiki.WebApp.Data.Models
{
    using System.Collections.Generic;

    public class Catalog
    {
        public Catalog()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int SheetIndex { get; set; }
        public string BrandColumn { get; set; }
        public string BasePriceColumn { get; set; }
        public string ReferenceColumn { get; set; }
        public string EANColumn { get; set; }
        public string DimensionColumn { get; set; }
        public string WidthColumn { get; set; }
        public string AspectRatioColumn { get; set; }
        public string DiameterColumn { get; set; }
        public string LoadIndexSpeedRatingColumn { get; set; }
        public string ProfilColumn { get; set; }
        public string Info1Column { get; set; }
        public string Info2Column { get; set; }
        public string Info3Column { get; set; }
        public int StartLineNumber { get; set; }
        public decimal DiscountPercentage { get; set; }
        public SizeFormatEnum SizeFormat { get; set; }
        public byte[] File { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
