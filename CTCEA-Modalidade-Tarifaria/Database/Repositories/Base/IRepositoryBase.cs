using CTCEA_Tariff_Modalities.Entities;

namespace CTCEA_Tariff_Modalities.Database.Repositories.Base;

public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class, IBaseEntity
{
    void Add(TEntity entity);

    TEntity GetById(int id);

    IEnumerable<TEntity> GetAll();

    void Update(TEntity entity);

    void Remove(TEntity entity);
    new void Dispose();
}
