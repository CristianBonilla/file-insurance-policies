using System.Net;

namespace Vehicle.InsurancePolicies.Contracts.DTO
{
  public class ServiceError
  {
    public HttpStatusCode Status { get; }

    public int StatusCode { get; }

    public ICollection<string> Errors { get; }

    public ServiceError(HttpStatusCode status, params string[] errors)
    {
      Status = status;
      StatusCode = (int)status;
      Errors = new HashSet<string>(errors);
    }
  }
}
