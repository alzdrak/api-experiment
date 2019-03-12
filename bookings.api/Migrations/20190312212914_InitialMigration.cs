using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bookings.api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flight",
                columns: table => new
                {
                    FlightId = table.Column<Guid>(nullable: false),
                    AircraftIcao24 = table.Column<string>(maxLength: 20, nullable: false),
                    Callsign = table.Column<string>(maxLength: 20, nullable: false),
                    FlightNumber = table.Column<string>(maxLength: 20, nullable: false),
                    DepartureAirport = table.Column<string>(maxLength: 255, nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "Date", nullable: false),
                    DepartureTime = table.Column<TimeSpan>(type: "Time", nullable: false),
                    DestinationAirport = table.Column<string>(maxLength: 255, nullable: false),
                    DestinationDate = table.Column<DateTime>(type: "Date", nullable: false),
                    DestinationTime = table.Column<TimeSpan>(type: "Time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flight", x => x.FlightId);
                });

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    PassengerId = table.Column<Guid>(nullable: false),
                    Firstname = table.Column<string>(maxLength: 150, nullable: false),
                    Lastname = table.Column<string>(maxLength: 150, nullable: false),
                    Age = table.Column<int>(maxLength: 150, nullable: false),
                    Email = table.Column<string>(maxLength: 1024, nullable: false),
                    Mobile = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.PassengerId);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingId = table.Column<Guid>(nullable: false),
                    FlightId = table.Column<Guid>(nullable: false),
                    PassengerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Booking_Flight_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flight",
                        principalColumn: "FlightId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_Passenger_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passenger",
                        principalColumn: "PassengerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Flight",
                columns: new[] { "FlightId", "AircraftIcao24", "Callsign", "DepartureAirport", "DepartureDate", "DepartureTime", "DestinationAirport", "DestinationDate", "DestinationTime", "FlightNumber" },
                values: new object[,]
                {
                    { new Guid("27340d4a-4e2a-40aa-9397-4846f113ff0a"), "406f74", "BAW39", "London Heathrow International Airport", new DateTime(2019, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 16, 25, 0, 0), "Los Angeles International Airport", new DateTime(2019, 3, 11, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 10, 25, 0, 0), "BA39" },
                    { new Guid("d53eee6f-e184-45c7-aa14-e2f1bba458f7"), "4ca4e6", "RYR61LP", "Lisbon International Airport", new DateTime(2019, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 18, 55, 0, 0), "Rome - Ciampino International Airport", new DateTime(2019, 2, 17, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 22, 55, 0, 0), "FR2693" },
                    { new Guid("79e640fb-9f34-4a68-93c7-ed331760c88b"), "00b205", "SAA041", "Victoria Falls Airport", new DateTime(2019, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 13, 25, 0, 0), "Johannesburg OR Tambo International Airport", new DateTime(2019, 1, 29, 0, 0, 0, 0, DateTimeKind.Utc), new TimeSpan(0, 15, 5, 0, 0), "SA41" }
                });

            migrationBuilder.InsertData(
                table: "Passenger",
                columns: new[] { "PassengerId", "Age", "Email", "Firstname", "Lastname", "Mobile" },
                values: new object[,]
                {
                    { new Guid("79a20e84-9884-435e-9d9b-d289f86ba417"), 44, "jeffrey.torres64@example.com", "Jeffrey", "Torres", "(928)-196-2012" },
                    { new Guid("1a932098-0370-4daa-afb4-dc9efbf8489a"), 38, "sergio.fowler60@example.com", "Sergio", "Fowler", "(718)-504-4291" },
                    { new Guid("6c01b522-8ccc-4d3a-ba80-bdecba3ccf11"), 30, "v.fisher88@example.com", "Victoria", "Fisher", "(322)-572-2823" },
                    { new Guid("d40b35dd-09d5-4409-8c6e-b7e7efd3be6d"), 41, "doris.watkins59@example.com", "Doris", "Watkins", "(559)-767-4133" }
                });

            migrationBuilder.InsertData(
                table: "Booking",
                columns: new[] { "BookingId", "FlightId", "PassengerId" },
                values: new object[,]
                {
                    { new Guid("1b9e4b3a-998e-4455-a140-8e08d2aec327"), new Guid("27340d4a-4e2a-40aa-9397-4846f113ff0a"), new Guid("79a20e84-9884-435e-9d9b-d289f86ba417") },
                    { new Guid("cb407a2d-80df-41a7-882b-6466e66fc0f9"), new Guid("27340d4a-4e2a-40aa-9397-4846f113ff0a"), new Guid("1a932098-0370-4daa-afb4-dc9efbf8489a") },
                    { new Guid("0eb96b73-7e26-4142-a45c-212fc1f4f34a"), new Guid("d53eee6f-e184-45c7-aa14-e2f1bba458f7"), new Guid("1a932098-0370-4daa-afb4-dc9efbf8489a") },
                    { new Guid("0c5a45b3-2f08-4ef9-8c07-ce52bcdb1519"), new Guid("79e640fb-9f34-4a68-93c7-ed331760c88b"), new Guid("6c01b522-8ccc-4d3a-ba80-bdecba3ccf11") },
                    { new Guid("82966a83-94ee-4c5a-be2c-f78e35f91b90"), new Guid("79e640fb-9f34-4a68-93c7-ed331760c88b"), new Guid("d40b35dd-09d5-4409-8c6e-b7e7efd3be6d") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_FlightId",
                table: "Booking",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_PassengerId",
                table: "Booking",
                column: "PassengerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Flight");

            migrationBuilder.DropTable(
                name: "Passenger");
        }
    }
}
