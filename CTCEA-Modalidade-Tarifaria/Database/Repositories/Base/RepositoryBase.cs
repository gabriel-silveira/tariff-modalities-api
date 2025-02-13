using CTCEA_Tariff_Modalities.Entities;
using CTCEA_Tariff_Modalities.Database.Context;

namespace CTCEA_Tariff_Modalities.Database.Repositories.Base;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : class, IBaseEntity
{
    protected readonly TariffModalitiesContext _context;

    public RepositoryBase(TariffModalitiesContext context)
    {
        _context = context;
    }

    public virtual void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        _context.SaveChanges();
    }

    public virtual void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        _context.SaveChanges();
    }

    public virtual void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        var query = _context.Set<TEntity>().AsQueryable();

        return Includes(query).ToList();
    }

    public virtual TEntity GetById(int id)
    {
        var query = _context.Set<TEntity>().AsQueryable();

        return Includes(query).FirstOrDefault(e => id.Equals(e.Id));
    }

    public virtual IQueryable<TEntity> Includes(IQueryable<TEntity> query)
    {
        return query;
    }
}
