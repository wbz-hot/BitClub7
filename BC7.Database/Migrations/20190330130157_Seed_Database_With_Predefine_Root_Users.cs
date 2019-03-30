﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BC7.Database.Migrations
{
    public partial class Seed_Database_With_Predefine_Root_Users : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserMultiAccounts",
                keyColumn: "Id",
                keyValue: new Guid("07e317c6-d25b-4a04-b734-0849eb8dd0d2"));

            migrationBuilder.DeleteData(
                table: "UserMultiAccounts",
                keyColumn: "Id",
                keyValue: new Guid("361ae396-fdc8-4a02-b578-df974e43e5d1"));

            migrationBuilder.DeleteData(
                table: "UserMultiAccounts",
                keyColumn: "Id",
                keyValue: new Guid("a66a3b65-440e-431c-ab6f-e2324d453e47"));

            migrationBuilder.DeleteData(
                table: "UserAccountsData",
                keyColumn: "Id",
                keyValue: new Guid("42735ea4-f04c-4560-aeca-1adbfb078e45"));

            migrationBuilder.DeleteData(
                table: "UserAccountsData",
                keyColumn: "Id",
                keyValue: new Guid("7773dd2f-a2c6-4eb7-a82a-8b1a620c56d7"));

            migrationBuilder.DeleteData(
                table: "UserAccountsData",
                keyColumn: "Id",
                keyValue: new Guid("eba72c0c-f6a9-4f60-9c41-1212ff2ec0b8"));

            migrationBuilder.InsertData(
                table: "UserAccountsData",
                columns: new[] { "Id", "BtcWalletAddress", "City", "Country", "CreatedAt", "Email", "FirstName", "Hash", "LastName", "Login", "Role", "Salt", "Street", "ZipCode" },
                values: new object[] { new Guid("b8f28295-923c-4616-aa46-81393ad7bdd2"), "BtcWalletAddressRoot1", "CityRoot1", "CountryRoot1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EmailRoot1", "FirstNameRoot1", "", "LastNameRoot1", "LoginRoot1", "Root", "", "StreetRoot1", "ZipCodeRoot1" });

            migrationBuilder.InsertData(
                table: "UserAccountsData",
                columns: new[] { "Id", "BtcWalletAddress", "City", "Country", "CreatedAt", "Email", "FirstName", "Hash", "LastName", "Login", "Role", "Salt", "Street", "ZipCode" },
                values: new object[] { new Guid("e71c10a5-bcb4-4ca8-9ab1-e60467baf44f"), "BtcWalletAddressRoot2", "CityRoot2", "CountryRoot2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EmailRoot2", "FirstNameRoot2", "", "LastNameRoot2", "LoginRoot2", "Root", "", "StreetRoot2", "ZipCodeRoot2" });

            migrationBuilder.InsertData(
                table: "UserAccountsData",
                columns: new[] { "Id", "BtcWalletAddress", "City", "Country", "CreatedAt", "Email", "FirstName", "Hash", "LastName", "Login", "Role", "Salt", "Street", "ZipCode" },
                values: new object[] { new Guid("2637699b-902d-4c13-a32b-0eb85ecdd25b"), "BtcWalletAddressRoot3", "CityRoot3", "CountryRoot3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EmailRoot3", "FirstNameRoot3", "", "LastNameRoot3", "LoginRoot3", "Root", "", "StreetRoot3", "ZipCodeRoot3" });

            migrationBuilder.InsertData(
                table: "UserMultiAccounts",
                columns: new[] { "Id", "CreatedAt", "MultiAccountName", "RefLink", "UserAccountDataId", "UserMultiAccountInvitingId" },
                values: new object[] { new Guid("18567dcb-042a-4e7a-b34b-539c12b04aa3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LoginRoot1", "111111", new Guid("b8f28295-923c-4616-aa46-81393ad7bdd2"), null });

            migrationBuilder.InsertData(
                table: "UserMultiAccounts",
                columns: new[] { "Id", "CreatedAt", "MultiAccountName", "RefLink", "UserAccountDataId", "UserMultiAccountInvitingId" },
                values: new object[] { new Guid("183bbc62-a871-488f-883d-970c09fb46ab"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LoginRoot2", "222222", new Guid("e71c10a5-bcb4-4ca8-9ab1-e60467baf44f"), null });

            migrationBuilder.InsertData(
                table: "UserMultiAccounts",
                columns: new[] { "Id", "CreatedAt", "MultiAccountName", "RefLink", "UserAccountDataId", "UserMultiAccountInvitingId" },
                values: new object[] { new Guid("1dd37daf-cb83-44a4-8c2c-2dd85d519b77"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LoginRoot3", "333333", new Guid("2637699b-902d-4c13-a32b-0eb85ecdd25b"), null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserMultiAccounts",
                keyColumn: "Id",
                keyValue: new Guid("183bbc62-a871-488f-883d-970c09fb46ab"));

            migrationBuilder.DeleteData(
                table: "UserMultiAccounts",
                keyColumn: "Id",
                keyValue: new Guid("18567dcb-042a-4e7a-b34b-539c12b04aa3"));

            migrationBuilder.DeleteData(
                table: "UserMultiAccounts",
                keyColumn: "Id",
                keyValue: new Guid("1dd37daf-cb83-44a4-8c2c-2dd85d519b77"));

            migrationBuilder.DeleteData(
                table: "UserAccountsData",
                keyColumn: "Id",
                keyValue: new Guid("2637699b-902d-4c13-a32b-0eb85ecdd25b"));

            migrationBuilder.DeleteData(
                table: "UserAccountsData",
                keyColumn: "Id",
                keyValue: new Guid("b8f28295-923c-4616-aa46-81393ad7bdd2"));

            migrationBuilder.DeleteData(
                table: "UserAccountsData",
                keyColumn: "Id",
                keyValue: new Guid("e71c10a5-bcb4-4ca8-9ab1-e60467baf44f"));

            migrationBuilder.InsertData(
                table: "UserAccountsData",
                columns: new[] { "Id", "BtcWalletAddress", "City", "Country", "CreatedAt", "Email", "FirstName", "Hash", "LastName", "Login", "Role", "Salt", "Street", "ZipCode" },
                values: new object[] { new Guid("42735ea4-f04c-4560-aeca-1adbfb078e45"), "BtcWalletAddressRoot1", "CityRoot1", "CountryRoot1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EmailRoot1", "FirstNameRoot1", "", "LastNameRoot1", "LoginRoot1", "Root", "", "StreetRoot1", "ZipCodeRoot1" });

            migrationBuilder.InsertData(
                table: "UserAccountsData",
                columns: new[] { "Id", "BtcWalletAddress", "City", "Country", "CreatedAt", "Email", "FirstName", "Hash", "LastName", "Login", "Role", "Salt", "Street", "ZipCode" },
                values: new object[] { new Guid("7773dd2f-a2c6-4eb7-a82a-8b1a620c56d7"), "BtcWalletAddressRoot2", "CityRoot2", "CountryRoot2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EmailRoot2", "FirstNameRoot2", "", "LastNameRoot2", "LoginRoot2", "Root", "", "StreetRoot2", "ZipCodeRoot2" });

            migrationBuilder.InsertData(
                table: "UserAccountsData",
                columns: new[] { "Id", "BtcWalletAddress", "City", "Country", "CreatedAt", "Email", "FirstName", "Hash", "LastName", "Login", "Role", "Salt", "Street", "ZipCode" },
                values: new object[] { new Guid("eba72c0c-f6a9-4f60-9c41-1212ff2ec0b8"), "BtcWalletAddressRoot3", "CityRoot3", "CountryRoot3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "EmailRoot3", "FirstNameRoot3", "", "LastNameRoot3", "LoginRoot3", "Root", "", "StreetRoot3", "ZipCodeRoot3" });

            migrationBuilder.InsertData(
                table: "UserMultiAccounts",
                columns: new[] { "Id", "CreatedAt", "MultiAccountName", "RefLink", "UserAccountDataId", "UserMultiAccountInvitingId" },
                values: new object[] { new Guid("07e317c6-d25b-4a04-b734-0849eb8dd0d2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LoginRoot1", null, new Guid("42735ea4-f04c-4560-aeca-1adbfb078e45"), null });

            migrationBuilder.InsertData(
                table: "UserMultiAccounts",
                columns: new[] { "Id", "CreatedAt", "MultiAccountName", "RefLink", "UserAccountDataId", "UserMultiAccountInvitingId" },
                values: new object[] { new Guid("361ae396-fdc8-4a02-b578-df974e43e5d1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LoginRoot2", null, new Guid("7773dd2f-a2c6-4eb7-a82a-8b1a620c56d7"), null });

            migrationBuilder.InsertData(
                table: "UserMultiAccounts",
                columns: new[] { "Id", "CreatedAt", "MultiAccountName", "RefLink", "UserAccountDataId", "UserMultiAccountInvitingId" },
                values: new object[] { new Guid("a66a3b65-440e-431c-ab6f-e2324d453e47"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LoginRoot3", null, new Guid("eba72c0c-f6a9-4f60-9c41-1212ff2ec0b8"), null });
        }
    }
}
