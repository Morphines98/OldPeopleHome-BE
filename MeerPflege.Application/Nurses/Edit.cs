using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;

namespace MeerPflege.Application.Nurses
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
    {
      public NurseDto NurseDto { get; set; }
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
                 var entity =await _context.Nurses.FindAsync(request.NurseDto.Id);

                if(entity == null) return null;
                entity.Name = request.NurseDto.Name;
                entity.Email = request.NurseDto.Email;
                entity.LastName = request.NurseDto.LastName;
                entity.PhoneNumber = request.NurseDto.PhoneNumber;
                entity.GroupId = request.NurseDto.GroupId;
                if(!string.IsNullOrEmpty(request.NurseDto.NurseAvatarUrl))
                {
                    entity.NurseAvatarUrl = request.NurseDto.NurseAvatarUrl;
                }
            
                var result = await _context.SaveChangesAsync() > 0;
                if(!result) return Result<Unit>.Failure("Failed to update.");
                return Result<Unit>.Success(Unit.Value);
            }
    }

    }
}