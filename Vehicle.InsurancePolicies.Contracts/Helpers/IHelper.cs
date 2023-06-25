namespace Vehicle.InsurancePolicies.Contracts.Helpers
{
  public interface IHelper
  {
    (DateTime StartDate, DateTime EndDate) RandomDates { get; }
  }
}
