using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Domain;
using MeerPflege.Persistence;

namespace MeerPflege.Application.NurseCommands.NurseWallItems
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public WallItemDto WallItemDto { get; set; }

            public int UserId { get; set; }
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
                request.WallItemDto.Date = DateTime.Now;
                if(request.WallItemDto.ForAllGroups == false)
                {
                    var groupId = _context.Nurses.First(n => n.Id == request.UserId).GroupId;
                    request.WallItemDto.GroupId = groupId;
                }
                var wallData = _mapper.Map<WallItem>(request.WallItemDto);
                if (request.WallItemDto.WallItemAttachments != null && request.WallItemDto.WallItemAttachments.Any())
                    wallData.WallItemAttachments = _mapper.Map<List<WallItemAttachment>>(request.WallItemDto.WallItemAttachments);
                else
                    wallData.WallItemAttachments = null;
                _context.WallItems.Add(wallData);

                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return Result<Unit>.Failure("Failed to create.");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
