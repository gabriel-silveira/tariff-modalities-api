namespace CTCEA_Modalidade_Tarifaria.Services.Interfaces;

public interface IServiceBase<TEntity> : IDisposable where TEntity : class
{
    void Add(TEntity entity);

    TEntity GetById(int id);

    IEnumerable<TEntity> GetAll();

    void Update(TEntity entity);

    void Remove(int id);

}
