namespace Common.Wrappers
{
    public class PagedResponse<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalRecords { get; set; }
        public T Data { get; set; }
    }
}