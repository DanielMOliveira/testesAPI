using HubPagamento.ApiExterna.API.DataContracs.Commands.Integration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubPagamento.ApiExterna.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IntegrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("m4u")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> M4U([FromBody] TokenizeCardCommand tokenizeCardCommand)
        {
            var response = await _mediator.Send(tokenizeCardCommand);

            if (response.IsSucess)
                return Ok(response.Result);

            return StatusCode((int)response.StatusCode, response.Result);
        }

        [Authorize]
        [HttpPost("m4u/auth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LoginM4U([FromBody] LoginM4UCommand loginCommand)
        {
            var response = await _mediator.Send(loginCommand);

            if (response.IsSucess)
                return Ok(response.Result);

            return StatusCode((int)response.StatusCode, response.Result);
        }
    }
}
