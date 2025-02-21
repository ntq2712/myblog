using System.Security.Claims;
using blog.Model;
using Microsoft.EntityFrameworkCore;

namespace blog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Post> Post { set; get; }
        public DbSet<PostSection> PostSection { set; get; }
        public DbSet<User> User { set; get; }
        public DbSet<Tag> Tag { set; get; }
        public DbSet<Like> Like { set; get; }

    }
}