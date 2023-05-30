using System.Security.Claims;
using MeerPflege.Application.DTOs;
using MeerPflege.Application.SendGrid;
using MeerPflege.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MeerPflege.Application.NurseCommands.NurseWallItems;

namespace MeerPflege.API.Controllers
{
    public class NurseWallItemsController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailSender _emailSender;

        public NurseWallItemsController(UserManager<AppUser> userManager, EmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        private int GetHomeId(HttpContext context)
        {
            return context.User.FindFirst("HomeId") != null ? Int32.Parse(context.User.FindFirst("HomeId").Value) : 0;
        }
        private string GetEmail(HttpContext context)
        {
            return context.User.FindFirst(ClaimTypes.Email) != null ? context.User.FindFirst(ClaimTypes.Email).Value : "";
        }

        [HttpGet(Name = "NurseGetWallItems")]
        public async Task<ActionResult<List<WallItemDto>>> GetWallItems()
        {
            var email = GetEmail(HttpContext);
            var user = await _userManager.FindByEmailAsync(email);
            var result = await Mediator.Send(new List.Query() { Id = user.NurseId.Value });
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallItem(WallItemDto wallItemDto)
        {
            wallItemDto.HomeId = GetHomeId(HttpContext);
             var email = GetEmail(HttpContext);
            var user = await _userManager.FindByEmailAsync(email);

            return HandleResult(await Mediator.Send(new Create.Command { WallItemDto = wallItemDto,UserId = user.NurseId.Value }));
        }
    }
}