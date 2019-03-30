﻿using System.Threading.Tasks;
using AutoMapper;
using BC7.Business.Implementation.Authentications.Commands.RegisterNewUserAccount;
using BC7.Business.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BC7.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AuthenticationController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("registerNewAccount")]
        [HttpPost("registerNewAccount/{reflink?}")]
        public async Task<IActionResult> RegisterNewAccount([FromBody] RegisterNewUserModel model, string reflink = null)
        {
            var command = _mapper.Map<RegisterNewUserAccountCommand>(model);
            command.InvitingRefLink = reflink;

            var resultId = await _mediator.Send(command);

            // TODO: Created (201) maybe?
            return Ok(new { Id = resultId });
        }
    }
}
