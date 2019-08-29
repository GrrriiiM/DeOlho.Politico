using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeOlho.Politico.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cargo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    AbrangenciaId = table.Column<long>(nullable: false),
                    Abrangencia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrauInstrucao",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrauInstrucao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ocupacao",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ocupacao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Partido",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Sigla = table.Column<string>(nullable: true),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partido", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sexo",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoEleicao",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEleicao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Politico",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CPF = table.Column<long>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Apelido = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    CidadeNascimento = table.Column<string>(nullable: true),
                    UFNascimento = table.Column<string>(nullable: true),
                    SexoId = table.Column<long>(nullable: false),
                    GrauInstrucaoId = table.Column<long>(nullable: false),
                    OcupacaoId = table.Column<long>(nullable: false),
                    DataInformacao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Politico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Politico_GrauInstrucao_GrauInstrucaoId",
                        column: x => x.GrauInstrucaoId,
                        principalTable: "GrauInstrucao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Politico_Ocupacao_OcupacaoId",
                        column: x => x.OcupacaoId,
                        principalTable: "Ocupacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Politico_Sexo_SexoId",
                        column: x => x.SexoId,
                        principalTable: "Sexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eleicao",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ano = table.Column<int>(nullable: false),
                    Descricao = table.Column<string>(nullable: true),
                    TipoId = table.Column<long>(nullable: false),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eleicao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eleicao_TipoEleicao_TipoId",
                        column: x => x.TipoId,
                        principalTable: "TipoEleicao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mandato",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PoliticoId = table.Column<long>(nullable: false),
                    EleicaoId = table.Column<long>(nullable: false),
                    PartidoId = table.Column<long>(nullable: false),
                    CargoId = table.Column<long>(nullable: false),
                    Suplente = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mandato", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mandato_Cargo_CargoId",
                        column: x => x.CargoId,
                        principalTable: "Cargo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mandato_Eleicao_EleicaoId",
                        column: x => x.EleicaoId,
                        principalTable: "Eleicao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mandato_Partido_PartidoId",
                        column: x => x.PartidoId,
                        principalTable: "Partido",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mandato_Politico_PoliticoId",
                        column: x => x.PoliticoId,
                        principalTable: "Politico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eleicao_TipoId",
                table: "Eleicao",
                column: "TipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mandato_CargoId",
                table: "Mandato",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mandato_EleicaoId",
                table: "Mandato",
                column: "EleicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mandato_PartidoId",
                table: "Mandato",
                column: "PartidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mandato_PoliticoId",
                table: "Mandato",
                column: "PoliticoId");

            migrationBuilder.CreateIndex(
                name: "IX_Politico_CPF",
                table: "Politico",
                column: "CPF");

            migrationBuilder.CreateIndex(
                name: "IX_Politico_GrauInstrucaoId",
                table: "Politico",
                column: "GrauInstrucaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Politico_OcupacaoId",
                table: "Politico",
                column: "OcupacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Politico_SexoId",
                table: "Politico",
                column: "SexoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mandato");

            migrationBuilder.DropTable(
                name: "Cargo");

            migrationBuilder.DropTable(
                name: "Eleicao");

            migrationBuilder.DropTable(
                name: "Partido");

            migrationBuilder.DropTable(
                name: "Politico");

            migrationBuilder.DropTable(
                name: "TipoEleicao");

            migrationBuilder.DropTable(
                name: "GrauInstrucao");

            migrationBuilder.DropTable(
                name: "Ocupacao");

            migrationBuilder.DropTable(
                name: "Sexo");
        }
    }
}
