using Api.Domain;
using Api.Domain.Repositories;
using System.Linq;
using Toolkit.Test;

namespace Test;

public class CarRepositoryMock : MockRepository<Car>, ICarRepository
{
    public Car GetByLicensePlate(string licensePlate)
    {
        lock (ToLock)
            return List.FirstOrDefault(o => o.LicensePlate == licensePlate);
    }
}