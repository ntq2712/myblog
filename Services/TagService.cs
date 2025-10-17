using blog.Data;
using blog.Model;
using blog.Repository;
using Microsoft.EntityFrameworkCore;

namespace blog.Services
{
    public class TagService : ITag
    {
        private readonly ApplicationDbContext dbContext;

        public TagService(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<List<Tag>> GetAll()
        {
            var tags = await dbContext.Tag.Where(t => t.IsDelete == false).ToListAsync();

            return tags;
        }

        public async Task<Tag?> Crate(string name, Guid idUser){
            bool isExits = await dbContext.Tag.AnyAsync(t => t.TagName == name);

            if(isExits){
                return null;
            }
            var tag = new Tag();

            tag.TagName = name;
            tag.IsDelete = false;
            tag.CreateAt = DateTime.Today;
            tag.CreateBy = idUser;
            tag.ModifyAt = DateTime.Today;
            tag.ModifyBy = idUser;

            await dbContext.AddAsync(tag);
            await dbContext.SaveChangesAsync();

            return tag;
        }
    }
}