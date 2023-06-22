using MongoDB.Bson;
using Vehicle.InsurancePolicies.Domain.Entities;

namespace Vehicle.InsurancePolicies.Tests.Commands
{
  class FakePolicyTermCommand
  {
    public static ObjectId PolicyTermId => new("647874112c728e9a99746440");

    public static ObjectId PolicyTermTwoId => new("649023071010edc293bb3ad5");

    public static PolicyTermEntity PolicyTerm => new()
    {
      PolicyTermId = PolicyTermId,
      PolicyId = FakePolicyCommand.PolicyId,
      StartDate = new DateTime(2023, 1, 23),
      EndDate = new DateTime(2023, 12, 31)
    };

    public static PolicyTermEntity PolicyTermTwo => new()
    {
      PolicyTermId = PolicyTermTwoId,
      PolicyId = FakePolicyCommand.PolicyTwoId,
      StartDate = new DateTime(2024, 10, 12, 23, 20, 32),
      EndDate = new DateTime(2025, 5, 17, 0, 21, 30)
    };

    public static IEnumerable<PolicyTermEntity> PolicyTerms => new PolicyTermEntity[]
    {
      PolicyTerm,
      PolicyTermTwo
    };
  }
}
