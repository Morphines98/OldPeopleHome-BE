using MeerPflege.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeerPflege.API.Controllers
{
  [Authorize]
  public class NewsController : BaseApiController
  {
    private int GetHomeId(HttpContext context)
    {
      return context.User.FindFirst("HomeId") != null ? Int32.Parse(context.User.FindFirst("HomeId").Value) : 0;
    }

    [HttpGet(Name = "GetNews")]
    public async Task<ActionResult<List<NewsItemDto>>> GetNews()
    {
      var result = await Mediator.Send(new Application.News.List.Query());
      return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateHomeGroups(NewsItemDto newsItem)
    {
      newsItem.HomeId = GetHomeId(HttpContext);
      newsItem.AddedDate = DateTime.Now;
      return HandleResult(await Mediator.Send(new Application.News.Create.Command { NewsItem = newsItem }));
    }
  }
}