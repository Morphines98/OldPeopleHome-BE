using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;

namespace MeerPflege.Application.WallItems
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public WallItemDto WallItemDto { get; set; }
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
                var entity = await _context.WallItems.FindAsync(request.WallItemDto.Id);

                if (entity == null) return null;

                entity.Description = request.WallItemDto.Description;
                entity.Title = request.WallItemDto.Title;
                entity.GroupId = request.WallItemDto.GroupId;
                entity.ForAllGroups = request.WallItemDto.ForAllGroups;

                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return Result<Unit>.Failure("Failed to update.");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}