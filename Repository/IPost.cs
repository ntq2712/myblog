using blog.DTO.Post;
using blog.Model;

namespace blog.Repository
{
    public interface IPost
    {
        public Task<List<Post>> GetAll(int pageSize, int pageIndex, string searchText);
        public Task<Post> GetPostById(Guid _id);
        public Task<Post> Update(Post _post);
        public Task<Post> Create(CreatePost _post, Guid _useId);
        public Task<bool> Delete(Guid _id);
    }
}