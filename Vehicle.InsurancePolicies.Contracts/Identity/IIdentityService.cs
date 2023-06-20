using Vehicle.InsurancePolicies.Contracts.DTO.Auth;

namespace Vehicle.InsurancePolicies.Contracts.Identity
{
  public interface IIdentityService
  {
    AuthResponse Authenticate();
  }
}
