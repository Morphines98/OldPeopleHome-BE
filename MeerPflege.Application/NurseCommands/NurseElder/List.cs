using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.NurseCommands.NurseElder
{
 public class List
    {
        public class Query : IRequest<Result<List<ElderDto>>>
        {
            public int Id { get; set; }
        }


        public class Handler : IRequestHandler<Query, Result<List<ElderDto>>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _dataContext = context;
                _mapper = mapper;
            }

            public async Task<Result<List<ElderDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var groupId = _dataContext.Nurses.First(n => n.Id == request.Id).GroupId;
                var newsData = await _dataContext.Elders.
                Where(a => a.IsDeleted == false && a.GroupId == groupId).ToListAsync();
                var result = _mapper.Map<List<ElderDto>>(newsData);

                return Result<List<ElderDto>>.Success(result);
            }
        }
    }
}