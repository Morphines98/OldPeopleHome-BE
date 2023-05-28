using MeerPflege.Application.DTOs;
using MeerPflege.Application.Elders;
using MeerPflege.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeerPflege.API.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EldersController :BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;

        public EldersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        private int GetHomeId(HttpContext context)
        {
            return context.User.FindFirst("HomeId") != null ? Int32.Parse(context.User.FindFirst("HomeId").Value) : 0;
        }

        [HttpGet(Name = "GetElders")]
        public async Task<ActionResult<List<ElderDto>>> GetCarers()
        {
            var result = await Mediator.Send(new List.Query());
            return HandleResult(result);
        }
    }
}