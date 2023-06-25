using MongoDB.Bson;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Tests.Commands
{
  class FakeVehicleCommand
  {
    public static ObjectId VehicleId => new("647825af2c728e9a9974643a");

    public static VehicleEntity Vehicle => new()
    {
      VehicleId = VehicleId,
      Plate = "AAA-123",
      Model = "Nissan",
      HasInspection = true
    };

    public static ICollection<VehicleEntity> Vehicles => new List<VehicleEntity>()
    {
      Vehicle,
      new()
      {
        VehicleId = new("647825af2c728e9a9974643b"),
        Plate = "BBB-123",
        Model = "Toyota",
        HasInspection = false
      },
      new()
      {
        VehicleId = new("647825af2c728e9a9974643c"),
        Plate = "CCC-123",
        Model = "Volkswagen",
        HasInspection = true
      }
    };
  }
}
