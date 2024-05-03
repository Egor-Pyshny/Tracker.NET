using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpApp.Migrations
{
    /// <inheritdoc />
    public partial class Add_m2m : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Diagnosis_Diagnosisid",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Diagnosisid",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Diagnosisid",
                table: "Patients");

            migrationBuilder.CreateTable(
                name: "DiagnosisPatients",
                columns: table => new
                {
                    diagnosesid = table.Column<int>(type: "int", nullable: false),
                    patientsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosisPatients", x => new { x.diagnosesid, x.patientsid });
                    table.ForeignKey(
                        name: "FK_DiagnosisPatients_Diagnosis_diagnosesid",
                        column: x => x.diagnosesid,
                        principalTable: "Diagnosis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiagnosisPatients_Patients_patientsid",
                        column: x => x.patientsid,
                        principalTable: "Patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosisPatients_patientsid",
                table: "DiagnosisPatients",
                column: "patientsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiagnosisPatients");

            migrationBuilder.AddColumn<int>(
                name: "Diagnosisid",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Diagnosisid",
                table: "Patients",
                column: "Diagnosisid");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Diagnosis_Diagnosisid",
                table: "Patients",
                column: "Diagnosisid",
                principalTable: "Diagnosis",
                principalColumn: "id");
        }
    }
}
