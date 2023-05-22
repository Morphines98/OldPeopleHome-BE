using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Domain;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.Homes
{
    public class List
    {
        public class Query : IRequest<Result<List<Home>>>{}

        public class Handler : IRequestHandler<Query, Result<List<Home>>>
        {
            private readonly DataContext _dataContext;
            public Handler(DataContext context)
            {
                _dataContext = context;
            }

            public async Task<Result<List<Home>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<Home>>.Success(await _dataContext.Homes.ToListAsync());
            }
        }
    }
}