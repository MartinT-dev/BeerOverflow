using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeerOverflow.Data.AppConfigurations;
using BeerOverflow.Data.Entities;
using BeerOverflow.Data.Seeder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeerOverflow.Data.BeerOverflowContext
{
    public class BeeroverflowContext : IdentityDbContext<User, Role, int>
    {
        public BeeroverflowContext() { }
        

        public BeeroverflowContext(DbContextOptions contextOptions)
            :base(contextOptions)
        {
            
        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet <Like> Likes { get; set; }
        public DbSet <Rating> Ratings { get; set; }
        public DbSet <Style> Styles { get; set; }
        public DbSet <WishList> WishLists { get; set; }
        public DbSet <DrankList> DrankLists { get; set; }

     
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BeerConfig());
            builder.ApplyConfiguration(new BreweryConfig());
            builder.ApplyConfiguration(new CountryConfig());
            builder.ApplyConfiguration(new DrankListConfig());
            builder.ApplyConfiguration(new LikeConfig());
            builder.ApplyConfiguration(new RatingConfig());
            builder.ApplyConfiguration(new ReviewConfig());
            builder.ApplyConfiguration(new StyleConfig());
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new RoleConfig());
            builder.ApplyConfiguration(new WishListConfig());
            
            builder.Seeder();
        
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //var cascadeFKs = builder.Model.GetEntityTypes()
            //    .SelectMany(t => t.GetForeignKeys())
            //    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            //foreach (var fk in cascadeFKs) fk.DeleteBehavior = DeleteBehavior.Restrict;
        
            base.OnModelCreating(builder);
        }
    }
}
