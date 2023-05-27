using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Persistence;

public class Delete
    {
        public class Command:IRequest<Result<Unit>>
        {
            public int Id { get; set; }
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
                var entity = await _context.Activities.FindAsync(request.Id);
                if(entity == null) return null;
                entity.IsDeleted = true;
                var result = await _context.SaveChangesAsync() > 0;

                if(!result) return Result<Unit>.Failure("Failed to delete.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }