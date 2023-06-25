using MongoDB.Bson;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Tests.Commands
{
  class FakePolicyTermCommand
  {
    public static ObjectId PolicyTermId => new("647874112c728e9a99746440");

    public static PolicyTermEntity PolicyTerm => new()
    {
      PolicyTermId = PolicyTermId,
      PolicyId = FakePolicyCommand.PolicyId,
      StartDate = new(2023, 1, 23),
      EndDate = new(2023, 12, 31)
    };

    public static (DateTime StartDate, DateTime EndDate) PolicyTermDates => (PolicyTerm.StartDate, PolicyTerm.EndDate);

    public static ICollection<PolicyTermEntity> PolicyTerms => new List<PolicyTermEntity>()
    {
      PolicyTerm,
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
