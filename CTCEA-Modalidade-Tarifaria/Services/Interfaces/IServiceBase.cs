namespace CTCEA_Tariff_Modalities.Services.Interfaces;

public interface IServiceBase<TEntity> : IDisposable where TEntity : class
{
    void Add(TEntity entity);

    TEntity GetById(int id);

    IEnumerable<TEntity> GetAll();

    void Update(TEntity entity);

    void Remove(int id);

}
