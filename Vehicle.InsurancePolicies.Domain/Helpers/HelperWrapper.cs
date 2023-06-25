using Vehicle.InsurancePolicies.Contracts.Helpers;

namespace Vehicle.InsurancePolicies.Domain.Helpers
{
  public class HelperWrapper : IHelper
  {
    public (DateTime StartDate, DateTime EndDate) RandomDates => (DateTimeHelper.GetRandomDate(), DateTimeHelper.GetRandomDate());
  }
}
