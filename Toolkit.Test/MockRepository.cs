using Toolkit.Interfaces;
using Toolkit.Repository;

namespace Toolkit.Test;

public class MockRepository<TEntity> : AbstractRepository<TEntity> where TEntity : IEntity
{
    private static int _Count = 0;
    protected readonly List<TEntity> List = new();

    protected override int DoAdd(TEntity entity)
    {
        entity.ID = Interlocked.Increment(ref _Count);
        List.Add(entity);
        return entity.ID;
    }

    protected override int DoCount()
    {
        return _Count;
    }

    protected override bool DoDelete(TEntity entity)
    {
        return List.Remove(entity);
    }

    protected override TEntity DoGetByID(int id)
    {
        return List.FirstOrDefault(o => o.ID == id);
    }

    protected override IEnumerable<TEntity> DoGetSome(int limit = 50, int skip = 0)
    {
        return List.Skip(skip).Take(limit);
    }

    protected override TEntity DoUpdate(TEntity oldEntity, TEntity updatedEntity)
    {
        List.Remove(oldEntity);
        List.Add(updatedEntity);
        return updatedEntity;
    }
}