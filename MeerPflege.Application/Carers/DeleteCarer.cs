using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Persistence;

namespace MeerPflege.Application.Carers
{
    public class DeleteCarer
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _dataContext;
            public Handler(DataContext context)
            {
                _dataContext = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await _dataContext.Carers.FindAsync(request.Id);
                if (entity == null) return null;
                entity.IsDeleted = true;

                var result = await _dataContext.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}