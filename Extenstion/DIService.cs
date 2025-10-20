using blog.Data;
using blog.Repository;
using blog.Services;

namespace blog.Extenstion
{
    public static class DIService
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITag, TagService>();
            services.AddScoped<IRSA, RSAService>();
            services.AddScoped<IMyProfileSevice, MyProfileSevice>();
            services.AddScoped<ICaseStudyService, CaseStudyService>();
            services.AddScoped<ICaseStudyTypeService, CaseStudyTypeService>();
            services.AddScoped<IWorkExperienceRepository, WorkExperience>();
            services.AddScoped<IGetInTouchRepository, GetInTouchService>();
            services.AddScoped<ITestimonialRepository, TestimonialsService>();
            services.AddScoped<IGuest, GeustService>();
            services.AddScoped<IVisitorRepository, VisitorService>();
        }

        public static void AddMap(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}