namespace M1MartAPI.Shared
{
    public class PaginationDto<T>
    {
        public List<T> Data { get; set; } = null!;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages
        {
            get
            {
                return (int)Math.Ceiling((double)TotalRecords / PageSize);
            }
        }
    }
}
