using BeerOverflow.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Data.AppConfigurations
{
    public class DrankListConfig : IEntityTypeConfiguration<DrankList>
    {
        public void Configure(EntityTypeBuilder<DrankList> builder)
        {
            builder.HasKey(x => new { x.UserId, x.BeerId});

            builder.HasOne(x => x.User)
                .WithMany(x => x.DrankLists)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Beer)
                .WithMany(x => x.DrankLists)
                .HasForeignKey(x => x.BeerId);
        }
    }
}
