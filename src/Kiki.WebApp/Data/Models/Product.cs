namespace Kiki.WebApp.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Brand{ get; set; }
        public decimal BasePrice { get; set; }
        public decimal FinalPrice { get; set; }
        public string Reference { get; set; }
        public string EAN { get; set; }
        public string Dimension { get; set; }
        public int? Width { get; set; }
        public int? AspectRatio { get; set; }
        public int Dimater { get; set; }
        public string LoadIndexSpeedRating { get; set; }
        public string Profil { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public string Info3 { get; set; }
        public int CatalogId { get; set; }
        public Catalog Catalog { get; set; }
    }
}
