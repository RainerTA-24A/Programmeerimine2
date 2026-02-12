using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.TellimuseRead
{
    [ExcludeFromCodeCoverage]
    public class DeleteTellimuseReadCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}