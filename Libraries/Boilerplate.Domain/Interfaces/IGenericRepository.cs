using System.Linq.Expressions;

namespace Boilerplate.Domain.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

    Task<List<T>> FindAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);

    Task<T> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);

    Task<T> AddAsync(T entity, CancellationToken cancellationToken);

    Task DeleteAsync(T entity, CancellationToken cancellationToken);

    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
}