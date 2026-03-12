namespace KooliProjekt.WindowsForms
{
    public class OperationResult
    {
        public IDictionary<string, string> PropertyErrors { get; set; }
        public IList<string> Errors { get; set; }
        public bool HasErrors => PropertyErrors?.Count > 0 || Errors?.Count > 0;
    }
}
