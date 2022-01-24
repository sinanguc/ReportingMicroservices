using Contact.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infrastructure.Configuration.PersonConfiguration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("person", "public");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
            builder.Property(x => x.Surname).HasColumnName("surname").HasMaxLength(150).IsRequired();
            builder.Property(x => x.Company).HasColumnName("company").HasMaxLength(500).IsRequired();

            builder.HasIndex(x => x.Id);
        }
    }
}
