#nullable disable

namespace WPFDemoApp.Core.Migrations
{
	/// <inheritdoc />
	public partial class UpdatedModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasBeenCompleted",
                table: "ToDoItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenCompleted",
                table: "ToDoItems");
        }
    }
}
