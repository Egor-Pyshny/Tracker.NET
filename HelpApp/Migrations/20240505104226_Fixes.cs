using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpApp.Migrations
{
    /// <inheritdoc />
    public partial class Fixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Passport_passportid",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "patientid",
                table: "Surgeries_graphic",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "passportid",
                table: "Patients",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "Passport",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Surgeries_graphic_patientid",
                table: "Surgeries_graphic",
                column: "patientid");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Passport_passportid",
                table: "Patients",
                column: "passportid",
                principalTable: "Passport",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Surgeries_graphic_Patients_patientid",
                table: "Surgeries_graphic",
                column: "patientid",
                principalTable: "Patients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Passport_passportid",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Surgeries_graphic_Patients_patientid",
                table: "Surgeries_graphic");

            migrationBuilder.DropIndex(
                name: "IX_Surgeries_graphic_patientid",
                table: "Surgeries_graphic");

            migrationBuilder.DropColumn(
                name: "patientid",
                table: "Surgeries_graphic");

            migrationBuilder.AlterColumn<int>(
                name: "passportid",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "Passport",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Passport_passportid",
                table: "Patients",
                column: "passportid",
                principalTable: "Passport",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
