

using blog.DTO.MyProfile;
using blog.Model;

namespace blog.Repository
{
    public interface ITestimonialRepository
    {
        public Task<List<TestimonialResponse>> GetTestimonials();
        public Task<Testimonials> Create(CTestimonial testimonial);
    }
}