using MeerPflege.Application.HomeGroups;
using MeerPflege.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeerPflege.API.Controllers
{
  [Authorize(Roles = "Admin")]
  public class HomeGroupsController : BaseApiController
  {
    private int GetHomeId(HttpContext context)
    {
      return context.User.FindFirst("HomeId") != null ? Int32.Parse(context.User.FindFirst("HomeId").Value) : 0;
    }

    [HttpGet(Name = "homeGroups")]
    public async Task<IActionResult> GetHomeGroups()
    {
      return HandleResult(await Mediator.Send(new List.Query{HomeId = GetHomeId(HttpContext)}));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetHomeGroup(int id)
    {
      return HandleResult(await Mediator.Send(new Details.Query { Id = id }));
    }

    [HttpPost]
    public async Task<IActionResult> CreateHomeGroups(HomeGroup homeGroup)
    {
      homeGroup.HomeId = GetHomeId(HttpContext);
      return HandleResult(await Mediator.Send(new Create.Command { HomeGroup = homeGroup }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditHomeGroup(int id, HomeGroup homeGroup)
    {
      
      homeGroup.HomeId = GetHomeId(HttpContext);
      homeGroup.Id = id;
      return Ok(await Mediator.Send(new Edit.Command { HomeGroup = homeGroup }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHomeGroup(int id)
    {
      return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
  }
}