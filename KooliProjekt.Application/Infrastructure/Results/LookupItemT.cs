namespace KooliProjekt.Application.Infrastructure.Results;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class LookupItem<T>
{
    public T Value { get; set; }
    public string Text { get; set; }
}