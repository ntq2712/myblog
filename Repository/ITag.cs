
using blog.Model;

namespace blog.Repository
{
    public interface ITag
    {
        public Task<List<Tag>> GetAll();
        public Task<Tag?> Crate(string name, Guid idUser);
    }
}