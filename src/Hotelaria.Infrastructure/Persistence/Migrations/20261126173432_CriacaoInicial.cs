using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotelaria.Infrastructure.Persistence.Migrations;

/// <inheritdoc />
public partial class _20261126173432_CriacaoInicial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Hospedes",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Nome = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                Cpf = table.Column<string>(type: "TEXT", maxLength: 11, nullable: false),
                Email = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                Telefone = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Hospedes", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Quartos",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Numero = table.Column<int>(type: "INTEGER", nullable: false),
                Tipo = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                Capacidade = table.Column<int>(type: "INTEGER", nullable: false),
                ValorDiaria = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                Ativo = table.Column<bool>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Quartos", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Reservas",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                HospedeId = table.Column<Guid>(type: "TEXT", nullable: false),
                QuartoId = table.Column<Guid>(type: "TEXT", nullable: false),
                DataEntrada = table.Column<DateOnly>(type: "TEXT", nullable: false),
                DataSaida = table.Column<DateOnly>(type: "TEXT", nullable: false),
                QuantidadeHospedes = table.Column<int>(type: "INTEGER", nullable: false),
                ValorTotal = table.Column<decimal>(type: "TEXT", precision: 10, scale: 2, nullable: false),
                Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reservas", x => x.Id);
                table.ForeignKey(
                    name: "FK_Reservas_Hospedes_HospedeId",
                    column: x => x.HospedeId,
                    principalTable: "Hospedes",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Reservas_Quartos_QuartoId",
                    column: x => x.QuartoId,
                    principalTable: "Quartos",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Hospedes_Cpf",
            table: "Hospedes",
            column: "Cpf",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Quartos_Numero",
            table: "Quartos",
            column: "Numero",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Reservas_HospedeId",
            table: "Reservas",
            column: "HospedeId");

        migrationBuilder.CreateIndex(
            name: "IX_Reservas_QuartoId",
            table: "Reservas",
            column: "QuartoId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Reservas");

        migrationBuilder.DropTable(
            name: "Hospedes");

        migrationBuilder.DropTable(
            name: "Quartos");
    }
}
