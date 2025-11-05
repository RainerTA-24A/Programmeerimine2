using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Arved
{
    public class ListArvedQuery : IRequest<OperationResult<PagedResult<Arve>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}