using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Vehicle.InsurancePolicies.Contracts.Identity;

namespace Vehicle.InsurancePolicies.API.Controllers
{
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  [ApiVersion("1.0")]
  [Produces("application/json")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class IdentityController : ControllerBase
  {
    readonly IIdentityService _identityService;

    public IdentityController(IIdentityService identityService) => _identityService = identityService;

    [AllowAnonymous]
    [HttpPost]
    public IActionResult Authenticate() => Ok(_identityService.Authenticate());
  }
}
