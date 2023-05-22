using FluentValidation;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Domain;
using MeerPflege.Persistence;

namespace MeerPflege.Application.HomeGroups
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public HomeGroup HomeGroup { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>{
            public CommandValidator()
            {
                RuleFor(a=> a.HomeGroup).SetValidator(new HomeGroupValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                request.HomeGroup.CreatedDate = DateTime.Now;
                _context.HomeGroups.Add(request.HomeGroup);
                var result = await _context.SaveChangesAsync() > 0;
                if(!result) return Result<Unit>.Failure("Failed to create.");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}