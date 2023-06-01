using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MeerPflege.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeerPflege.API.Controllers
{

  public class VisitsController : BaseApiController
  {

    public VisitsController(ILogger<VisitsController> logger)
    {

    }

    private int GetHomeId(HttpContext context)
    {
      return context.User.FindFirst("HomeId") != null ? Int32.Parse(context.User.FindFirst("HomeId").Value) : 0;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> UpdateVisits(List<WorkingHourseDto> workingHoursDto)
    {
      var homeId = GetHomeId(HttpContext);
      var update = await Mediator.Send(new MeerPflege.Application.Visits.Update.Command { WorkingHours = workingHoursDto, HomeId = homeId });
      return HandleResult(update);
    }

    [Authorize(Roles = "Admin,Nurse,CareTaker")]
    [HttpGet(Name = "GetVisits")]
    public async Task<ActionResult<List<WorkingHourseDto>>> GetVisits()
    {
      var homeId = GetHomeId(HttpContext);
      var update = await Mediator.Send(new MeerPflege.Application.Visits.List.Query { HomeId = homeId });
      return HandleResult(update);
    }

  }
}