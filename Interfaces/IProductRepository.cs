using System.Collections.Generic;
using System.Threading.Tasks;
using UdemyWEBAPI.Data;

namespace UdemyWEBAPI.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllAsync();
        public Task<Product> GetByIdAsync(int id);
        public Task<Product> CreateProductAsync(Product product);
        public Task UpdateProductAsync(Product product);
        public Task RemoveProductAsync(int id);
    }
}
