using blog.Data;
using blog.DTO.MyProfile;
using blog.Extenstion;
using blog.Model;
using blog.Repository;
using Microsoft.EntityFrameworkCore;

namespace blog.Services
{
    public class TestimonialsService(ApplicationDbContext dbContext) : ITestimonialRepository
    {
        public async Task<List<TestimonialResponse>> GetTestimonials()
        {
            var users = dbContext.User;
            var testimonials = dbContext.Testimonials;

            var result = await (
                from t in testimonials
                join u in users on t.UserId equals u.UserId
                select new TestimonialResponse
                {
                    Id = t.Id,
                    CompanyName = u.CompanyName,
                    FullName = u.FullName,
                    ProfilePic = u.ProfilePic,
                    Content = t.Content,
                    WorkingTitle = u.WorkingTitle
                }
            ).ToListAsync();

            return result;
        }
        public async Task<Testimonials> Create(CTestimonial testimonial)
        {
            var userExists = await dbContext.User.AnyAsync(u => u.UserId == testimonial.UserId);

            if (userExists == false)
            {
                throw new HttpStatusCodeException(400, "User not exists");
            }

            var newItem = new Testimonials
            {
                Content = testimonial.Content,
                UserId = testimonial.UserId,
                CreateAt = DateTime.UtcNow,
            };

            await dbContext.Testimonials.AddAsync(newItem);
            await dbContext.SaveChangesAsync();

            return newItem;
        }
    }
}