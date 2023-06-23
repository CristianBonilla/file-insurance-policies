using MongoDB.Bson;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Tests.Commands
{
  class FakeVehicleCommand
  {
    public static ObjectId VehicleId => new("64952b6c344c234613628171");

    public static VehicleEntity Vehicle => new()
    {
      VehicleId = VehicleId,
      Plate = "DDD-123",
      Model = "Lamborghini",
      HasInspection = false
    };

    public static IEnumerable<VehicleEntity> Vehicles => new VehicleEntity[]
    {
      new()
      {
        VehicleId = new("647825af2c728e9a9974643a"),
        Plate = "AAA-123",
        Model = "Nissan",
        HasInspection = true
      },
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
