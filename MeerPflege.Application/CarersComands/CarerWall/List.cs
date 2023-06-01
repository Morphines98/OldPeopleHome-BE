using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.CarersComands.CarerWall
{
    public class List
    {
        public class Query : IRequest<Result<List<WallItemDto>>>
        {
            public int Id { get; set; }
        }


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
                  var getCarer =  _dataContext.Carers.Include("Elders").First(c => c.Id == request.Id);
                var groupListIds = getCarer.Elders.Select(x=>x.GroupId).ToList();
                var wallData = await _dataContext.WallItems.Include("WallItemAttachments")
                .Where(a => a.IsDeleted == false && ((a.GroupId.HasValue && groupListIds.Contains(a.GroupId.Value))|| (a.ForAllGroups.HasValue && a.ForAllGroups.Value))).ToListAsync();
                var result = _mapper.Map<List<WallItemDto>>(wallData);

                return Result<List<WallItemDto>>.Success(result);
            }
        }
    }
}