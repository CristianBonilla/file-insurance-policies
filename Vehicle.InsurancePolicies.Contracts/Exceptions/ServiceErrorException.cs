using System.Net;
using Vehicle.InsurancePolicies.Contracts.DTO;

namespace Vehicle.InsurancePolicies.Contracts.Exceptions
{
  public class ServiceErrorException : Exception
  {
    public ServiceError ServiceError { get; }

    public ServiceErrorException(HttpStatusCode status, params string[] errors)
    {
      ServiceError = new(status, errors.Where(error => !string.IsNullOrWhiteSpace(error)).ToArray());
    }
  }
}
