using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Tooted
{
    public class ListTootedQuery : IRequest<OperationResult<PagedResult<Toode>>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
