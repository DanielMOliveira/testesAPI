﻿using HubPagamento.ApiExterna.API.DataContracs.Commands.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HubPagamento.ApiExterna.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

            if (response.IsSucess)
                return Ok(response.Result);

            return StatusCode((int)response.StatusCode, response.Result);

        }
    }
}
