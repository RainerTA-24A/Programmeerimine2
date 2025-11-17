using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Kliendid
{
    public class DeleteKlientCommandHandler : IRequestHandler<DeleteKlientCommand, OperationResult>
    {
        private readonly ApplicationDbContext _dbContext;

        public DeleteKlientCommandHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> Handle(DeleteKlientCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            await _dbContext.Kliendid
                .Where(k => k.Id == request.Id)
                .ExecuteDeleteAsync();

            return result;
        }
    }
}