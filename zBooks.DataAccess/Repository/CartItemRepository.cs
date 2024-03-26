using Microsoft.EntityFrameworkCore;
using zBooks.DataAccess.Data;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;

namespace zBooks.DataAccess.Repository;

public class CartItemRepository(ApplicationDbContext db) : Repository<CartItem>(db), ICartItemRepository
{
    private ApplicationDbContext _db = db;

    public void Update(CartItem obj)
    {
        var objFromDb = _db.CartItems.FirstOrDefault(u => u.Id == obj.Id);
        if (objFromDb is not null)
        {
            objFromDb.Quantity = obj.Quantity;
        }
    }
}