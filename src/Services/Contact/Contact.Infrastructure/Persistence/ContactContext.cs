using Contact.Domain.Entities;
using Contact.Infrastructure.Configuration.PersonConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Contact.Infrastructure.Persistence
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) 
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new PersonContactInfoConfiguration());
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonContactInfo> PersonContactInfos { get; set; }

    }
}
