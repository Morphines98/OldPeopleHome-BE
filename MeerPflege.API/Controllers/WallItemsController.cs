using MeerPflege.Application.DTOs;
using MeerPflege.Application.WallItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeerPflege.API.Controllers
{
    [Authorize]
    public class WallItemsController : BaseApiController
    {

        private int GetHomeId(HttpContext context)
        {
            return context.User.FindFirst("HomeId") != null ? Int32.Parse(context.User.FindFirst("HomeId").Value) : 0;
        }

        [HttpGet(Name = "GetWallItem")]
        public async Task<ActionResult<List<WallItemDto>>> GetWallItem()
        {
            var result = await Mediator.Send(new List.Query());
            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallItem(WallItemDto wallItemDto)
        {
            wallItemDto.HomeId = GetHomeId(HttpContext);
            return HandleResult(await Mediator.Send(new Create.Command {WallItemDto = wallItemDto }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditWallItem(int id, WallItemDto wallItemDto)
        {
            wallItemDto.HomeId = GetHomeId(HttpContext);
            return HandleResult(await Mediator.Send(new Edit.Command { WallItemDto = wallItemDto }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallItem(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}