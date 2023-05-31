using AutoMapper;
using MediatR;
using MeerPflege.Persistence;
using MeerPflege.Application.DTOs;
using MeerPflege.Application.Core;

namespace MeerPflege.Application.News
{
    public class Edit
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
                 var entity =await _context.NewsItems.FindAsync(request.NewsItem.Id);

                if(entity == null) return null;
                entity.Content = request.NewsItem.Content;
                entity.Title = request.NewsItem.Title;
                entity.GroupId = request.NewsItem.GroupId;
            
                var result = await _context.SaveChangesAsync() > 0;
                if(!result) return Result<Unit>.Failure("Failed to update.");
                return Result<Unit>.Success(Unit.Value);
            }
    }
    }
}
