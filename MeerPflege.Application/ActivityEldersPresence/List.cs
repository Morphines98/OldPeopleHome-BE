using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.ActivityEldersPresence
{
    public class List
    {
        public class Query : IRequest<Result<List<ActivityElderDto>>> { 
            public int ActivityId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<ActivityElderDto>>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _dataContext = context;
                _mapper = mapper;
            }

            public async Task<Result<List<ActivityElderDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = _dataContext.Activities.FirstOrDefault(a => a.Id == request.ActivityId);
                var group = activity.GroupId;
                var elders = _dataContext.Elders.Where(e => e.GroupId == group && !e.IsDeleted).ToList();
                var activityPresence = await _dataContext.ActivityElderPresences.Where(a => a.ActivityId == request.ActivityId).ToListAsync();

                var data = new List<ActivityElderDto>();
                foreach(var elder in elders)
                {
                    var present = new ActivityElderDto();
                    present.ActivityId = activity.Id;
                    present.ElderId = elder.Id;
                    present.ElderName = elder.Name +" "+elder.LastName;
                    if(activityPresence != null && activityPresence.Any() && activityPresence.Any(a => a.ElderId == elder.Id))
                    {
                        present.IsPresent = activityPresence.FirstOrDefault(a => a.ElderId == elder.Id).IsPresent;
                    }
                    else{
                        present.IsPresent = false;
                    }
                    data.Add(present);
                }

                return Result<List<ActivityElderDto>>.Success(data);
            }
        }
    }
}