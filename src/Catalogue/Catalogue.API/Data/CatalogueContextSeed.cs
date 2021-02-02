using Catalogue.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogue.API.Data
{
    public class CatalogueContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (!existProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        public static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>() {
                new Product(){ Name = "IPhone X", Summary = "asd", Description ="IPhone X", ImageFile = "iphone1.jpg", Price = 950.00M, Category="SmartPhone"},
                new Product(){ Name = "Samsung 10", Summary = "asd", Description ="Samsung 10", ImageFile = "samsung1.jpg", Price = 600.00M, Category="SmartPhone"},
                new Product(){ Name = "Huawei Plus", Summary = "asd", Description ="Huawei Plus", ImageFile = "huawei.jpg", Price = 400.00M, Category="White Applications"}
            };

        }
    }
}
