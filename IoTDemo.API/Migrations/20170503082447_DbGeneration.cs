using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IoTDemo.API.Migrations
{
    public partial class DbGeneration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameId",
                table: "IoTData",
                newName: "IoTDataNameId");

            migrationBuilder.CreateTable(
                name: "IoTKeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    Key = table.Column<Guid>(nullable: false),
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IoTKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IoTDataNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IoTKeyId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IoTDataNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IoTDataNames_IoTKeys_IoTKeyId",
                        column: x => x.IoTKeyId,
                        principalTable: "IoTKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IoTData_IoTDataNameId",
                table: "IoTData",
                column: "IoTDataNameId");

            migrationBuilder.CreateIndex(
                name: "IX_IoTDataNames_IoTKeyId",
                table: "IoTDataNames",
                column: "IoTKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_IoTData_IoTDataNames_IoTDataNameId",
                table: "IoTData",
                column: "IoTDataNameId",
                principalTable: "IoTDataNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IoTData_IoTDataNames_IoTDataNameId",
                table: "IoTData");

            migrationBuilder.DropTable(
                name: "IoTDataNames");

            migrationBuilder.DropTable(
                name: "IoTKeys");

            migrationBuilder.DropIndex(
                name: "IX_IoTData_IoTDataNameId",
                table: "IoTData");

            migrationBuilder.RenameColumn(
                name: "IoTDataNameId",
                table: "IoTData",
                newName: "NameId");
        }
    }
}
