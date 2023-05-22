using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Domain;
using MeerPflege.Persistence;

namespace MeerPflege.Application.HomeGroups
{
    public class Details
    {
        public class Query:IRequest<Result<HomeGroup>>{
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<HomeGroup>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task<Result<HomeGroup>> Handle(Query request, CancellationToken cancellationToken)
            {
                var group = await _context.HomeGroups.FindAsync(request.Id);
                return Result<HomeGroup>.Success(group);
            }
        }
    }
}