using Catalogue.API.Data.Interfaces;
using Catalogue.API.Entities;
using Catalogue.API.Repositories.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalogue.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogueContext _context;

        public ProductRepository(ICatalogueContext context) {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Create(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task<Product> GetProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Id, id);
            return await _context.Products.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Category, category);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Name,name);
            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(p => true).ToListAsync();
        }

        public async Task<bool> Update(Product product)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Id, product.Id);
            var updateResult = await _context.Products.ReplaceOneAsync(filter: filter, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> Delete(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.ElemMatch(p => p.Id, id);
            var deleteResult = await _context.Products.DeleteOneAsync(filter: filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
