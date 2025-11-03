using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KanbanBoard.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Columns_ColumnId",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "TaskItems");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ColumnId",
                table: "TaskItems",
                newName: "IX_TaskItems_ColumnId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TaskItems",
                table: "TaskItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_Columns_ColumnId",
                table: "TaskItems",
                column: "ColumnId",
                principalTable: "Columns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_Columns_ColumnId",
                table: "TaskItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TaskItems",
                table: "TaskItems");

            migrationBuilder.RenameTable(
                name: "TaskItems",
                newName: "Items");

            migrationBuilder.RenameIndex(
                name: "IX_TaskItems_ColumnId",
                table: "Items",
                newName: "IX_Items_ColumnId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Columns_ColumnId",
                table: "Items",
                column: "ColumnId",
                principalTable: "Columns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
