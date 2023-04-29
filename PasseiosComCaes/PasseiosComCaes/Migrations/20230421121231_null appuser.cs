using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasseiosComCaes.Migrations
{
    public partial class nullappuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Enderecos_EnderecoId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "EnderecoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Enderecos_EnderecoId",
                table: "AspNetUsers",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Enderecos_EnderecoId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "EnderecoId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Enderecos_EnderecoId",
                table: "AspNetUsers",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
