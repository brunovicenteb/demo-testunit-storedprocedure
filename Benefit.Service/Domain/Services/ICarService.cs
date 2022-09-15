namespace Api.Domain.Services;

public interface ICarService
{
    public Car InsertCar(Car car);

    public Car UpdateCar(Car car);

    public bool DeleteCar(int carID);

    public Car GetCar(int carID);

    public int GetCount();

    public IEnumerable<Car> GetCars(int limit = 50, int skip = 0);
}