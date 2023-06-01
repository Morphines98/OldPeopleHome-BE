using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.CarersComands.CarerActivities
{
    public class List
    {
         public class Query : IRequest<Result<List<ActivityDto>>>
        {
            public int Id { get; set; }
        }


        public class Handler : IRequestHandler<Query, Result<List<ActivityDto>>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _dataContext = context;
                _mapper = mapper;
            }

            public async Task<Result<List<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                  var getCarer =  _dataContext.Carers.Include("Elders").First(c => c.Id == request.Id);
                var groupListIds = getCarer.Elders.Select(x=>x.GroupId).ToList();
                var newsData = await _dataContext.Activities
                .Where(a => a.IsDeleted == false && groupListIds.Contains(a.GroupId )).ToListAsync();
                var result = _mapper.Map<List<ActivityDto>>(newsData);

                return Result<List<ActivityDto>>.Success(result);
            }
        }
    }
}