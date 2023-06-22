using MongoDB.Bson;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Tests.Commands
{
  class FakePolicyCommand
  {
    private static readonly ObjectId[] _coverageIds = FakeCoverageCommand.Coverages
      .Select(coverage => coverage.CoverageId)
      .ToArray();

    public static ObjectId PolicyId => new("64782b6c2c728e9a9974643f");

    public static ObjectId PolicyTwoId => new("649023051010edc293bb3ad4");

    public static PolicyEntity Policy => new()
    {
      PolicyId = PolicyId,
      CustomerId = FakeCustomerCommand.CustomerId,
      VehicleId = FakeVehicleCommand.VehicleId,
      PlanName = "plan-1",
      MaxValueCovered = 120000000,
      TakenDate = new DateTime(2023, 1, 13),
      PolicyNumber = new Guid("f249878b-d86e-4789-96a4-4640ac31e8ed"),
      Coverages = _coverageIds
    };

    public static PolicyEntity PolicyTwo => new()
    {
      PolicyId = PolicyTwoId,
      CustomerId = FakeCustomerCommand.CustomerTwoId,
      VehicleId = FakeVehicleCommand.VehicleTwoId,
      PlanName = "plan-2",
      MaxValueCovered = 322000000,
      TakenDate = new DateTime(2023, 3, 13),
      PolicyNumber = new Guid("f249878b-d86e-4789-96a4-4640ac31e8ed"),
      Coverages = _coverageIds.Skip(1).ToArray()
    };

    public static IEnumerable<PolicyEntity> Policies => new PolicyEntity[]
    {
      Policy,
      PolicyTwo
    };
  }
}
