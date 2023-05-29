using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.WallItems
{
    public class List
    {
        public class Query : IRequest<Result<List<WallItemDto>>> { }

        public class Handler : IRequestHandler<Query, Result<List<WallItemDto>>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _dataContext = context;
                _mapper = mapper;
            }

            public async Task<Result<List<WallItemDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var wallData = await _dataContext.WallItems.Include("WallItemAttachments").Where(a => a.IsDeleted == false).ToListAsync();
                var result = _mapper.Map<List<WallItemDto>>(wallData);

                return Result<List<WallItemDto>>.Success(result);
            }
        }
    }
}