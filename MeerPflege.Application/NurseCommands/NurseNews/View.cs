using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.NurseCommands.NurseNews
{
    public class View
    {
        public class Query : IRequest<Result<NewsItemDto>>
        {
            public int Id { get; set; }
            public int NewsId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<NewsItemDto>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _dataContext = context;
                _mapper = mapper;
            }

            public async Task<Result<NewsItemDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var groupId = _dataContext.Nurses.First(n => n.Id == request.Id).GroupId;
                var newsData = _dataContext.NewsItems.Include("NewsItemAttachments")
                .FirstOrDefault(a => a.IsDeleted == false && a.Id == request.NewsId && (a.GroupId == groupId || (a.ForAllGroups.HasValue && a.ForAllGroups.Value)));
                var result = _mapper.Map<NewsItemDto>(newsData);

                return Result<NewsItemDto>.Success(result);
            }
        }
    }
}