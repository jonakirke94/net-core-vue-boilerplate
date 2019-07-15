using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chrochet.Features.Users;
using Crochet.Infrastructure;
using Crochet.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Crochet.Features.Users
{
    [Route("users")]
    public class UsersController
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUserAccessor _currentUserAccessor;

        public UsersController(IMediator mediator, ICurrentUserAccessor currentUserAccessor)
        {
            _mediator = mediator;
            _currentUserAccessor = currentUserAccessor;
        }

        [HttpPost]
        public async Task<UserEnvelope> Create([FromBody] Create.Command command)
        {
            return await _mediator.Send(command);
        }


        [HttpPost("login")]
        public async Task<UserEnvelope> Login([FromBody] Login.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<UserEnvelope> GetCurrent()
        {
            return await _mediator.Send(new Details.Query()
            {
                Username = _currentUserAccessor.GetCurrentUsername()
            });
        }
    }
}