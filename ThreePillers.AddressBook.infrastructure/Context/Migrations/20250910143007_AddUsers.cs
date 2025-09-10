using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThreePillers.AddressBook.infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressBookEntry",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HashedPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressBookEntry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressBookEntry_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddressBookEntry_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressBookEntry_DepartmentId",
                table: "AddressBookEntry",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressBookEntry_Email",
                table: "AddressBookEntry",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddressBookEntry_JobId",
                table: "AddressBookEntry",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_AddressBookEntry_Phone",
                table: "AddressBookEntry",
                column: "Phone",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressBookEntry");
        }
    }
}
