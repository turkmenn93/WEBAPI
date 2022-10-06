using System.Collections.Generic;

namespace UdemyWEBAPI.Data
{
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }  
}
