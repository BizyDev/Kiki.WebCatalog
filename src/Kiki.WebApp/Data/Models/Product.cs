namespace Kiki.WebApp.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal BasePrice { get; set; }
        public decimal FinalPrice { get; set; }
        public int Size { get; set; }
        public string Dimension { get; set; }
        public string Reference { get; set; }
        public string EAN { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public string Info3 { get; set; }
        public string Info4 { get; set; }
        public string Info5 { get; set; }
        public int CatalogId { get; set; }
        public Catalog Catalog { get; set; }
    }
}
