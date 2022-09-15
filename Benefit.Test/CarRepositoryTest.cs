using Xunit;
using System;
using System.IO;
using Api.Domain;
using System.Linq;
using Api.Services;
using Api.Domain.Services;
using Api.Domain.Repositories;

namespace Test;

public class CarRepositoryTest
{

    [Fact]
    public void InsertNullCarTest()
    {
        var svc = CreateService();
        var exception = Assert.Throws<ArgumentNullException>(() => svc.InsertCar(null));
        Assert.Equal("Value cannot be null. (Parameter 'car')", exception.Message);
    }

    [Fact]
    public void InsertCarWithIDTest()
    {
        var svc = CreateService();
        var car = CreateCar(int.MaxValue, "BMW", "ERS-8579");
        var exception = Assert.Throws<InvalidDataException>(() => svc.InsertCar(car));
        Assert.Equal("The entity cannot have a ID value on Add", exception.Message);
    }

    [Fact]
    public void InsertCarWithoutModelTest()
    {
        var svc = CreateService();
        var car = CreateCar(0, string.Empty, "ERS-8579");
        var exception = Assert.Throws<InvalidDataException>(() => svc.InsertCar(car));
        Assert.Equal("The Car Model cannot be empty", exception.Message);
    }

    [Fact]
    public void InsertCarWithoutLicensePlateTest()
    {
        var svc = CreateService();
        var car = CreateCar(0, "BMW", string.Empty);
        var exception = Assert.Throws<InvalidDataException>(() => svc.InsertCar(car));
        Assert.Equal("The License Plate cannot be empty", exception.Message);
    }

    [Fact]
    public void InsertCarSucefullyTest()
    {
        var svc = CreateService();
        var car = CreateCar(0, "BMW", "ERS-8579");
        car = svc.InsertCar(car);
        Assert.True(car.ID > 0);
        Assert.Equal("BMW", car.Model);
        Assert.Equal("ERS-8579", car.LicensePlate);
    }

    [Fact]
    public void DeleteCarInexistentTest()
    {
        var svc = CreateService();
        var deleted = svc.DeleteCar(int.MaxValue);
        Assert.False(deleted);
    }

    [Fact]
    public void DeleteCarSucefullyTest()
    {
        var svc = CreateService();
        var car = CreateCar(0, "Chevrolet ", "NGS-5869");
        car = svc.InsertCar(car);
        var deleted = svc.DeleteCar(car.ID);
        Assert.True(deleted);
    }

    [Fact]
    public void GetCarInexistentTest()
    {
        var svc = CreateService();
        var car = svc.GetCar(int.MaxValue);
        Assert.Null(car);
    }

    [Fact]
    public void UpdateInexistentCarTest()
    {
        var svc = CreateService();
        var car = CreateCar(int.MaxValue, "Jeep ", "EPS-8571");
        var exception = Assert.Throws<InvalidDataException>(() => svc.UpdateCar(car));
        Assert.Equal($"Entity not found with the ID=\"{int.MaxValue}.\"", exception.Message);
    }

    [Fact]
    public void GetCarTest()
    {
        var svc = CreateService();
        var car = CreateCar(0, "GMC", "ERP-8819");
        svc.InsertCar(car);
        car = svc.GetCar(car.ID);
        Assert.NotNull(car);
        Assert.True(car.ID > 0);
        Assert.Equal("GMC", car.Model);
        Assert.Equal("ERP-8819", car.LicensePlate);
    }

    [Fact]
    public void GetNoneCarsTest()
    {
        var svc = CreateService();
        var cars = svc.GetCars();
        Assert.NotNull(cars);
        Assert.Empty(cars);
    }

    [Fact]
    public void GetCarsTest()
    {
        var svc = CreateService();
        svc.InsertCar(CreateCar(0, "Dodge ", "ERX-8119"));
        svc.InsertCar(CreateCar(0, "RAM", "LRP-8899"));
        var cars = svc.GetCars();
        Assert.NotNull(cars);
        Assert.Equal(2, cars.Count());
    }

    [Fact]
    public void GetCarsPagination()
    {
        var svc = CreateService();
        svc.InsertCar(CreateCar(0, "Dodge ", "ERX-8119"));
        svc.InsertCar(CreateCar(0, "RAM", "LRP-8899"));
        svc.InsertCar(CreateCar(0, "Buick", "LRZ-8899"));

        var cars = svc.GetCars();
        Assert.NotNull(cars);
        Assert.True(cars.Count() == 3);

        cars = svc.GetCars(limit: 1);
        Assert.NotNull(cars);
        Assert.True(cars.Count() == 1);

        cars = svc.GetCars(skip: 2);
        Assert.NotNull(cars);
        Assert.True(cars.Count() == 1);
    }

    private ICarService CreateService()
    {
        ICarRepository repo = new CarRepositoryMock();
        return new CarService(repo);
    }

    private Car CreateCar(int carID, string model, string licensePlate)
    {
        return new Car()
        {
            ID = carID,
            Model = model,
            LicensePlate = licensePlate
        };
    }
}