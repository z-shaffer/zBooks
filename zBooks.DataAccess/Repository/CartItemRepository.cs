using Microsoft.EntityFrameworkCore;
using zBooks.DataAccess.Data;
using zBooks.DataAccess.Repository.IRepository;
using zBooks.Models;

namespace zBooks.DataAccess.Repository;

public class CartItemRepository(ApplicationDbContext db) : Repository<CartItem>(db), ICartItemRepository
{
    private ApplicationDbContext _db = db;

    public void Update(CartItem item)
    {
        _db.Entry(item).State = EntityState.Modified;
    }
}