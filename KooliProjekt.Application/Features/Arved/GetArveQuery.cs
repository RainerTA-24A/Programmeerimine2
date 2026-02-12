using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Arved
{
    [ExcludeFromCodeCoverage]
    public class GetArveQuery : IRequest<OperationResult<ArveDto>>
    {
        public int Id { get; set; }
    }
}