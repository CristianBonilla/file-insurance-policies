using System.Net;
using Vehicle.InsurancePolicies.Contracts.DTO;

namespace Vehicle.InsurancePolicies.Contracts.Exceptions
{
  public class ServiceErrorException : Exception
  {
    public ServiceError ServiceError { get; }

    public ServiceErrorException(HttpStatusCode status, params string[] errors) : base(string.Join(", ", GetErrors(errors)))
    {
      ServiceError = new(status, GetErrors(errors));
    }

    private static string[] GetErrors(string[] errors) =>
      errors.Where(error => !string.IsNullOrWhiteSpace(error)).ToArray();
  }
}
