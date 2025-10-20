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
            var users = dbContext.Visitor;
            var testimonials = dbContext.Testimonials.Where(t => t.Status == 2);

            var result = await (
                from t in testimonials
                join u in users on t.UserId equals u.Id
                select new TestimonialResponse
                {
                    Id = t.Id,
                    CompanyName = u.CompanyName,
                    FullName = u.FullName,
                    ProfilePic = u.Avatar,
                    Content = t.Content,
                    WorkingTitle = u.PositionTitle
                }
            ).ToListAsync();

            return result;
        }

        public async Task<List<TestimonialFilterResponse>> GetTestimonialByFilter(int status)
        {
            IQueryable<Visitor> users = dbContext.Visitor;

            IQueryable<Testimonials> testimonialsQuery = dbContext.Testimonials;

            if (status != 0)
            {
                testimonialsQuery = testimonialsQuery.Where(t => t.Status == status);
            }

            var result = await (
               from t in testimonialsQuery
               join u in users on t.UserId equals u.Id
               select new TestimonialFilterResponse
               {
                   Id = t.Id,
                   CompanyName = u.CompanyName,
                   FullName = u.FullName,
                   ProfilePic = u.Avatar,
                   Content = t.Content,
                   WorkingTitle = u.PositionTitle,
                   Status = t.Status
               }
            ).ToListAsync();

            return result;
        }

        public async Task<Testimonials?> UpdateStatusTestimonial(Guid id, int status)
        {
            var testimonial = await dbContext.Testimonials.FirstOrDefaultAsync(t => t.Id == id);

            if (testimonial == null)
            {
                return null;
            }

            testimonial.Status = status;

            dbContext.Testimonials.Update(testimonial);
            await dbContext.SaveChangesAsync();

            return testimonial;
        }

        public async Task<Testimonials> Create(CTestimonial testimonial)
        {
            var userExists = await dbContext.Visitor.AnyAsync(u => u.Id == testimonial.UserId);

            if (userExists == false)
            {
                throw new HttpStatusCodeException(400, "User not exists");
            }

            var newItem = new Testimonials
            {
                Content = testimonial.Content,
                UserId = testimonial.UserId,
                CreateAt = DateTime.UtcNow,
                Status = 1
            };

            await dbContext.Testimonials.AddAsync(newItem);
            await dbContext.SaveChangesAsync();

            return newItem;
        }
    }
}