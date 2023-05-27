using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Domain;
using MeerPflege.Persistence;

namespace MeerPflege.Application.Carers
{
    public class Create
    {
        public class Command : IRequest<Result<CarersDto>>
    {
      public CarersDto Carer { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<CarersDto>>
    {
      private readonly IMapper _mapper;
      private readonly DataContext _context;
      public Handler(DataContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Result<CarersDto>> Handle(Command request, CancellationToken cancellationToken)
      {
        var carerData = _mapper.Map<Carer>(request.Carer);
        _context.Carers.Add(carerData);
        _context.SaveChanges();
        request.Carer.Id = carerData.Id;
        return Result<CarersDto>.Success(request.Carer);
      }
    }
    }
}