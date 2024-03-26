using zBooks.DataAccess.Data;
using zBooks.DataAccess.Repository.IRepository;

namespace zBooks.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }
    public IShoppingCartRepository ShoppingCart { get; private set; }
    public ICartItemRepository CartItem { get; private set; }
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        Product = new ProductRepository(_db);
        ShoppingCart = new ShoppingCartRepository(_db);
        CartItem = new CartItemRepository(_db);
    }
    
    public void Save()
    {
        _db.SaveChanges();
    }
    
    public void Dispose()
    {
        _db.Dispose();
    }
}