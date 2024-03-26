using zBooks.Models;

namespace zBooks.DataAccess.Repository.IRepository;

public interface IShoppingCartRepository :IRepository<ShoppingCart>
{
    void Update(ShoppingCart obj);
}