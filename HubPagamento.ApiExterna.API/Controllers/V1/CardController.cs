﻿using HubPagamento.ApiExterna.API.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HubPagamento.ApiExterna.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] AddCardCommand addCardCommand)
        {
            var response = await _mediator.Send(addCardCommand);
            return Ok(response);
        }

    }
}
