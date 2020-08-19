using BeerOverflow.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Data.AppConfigurations
{
    public class RatingConfig : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(x => new { x.UserId, x.BeerId });

            builder.Property(x => x.RatingValue)
                .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Beer)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.BeerId);
        }
    }
}
