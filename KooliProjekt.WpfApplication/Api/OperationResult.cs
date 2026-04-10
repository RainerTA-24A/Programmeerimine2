using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace KooliProjekt.WpfApplication
{
    [ExcludeFromCodeCoverage]
    public class OperationResult
    {
        public IDictionary<string, string> PropertyErrors { get; set; } = new Dictionary<string, string>();
        public IList<string> Errors { get; set; } = [];
        public bool HasErrors => PropertyErrors?.Count > 0 || Errors?.Count > 0;

        public bool ShouldSerializeHasErrors()
        {
            return HasErrors;
        }

        public OperationResult AddError(string error)
        {
            Errors ??= [];
            Errors.Add(error);
            return this;
        }

        public OperationResult AddPropertyError(string property, string error)
        {
            PropertyErrors ??= new Dictionary<string, string>();
            if (PropertyErrors.ContainsKey(property))
            {
                return this;
            }

            PropertyErrors.Add(property, error);
            return this;
        }
    }
}