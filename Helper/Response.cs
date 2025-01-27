
namespace blog.Helper
{
    public class Response<T> : PageResponse
    {
        public int Status {set;get;}
        public bool Success {set;get;}
        public T? Data {set;get;}
        public string? Message {set;get;}
    }
}