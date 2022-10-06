using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyWEBAPI.Data;
using UdemyWEBAPI.Interfaces;

namespace UdemyWEBAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();  

            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.SingleOrDefaultAsync(x => x.ID == id);

        }

        public async Task RemoveProductAsync(int id)
        {
            var removedEntity = await _context.Products.FindAsync(id);
            _context.Products.Remove(removedEntity);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateProductAsync(Product product)
        {
            var unchangedEntity = await _context.Products.FindAsync(product.ID);

            

            _context.Entry(unchangedEntity).CurrentValues.SetValues(product);   //  --> //_context.Update(product); bu da olabilir mi ? 

            await _context.SaveChangesAsync();
        }  
    }
}
