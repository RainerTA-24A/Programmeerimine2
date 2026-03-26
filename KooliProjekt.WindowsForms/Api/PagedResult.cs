namespace KooliProjekt.WindowsForms.Api
{
    public class PagedResult<T> : PagedResultBase
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = [];
        }
    }
}
