using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgenda.Infraestrutura.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_TBCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaDespesa_Categorias_CategoriaId",
                table: "CategoriaDespesa");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriaDespesa_Despesas_DespesaId",
                table: "CategoriaDespesa");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriaDespesa",
                table: "CategoriaDespesa");

            migrationBuilder.DropColumn(
                name: "categorias",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "categoriasTitulo",
                table: "Despesas");

            migrationBuilder.DropColumn(
                name: "idDespesas",
                table: "Categorias");

            migrationBuilder.RenameTable(
                name: "CategoriaDespesa",
                newName: "CategoriasDespesas");

            migrationBuilder.RenameColumn(
                name: "DespesaId",
                table: "CategoriasDespesas",
                newName: "despesasId");

            migrationBuilder.RenameColumn(
                name: "CategoriaId",
                table: "CategoriasDespesas",
                newName: "CategoriasId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriaDespesa_DespesaId",
                table: "CategoriasDespesas",
                newName: "IX_CategoriasDespesas_despesasId");

            migrationBuilder.AlterColumn<string>(
                name: "formaDoPagamento",
                table: "Despesas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "Despesas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriasDespesas",
                table: "CategoriasDespesas",
                columns: new[] { "CategoriasId", "despesasId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriasDespesas_Categorias_CategoriasId",
                table: "CategoriasDespesas",
                column: "CategoriasId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriasDespesas_Despesas_despesasId",
                table: "CategoriasDespesas",
                column: "despesasId",
                principalTable: "Despesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoriasDespesas_Categorias_CategoriasId",
                table: "CategoriasDespesas");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriasDespesas_Despesas_despesasId",
                table: "CategoriasDespesas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoriasDespesas",
                table: "CategoriasDespesas");

            migrationBuilder.RenameTable(
                name: "CategoriasDespesas",
                newName: "CategoriaDespesa");

            migrationBuilder.RenameColumn(
                name: "despesasId",
                table: "CategoriaDespesa",
                newName: "DespesaId");

            migrationBuilder.RenameColumn(
                name: "CategoriasId",
                table: "CategoriaDespesa",
                newName: "CategoriaId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoriasDespesas_despesasId",
                table: "CategoriaDespesa",
                newName: "IX_CategoriaDespesa_DespesaId");

            migrationBuilder.AlterColumn<string>(
                name: "formaDoPagamento",
                table: "Despesas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "Despesas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "categorias",
                table: "Despesas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "categoriasTitulo",
                table: "Despesas",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "idDespesas",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoriaDespesa",
                table: "CategoriaDespesa",
                columns: new[] { "CategoriaId", "DespesaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaDespesa_Categorias_CategoriaId",
                table: "CategoriaDespesa",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriaDespesa_Despesas_DespesaId",
                table: "CategoriaDespesa",
                column: "DespesaId",
                principalTable: "Despesas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
