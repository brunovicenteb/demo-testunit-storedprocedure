using Toolkit.Interfaces;

namespace Toolkit.Repository;

public abstract class AbstractRepository<TEntity> : IRepository<TEntity> where TEntity : IEntity
{
    protected readonly object ToLock = new();

    protected abstract int DoAdd(TEntity entity);

    protected abstract int DoCount();

    protected abstract bool DoDelete(TEntity entity);

    protected abstract TEntity DoGetByID(int id);

    protected abstract IEnumerable<TEntity> DoGetSome(int limit, int skip);

    protected abstract TEntity DoUpdate(TEntity oldEntity, TEntity updatedEntity);

    public TEntity Add(TEntity entity)
    {
        lock (ToLock)
        {
            int id = DoAdd(entity);
            return GetByID(id);
        }
    }

    public int Count()
    {
        lock (ToLock)
            return DoCount();
    }

    public bool Delete(int id)
    {
        lock (ToLock)
        {
            var entity = GetByID(id);
            if (entity == null)
                return false;
            return DoDelete(entity);
        }
    }

    public TEntity GetByID(int id)
    {
        lock (ToLock)
            return DoGetByID(id);
    }

    public IEnumerable<TEntity> GetSome(int limit = 50, int skip = 0)
    {
        lock (ToLock)
            return DoGetSome(limit, skip);
    }

    public TEntity Update(TEntity entity)
    {
        lock (ToLock)
        {
            if (entity.ID == 0)
                throw new InvalidDataException("The entity can't have a ID empty on Update.");
            var oldEntity = GetByID(entity.ID);
            if (oldEntity == null)
                throw new InvalidDataException($"Entity not found with the ID=\"{entity.ID}.\"");
            return DoUpdate(oldEntity, entity);
        }
    }
}