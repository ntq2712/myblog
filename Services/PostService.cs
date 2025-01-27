using AutoMapper;
using blog.Data;
using blog.DTO.Post;
using blog.Model;
using blog.Repository;
using Microsoft.EntityFrameworkCore;

namespace blog.Services
{
    public class PostService(ApplicationDbContext dbContext, IMapper mapper) : IPost
    {
        public async Task<List<Post>> GetAll(int pageSize, int pageIndex, string searchText)
        {
            var posts = await dbContext.Post.Where(e => e.IsDelete == true).Skip((pageIndex - 1) * pageSize).Take(pageIndex * pageSize).Where(s => string.IsNullOrEmpty(searchText) || s.Title.Contains(searchText)).ToListAsync();

            if (posts == null)
            {
                throw new KeyNotFoundException("Don't find post table");
            }

            return posts;
        }

        public async Task<Post> Create(CreatePost _post, Guid _useId)
        {
            var post = mapper.Map<Post>(_post);

            if (post == null)
            {
                throw new KeyNotFoundException("Don't create post map");
            }

            post.CreateBy = _useId;
       
            await dbContext.Post.AddAsync(post);
            await dbContext.SaveChangesAsync();

            return post;
        }

        public async Task<Post> GetPostById(Guid _id)
        {
            var post = await dbContext.Post.FirstOrDefaultAsync(e => e.PostId == _id);

            if (post == null)
            {
                throw new KeyNotFoundException("Don't find post by this id");
            }

            return post;
        }

        public async Task<bool> Delete(Guid _id)
        {
            var post = await GetPostById(_id);

            if (post == null)
            {
                throw new KeyNotFoundException("Don't find post by this id");
            }

            post.IsDelete = false;

            dbContext.Post.Update(post);
            await dbContext.SaveChangesAsync();

            return true;

        }

        public async Task<Post> Update(Post _post)
        {
            var post = await GetPostById(_post.PostId);

            if (post == null)
            {
                throw new KeyNotFoundException("Don't find post by this id");
            }

            post.Content = _post.Content;
            post.Title = _post.Title;
            post.CoverImage = _post.CoverImage;
            post.ModifyAt = new DateTime();

            dbContext.Update(post);
            await dbContext.SaveChangesAsync();

            return post;
        }
    }
}