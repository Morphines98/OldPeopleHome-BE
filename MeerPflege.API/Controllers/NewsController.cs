using MeerPflege.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MeerPflege.Application.News;

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
      var result = await Mediator.Send(new List.Query());
      return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NewsItemDto>> GetNewsById(int id)
    {
      var result = await Mediator.Send(new View.Query{Id = id});
      return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNews(NewsItemDto newsItem)
    {
      newsItem.HomeId = GetHomeId(HttpContext);
      newsItem.AddedDate = DateTime.Now;
      return HandleResult(await Mediator.Send(new Create.Command { NewsItem = newsItem }));
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> EditNews(int id,NewsItemDto newsItem)
    {
      newsItem.HomeId = GetHomeId(HttpContext);
      return HandleResult(await Mediator.Send(new Edit.Command { NewsItem = newsItem }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNews(int id)
    {
      return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
  }
}