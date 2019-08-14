using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_UserGroup_UserGroupId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserGroup_UserGroupId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "UserGroupId",
                table: "Product",
                newName: "ProductGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_UserGroupId",
                table: "Product",
                newName: "IX_Product_ProductGroupId");

            migrationBuilder.AlterColumn<int>(
                name: "UserGroupId",
                table: "User",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "Sale",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "ProductGroup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroup", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Product_ProductGroup_ProductGroupId",
                table: "Product",
                column: "ProductGroupId",
                principalTable: "ProductGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserGroup_UserGroupId",
                table: "User",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_ProductGroup_ProductGroupId",
                table: "Product");

            migrationBuilder.DropForeignKey(
                name: "FK_User_UserGroup_UserGroupId",
                table: "User");

            migrationBuilder.DropTable(
                name: "ProductGroup");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Sale");

            migrationBuilder.RenameColumn(
                name: "ProductGroupId",
                table: "Product",
                newName: "UserGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Product_ProductGroupId",
                table: "Product",
                newName: "IX_Product_UserGroupId");

            migrationBuilder.AlterColumn<int>(
                name: "UserGroupId",
                table: "User",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Senha",
                table: "User",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Product_UserGroup_UserGroupId",
                table: "Product",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_UserGroup_UserGroupId",
                table: "User",
                column: "UserGroupId",
                principalTable: "UserGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
