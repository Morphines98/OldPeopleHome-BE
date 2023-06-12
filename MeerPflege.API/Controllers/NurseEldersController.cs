using System.Security.Claims;
using MeerPflege.Application.DTOs;
using MeerPflege.Application.SendGrid;
using MeerPflege.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MeerPflege.Application.NurseCommands.NurseElder;
using Microsoft.AspNetCore.Authorization;

namespace MeerPflege.API.Controllers
{
    [Authorize(Roles = Role.Nurse)]
    public class NurseEldersController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly EmailSender _emailSender;

        public NurseEldersController(UserManager<AppUser> userManager, EmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }
        
        private string GetEmail(HttpContext context)
        {
            return context.User.FindFirst(ClaimTypes.Email) != null ? context.User.FindFirst(ClaimTypes.Email).Value : "";
        }

        [HttpGet(Name = "NurseGetElders")]
        public async Task<ActionResult<List<ElderDto>>> GetElders()
        {
            var email = GetEmail(HttpContext);
            var user = await _userManager.FindByEmailAsync(email);
            var result = await Mediator.Send(new List.Query() { Id = user.NurseId.Value });
            return HandleResult(result);
        }
    }
}