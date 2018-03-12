﻿namespace Kiki.WebApp.Data.Models
{
    using System;

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
    }
}
