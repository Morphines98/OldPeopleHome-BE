using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.Carers
{

    public class List
    {
        public class Query : IRequest<Result<List<CarersDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<CarersDto>>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _dataContext = context;
                _mapper = mapper;
            }

            public async Task<Result<List<CarersDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var carersItem = await _dataContext.Carers.Where(a => a.IsDeleted == false).ToListAsync();
                var result = _mapper.Map<List<CarersDto>>(carersItem);

                return Result<List<CarersDto>>.Success(result);
            }
        }
    }
}
