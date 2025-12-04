using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using KooliProjekt.Application.Data.Repositories; 
using MediatR;

namespace KooliProjekt.Application.Features.Arved
{
    public class ListArvedQueryHandler : IRequestHandler<ListArvedQuery, OperationResult<PagedResult<ArveListDto>>>
    {
        private readonly IArveRepository _arveRepository;

        public ListArvedQueryHandler(IArveRepository arveRepository)
        {
            _arveRepository = arveRepository;
        }

        public async Task<OperationResult<PagedResult<ArveListDto>>> Handle(ListArvedQuery request, CancellationToken cancellationToken)
        {
            // Kasutame repositorit leheküljendatud loendi lugemiseks
            var pagedDtoResult = await _arveRepository.GetPagedListAsync(request.Page, request.PageSize);

            return OperationResult<PagedResult<ArveListDto>>.Success(pagedDtoResult);
        }
    }
}