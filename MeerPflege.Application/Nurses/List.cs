// using MediatR;

// namespace MeerPflege.Application.Nurses
// {
//         public class List
//   {
//     public class Query : IRequest<Result<List<Nurses>>> { }

//     public class Handler : IRequestHandler<Query, Result<List<NewsItemDto>>>
//     {
//       private readonly DataContext _dataContext;
//       private readonly IMapper _mapper;
//       public Handler(DataContext context, IMapper mapper)
//       {
//         _dataContext = context;
//         _mapper = mapper;
//       }

//       public async Task<Result<List<NewsItemDto>>> Handle(Query request, CancellationToken cancellationToken)
//       {
//         var newsData = await _dataContext.NewsItems.Include("NewsItemAttachments").Where(a => a.IsDeleted == false).ToListAsync();
//         var result = _mapper.Map<List<NewsItemDto>>(newsData);

//         return Result<List<NewsItemDto>>.Success(result);
//       }
//     }
//   }
//     }
