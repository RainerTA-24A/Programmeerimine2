using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Arved
{
    public class ListArvedQueryHandler : IRequestHandler<ListArvedQuery, OperationResult<PagedResult<Arve>>>
    {
        private readonly ApplicationDbContext _db;

        public ListArvedQueryHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<OperationResult<PagedResult<Arve>>> Handle(ListArvedQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Arve>>();

            result.Value = await _db.Arved
                .Include(a => a.Klient)
                .OrderBy(a => a.InvoiceNumber)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}