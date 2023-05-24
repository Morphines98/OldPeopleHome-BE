using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Domain;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.HomeGroups
{
  public class List
  {
    public class Query : IRequest<Result<List<HomeGroup>>>
    {
      public int HomeId { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<List<HomeGroup>>>
    {
      private readonly DataContext _dataContext;
      public Handler(DataContext context)
      {
        _dataContext = context;
      }

      public async Task<Result<List<HomeGroup>>> Handle(Query request, CancellationToken cancellationToken)
      {
        return Result<List<HomeGroup>>.Success(await _dataContext.HomeGroups.Where(a => a.HomeId == request.HomeId && a.IsDeleted == false).ToListAsync());
      }
    }
  }
}