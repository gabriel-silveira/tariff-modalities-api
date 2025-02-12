using CTCEA_Modalidade_Tarifaria.Entities;

namespace CTCEA_Modalidade_Tarifaria.Database.Repositories.Base;

public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class, IBaseEntity
{
    void Add(TEntity entity);

    TEntity GetById(int id);

    IEnumerable<TEntity> GetAll();

    void Update(TEntity entity);

    void Remove(TEntity entity);

    void Dispose();
}
