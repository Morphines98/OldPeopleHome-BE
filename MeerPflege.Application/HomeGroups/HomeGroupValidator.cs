using FluentValidation;
using MeerPflege.Domain;

namespace MeerPflege.Application.HomeGroups
{
    public class HomeGroupValidator : AbstractValidator<HomeGroup>
    {
        public HomeGroupValidator()
        {
            RuleFor(a=> a.Name).NotEmpty().NotNull();
            RuleFor(a=> a.HomeId).NotNull();
        }
    }
}