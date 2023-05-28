using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Domain;
using MeerPflege.Persistence;

namespace MeerPflege.Application.Elders
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
    {
      public ElderDto ElderDto { get; set; }
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
        var newsData = _mapper.Map<Elder>(request.ElderDto);
        _context.Elders.Add(newsData);
        var result = await _context.SaveChangesAsync() > 0;
        if (!result) return Result<Unit>.Failure("Failed to create.");
        return Result<Unit>.Success(Unit.Value);
      }
    }
    }
}