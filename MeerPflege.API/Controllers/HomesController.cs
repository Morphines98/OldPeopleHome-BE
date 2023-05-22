using MeerPflege.Application.Homes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeerPflege.API.Controllers
{
  [Authorize]
  public class HomesController : BaseApiController
  {
    
    [HttpGet(Name = "homes")]
    public async Task<IActionResult> GetHomes()
    {
      return HandleResult(await Mediator.Send(new List.Query()));
    }
  }
}