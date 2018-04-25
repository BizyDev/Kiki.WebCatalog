namespace Kiki.WebApp.Data.Models
{
    public class DiscountRule
    {
        public int Id { get; set; }
        public decimal FromPrice { get; set; }
        public decimal ToPrice { get; set; }
        public int Size { get; set; }
        public decimal Margin { get; set; }
    }
}