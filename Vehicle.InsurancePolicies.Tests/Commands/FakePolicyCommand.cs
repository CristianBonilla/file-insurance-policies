using MongoDB.Bson;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Extensions;

namespace Vehicle.InsurancePolicies.Tests.Commands
{
  class FakePolicyCommand
  {
    private static readonly ObjectId[] _coverageIds = FakeCoverageCommand.Coverages
      .Select(coverage => coverage.CoverageId)
      .ToArray();

    public static ObjectId PolicyId => new("64952cba344c23461362817b");

    public static PolicyEntity Policy => new()
    {
      PolicyId = PolicyId,
      CustomerId = FakeCustomerCommand.CustomerId,
      VehicleId = FakeVehicleCommand.VehicleId,
      PlanName = "plan-3",
      MaxValueCovered = 675920000,
      TakenDate = new(2022, 11, 24),
      PolicyNumber = new("b29bff0a-9bb7-4734-9672-cf255457198b"),
      Coverages = _coverageIds.Skip(2).ToArray()
    };

    public static IEnumerable<PolicyEntity> Policies => new PolicyEntity[]
    {
      new()
      {
        PolicyId = new("64782b6c2c728e9a9974643f"),
        CustomerId = new("647825cf2c728e9a9974643d"),
        VehicleId = new("647825af2c728e9a9974643c"),
        PlanName = "plan-1",
        MaxValueCovered = 120000000,
        TakenDate = new(2023, 1, 13, 14, 10, 30),
        PolicyNumber = new("73b22b6e-3596-450e-8e9e-a5b5fd36dad1"),
        Coverages = _coverageIds
      },
      new()
      {
        PolicyId = new("649023051010edc293bb3ad4"),
        CustomerId = new("647825cf2c728e9a9974643d"),
        VehicleId = new("647825af2c728e9a9974643b"),
        PlanName = "plan-2",
        MaxValueCovered = 322000000,
        TakenDate = new(2023, 3, 13, 14, 10, 30),
        PolicyNumber = new("2fa069d1-44e8-4496-8cf3-d171631cca7f"),
        Coverages = _coverageIds.SkipLast(1).ToArray()
      }
    };

    static FakePolicyCommand()
    {
      Policy.SetSourceValues(new()
      {
        VehicleId = Policy.VehicleId.ToString(),
        CustomerId = Policy.CustomerId.ToString(),
        Coverages = Policy.Coverages
        .Select(coverageId => coverageId.ToString())
        .ToArray()
      });
    }
  }
}
