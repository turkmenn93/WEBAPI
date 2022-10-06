using System;

namespace UdemyWEBAPI.Data
{
    public class Product
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string ImagePath { get; set; }
        public int? categoryId { get; set; }
        public Category Category { get; set; }
    }
}
 