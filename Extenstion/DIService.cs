using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        }

        public static void AddMap(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}