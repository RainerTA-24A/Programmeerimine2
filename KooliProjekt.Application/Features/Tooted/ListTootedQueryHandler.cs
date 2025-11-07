using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Tooted
{
    public class ListTootedQueryHandler : IRequestHandler<ListTootedQuery, OperationResult<PagedResult<Toode>>>
    {
        private readonly ApplicationDbContext _db;

        public ListTootedQueryHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<OperationResult<PagedResult<Toode>>> Handle(ListTootedQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Toode>>();

            result.Value = await _db.Tooted
                .OrderBy(p => p.Name)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}