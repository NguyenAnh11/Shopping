using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Shopping.Data.Entities;

namespace Shopping.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();

            builder.HasOne(x => x.Product).WithMany(x => x.ProductImages).HasForeignKey(x => x.ProductId);

            builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(500);

            builder.Property(x => x.IsDefault).HasDefaultValue(false);

            builder.Property(x => x.Caption).IsRequired().HasMaxLength(200);

            builder.Property(x => x.DateCreated).IsRequired();
        }
    }
}
