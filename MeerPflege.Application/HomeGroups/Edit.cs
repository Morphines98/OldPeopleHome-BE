using AutoMapper;
using FluentValidation;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Domain;
using MeerPflege.Persistence;

namespace MeerPflege.Application.HomeGroups
{
    public class Edit
    {
        public class Command:IRequest<Result<Unit>>
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
            private readonly IMapper _mapper;
            
            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity =await _context.HomeGroups.FindAsync(request.HomeGroup.Id);

                if(entity == null) return null;
                _mapper.Map(request.HomeGroup, entity);
            
                var result = await _context.SaveChangesAsync() > 0;
                if(!result) return Result<Unit>.Failure("Failed to update.");
                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}