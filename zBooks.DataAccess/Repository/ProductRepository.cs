using zBooks.DataAccess.Data;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;

namespace zBooks.DataAccess.Repository;

public class ProductRepository(ApplicationDbContext db) : Repository<Product>(db), IProductRepository
{
    private ApplicationDbContext _db = db;

    public void Update(Product obj)
    {
        _db.Products.Update(obj);
    }
}