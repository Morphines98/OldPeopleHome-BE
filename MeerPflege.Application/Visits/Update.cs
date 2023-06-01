using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Domain;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.Visits
{
  public class Update
  {
    public class Command : IRequest<Result<List<WorkingHourseDto>>>
    {
      public List<WorkingHourseDto> WorkingHours { get; set; }
      public int HomeId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<List<WorkingHourseDto>>>
    {
      private readonly IMapper _mapper;
      private readonly DataContext _context;
      public Handler(DataContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Result<List<WorkingHourseDto>>> Handle(Command request, CancellationToken cancellationToken)
      {
        var existingWorkingHours = _context.WorkingHoursForDays.Include("WorkingIntervals").Where(a => a.HomeId == request.HomeId).ToList();
        if (existingWorkingHours != null && existingWorkingHours.Any())
        {
          foreach (var item in existingWorkingHours)
          {
            foreach (var child in item.WorkingIntervals)
            {
              _context.WorkingIntervals.Remove(child);
            }
            _context.WorkingHoursForDays.Remove(item);
          }
          await _context.SaveChangesAsync();
        }

        foreach (var item in request.WorkingHours)
        {
          var tempData = new WorkingHoursForDay()
          {
            HomeId = request.HomeId,
            DayId = item.DayId,
            WorkingIntervals = item.WorkingIntervals.Select(a => new WorkingInterval() { StartHours = a.StartHours, EndHours = a.EndHours }).ToList()
          };
          _context.WorkingHoursForDays.Add(tempData);
        }

        await _context.SaveChangesAsync();

        return Result<List<WorkingHourseDto>>.Success(request.WorkingHours);
      }
    }
  }
}