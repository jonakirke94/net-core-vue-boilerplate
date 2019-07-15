using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Crochet.Features.Users;
using Crochet.Infrastructure;
using Crochet.Infrastructure.Errors;
using Crochet.Infrastructure.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chrochet.Features.Users
{
    public class Details
    {
        public class Query : IRequest<UserEnvelope>
        {
            public string Username { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Username).NotNull().NotEmpty();
            }
        }

        public class QueryHandler : IRequestHandler<Query, UserEnvelope>
        {
            private readonly CrochetContext _context;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;
            private readonly IMapper _mapper;

            public QueryHandler(CrochetContext context, IJwtTokenGenerator jwtTokenGenerator, IMapper mapper)
            {
                _context = context;
                _jwtTokenGenerator = jwtTokenGenerator;
                _mapper = mapper;
            }

            public async Task<UserEnvelope> Handle(Query message, CancellationToken cancellationToken)
            {
                var person = await _context.Persons
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Username == message.Username, cancellationToken);
                if (person == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { User = Constants.NOT_FOUND });
                }
                var user = _mapper.Map<Crochet.Domain.Person, User>(person);
                user.Token = await _jwtTokenGenerator.CreateToken(person.Username);
                return new UserEnvelope(user);
            }
        }
    }
}
