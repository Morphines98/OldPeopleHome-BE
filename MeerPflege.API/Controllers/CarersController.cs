using System.Security.Claims;
using MediatR;
using MeerPflege.Application.Carers;
using MeerPflege.Application.DTOs;
using MeerPflege.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MeerPflege.API.Controllers
{
    [Authorize(Roles = "Admin,CareTaker")]
    public class CarersController : BaseApiController
    {

        private readonly UserManager<AppUser> _userManager;

        public CarersController(UserManager<AppUser> userManager)
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

        [HttpPost]
        public async Task<IActionResult> CreateCarer(CarersDto carerDto)
        {
            carerDto.HomeId = GetHomeId(HttpContext);

            var user = await _userManager.FindByEmailAsync(carerDto.Email);

            if (user == null)
            {
                var createCarer = await Mediator.Send(new Create.Command { Carer = carerDto });
                if (!createCarer.IsSuccess)
                    return HandleResult(createCarer);

                var generatedPassword = "Password#1";
                var rez = await _userManager.CreateAsync(new AppUser() { UserName = carerDto.Email, Email = carerDto.Email, CarerId = carerDto.Id, HomeId = GetHomeId(HttpContext) }, generatedPassword);
                user = await _userManager.FindByEmailAsync(carerDto.Email);
                await _userManager.AddToRoleAsync(user, Role.CareTaker);
                return HandleResult(createCarer);
            }
            else
            {
                return HandleResult(Application.Core.Result<Nurse>.Failure("User already exists!"));
            }
        }

        [HttpGet(Name = "GetCarers")]
        public async Task<ActionResult<List<CarersDto>>> GetCarers()
        {
            var result = await Mediator.Send(new List.Query());
            return HandleResult(result);
        }

        [Authorize(Roles = Role.CareTaker)]
        [HttpGet("GetProfileInfo")]
        public async Task<ActionResult<CarersDto>> GetProfileInfo()
        {
            var email = GetEmail(HttpContext);
            var user = await _userManager.FindByEmailAsync(email);

            var result = await Mediator.Send(new List.Query() { Id = user.CarerId });
            var data = Application.Core.Result<CarersDto>.Success(result.Value.FirstOrDefault());
            return HandleResult(data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCarer(int id, CarersDto carerDto)
        {

            carerDto.HomeId = GetHomeId(HttpContext);
            var editNurse = await Mediator.Send(new Edit.Command { CarersDto = carerDto });
            if (!editNurse.IsSuccess)
                return HandleResult(editNurse);
            var user = _userManager.Users.FirstOrDefault(a => a.CarerId.HasValue && a.CarerId.Value == carerDto.Id);
            if (user.Email != carerDto.Email)
            {
                var result = await _userManager.SetEmailAsync(user, carerDto.Email);
                var resultName = await _userManager.SetUserNameAsync(user, carerDto.Email);
                await _userManager.UpdateAsync(user);
            }

            return HandleResult(editNurse);


        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarer(int id)
        {
            var user = _userManager.Users.FirstOrDefault(a => a.CarerId.HasValue && a.CarerId.Value == id);
            await _userManager.UpdateAsync(user);
            return HandleResult(await Mediator.Send(new DeleteCarer.Command { Id = id }));
        }
    }
}