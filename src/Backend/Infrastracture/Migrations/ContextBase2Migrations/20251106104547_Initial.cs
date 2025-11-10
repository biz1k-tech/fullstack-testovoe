#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations.ContextBase2Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "ImagesCopies",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                SourceId = table.Column<Guid>(type: "uuid", nullable: false),
                SourceDescription = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Blob = table.Column<byte[]>(type: "bytea", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_ImagesCopies", x => x.Id); });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ImagesCopies");
    }
}