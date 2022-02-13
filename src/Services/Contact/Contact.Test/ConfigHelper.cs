using AutoMapper;
using Contact.Application.Mappings;
using Contact.Infrastructure.Persistence;
using Contact.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

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
            var optionsBuilder = new DbContextOptionsBuilder<ContactContext>();
            optionsBuilder.UseInMemoryDatabase("MyInMemoryDatabseName");
            var context = new ContactContext(optionsBuilder.Options);
            return context;
        }

        public static ContactRepository GetContactRepository()
        {
            return new ContactRepository(GetContactDbContext());
        }
    }
}
