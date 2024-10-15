namespace M1MartAPI.Shared
{
    public class ResponseDto<T>
    {
        public string Status { get; set; } = null!;
        //public int Code { get; set; }
        public string Message { get; set; } = null!;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
    }
}
