using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.Visits
{
  public class List
  {
    public class Query : IRequest<Result<List<WorkingHourseDto>>>
    {
      public int HomeId { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<List<WorkingHourseDto>>>
    {
      private readonly DataContext _dataContext;
      private readonly IMapper _mapper;
      public Handler(DataContext context, IMapper mapper)
      {
        _dataContext = context;
        _mapper = mapper;
      }

      public async Task<Result<List<WorkingHourseDto>>> Handle(Query request, CancellationToken cancellationToken)
      {
        var data = _dataContext.WorkingHoursForDays.Include("WorkingIntervals").Where(a => a.HomeId == request.HomeId).ToList();
        var result = new List<WorkingHourseDto>();
        foreach (var item in data)
        {
          var temp = new WorkingHourseDto()
          {
            DayId = item.DayId,
            WorkingIntervals = item.WorkingIntervals.Select(a => new DayWorkingHours() { StartHours = a.StartHours, EndHours = a.EndHours }).ToList()
          };
          result.Add(temp);
        }

        return Result<List<WorkingHourseDto>>.Success(result);
      }
    }
  }
}