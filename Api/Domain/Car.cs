using Toolkit.Interfaces;

namespace Api.Domain;

public class Car : IEntity
{
    public int ID { get; set; }

    public string Model { get; set; }

    public string LicensePlate { get; set; }
}