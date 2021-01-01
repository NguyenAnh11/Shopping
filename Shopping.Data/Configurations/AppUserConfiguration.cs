using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopping.Data.Entities;

namespace Shopping.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");

            builder.Property(x => x.FristName).IsRequired()
                    .HasMaxLength(200);

            builder.Property(x => x.LastName).IsRequired()
                    .HasMaxLength(200);

            builder.Property(x => x.DoB).IsRequired();
        }
    }
}
