using System.Globalization;
using MeerPflege.Application;
using MeerPflege.Application.DTOs;
using MeerPflege.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeerPflege.API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;

        public ActivitiesController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        private int GetHomeId(HttpContext context)
        {
            return context.User.FindFirst("HomeId") != null ? Int32.Parse(context.User.FindFirst("HomeId").Value) : 0;
        }

        [HttpGet(Name = "GetActivties")]
        public async Task<ActionResult<List<ActivityDto>>> GetActivities()
        {
            var result = await Mediator.Send(new Application.Activities.List.Query());
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(ActivityDto activityDto)
        {
            activityDto.HomeId = GetHomeId(HttpContext);
            var date = new DateTime();
            date = DateTime.ParseExact(activityDto.StringDate, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            activityDto.Date = date;
            return HandleResult(await Mediator.Send(new Application.Activities.Create.Command { ActivityDto = activityDto }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            return HandleResult(await Mediator.Send(new Application.Activities.Delete.Command { Id = id }));
        }
    }
}