using Dapper;
using System.Data;
using Toolkit.Interfaces;
using System.Data.Common;

namespace Toolkit.Repository;

public abstract class StoredProcedureRepository<TEntity> : AbstractRepository<TEntity> where TEntity : IEntity
{
    protected abstract DbConnection CreateConnection();

    protected T ExecProc<T>(string procName, DynamicParameters dynamicParameters, string outputParam)
    {
        using DbConnection db = CreateConnection();
        object ret = db.Execute(procName, dynamicParameters, commandType: CommandType.StoredProcedure);
        return dynamicParameters.Get<T>(outputParam);
    }

    protected T GetSingleProc<T>(string procName, string paramName, int paramValue) where T : IEntity
    {
        var objParm = new DynamicParameters();
        objParm.Add(paramName, paramValue);
        using DbConnection db = CreateConnection();
        return db.Query<T>(procName, objParm, commandType: CommandType.StoredProcedure).FirstOrDefault();
    }

    protected IEnumerable<T> GetMany<T>(string procName, DynamicParameters objParm) where T : IEntity
    {
        using DbConnection db = CreateConnection();
        return db.Query<T>(procName, objParm, commandType: CommandType.StoredProcedure);
    }

    protected IEnumerable<T> QueryProc<T>(string procName, DynamicParameters dynamicParameters) where T : IEntity
    {
        using DbConnection db = CreateConnection();
        return db.Query<T>(procName, dynamicParameters);
    }
}