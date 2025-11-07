using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Features.Kliendid
{
    public class ListKliendidQueryHandler : IRequestHandler<ListKliendidQuery, OperationResult<PagedResult<Klient>>>
    {
        private readonly ApplicationDbContext _db;

        public ListKliendidQueryHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<OperationResult<PagedResult<Klient>>> Handle(ListKliendidQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<Klient>>();

            result.Value = await _db.Kliendid
                .OrderBy(k => k.Name)
                .GetPagedAsync(request.Page, request.PageSize);

            return result;
        }
    }
}