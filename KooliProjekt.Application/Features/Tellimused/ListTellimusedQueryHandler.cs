using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Tellimused
{
    public class ListTellimusedQueryHandler : IRequestHandler<ListTellimusedQuery, OperationResult<PagedResult<Tellimus>>>
    {
        private readonly ApplicationDbContext _db;

        public ListTellimusedQueryHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<OperationResult<PagedResult<Tellimus>>> Handle(ListTellimusedQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Tellimus>>();

            result.Value = await _db.Tellimused
                .Include(t => t.Toode)
                .Include(t => t.Arve)
                .OrderBy(t => t.Id)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}