using MeerPflege.Application.ActivityEldersPresence;
using MeerPflege.Application.DTOs;
using MeerPflege.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeerPflege.API.Controllers
{
    public class ActivityPresenceController:BaseApiController
    {
       private readonly UserManager<AppUser> _userManager;

        public ActivityPresenceController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<ActivityElderDto>>> GetActivitiesPresence(int id)
        {
            var result = await Mediator.Send(new List.Query{ActivityId = id});
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(List<ActivityElderDto> activityElderDto)
        {
            return HandleResult(await Mediator.Send(new Create.Command { ActivityPresenceDto = activityElderDto }));
        }
        
    }
}