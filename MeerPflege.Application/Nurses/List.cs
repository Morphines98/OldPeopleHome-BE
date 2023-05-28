using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.Nurses
{
  public class List
  {
    public class Query : IRequest<Result<List<NurseDto>>>
    {
      public int? Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<List<NurseDto>>>
    {
      private readonly DataContext _dataContext;
      private readonly IMapper _mapper;
      public Handler(DataContext context, IMapper mapper)
      {
        _dataContext = context;
        _mapper = mapper;
      }

      public async Task<Result<List<NurseDto>>> Handle(Query request, CancellationToken cancellationToken)
      {
        var newsData = await _dataContext.Nurses.Where(a => a.IsDeleted == false).ToListAsync();
        if (request.Id != null)
        {
          newsData = newsData.Where(a=> a.Id == request.Id).ToList();
        }
        var result = _mapper.Map<List<NurseDto>>(newsData);

        return Result<List<NurseDto>>.Success(result);
      }
    }
  }
}
