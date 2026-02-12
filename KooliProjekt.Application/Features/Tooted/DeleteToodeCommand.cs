using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Tooted
{
    [ExcludeFromCodeCoverage]
    public class DeleteToodeCommand : IRequest<OperationResult>
    {
        public int Id { get; set; }
    }
}