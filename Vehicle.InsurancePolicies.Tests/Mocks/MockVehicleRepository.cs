using System.Linq.Expressions;
using MongoDB.Bson;
using Moq;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Repositories;
using Vehicle.InsurancePolicies.Tests.Commands;

namespace Vehicle.InsurancePolicies.Tests.Mocks
{
  class MockVehicleRepository
  {
    static readonly ICollection<VehicleEntity> _vehicles = FakeVehicleCommand.Vehicles;
    static readonly IQueryable<VehicleEntity> _vehiclesQuery = _vehicles.AsQueryable();

    public static Mock<IVehicleRepository> GetMock()
    {
      Mock<IVehicleRepository> mockVehicleRepository = new();
      mockVehicleRepository.Setup(expression => expression.Get()).Returns(() => _vehicles);
      mockVehicleRepository.Setup(expression => expression.Exists(It.IsAny<Expression<Func<VehicleEntity, bool>>>()))
        .Returns<Expression<Func<VehicleEntity, bool>>>(expression => _vehiclesQuery.Any(expression));
      mockVehicleRepository.Setup(expression => expression.Find(It.IsAny<ObjectId>()))
        .Returns<ObjectId>(vehicleId => _vehiclesQuery.FirstOrDefault(vehicle => vehicle.VehicleId == vehicleId));
      mockVehicleRepository.Setup(expression => expression.Find(It.IsAny<Expression<Func<VehicleEntity, bool>>>()))
        .Returns<Expression<Func<VehicleEntity, bool>>>(expression => _vehiclesQuery.FirstOrDefault(expression));

      return mockVehicleRepository;
    }
  }
}
