using Api.Domain;
using Api.Domain.Services;
using Api.Domain.Repositories;

namespace Api.Services;

public class CarService : ICarService
{
    public CarService(ICarRepository repository)
    {
        _Repository = repository;
    }

    private readonly ICarRepository _Repository;

    public int GetCount()
    {
        return _Repository.Count();
    }

    public bool DeleteCar(int carID)
    {
        return _Repository.Delete(carID);
    }

    public IEnumerable<Car> GetCars(int limit = 50, int skip = 0)
    {
        return _Repository.GetSome(limit, skip);
    }

    public Car GetCar(int carID)
    {
        return _Repository.GetByID(carID);
    }

    public Car InsertCar(Car car)
    {
        if (car == null)
            throw new ArgumentNullException(nameof(car));
        if (car.ID > 0)
            throw new InvalidDataException("The entity cannot have a ID value on Add");
        if (string.IsNullOrEmpty(car.Model))
            throw new InvalidDataException("The Car Model cannot be empty");
        if (string.IsNullOrEmpty(car.LicensePlate))
            throw new InvalidDataException("The License Plate cannot be empty");
        return _Repository.Add(car);
    }

    public Car UpdateCar(Car car)
    {
        if (car == null)
            throw new ArgumentNullException(nameof(car));
        if (car.ID < 1)
            throw new InvalidDataException("The entity cannot have a invalid ID");
        if (string.IsNullOrEmpty(car.Model))
            throw new InvalidDataException("The Car Model cannot be empty");
        if (string.IsNullOrEmpty(car.LicensePlate))
            throw new InvalidDataException("The License Plate cannot be empty");
        return _Repository.Update(car);
    }
}