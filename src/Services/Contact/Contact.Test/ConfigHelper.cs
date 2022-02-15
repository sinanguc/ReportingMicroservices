using AutoMapper;
using Contact.Application.Mappings;
using Contact.Infrastructure.Persistence;
using Contact.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Contact.Test
{
    public static class ConfigHelper
    {
        public static IMapper GetAutoMapperConfig()
        {
            var mapperConfig = new MapperConfiguration(d =>
            {
                d.AddProfile<MappingProfile>();
            });
            return mapperConfig.CreateMapper();
        }

        public static ContactContext GetContactDbContext()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ContactContext>();
            builder.UseInMemoryDatabase("MyInMemoryDatabseName")
                   .UseInternalServiceProvider(serviceProvider);

            var context = new ContactContext(builder.Options);
            return context;
        }

        public static ContactRepository GetContactRepository()
        {
            return new ContactRepository(GetContactDbContext());
        }
    }
}
