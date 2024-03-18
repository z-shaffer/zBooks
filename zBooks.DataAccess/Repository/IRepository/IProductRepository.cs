using zBooks.Models;

namespace zBooks.DataAccess.Repository.IRepository;

public interface IProductRepository :IRepository<Product>
{
    void Update(Product obj);
}