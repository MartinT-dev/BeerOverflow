using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BeerOverflow.Data.Migrations
{
    public partial class AddingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BreweryId", "CountryId", "Name", "StyleId" },
                values: new object[] { 1, 1, "Zagorka Retro", 1 });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Abv", "Name" },
                values: new object[] { 5.0, "Zagorka Spetsialno" });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Abv", "BreweryId", "CountryId", "Name" },
                values: new object[] { 5.0, 1, 1, "Amstel Dark" });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Abv", "CountryId", "Name", "StyleId" },
                values: new object[] { 4.7999999999999998, 1, "Kamenitza Staro Pivo", 5 });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "Abv", "BreweryId", "CountryId", "CreatedOn", "DeletedOn", "ModifiedOn", "Name", "StyleId", "isDeleted" },
                values: new object[,]
                {
                    { 7, 4.4000000000000004, 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Kamenitza 1881", 1, false },
                    { 10, 4.0999999999999996, 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Pleven Svetlo Pivo", 1, false },
                    { 9, 4.0999999999999996, 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Slavena svetlo", 1, false },
                    { 8, 6.0, 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Kamenitza Tamno", 2, false }
                });

            migrationBuilder.UpdateData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Zagorka");

            migrationBuilder.UpdateData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 1, "Kamenitza" });

            migrationBuilder.UpdateData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 1, "Glarus" });

            migrationBuilder.InsertData(
                table: "Breweries",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "ModifiedOn", "Name", "isDeleted" },
                values: new object[,]
                {
                    { 4, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "De Brabandere", false },
                    { 5, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "De Dochter van de Korenaar", false },
                    { 9, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Zatecky Pivovar", false },
                    { 8, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Starobrno", false },
                    { 7, 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Staropramen", false },
                    { 6, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "De Graal", false }
                });

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Bulgaria");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Belgium");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Czech Republic");

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedOn", "DeletedOn", "ModifiedOn", "Name", "isDeleted" },
                values: new object[,]
                {
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Germany", false },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "The Netherlands", false }
                });

            migrationBuilder.UpdateData(
                table: "Styles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Pale Lager");

            migrationBuilder.UpdateData(
                table: "Styles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Dark Lager");

            migrationBuilder.UpdateData(
                table: "Styles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Amber Lager");

            migrationBuilder.UpdateData(
                table: "Styles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Stout");

            migrationBuilder.UpdateData(
                table: "Styles",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Pilsner");

            migrationBuilder.InsertData(
                table: "Styles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 7, "Pale Ale" },
                    { 6, "IPA" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Country", "Username" },
                values: new object[] { "Bulgaria", "TestUser1" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Country", "Username" },
                values: new object[] { "England", "TestUser2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Country", "Username" },
                values: new object[] { "Germany", "TestUser3" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Country", "Username" },
                values: new object[] { "Spain", "TestUser4" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Country", "Username" },
                values: new object[] { "USA", "TestUser5" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Country", "CreatedOn", "DeletedOn", "ModifiedOn", "Username", "isDeleted" },
                values: new object[,]
                {
                    { 10, "England", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "TestUser10", false },
                    { 9, "Bulgaria", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "TestUser9", false },
                    { 8, "Bulgaria", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "TestUser8", false },
                    { 7, "USA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "TestUser7", false },
                    { 6, "Bulgaria", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "TestUser6", false }
                });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Abv", "Name", "StyleId" },
                values: new object[] { 5.5, "Stolichno Pale Ale", 7 });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Abv", "BreweryId", "CountryId", "Name", "StyleId" },
                values: new object[] { 4.7999999999999998, 1, 1, "Zagorka IPA", 6 });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "Abv", "BreweryId", "CountryId", "CreatedOn", "DeletedOn", "ModifiedOn", "Name", "StyleId", "isDeleted" },
                values: new object[,]
                {
                    { 11, 6.0, 3, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Glarus Black Sea IPA", 6, false },
                    { 12, 4.2000000000000002, 3, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Glarus Jester", 7, false },
                    { 13, 4.2000000000000002, 3, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Glarus Halo", 7, false }
                });

            migrationBuilder.InsertData(
                table: "Breweries",
                columns: new[] { "Id", "CountryId", "CreatedOn", "DeletedOn", "ModifiedOn", "Name", "isDeleted" },
                values: new object[,]
                {
                    { 10, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Heineken", false },
                    { 11, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "De Natte Gijt", false },
                    { 12, 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "De Vergeten Appel", false },
                    { 13, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Allgauer Brauhaus", false },
                    { 14, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Heidenpeters", false },
                    { 15, 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Weyermann Versuchsbrauerei", false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Styles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Styles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 1,
                column: "StyleId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 2,
                column: "StyleId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Abv", "Name", "StyleId" },
                values: new object[] { 3.5, "Thin Steel", 1 });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Abv", "BreweryId", "CountryId", "Name", "StyleId" },
                values: new object[] { 7.5, 2, 2, "Heady Rake", 4 });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BreweryId", "CountryId", "Name", "StyleId" },
                values: new object[] { 3, 3, "Viral Hand", 5 });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Abv", "Name" },
                values: new object[] { 5.5, "Eleven Mist" });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Abv", "BreweryId", "CountryId", "Name" },
                values: new object[] { 9.8000000000000007, 2, 2, "Burnt Wolf" });

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Abv", "CountryId", "Name", "StyleId" },
                values: new object[] { 2.5, 3, "Khaki Lava", 3 });

            migrationBuilder.UpdateData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "The Weald Guardian");

            migrationBuilder.UpdateData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 2, "The Fiery Vision Bull" });

            migrationBuilder.UpdateData(
                table: "Breweries",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CountryId", "Name" },
                values: new object[] { 3, "Qadir" });

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Lordaeron");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Draenor");

            migrationBuilder.UpdateData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Silvermoon");

            migrationBuilder.UpdateData(
                table: "Styles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Light");

            migrationBuilder.UpdateData(
                table: "Styles",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Frost");

            migrationBuilder.UpdateData(
                table: "Styles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Shadow");

            migrationBuilder.UpdateData(
                table: "Styles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Blood");

            migrationBuilder.UpdateData(
                table: "Styles",
                keyColumn: "Id",
                keyValue: 5,
                column: "Name",
                value: "Poison");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Country", "Username" },
                values: new object[] { "Draenor", "Garosh" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Country", "Username" },
                values: new object[] { "Lordaeron", "Arthas" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Country", "Username" },
                values: new object[] { "Lordaeron", "Bolvar" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Country", "Username" },
                values: new object[] { "Lordaeron", "Jaina" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Country", "Username" },
                values: new object[] { "Lordaeron", "Varian" });
        }
    }
}
