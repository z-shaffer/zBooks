using zBooks.DataAccess.Data;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;

namespace zBooks.DataAccess.Repository;

public class CategoryRepository(ApplicationDbContext db) : Repository<Category>(db), ICategoryRepository
{
    private ApplicationDbContext _db = db;

    public void Update(Category obj)
    {
        _db.SaveChanges();
    }

    public void Save()
    {
        _db.Categories.Update(obj);
    }
}