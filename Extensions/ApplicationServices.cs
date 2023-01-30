using API.Data;
using API.services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                 opt.UseSqlite(config.GetConnectionString("DefaultConnection")); 
                });
            services.AddScoped<interfaces.ITokenService, TokenService>();
            services.AddScoped<interfaces.IUserRepository, UserRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;

        }        
    }
}