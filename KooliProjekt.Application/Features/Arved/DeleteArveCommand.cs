using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Arved
{
    [ExcludeFromCodeCoverage]
    public class DeleteArveCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}