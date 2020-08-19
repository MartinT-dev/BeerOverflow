using BeerOverflow.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Data.AppConfigurations
{
    public class BeerConfig : IEntityTypeConfiguration<Beer>
    {
        public void Configure(EntityTypeBuilder<Beer> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();
                
            builder.Property(x => x.Abv)
                .IsRequired();

            builder.HasOne(x => x.Brewery).WithMany(x => x.Beers).HasForeignKey(x => x.BreweryId);

            builder.HasOne(x => x.Country).WithMany(x => x.Beers).HasForeignKey(x => x.CountryId);

            builder.HasOne(x => x.Style).WithMany(x => x.Beers).HasForeignKey(x => x.StyleId);
        }
    }
}
