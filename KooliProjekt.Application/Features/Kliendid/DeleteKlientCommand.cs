using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kliendid
{
    [ExcludeFromCodeCoverage]
    public class DeleteKlientCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}