using CTCEA_Tariff_Modalities.Database.Repositories.Base;
using CTCEA_Tariff_Modalities.Services.Interfaces;
using CTCEA_Tariff_Modalities.Entities;

namespace CTCEA_Tariff_Modalities.Services;

public class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class, IBaseEntity
{
    private readonly IRepositoryBase<TEntity> _repository;

    public ServiceBase(IRepositoryBase<TEntity> repository)
    {
        _repository = repository;
    }

    public virtual void Add(TEntity entity)
    {
        _repository.Add(entity);
    }

    public virtual void Dispose()
    {
        _repository?.Dispose();
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
        return _repository.GetAll();
    }

    public virtual TEntity GetById(int id)
    {
        return _repository.GetById(id);
    }

    public virtual void Remove(int id)
    {
        var entity = _repository.GetById(id);
        _repository.Remove(entity);
    }

    public virtual void Update(TEntity entity)
    {
        _repository.Update(entity);
    }

}


