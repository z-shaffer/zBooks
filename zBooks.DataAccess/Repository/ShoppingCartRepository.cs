using Microsoft.EntityFrameworkCore;
using zBooks.DataAccess.Data;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;

namespace zBooks.DataAccess.Repository;

public class ShoppingCartRepository(ApplicationDbContext db) : Repository<ShoppingCart>(db), IShoppingCartRepository
{
    private ApplicationDbContext _db = db;

    public void Update(ShoppingCart obj)
    {
        var objFromDb = _db.ShoppingCarts.FirstOrDefault(u => u.Id == obj.Id);
        if (objFromDb is not null)
        {
            objFromDb.DateCreated = obj.DateCreated;
        }
    }
}