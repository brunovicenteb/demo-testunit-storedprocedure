using Api.Domain;
using Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    public CarController(ICarService service)
    {
        _Service = service;
    }

    private readonly ICarService _Service;

    [HttpGet("Count")]
    public IActionResult GetCars()
    {
        var count = _Service.GetCount();
        return Ok(count);
    }

    [HttpGet("GetCars")]
    public IActionResult GetCars(int limit = 50, int skip = 0)
    {
        var cars = _Service.GetCars(limit, skip);
        return Ok(cars);
    }

    [HttpPost]
    public IActionResult InsertCar(Car car)
    {
        car = _Service.InsertCar(car);
        return CreatedAtAction(nameof(GetCar), new { id = car.ID });
    }

    [HttpGet]
    public IActionResult GetCar(int carID)
    {
        var car = _Service.GetCar(carID);
        return Ok(car);
    }

    [HttpPut]
    public IActionResult UpdateCar(Car car)
    {
        car = _Service.UpdateCar(car);
        return Ok(car);
    }

    [HttpDelete]
    public IActionResult DeleteCar(int carID)
    {
        if (_Service.DeleteCar(carID))
            return NoContent();
        return NotFound();
    }
}