using CTCEA_Modalidade_Tarifaria.Entities;
using CTCEA_Modalidade_Tarifaria.Database.Repositories.Base;
using CTCEA_Modalidade_Tarifaria.Database.Context;

namespace CTCEA_Modalidade_Tarifaria.Repositories.Base;

public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
    where TEntity : class, IBaseEntity
{
    protected readonly ModalidadeTarifariaContext _context;

    public RepositoryBase(ModalidadeTarifariaContext context)
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
