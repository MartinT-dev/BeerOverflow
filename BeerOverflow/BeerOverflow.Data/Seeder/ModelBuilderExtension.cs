using BeerOverflow.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflow.Data.Seeder
{
    public static class ModelBuilderExtension
    {
        public static void Seeder(this ModelBuilder builder)
        {
            //Role
            builder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "Admin", NormalizedName = "ADMIN" },
                new Role { Id = 2, Name = "User", NormalizedName = "USER" }
            );

            //Admin Account
            var hasher = new PasswordHasher<User>();

            User adminUser = new User
            {
                Id = 11,
                UserName = "admin@bo.com",
                Country = "Bulgaria",
                NormalizedUserName = "ADMIN@BO.COM",
                Email = "admin@bo.com",
                NormalizedEmail = "ADMIN@BO.COM",
                CreatedOn = DateTime.UtcNow,
                LockoutEnabled = true,
                SecurityStamp = "DC6E275DD1E24957A7781D42BB68293B"
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "admin1");

            builder.Entity<User>().HasData(adminUser);

            builder.Entity<IdentityUserRole<int>>().HasData(
                new IdentityUserRole<int>
                {
                    RoleId = 1,
                    UserId = adminUser.Id
                });
            //User
            builder.Entity<User>().HasData(

                new User
                {
                    Id = 1,
                    UserName = "TestUser1",
                    Country = "Bulgaria"
                },
                new User
                {
                    Id = 2,
                    UserName = "TestUser2",
                    Country = "England"
                },
                new User
                {
                    Id = 3,
                    UserName = "TestUser3",
                    Country = "Germany",
                },
                new User
                {
                    Id = 4,
                    UserName = "TestUser4",
                    Country = "Spain"
                },
                new User
                {
                    Id = 5,
                    UserName = "TestUser5",
                    Country = "USA"
                },
                new User
                {
                    Id = 6,
                    UserName = "TestUser6",
                    Country = "Bulgaria"
                },
                new User
                {
                    Id = 7,
                    UserName = "TestUser7",
                    Country = "USA"
                },
                new User
                {
                    Id = 8,
                    UserName = "TestUser8",
                    Country = "Bulgaria"
                },
                new User
                {
                    Id = 9,
                    UserName = "TestUser9",
                    Country = "Bulgaria"
                },
                new User
                {
                    Id = 10,
                    UserName = "TestUser10",
                    Country = "England"
                });
            //Country
            builder.Entity<Country>().HasData(
               new Country
               {
                   Id = 1,
                   Name = "Bulgaria",
               },
               new Country
               {
                   Id = 2,
                   Name = "Belgium",
               },
               new Country
               {
                   Id = 3,
                   Name = "Czech Republic"
               },
               new Country
               {
                   Id = 4,
                   Name = "The Netherlands"
               },
               new Country
               {
                   Id = 5,
                   Name = "Germany"
               });

            //Brewery
            builder.Entity<Brewery>().HasData(
                new Brewery
                {
                    Id = 1,
                    Name = "Zagorka",
                    CountryId = 1
                },
                new Brewery
                {
                    Id = 2,
                    Name = "Kamenitza",
                    CountryId = 1
                },
                new Brewery
                {
                    Id = 3,
                    Name = "Glarus",
                    CountryId = 1
                },
                new Brewery
                {
                    Id = 4,
                    Name = "De Brabandere",
                    CountryId = 2
                },
                new Brewery
                {
                    Id = 5,
                    Name = "De Dochter van de Korenaar",
                    CountryId = 2
                },
                new Brewery
                {
                    Id = 6,
                    Name = "De Graal",
                    CountryId = 2
                },
                new Brewery
                {
                    Id = 7,
                    Name = "Staropramen",
                    CountryId = 3
                },
                new Brewery
                {
                    Id = 8,
                    Name = "Starobrno",
                    CountryId = 3
                },
                new Brewery
                {
                    Id = 9,
                    Name = "Zatecky Pivovar",
                    CountryId = 3
                },
                new Brewery
                {
                    Id = 10,
                    Name = "Heineken",
                    CountryId = 4
                },
                new Brewery
                {
                    Id = 11,
                    Name = "De Natte Gijt",
                    CountryId = 4
                },
                new Brewery
                {
                    Id = 12,
                    Name = "Eem Bier",
                    CountryId = 4
                },
                new Brewery
                {
                    Id = 13,
                    Name = "Allgauer Brauhaus",
                    CountryId = 5
                },
                new Brewery
                {
                    Id = 14,
                    Name = "Heidenpeters",
                    CountryId = 5
                },
                new Brewery
                {
                    Id = 15,
                    Name = "Weyermann Versuchsbrauerei",
                    CountryId = 5
                });

            //Style
            builder.Entity<Style>().HasData(
                new Style
                {
                    Id = 1,
                    Name = "Pale Lager"
                },
                new Style
                {
                    Id = 2,
                    Name = "Dark Lager"
                },
                new Style
                {
                    Id = 3,
                    Name = "Amber Lager"
                },
                new Style
                {
                    Id = 4,
                    Name = "Stout"
                },
                new Style
                {
                    Id = 5,
                    Name = "Pilsner"
                },
                new Style
                {
                    Id = 6,
                    Name = "IPA"
                },
                new Style
                {
                    Id = 7,
                    Name = "Pale Ale"
                },
                new Style
                {
                    Id = 8,
                    Name = "Blond Ale"
                })
            ;

            //Beer
            builder.Entity<Beer>().HasData(
                new Beer
                {
                    Id = 1,
                    Name = "Stolichno Pale Ale",
                    CountryId = 1,
                    StyleId = 7,
                    BreweryId = 1,
                    Abv = double.Parse("5.5")
                },
                new Beer
                {
                    Id = 2,
                    Name = "Zagorka IPA",
                    CountryId = 1,
                    StyleId = 6,
                    BreweryId = 1,
                    Abv = double.Parse("4.8")
                },
                new Beer
                {
                    Id = 3,
                    Name = "Zagorka Retro",
                    CountryId = 1,
                    StyleId = 1,
                    BreweryId = 1,
                    Abv = double.Parse("4.5")
                },
                new Beer
                {
                    Id = 4,
                    Name = "Zagorka Spetsialno",
                    CountryId = 1,
                    StyleId = 1,
                    BreweryId = 1,
                    Abv = double.Parse("5.0")
                },
                new Beer
                {
                    Id = 5,
                    Name = "Amstel Dark",
                    CountryId = 1,
                    StyleId = 2,
                    BreweryId = 1,
                    Abv = double.Parse("5.0")
                },
                new Beer
                {
                    Id = 6,
                    Name = "Kamenitza Staro Pivo",
                    CountryId = 1,
                    StyleId = 5,
                    BreweryId = 2,
                    Abv = double.Parse("4.8")
                },
                new Beer
                {
                    Id = 7,
                    Name = "Kamenitza 1881",
                    CountryId = 1,
                    StyleId = 1,
                    BreweryId = 2,
                    Abv = double.Parse("4.4")
                },
                new Beer
                {
                    Id = 8,
                    Name = "Kamenitza Tamno",
                    CountryId = 1,
                    StyleId = 2,
                    BreweryId = 2,
                    Abv = double.Parse("6.0")
                },
                new Beer
                {
                    Id = 9,
                    Name = "Slavena svetlo",
                    CountryId = 1,
                    StyleId = 1,
                    BreweryId = 2,
                    Abv = double.Parse("4.1")
                },
                new Beer
                {
                    Id = 10,
                    Name = "Pleven Svetlo Pivo",
                    CountryId = 1,
                    StyleId = 1,
                    BreweryId = 2,
                    Abv = double.Parse("4.1")
                },
                new Beer
                {
                    Id = 11,
                    Name = "Glarus Black Sea IPA",
                    CountryId = 1,
                    StyleId = 6,
                    BreweryId = 3,
                    Abv = double.Parse("6.0")
                },
                new Beer
                {
                    Id = 12,
                    Name = "Glarus Jester",
                    CountryId = 1,
                    StyleId = 7,
                    BreweryId = 3,
                    Abv = double.Parse("4.2")
                },
                new Beer
                {
                    Id = 13,
                    Name = "Glarus Halo",
                    CountryId = 1,
                    StyleId = 7,
                    BreweryId = 3,
                    Abv = double.Parse("4.2")
                },
                new Beer
                {
                    Id = 14,
                    Name = "Glarus Premium Pale Ale",
                    CountryId = 1,
                    StyleId = 8,
                    BreweryId = 3,
                    Abv = double.Parse("4.2")
                },
                new Beer
                {
                    Id = 15,
                    Name = "Glarus Holy Night",
                    CountryId = 1,
                    StyleId = 4,
                    BreweryId = 3,
                    Abv = double.Parse("5.0")
                },
                new Beer
                {
                    Id = 16,
                    Name = "Bavik Super Pils",
                    CountryId = 2,
                    StyleId = 5,
                    BreweryId = 4,
                    Abv = double.Parse("5.2")
                },
                new Beer
                {
                    Id = 17,
                    Name = "Bavik Premium Export",
                    CountryId = 2,
                    StyleId = 1,
                    BreweryId = 4,
                    Abv = double.Parse("5.0")
                },
                new Beer
                {
                    Id = 18,
                    Name = "Bavik Pony-Stout",
                    CountryId = 2,
                    StyleId = 4,
                    BreweryId = 4,
                    Abv = double.Parse("5.2")
                },
                new Beer
                {
                    Id = 19,
                    Name = "Bavik Excel Premium Pils",
                    CountryId = 2,
                    StyleId = 5,
                    BreweryId = 4,
                    Abv = double.Parse("5.0")
                },
                new Beer
                {
                    Id = 20,
                    Name = "Snoek Blond",
                    CountryId = 2,
                    StyleId = 7,
                    BreweryId = 4,
                    Abv = double.Parse("7.5")
                },
                new Beer
                {
                    Id = 21,
                    Name = "De Dochter van de Korenaar Sans Pardon Pure-Oak",
                    CountryId = 2,
                    StyleId = 4,
                    BreweryId = 5,
                    Abv = double.Parse("11.0")
                },
                new Beer
                {
                    Id = 22,
                    Name = "De Dochter van de Korenaar La Renaissance Gold",
                    CountryId = 2,
                    StyleId = 6,
                    BreweryId = 5,
                    Abv = double.Parse("7.0")
                },
                new Beer
                {
                    Id = 23,
                    Name = "De Dochter van de Korenaar Crime Passionnel",
                    CountryId = 2,
                    StyleId = 6,
                    BreweryId = 5,
                    Abv = double.Parse("7.5")
                },
                new Beer
                {
                    Id = 24,
                    Name = "De Dochter van de Korenaar La Frontière",
                    CountryId = 2,
                    StyleId = 7,
                    BreweryId = 5,
                    Abv = double.Parse("5.2")
                },
                new Beer
                {
                    Id = 25,
                    Name = "De Dochter van de Korenaar / Hof Ten Dormaal Amitié",
                    CountryId = 2,
                    StyleId = 6,
                    BreweryId = 5,
                    Abv = double.Parse("7.0")
                },
                new Beer
                {
                    Id = 26,
                    Name = "Hopduvel Gentse Stouter",
                    CountryId = 2,
                    StyleId = 4,
                    BreweryId = 6,
                    Abv = double.Parse("11.0")
                },
                new Beer
                {
                    Id = 27,
                    Name = "Hopjutters Triple Hop",
                    CountryId = 2,
                    StyleId = 6,
                    BreweryId = 6,
                    Abv = double.Parse("7.3")
                },
                new Beer
                {
                    Id = 28,
                    Name = "Ne Stoute Loemelaer",
                    CountryId = 2,
                    StyleId = 4,
                    BreweryId = 6,
                    Abv = double.Parse("9.0")
                },
                new Beer
                {
                    Id = 29,
                    Name = "Stoute René",
                    CountryId = 2,
                    StyleId = 4,
                    BreweryId = 6,
                    Abv = double.Parse("7.0")
                },
                new Beer
                {
                    Id = 30,
                    Name = "Jean Nicot Dubbel Maduro",
                    CountryId = 2,
                    StyleId = 7,
                    BreweryId = 6,
                    Abv = double.Parse("7.2")
                },
                new Beer
                {
                    Id = 31,
                    Name = "Staropramen Černý / Dark / Temné",
                    CountryId = 3,
                    StyleId = 2,
                    BreweryId = 7,
                    Abv = double.Parse("4.4")
                },
                new Beer
                {
                    Id = 32,
                    Name = "Staropramen Granát / Garnet",
                    CountryId = 3,
                    StyleId = 3,
                    BreweryId = 7,
                    Abv = double.Parse("4.8")
                },
                new Beer
                {
                    Id = 33,
                    Name = "Braník Světlý",
                    CountryId = 3,
                    StyleId = 1,
                    BreweryId = 7,
                    Abv = double.Parse("4.1")
                },
                new Beer
                {
                    Id = 34,
                    Name = "Staropramen Ležák",
                    CountryId = 3,
                    StyleId = 5,
                    BreweryId = 7,
                    Abv = double.Parse("5.0")
                },
                new Beer
                {
                    Id = 35,
                    Name = "Staropramen Velvet",
                    CountryId = 3,
                    StyleId = 3,
                    BreweryId = 7,
                    Abv = double.Parse("5.3")
                },
                new Beer
                {
                    Id = 36,
                    Name = "Starobrno Drak Extra Chmelený Ležák",
                    CountryId = 3,
                    StyleId = 5,
                    BreweryId = 8,
                    Abv = double.Parse("5.1")
                },
                new Beer
                {
                    Id = 37,
                    Name = "Starobrno Baron Trenck",
                    CountryId = 3,
                    StyleId = 1,
                    BreweryId = 8,
                    Abv = double.Parse("6.0")
                },
                new Beer
                {
                    Id = 38,
                    Name = "Starobrno Premium Lager",
                    CountryId = 3,
                    StyleId = 5,
                    BreweryId = 8,
                    Abv = double.Parse("5.0")
                },
                new Beer
                {
                    Id = 39,
                    Name = "Hostan Hradni",
                    CountryId = 3,
                    StyleId = 5,
                    BreweryId = 8,
                    Abv = double.Parse("4.9")
                },
                new Beer
                {
                    Id = 40,
                    Name = "Hostan Naše Pivko",
                    CountryId = 3,
                    StyleId = 5,
                    BreweryId = 8,
                    Abv = double.Parse("4.0")
                },
                new Beer
                {
                    Id = 41,
                    Name = "Žatec Dark",
                    CountryId = 3,
                    StyleId = 2,
                    BreweryId = 9,
                    Abv = double.Parse("5.7")
                },
                new Beer
                {
                    Id = 42,
                    Name = "Žatec Celia Dark",
                    CountryId = 3,
                    StyleId = 2,
                    BreweryId = 9,
                    Abv = double.Parse("5.7")
                },
                new Beer
                {
                    Id = 43,
                    Name = "Žatec Blue Label",
                    CountryId = 3,
                    StyleId = 5,
                    BreweryId = 9,
                    Abv = double.Parse("4.6")
                },
                new Beer
                {
                    Id = 44,
                    Name = "Ava Zêr",
                    CountryId = 3,
                    StyleId = 1,
                    BreweryId = 9,
                    Abv = double.Parse("4.8")
                },
                new Beer
                {
                    Id = 45,
                    Name = "Žatec Premium",
                    CountryId = 3,
                    StyleId = 5,
                    BreweryId = 9,
                    Abv = double.Parse("4.8")
                },
                new Beer
                {
                    Id = 46,
                    Name = "Heineken H41",
                    CountryId = 4,
                    StyleId = 1,
                    BreweryId = 10,
                    Abv = double.Parse("5.3")
                },
                new Beer
                {
                    Id = 47,
                    Name = "Heineken Dark Lager",
                    CountryId = 4,
                    StyleId = 2,
                    BreweryId = 10,
                    Abv = double.Parse("5.2")
                },
                new Beer
                {
                    Id = 48,
                    Name = "Heineken",
                    CountryId = 4,
                    StyleId = 1,
                    BreweryId = 10,
                    Abv = double.Parse("5.0")
                },
                new Beer
                {
                    Id = 49,
                    Name = "Heineken Cold Filtered",
                    CountryId = 4,
                    StyleId = 1,
                    BreweryId = 10,
                    Abv = double.Parse("3.4")
                },
                new Beer
                {
                    Id = 50,
                    Name = "Heineken Premium Light",
                    CountryId = 4,
                    StyleId = 1,
                    BreweryId = 10,
                    Abv = double.Parse("3.3")
                },
                new Beer
                {
                    Id = 51,
                    Name = "De Natte Gijt Hellegijt (Maccallan Barrel Aged)",
                    CountryId = 4,
                    StyleId = 4,
                    BreweryId = 11,
                    Abv = double.Parse("12.0")
                },
                new Beer
                {
                    Id = 52,
                    Name = "De Natte Gijt Hellegijt (French Oak Barrel Aged)",
                    CountryId = 4,
                    StyleId = 4,
                    BreweryId = 11,
                    Abv = double.Parse("11.0")
                },
                new Beer
                {
                    Id = 53,
                    Name = "De Natte Gijt Hop Met de Gijt Speciale Editie",
                    CountryId = 4,
                    StyleId = 6,
                    BreweryId = 11,
                    Abv = double.Parse("6.5")
                },
                new Beer
                {
                    Id = 54,
                    Name = "De Natte Gijt Stoute Gijt",
                    CountryId = 4,
                    StyleId = 4,
                    BreweryId = 11,
                    Abv = double.Parse("8.8")
                },
                new Beer
                {
                    Id = 55,
                    Name = "De Natte Gijt Vredesgijt",
                    CountryId = 4,
                    StyleId = 6,
                    BreweryId = 11,
                    Abv = double.Parse("6.8")
                },
                new Beer
                {
                    Id = 56,
                    Name = "Eem Krachtig",
                    CountryId = 4,
                    StyleId = 4,
                    BreweryId = 12,
                    Abv = double.Parse("9.8")
                },
                new Beer
                {
                    Id = 57,
                    Name = "Eem Potig",
                    CountryId = 4,
                    StyleId = 4,
                    BreweryId = 12,
                    Abv = double.Parse("7.5")
                },
                new Beer
                {
                    Id = 58,
                    Name = "Eem Tierig",
                    CountryId = 4,
                    StyleId = 6,
                    BreweryId = 12,
                    Abv = double.Parse("6.5")
                },
                new Beer
                {
                    Id = 59,
                    Name = "Tasty Lady",
                    CountryId = 4,
                    StyleId = 6,
                    BreweryId = 12,
                    Abv = double.Parse("6.2")
                },
                new Beer
                {
                    Id = 60,
                    Name = "Orango Sweet Potato Beer",
                    CountryId = 4,
                    StyleId = 8,
                    BreweryId = 12,
                    Abv = double.Parse("6.5")
                },
                new Beer
                {
                    Id = 61,
                    Name = "Allgäuer Büble Bier Urbayrisch Dunkel",
                    CountryId = 5,
                    StyleId = 2,
                    BreweryId = 13,
                    Abv = double.Parse("5.3")
                },
                new Beer
                {
                    Id = 62,
                    Name = "Altenmünster Winterbier Dunkel",
                    CountryId = 5,
                    StyleId = 2,
                    BreweryId = 13,
                    Abv = double.Parse("5.5")
                },
                new Beer
                {
                    Id = 63,
                    Name = "Allgäuer Büble Bier Edelbräu",
                    CountryId = 5,
                    StyleId = 3,
                    BreweryId = 13,
                    Abv = double.Parse("5.3")
                },
                new Beer
                {
                    Id = 64,
                    Name = "Allgäuer Teutsch Pils",
                    CountryId = 5,
                    StyleId = 5,
                    BreweryId = 13,
                    Abv = double.Parse("4.8")
                },
                new Beer
                {
                    Id = 65,
                    Name = "Altenmünster Brauer Bier Urig Würzig",
                    CountryId = 5,
                    StyleId = 1,
                    BreweryId = 13,
                    Abv = double.Parse("4.9")
                },
                new Beer
                {
                    Id = 66,
                    Name = "Heidenpeters Pale Ale",
                    CountryId = 5,
                    StyleId = 7,
                    BreweryId = 14,
                    Abv = double.Parse("5.3")
                },
                new Beer
                {
                    Id = 67,
                    Name = "Heidenpeters Easy Citra Ale",
                    CountryId = 5,
                    StyleId = 7,
                    BreweryId = 14,
                    Abv = double.Parse("4.0")
                },
                new Beer
                {
                    Id = 68,
                    Name = "Heidenpeters Wild IPA",
                    CountryId = 5,
                    StyleId = 6,
                    BreweryId = 14,
                    Abv = double.Parse("7.5")
                },
                new Beer
                {
                    Id = 69,
                    Name = "Heidenpeters New England IPA",
                    CountryId = 5,
                    StyleId = 6,
                    BreweryId = 14,
                    Abv = double.Parse("7.0")
                },
                new Beer
                {
                    Id = 70,
                    Name = "Heidenpeters Stout",
                    CountryId = 5,
                    StyleId = 4,
                    BreweryId = 14,
                    Abv = double.Parse("6.1")
                },
                new Beer
                {
                    Id = 71,
                    Name = "Weyermann Imperial Stout",
                    CountryId = 5,
                    StyleId = 4,
                    BreweryId = 15,
                    Abv = double.Parse("8.0")
                },
                new Beer
                {
                    Id = 72,
                    Name = "Weyermann RyePA",
                    CountryId = 5,
                    StyleId = 7,
                    BreweryId = 15,
                    Abv = double.Parse("7.0")
                },
                new Beer
                {
                    Id = 73,
                    Name = "Weyermann Double Imperial Black IPA",
                    CountryId = 5,
                    StyleId = 6,
                    BreweryId = 15,
                    Abv = double.Parse("7.0")
                },
                new Beer
                {
                    Id = 74,
                    Name = "Weyermann Summer Ale",
                    CountryId = 5,
                    StyleId = 7,
                    BreweryId = 15,
                    Abv = double.Parse("5.3")
                },
                new Beer
                {
                    Id = 75,
                    Name = "Weyermann Coffee Stout",
                    CountryId = 5,
                    StyleId = 4,
                    BreweryId = 15,
                    Abv = double.Parse("4.9")
                });
        }
    }
}
