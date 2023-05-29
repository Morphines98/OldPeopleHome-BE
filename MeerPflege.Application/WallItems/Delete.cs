using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeerPflege.Application.WallItems
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _dataContext;
            public Handler(DataContext context, IMapper mapper)
            {
                _dataContext = context;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity =  _dataContext.WallItems.Include("WallItemAttachments").First(x => x.Id == request.Id);
                if (entity == null) return null;
                entity.IsDeleted = true;
                foreach (var wallAtach in entity.WallItemAttachments)
                {
                    wallAtach.IsDeleted = true;
                }

                var result = await _dataContext.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to delete.");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
