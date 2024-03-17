using zBooks.Models;

namespace zBooks.DataAccess.Repository.IRepository;

public interface ICategoryRepository :IRepository<Category>
{
    void Update(Category obj);
}