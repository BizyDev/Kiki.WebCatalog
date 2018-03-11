using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kiki.WebApp.Data.Models
{
    public class Catalog
    {
        public Catalog()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PriceColumn { get; set; }
        public string SizeColumn { get; set; }
        public string ReferenceColumn { get; set; }
        public string Info1Column { get; set; }
        public string Info2Column { get; set; }
        public string Info3Column { get; set; }
        public int StartLineNumber { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string SizeColumnRegex { get; set; }
        public string FilePath { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
