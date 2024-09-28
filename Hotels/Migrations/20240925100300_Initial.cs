using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HW_Hotels.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Hotel");

            migrationBuilder.CreateTable(
                name: "Bookings",
                schema: "Hotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    BookerName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                schema: "Hotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                schema: "Hotel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalSchema: "Hotel",
                        principalTable: "Hotels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
         schema: "Hotel",
         table: "Hotels",
         columns: new[] { "Id", "Name", "Adress", "Stars" },
         values: new object[,]
         {
                  {1, "Alcor", "Some adress here 1", 4 },
                  {2, "Hotel2", "Some adress here 2",5 },
         }
       );

               migrationBuilder.InsertData(
         schema: "Hotel",
         table: "Rooms",
         columns: new[] { "Id", "Type", "Price", "HotelId" },
         values: new object[,]
         {
                  {1, "Small", 120, 1 },
                  {2, "Big", 140,1 },
                  {3, "Small", 140, 2 },
                  {4, "Big", 160,2 },
         }
       );
           

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelId",
                schema: "Hotel",
                table: "Rooms",
                column: "HotelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings",
                schema: "Hotel");

            migrationBuilder.DropTable(
                name: "Rooms",
                schema: "Hotel");

            migrationBuilder.DropTable(
                name: "Hotels",
                schema: "Hotel");
        }
    }
}
