using System.Net;
using Vehicle.InsurancePolicies.Contracts.DTO;

namespace Vehicle.InsurancePolicies.Contracts.Exceptions
{
  public class ServiceErrorException : Exception
  {
    public HttpStatusCode Status { get; }

    public ServiceError Errors { get; }

    public ServiceErrorException(HttpStatusCode status, params string[] errors)
    {
      Status = status;
      Errors = new() { Errors = new HashSet<string>(errors) };
    }
  }
}
