using AutoMapper;
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
      PolicyTransfer policyTransfer = await _service.AddPolicy(policy, policyRequest.StartDate, policyRequest.EndDate);
      PolicyResponse policyResponse = _mapper.Map<PolicyResponse>(policyTransfer);

      return CreatedAtAction(nameof(AddPolicy), policyResponse);
    }
  }
}
