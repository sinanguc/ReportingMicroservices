using Contact.Application.Contracts.Persistence;
using Contact.Infrastructure.Persistence;
using Contact.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Contact.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContactContext>(options => options.UseNpgsql(configuration["DatabaseSettings:ConnectionString"]));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }
    }
}
