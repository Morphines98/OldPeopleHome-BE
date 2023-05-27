using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Domain;
using MeerPflege.Persistence;

namespace MeerPflege.Application.News
{
    public class Create
  {
    public class Command : IRequest<Result<Unit>>
    {
      public NewsItemDto NewsItem { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
      private readonly IMapper _mapper;
      private readonly DataContext _context;
      public Handler(DataContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
      {
        request.NewsItem.AddedDate = DateTime.Now;
        var newsData = _mapper.Map<NewsItem>(request.NewsItem);
        if (request.NewsItem.NewsItemAttachments != null && request.NewsItem.NewsItemAttachments.Any())
          newsData.NewsItemAttachments = _mapper.Map<List<NewsItemAttachment>>(request.NewsItem.NewsItemAttachments);
        else
          newsData.NewsItemAttachments = null;
        _context.NewsItems.Add(newsData);

        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return Result<Unit>.Failure("Failed to create.");
        return Result<Unit>.Success(Unit.Value);
      }
    }
  }
}