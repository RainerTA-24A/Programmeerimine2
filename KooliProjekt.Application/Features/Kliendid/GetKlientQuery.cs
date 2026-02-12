using System.Diagnostics.CodeAnalysis;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Kliendid
{
    [ExcludeFromCodeCoverage]
    public class GetKlientQuery : IRequest<OperationResult<KlientDto>>
    {
        public int Id { get; set; }
    }
}