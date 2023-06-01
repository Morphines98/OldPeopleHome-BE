using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.CarersComands.CarerNews
{
    public class List
    {
        
        public class Query : IRequest<Result<List<NewsItemDto>>>
        {
            public int Id { get; set; }
        }


        public class Handler : IRequestHandler<Query, Result<List<NewsItemDto>>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _dataContext = context;
                _mapper = mapper;
            }

            public async Task<Result<List<NewsItemDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var getCarer =  _dataContext.Carers.Include("Elders").First(c => c.Id == request.Id);
                var groupListIds = getCarer.Elders.Select(x=>x.GroupId).ToList();
                var newsData = await _dataContext.NewsItems.Include("NewsItemAttachments")
                .Where(a => a.IsDeleted == false && ((a.GroupId.HasValue && groupListIds.Contains(a.GroupId.Value)) || (a.ForAllGroups.HasValue && a.ForAllGroups.Value))).ToListAsync();
                var result = _mapper.Map<List<NewsItemDto>>(newsData);

                return Result<List<NewsItemDto>>.Success(result);
            }
        }
    }
}