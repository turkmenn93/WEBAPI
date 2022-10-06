using System.Threading.Tasks;
using UdemyWEBAPI.Data;

namespace UdemyWEBAPI.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category> GetByIdAsync(int id);
    }
}
