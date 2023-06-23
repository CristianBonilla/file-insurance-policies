using MongoDB.Bson;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Tests.Commands
{
  class FakePolicyTermCommand
  {
    public static ObjectId PolicyTermId => new("6495323f344c234613628180");

    public static PolicyTermEntity PolicyTerm => new()
    {
      PolicyTermId = PolicyTermId,
      PolicyId = FakePolicyCommand.PolicyId,
      StartDate = new(2022, 11, 24, 10, 30, 0),
      EndDate = new(2023, 6, 20, 15, 1, 50)
    };

    public static IEnumerable<PolicyTermEntity> PolicyTerms => new PolicyTermEntity[]
    {
      new()
      {
        PolicyTermId = new("647874112c728e9a99746440"),
        PolicyId = new("64782b6c2c728e9a9974643f"),
        StartDate = new(2023, 1, 23),
        EndDate = new(2023, 12, 31)
      },
      new()
      {
        PolicyTermId = new("649023071010edc293bb3ad5"),
        PolicyId = new("649023051010edc293bb3ad4"),
        StartDate = new(2024, 10, 12, 23, 20, 32),
        EndDate = new(2025, 5, 17, 0, 21, 30)
      }
    };
  }
}
