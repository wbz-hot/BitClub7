﻿using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BC7.Business.Implementation.Users.Commands.CreateMultiAccount;
using BC7.Business.Implementation.Users.Commands.RegisterNewUserAccount;
using BC7.Business.Implementation.Users.Commands.UpdateUser;
using BC7.Business.Implementation.Users.Requests.GetMultiAccounts;
using BC7.Business.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BC7.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UsersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Register new user account
        /// </summary>
        /// <param name="model">A model with all user account data</param>
        /// <param name="reflink">Optional query parameter. If send it will attach the main account to this reflink's user</param>
        /// <returns>Returns the Id of the created user account</returns>
        /// <response code="200">Returns the Id of the newly created user account</response>
        [HttpPost]
        public async Task<IActionResult> RegisterNewAccount([FromBody] RegisterNewUserModel model, [FromQuery] string reflink = null)
        {
            var command = _mapper.Map<RegisterNewUserAccountCommand>(model);
            command.SponsorRefLink = reflink;

            var resultId = await _mediator.Send(command);

            // TODO: Created (201) maybe?
            return Ok(new { Id = resultId });
        }

        /// <summary>
        /// Create new multi account for the user
        /// </summary>
        /// <param name="model">A model with the reflink of user who invites the requested user</param>
        /// <param name="userId">User account ID</param>
        /// <returns>Returns the Id of the newly created user multi account</returns>
        /// <response code="200">Returns the Id of the newly created user multi account</response>
        [HttpPost("{userId}/multiAccounts")]
        [Authorize]
        public async Task<IActionResult> CreateMultiAccount([FromBody] CreateMultiAccountModel model, [FromRoute] Guid userId)
        {
            var command = new CreateMultiAccountCommand
            {
                UserAccountId = userId,
                SponsorReflink = model.SponsorReflink
            };

            // TODO: Maybe 201 created?
            return Ok(new { Id = await _mediator.Send(command) });
        }

        /// <summary>
        /// GetAsync multi accounts for given User ID
        /// </summary>
        /// <param name="userId">User ID for whom multi accounts will be returned</param>
        /// <returns>Returns list of multi accounts</returns>
        /// <response code="200">Returns list of multi accounts</response>
        [HttpGet("{userId}/multiAccounts")]
        [Authorize]
        public async Task<IActionResult> GetAllMultiAccounts([FromRoute] Guid userId)
        {
            var request = new GetMultiAccountsRequest { UserAccountId = userId };

            return Ok(await _mediator.Send(request));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserModel model)
        {
            var command = _mapper.Map<UpdateUserCommand>(model);
            var sidClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);

            command.UserId = id;
            command.RequestedUserId = Guid.Parse(sidClaim?.Value);

            await _mediator.Send(command);;

            return NoContent();
        }
    }
}