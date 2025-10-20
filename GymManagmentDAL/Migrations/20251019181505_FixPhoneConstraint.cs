using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagmentDAL.Migrations
{
    /// <inheritdoc />
    public partial class FixPhoneConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "GymUserPhoneConstrain1",
                table: "Trainers");

            migrationBuilder.DropCheckConstraint(
                name: "GymUserPhoneConstrain",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Trainers");

            migrationBuilder.AddCheckConstraint(
                name: "GymUserPhoneConstrain1",
                table: "Trainers",
                sql: "Phone LIKE '01%' AND Phone NOT LIKE '%[^0-9]%'");

            migrationBuilder.AddCheckConstraint(
                name: "GymUserPhoneConstrain",
                table: "Members",
                sql: "Phone LIKE '01%' AND Phone NOT LIKE '%[^0-9]%'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "GymUserPhoneConstrain1",
                table: "Trainers");

            migrationBuilder.DropCheckConstraint(
                name: "GymUserPhoneConstrain",
                table: "Members");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Trainers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddCheckConstraint(
                name: "GymUserPhoneConstrain1",
                table: "Trainers",
                sql: "Phone Like '01' and Phone Not Like '%[^0-9]%'");

            migrationBuilder.AddCheckConstraint(
                name: "GymUserPhoneConstrain",
                table: "Members",
                sql: "Phone Like '01' and Phone Not Like '%[^0-9]%'");
        }
    }
}
