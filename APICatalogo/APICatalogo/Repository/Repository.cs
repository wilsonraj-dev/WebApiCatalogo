using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected AppDbContext _context;
    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public IQueryable<T> Get()
    {
        return _context.Set<T>().AsNoTracking();
    }

    public T GetById(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().SingleOrDefault(predicate);
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.Set<T>().Remove(entity);
    }
}
