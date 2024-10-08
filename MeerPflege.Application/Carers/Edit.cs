using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;

namespace MeerPflege.Application.Carers
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
    {
      public CarersDto CarersDto { get; set; }
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
                 var entity =await _context.Carers.FindAsync(request.CarersDto.Id);

                if(entity == null) return null;
                entity.Name = request.CarersDto.Name;
                entity.Email = request.CarersDto.Email;
                entity.LastName = request.CarersDto.LastName;
                entity.PhoneNumber = request.CarersDto.PhoneNumber;
                entity.Adress = request.CarersDto.Adress;
                entity.City = request.CarersDto.City;
                entity.PostCode = request.CarersDto.PostCode;
            
                var result = await _context.SaveChangesAsync() > 0;
                if(!result) return Result<Unit>.Failure("Failed to update.");
                return Result<Unit>.Success(Unit.Value);
            }
    }

    }
}