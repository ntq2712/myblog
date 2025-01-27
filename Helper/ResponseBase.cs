namespace blog.Helper
{
    public class ResponseBase<T>
    {
        public int Status { set; get; }
        public bool Success { set; get; }
        public T? Data { set; get; }
        public string? Message { set; get; }
    }
}