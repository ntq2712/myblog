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
        public DbSet<MyProfile> MyProfiles { set; get; }
        public DbSet<MyCaseStudies> MyCaseStudies { set; get; }
        public DbSet<MyWorkExperience> MyWorkExperience { set; get; }
        public DbSet<CaseStudyType> CaseStudyType { set; get; }
        public DbSet<CaseStudy> CaseStudy { set; get; }
        public DbSet<GetInTouch> GetInTouch { set; get; }
        public DbSet<Testimonials> Testimonials { set; get; }
        public DbSet<CaseStudyDetail> CaseStudyDetail { set; get; }
        public DbSet<Guest> Guest { set; get; }
        public DbSet<Visitor> Visitor { set; get; }
    }
}