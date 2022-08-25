using HubPagamento.ApiExterna.API.DataContracs.Commands.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubPagamento.ApiExterna.API.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authorize([FromBody] AuthorizeCommand authorize)
        {
            var response = await _mediator.Send(authorize);
            return Ok(response);
        }
    }
}
