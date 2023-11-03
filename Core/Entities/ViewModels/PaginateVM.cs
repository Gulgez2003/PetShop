namespace Core.Entities.ViewModels
{
    public class PaginateVM<T>
    {
        public List<T> Models { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public bool Next { get; set; }
        public bool Previous { get; set; }
    }
}
