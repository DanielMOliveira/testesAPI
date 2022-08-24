using HubPagamento.ApiExterna.API.DataContracs.Commands.Integration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubPagamento.ApiExterna.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class IntegrationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public IntegrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> M4U([FromBody] TokenizeCardCommand tokenizeCardCommand)
        {
            var response = await _mediator.Send(tokenizeCardCommand);
            return Ok(response);
        }
    }
}
