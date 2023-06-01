using System.Security.Claims;
using MeerPflege.Application.DTOs;
using MeerPflege.Application.SendGrid;
using MeerPflege.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MeerPflege.Application.CarersComands.CarerNews;

namespace MeerPflege.API.Controllers
{
    public class CarersNewsController:BaseApiController
    {
         private readonly UserManager<AppUser> _userManager;
        private readonly EmailSender _emailSender;

        public CarersNewsController(UserManager<AppUser> userManager, EmailSender emailSender)
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

        [HttpGet(Name = "CarersGetNews")]
        public async Task<ActionResult<List<NewsItemDto>>> GetNews()
        {
            var email = GetEmail(HttpContext);
            var user = await _userManager.FindByEmailAsync(email);
            var result = await Mediator.Send(new List.Query() { Id = user.CarerId.Value });
            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsItemDto>> GetNewsById(int id)
        {
            var result = await Mediator.Send(new View.Query { NewsId = id  });
            return HandleResult(result);
        }
    }
}