using System.Security.Claims;
using MeerPflege.Application.DTOs;
using MeerPflege.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MeerPflege.Application.CarersComands.CarerElders;

namespace MeerPflege.API.Controllers
{
    public class CarerEldersController:BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;

        public CarerEldersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        private int GetHomeId(HttpContext context)
        {
            return context.User.FindFirst("HomeId") != null ? Int32.Parse(context.User.FindFirst("HomeId").Value) : 0;
        }
        private string GetEmail(HttpContext context)
        {
            return context.User.FindFirst(ClaimTypes.Email) != null ? context.User.FindFirst(ClaimTypes.Email).Value : "";
        }

        [HttpGet(Name = "CarersGetElders")]
        public async Task<ActionResult<List<ElderDto>>> GetActivities()
        {
            var email = GetEmail(HttpContext);
            var user = await _userManager.FindByEmailAsync(email);
            var result = await Mediator.Send(new List.Query() { Id = user.CarerId.Value });
            return HandleResult(result);
        }
    }
}