namespace Boilerplate.Infrastructure.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly DataContext _context;

    public GenericRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken)
    {
        return await _context.Set<T>().Where(expression).ToListAsync(cancellationToken);
    }

    public async Task<T> GetByIdAsync(long id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().SingleOrDefaultAsync(expression, cancellationToken);
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        await SaveChangesAsync(cancellationToken);

        return entity;
    }


    public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
    {
        _context.Set<T>().Remove(entity);

        await SaveChangesAsync(cancellationToken);
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        var existingEntity = await _context.Set<T>().FindAsync(new object[] { entity.Id }, cancellationToken);

        if (existingEntity != null)
        {
            _context.Entry(existingEntity).State = EntityState.Modified;
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);

            await SaveChangesAsync(cancellationToken);
        }

        return existingEntity!;
    }

    private async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            _context.ChangeTracker.Clear();

            throw;
        }
    }
}