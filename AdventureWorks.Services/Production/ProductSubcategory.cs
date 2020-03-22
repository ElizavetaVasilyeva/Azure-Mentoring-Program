using System;

namespace AdventureWorks.Services.Production
{
    public class ProductSubcategory
    {
        public int Id { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
