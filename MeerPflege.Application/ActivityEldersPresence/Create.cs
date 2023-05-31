using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Domain;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.ActivityEldersPresence
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public List<ActivityElderDto> ActivityPresenceDto { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var dataPresence = await _context.ActivityElderPresences.Where(a => a.ActivityId == request.ActivityPresenceDto[0].ActivityId).ToListAsync();
                foreach (var activityElderPresence in request.ActivityPresenceDto)
                {
                    if (dataPresence != null && dataPresence.Any())
                    {
                        var dataP = dataPresence.FirstOrDefault(p => p.ActivityId == activityElderPresence.ActivityId && p.ElderId == activityElderPresence.ElderId);
                        if (dataP != null)
                        {
                            dataP.IsPresent = activityElderPresence.IsPresent;
                        }
                        else
                        {
                            dataP = _mapper.Map<ActivityElderPresence>(activityElderPresence);
                            _context.ActivityElderPresences.Add(dataP);
                        }

                    }
                    else
                    {
                        var activityPresenceData = _mapper.Map<ActivityElderPresence>(activityElderPresence);
                        _context.ActivityElderPresences.Add(activityPresenceData);
                    }
                }

                var result = await _context.SaveChangesAsync() > 0;
                if (!result) return Result<Unit>.Failure("Failed to create.");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}