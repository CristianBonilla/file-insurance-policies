using MongoDB.Bson;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Tests.Commands
{
  class FakeVehicleCommand
  {
    public static ObjectId VehicleId => new("647825af2c728e9a9974643a");

    public static ObjectId VehicleTwoId => new("647825af2c728e9a9974643b");

    public static VehicleEntity Vehicle => new()
    {
      VehicleId = VehicleId,
      Plate = "AAA-123",
      Model = "Nissan",
      HasInspection = true
    };

    public static VehicleEntity VehicleTwo => new()
    {
      VehicleId = VehicleTwoId,
      Plate = "BBB-123",
      Model = "Toyota",
      HasInspection = false
    };

    public static IEnumerable<VehicleEntity> Vehicles => new VehicleEntity[]
    {
      Vehicle,
      VehicleTwo,
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
