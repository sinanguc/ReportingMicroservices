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
    public class PersonContactInfoConfiguration : IEntityTypeConfiguration<PersonContactInfo>
    {
        public void Configure(EntityTypeBuilder<PersonContactInfo> builder)
        {
            builder.ToTable("person_contact_info", "public");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").IsRequired();
            builder.Property(x => x.PersonId).HasColumnName("person_id").IsRequired();
            builder.Property(x => x.InfoType).HasColumnName("info_type").IsRequired();
            builder.Property(x => x.InfoDetail).HasColumnName("info_detail").HasMaxLength(500).IsRequired();
            
            builder.HasOne<Person>(x => x.Person).WithMany(x => x.PersonContactInfo).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.Id).IsUnique();
            builder.HasIndex(x => x.PersonId).IsUnique(false);
            builder.HasIndex(x => x.InfoType).IsUnique(false);

        }
    }
}
