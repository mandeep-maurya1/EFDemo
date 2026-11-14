using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFDemo2.Migrations
{
    /// <inheritdoc />
    public partial class FeeIdInttoString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fee_Student_StudentStuId",
                table: "Fee");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Student_StuId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Course_CourseId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Photo_StuId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Fee_StudentStuId",
                table: "Fee");

            migrationBuilder.DropColumn(
                name: "StudentStuId",
                table: "Fee");

            migrationBuilder.RenameColumn(
                name: "CoourseName",
                table: "Course",
                newName: "CourseName");

            migrationBuilder.AlterColumn<string>(
                name: "CourseId",
                table: "Student",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "StuId",
                table: "Photo",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoPath",
                table: "Photo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "StuId",
                table: "Fee",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FeeId",
                table: "Fee",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_StuId",
                table: "Photo",
                column: "StuId",
                unique: true,
                filter: "[StuId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Fee_StuId",
                table: "Fee",
                column: "StuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fee_Student_StuId",
                table: "Fee",
                column: "StuId",
                principalTable: "Student",
                principalColumn: "StuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Student_StuId",
                table: "Photo",
                column: "StuId",
                principalTable: "Student",
                principalColumn: "StuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Course_CourseId",
                table: "Student",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fee_Student_StuId",
                table: "Fee");

            migrationBuilder.DropForeignKey(
                name: "FK_Photo_Student_StuId",
                table: "Photo");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Course_CourseId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Photo_StuId",
                table: "Photo");

            migrationBuilder.DropIndex(
                name: "IX_Fee_StuId",
                table: "Fee");

            migrationBuilder.RenameColumn(
                name: "CourseName",
                table: "Course",
                newName: "CoourseName");

            migrationBuilder.AlterColumn<string>(
                name: "CourseId",
                table: "Student",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StuId",
                table: "Photo",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhotoPath",
                table: "Photo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StuId",
                table: "Fee",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FeeId",
                table: "Fee",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "StudentStuId",
                table: "Fee",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photo_StuId",
                table: "Photo",
                column: "StuId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fee_StudentStuId",
                table: "Fee",
                column: "StudentStuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fee_Student_StudentStuId",
                table: "Fee",
                column: "StudentStuId",
                principalTable: "Student",
                principalColumn: "StuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_Student_StuId",
                table: "Photo",
                column: "StuId",
                principalTable: "Student",
                principalColumn: "StuId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Course_CourseId",
                table: "Student",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
