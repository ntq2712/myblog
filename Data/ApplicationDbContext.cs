using System.Security.Claims;
using blog.Model;
using Microsoft.EntityFrameworkCore;

namespace blog.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor _httpContextAccessor) : DbContext(options)
    {
        private readonly IHttpContextAccessor httpContextAccessor = _httpContextAccessor;

        public DbSet<Post> Post { set; get; }
        public DbSet<PostSection> PostSection { set; get; }
        public DbSet<User> User { set; get; }
        public DbSet<Tag> Tag { set; get; }
        public DbSet<Like> Like { set; get; }

        public void UpdateAuditFields()
        {
            var useId = httpContextAccessor.HttpContext.User.FindFirstValue("Id");
        }
    }
}