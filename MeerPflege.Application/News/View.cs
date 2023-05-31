using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.News{
public class View
  {
    public class Query : IRequest<Result<NewsItemDto>> {
        public int Id { get; set; }
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
        var newsData =  _dataContext.NewsItems.Include("NewsItemAttachments").FirstOrDefault(a => a.IsDeleted == false && a.Id == request.Id);
        var result = _mapper.Map<NewsItemDto>(newsData);

        return Result<NewsItemDto>.Success(result);
      }
    }
  }
  }