using Dapper;
using Api.Domain;
using System.Data;
using Toolkit.Repository;
using System.Data.Common;
using Api.Domain.Repositories;
using Microsoft.Data.SqlClient;

namespace Api.Repositories;

public class CarRepository : StoredProcedureRepository<Car>, ICarRepository
{
    public static string StringConnection;

    protected override DbConnection CreateConnection()
    {
        var conn = new SqlConnection(StringConnection);
        conn.Open();
        return conn;
    }

    public Car GetByLicensePlate(string licensePlate)
    {
        return new Car();
    }

    protected override int DoAdd(Car entity)
    {
        var objParm = new DynamicParameters();
        objParm.Add("@model", entity.Model);
        objParm.Add("@licensePlane", entity.LicensePlate);
        objParm.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);
        return ExecProc<int>("AddCar", objParm, "@id");
    }

    protected override int DoCount()
    {
        var objParm = new DynamicParameters();
        objParm.Add("@count", dbType: DbType.Int32, direction: ParameterDirection.Output);
        return ExecProc<int>("GetCount", objParm, "@count");
    }

    protected override bool DoDelete(Car entity)
    {
        var objParm = new DynamicParameters();
        objParm.Add("@id", entity.ID);
        objParm.Add("@count", dbType: DbType.Int32, direction: ParameterDirection.Output);
        return ExecProc<int>("DeleteCar", objParm, "@count") == 1;
    }

    protected override Car DoGetByID(int id)
    {
        return GetSingleProc<Car>("GetSingleCar", "@id", id);
    }

    protected override IEnumerable<Car> DoGetSome(int limit, int skip)
    {
        var objParm = new DynamicParameters();
        objParm.Add("@skip", skip, DbType.Int32, direction: ParameterDirection.Input);
        objParm.Add("@limit", limit, DbType.Int32, direction: ParameterDirection.Input);
        return GetMany<Car>("GetCars", objParm);
    }

    protected override Car DoUpdate(Car oldEntity, Car updatedEntity)
    {
        var objParm = new DynamicParameters();
        objParm.Add("@id", oldEntity.ID);
        objParm.Add("@model", updatedEntity.Model);
        objParm.Add("@licensePlane", updatedEntity.LicensePlate);
        objParm.Add("@count", dbType: DbType.Int32, direction: ParameterDirection.Output);
        if (ExecProc<int>("UpdateCar", objParm, "@count") == 1)
            return GetByID(oldEntity.ID);
        return null;
    }
}