using Vehicle.InsurancePolicies.Contracts.Identity.Auth;

namespace Vehicle.InsurancePolicies.Contracts.Identity
{
  public interface IIdentityService
  {
    AuthResponse Authenticate();
  }
}
