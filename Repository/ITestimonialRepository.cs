

using blog.DTO.MyProfile;
using blog.Model;

namespace blog.Repository
{
    public interface ITestimonialRepository
    {
        public Task<List<TestimonialResponse>> GetTestimonials();
        public Task<List<TestimonialFilterResponse>> GetTestimonialByFilter(int status);
        public Task<Testimonials> Create(CTestimonial testimonial);
        public Task<Testimonials?> UpdateStatusTestimonial(Guid id, int status);
        public Task<bool> DeleteTestimonial(Guid id);
    }
}