using zBooks.Models;

namespace zBooks.DataAccess.Repository.IRepository;

public interface ICartItemRepository :IRepository<CartItem>
{
    void Update(CartItem obj);
}