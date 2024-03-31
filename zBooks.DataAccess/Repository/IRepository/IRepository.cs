using System.Linq.Expressions;

namespace zBooks.DataAccess.Repository.IRepository;

public interface IRepository<T> where T : class
{
    //T - Category
    IEnumerable<T> GetAll(string? includeProperties = null);
    IEnumerable<T> FilterAll(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null, string? property = "");
    T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entity);
}