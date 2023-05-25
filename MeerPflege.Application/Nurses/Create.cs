using AutoMapper;
using MediatR;
using MeerPflege.Application.Core;
using MeerPflege.Application.DTOs;
using MeerPflege.Domain;
using MeerPflege.Persistence;

namespace MeerPflege.Application.Nurses
{
  public class Create
  {
    public class Command : IRequest<Result<NurseDto>>
    {
      public NurseDto Nurse { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<NurseDto>>
    {
      private readonly IMapper _mapper;
      private readonly DataContext _context;
      public Handler(DataContext context, IMapper mapper)
      {
        _context = context;
        _mapper = mapper;
      }

      public async Task<Result<NurseDto>> Handle(Command request, CancellationToken cancellationToken)
      {
        request.Nurse.StartWorkingDate = DateTime.Now;
        var nurseData = _mapper.Map<Nurse>(request.Nurse);
        _context.Nurses.Add(nurseData);
        _context.SaveChanges();
        request.Nurse.Id = nurseData.Id;
        return Result<NurseDto>.Success(request.Nurse);
      }
    }
  }
}