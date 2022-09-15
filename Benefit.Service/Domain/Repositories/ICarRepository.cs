using Toolkit.Interfaces;

namespace Api.Domain.Repositories;

public interface ICarRepository : IRepository<Car>
{
    Car GetByLicensePlate(string licensePlate);
}