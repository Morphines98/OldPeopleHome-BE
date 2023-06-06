using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.CarersComands.CarerElders
{

    public class List
    {
        public class Query : IRequest<Result<List<ElderDto>>>
        {
            public int Id { get; set; }
            public int? ElderId { get; set; }
        }


        public class Handler : IRequestHandler<Query, Result<List<ElderDto>>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _dataContext = context;
                _mapper = mapper;
            }

            public async Task<Result<List<ElderDto>>> Handle(Query request, CancellationToken cancellationToken)
            {

                var elders = await _dataContext.Elders
                .Where(a => a.IsDeleted == false && a.CarerId == request.Id).ToListAsync();
                if (request.ElderId != null)
                {
                    elders = elders.Where(x => x.Id == request.ElderId).ToList();
                }
                var result = _mapper.Map<List<ElderDto>>(elders);

                return Result<List<ElderDto>>.Success(result);
            }
        }
    }
}