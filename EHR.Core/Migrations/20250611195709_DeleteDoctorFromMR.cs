using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EHR.Core.Migrations
{
    /// <inheritdoc />
    public partial class DeleteDoctorFromMR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CachedDoctorId",
                table: "MedicalRecords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CachedDoctorId",
                table: "MedicalRecords",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
