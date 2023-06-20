using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Vehicle.InsurancePolicies.Contracts.DTO.Policy;
using Vehicle.InsurancePolicies.Contracts.Services;
using Vehicle.InsurancePolicies.Domain.Entities;
using Vehicle.InsurancePolicies.Domain.Entities.Transfers;

namespace Vehicle.InsurancePolicies.API.Controllers
{
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  [ApiVersion("1.0")]
  [Produces("application/json")]
  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
  public class PolicyController : ControllerBase
  {
    readonly IMapper _mapper;
    readonly IPolicyService _service;

    public PolicyController(IMapper mapper, IPolicyService service)
    {
      _mapper = mapper;
      _service = service;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PolicyResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddPolicy([FromBody] PolicyRequest policyRequest)
    {
      PolicyEntity policy = _mapper.Map<PolicyEntity>(policyRequest);
      PolicyTransfer policyTransfer = await _service.AddPolicy(policy);
      PolicyResponse policyResponse = _mapper.Map<PolicyResponse>(policyTransfer);

      return CreatedAtAction(nameof(AddPolicy), policyResponse);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IAsyncEnumerable<PolicyResponse>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IAsyncEnumerable<PolicyResponse> Get()
    {
      var policies = _service.GetPolicies();

      return _mapper.Map<IAsyncEnumerable<PolicyResponse>>(policies);
    }

    [HttpGet("{policyNumber:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PolicyResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetPolicyByNumber(Guid policyNumber)
    {
      PolicyTransfer policyTransfer = _service.FindPolicyByNumber(policyNumber);
      PolicyResponse policyResponse = _mapper.Map<PolicyResponse>(policyTransfer);

      return Ok(policyResponse);
    }

    [HttpGet("{plate}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PolicyResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetPolicyByPlateVehicle(string? plate)
    {
      PolicyTransfer policyTransfer = _service.FindPolicyByPlateVehicle(plate);
      PolicyResponse policyResponse = _mapper.Map<PolicyResponse>(policyTransfer);

      return Ok(policyResponse);
    }
  }
}
