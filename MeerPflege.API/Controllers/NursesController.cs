using System.Collections.Generic;
using MeerPflege.Application.DTOs;
using MeerPflege.Application.Nurses;
using MeerPflege.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeerPflege.API.Controllers
{
  [Authorize(Roles = "Admin")]
  public class NursesController : BaseApiController
  {
    private readonly UserManager<AppUser> _userManager;

    public NursesController(UserManager<AppUser> userManager)
    {
      _userManager = userManager;
    }

    private int GetHomeId(HttpContext context)
    {
      return context.User.FindFirst("HomeId") != null ? Int32.Parse(context.User.FindFirst("HomeId").Value) : 0;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNurse(NurseDto nurse)
    {
      nurse.HomeId = GetHomeId(HttpContext);

      var user = await _userManager.FindByEmailAsync(nurse.Email);

      if (user == null)
      {
        var generatedPassword = "Password#1";
        var rez = await _userManager.CreateAsync(new AppUser() { UserName = nurse.Email, Email = nurse.Email, HomeId = GetHomeId(HttpContext) }, generatedPassword);
        user = await _userManager.FindByEmailAsync(nurse.Email);
        await _userManager.AddToRoleAsync(user, Role.Nurse);
        return HandleResult(await Mediator.Send(new Create.Command { Nurse = nurse }));
      }
      else
      {
        return HandleResult(Application.Core.Result<Nurse>.Failure("User already exists!"));
      }
    }

    [HttpGet(Name = "GetNurses")]
    public async Task<ActionResult<List<NewsItemDto>>> GetNurses()
    {
      var result = await Mediator.Send(new List.Query());
      return HandleResult(result);
    }

  }
}