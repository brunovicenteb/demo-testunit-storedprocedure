namespace Toolkit.Interfaces;

public interface IRepository<TEntity> where TEntity : IEntity
{
    int Count();
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    bool Delete(int id);
    TEntity GetByID(int id);
    IEnumerable<TEntity> GetSome(int limit = 50, int skip = 0);
}