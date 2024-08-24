namespace insuranceLeadApi.Models.ViewModels
{
    public class PaginatedList<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Element { get; set; }

        public PaginatedList() { 
        Element = new List<T>();
        Total = 0;
        }
    }
}
