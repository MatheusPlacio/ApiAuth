using System.Linq.Expressions;

namespace ApiAuth.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> Buscar(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        Task Delete(T entity);
        Task<IList<T>> Get();
        Task<T?> GetById(int id);
        Task Update(T entity);
    }
}
