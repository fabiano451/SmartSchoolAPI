using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.API.Migrations
{
    public partial class initialMySql : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Matricula = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    DataNasc = table.Column<DateTime>(nullable: false),
                    DataIni = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Professores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Registro = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    DataIni = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlunoCursos",
                columns: table => new
                {
                    AlunoId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    DataIni = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoCursos", x => new { x.AlunoId, x.CursoId });
                    table.ForeignKey(
                        name: "FK_AlunoCursos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoCursos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    CargaHoraria = table.Column<int>(nullable: false),
                    PrerequisitoId = table.Column<int>(nullable: true),
                    ProfessorId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Disciplinas_PrerequisitoId",
                        column: x => x.PrerequisitoId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Professores_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlunoDisciplinas",
                columns: table => new
                {
                    AlunoId = table.Column<int>(nullable: false),
                    DisciplinaId = table.Column<int>(nullable: false),
                    DataIni = table.Column<DateTime>(nullable: false),
                    DataFim = table.Column<DateTime>(nullable: true),
                    Nota = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlunoDisciplinas", x => new { x.AlunoId, x.DisciplinaId });
                    table.ForeignKey(
                        name: "FK_AlunoDisciplinas_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlunoDisciplinas_Disciplinas_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "Disciplinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "Ativo", "DataFim", "DataIni", "DataNasc", "Matricula", "Nome", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(1329), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Marta", "Kent", "33225555" },
                    { 2, true, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(2877), new DateTime(1982, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Fabiano", "Silva", "33225555" },
                    { 3, true, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(2934), new DateTime(2000, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Marta", "Kent", "33225555" },
                    { 4, true, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(2945), new DateTime(1999, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Maria", "AKent", "33225478" },
                    { 5, true, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(2949), new DateTime(2005, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Marta", "Kent", "33225555" },
                    { 6, true, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(2957), new DateTime(2004, 5, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Marta", "Kent", "33225555" },
                    { 7, true, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(2961), new DateTime(1997, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Gilmar Mendes", "Kent Live", "3324125" }
                });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Tecnologia da Informação" },
                    { 2, "Sistema da Informação" },
                    { 3, "Ciência da Computação" }
                });

            migrationBuilder.InsertData(
                table: "Professores",
                columns: new[] { "Id", "Ativo", "DataFim", "DataIni", "Nome", "Registro", "Sobrenome", "Telefone" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2024, 12, 20, 15, 5, 46, 47, DateTimeKind.Local).AddTicks(6545), "Lauro", 1, "Matias", "398541085" },
                    { 2, true, null, new DateTime(2024, 12, 20, 15, 5, 46, 48, DateTimeKind.Local).AddTicks(3270), "Roberto", 2, "Lima", "4545454" },
                    { 3, true, null, new DateTime(2024, 12, 20, 15, 5, 46, 48, DateTimeKind.Local).AddTicks(3327), "Ronaldo", 3, "JUIL", "48878" },
                    { 4, true, null, new DateTime(2024, 12, 20, 15, 5, 46, 48, DateTimeKind.Local).AddTicks(3330), "Rodrigo", 4, "Stola", "4545454" },
                    { 5, true, null, new DateTime(2024, 12, 20, 15, 5, 46, 48, DateTimeKind.Local).AddTicks(3332), "Alexandre", 5, "Mik", "4545454" }
                });

            migrationBuilder.InsertData(
                table: "Disciplinas",
                columns: new[] { "Id", "CargaHoraria", "CursoId", "Nome", "PrerequisitoId", "ProfessorId" },
                values: new object[,]
                {
                    { 1, 0, 1, "Matemática", null, 1 },
                    { 2, 0, 3, "Matemática", null, 1 },
                    { 3, 0, 3, "Física", null, 2 },
                    { 4, 0, 1, "Português", null, 3 },
                    { 5, 0, 1, "Inglês", null, 4 },
                    { 6, 0, 2, "Inglês", null, 4 },
                    { 7, 0, 3, "Inglês", null, 4 },
                    { 8, 0, 1, "Programação", null, 5 },
                    { 9, 0, 2, "Programação", null, 5 },
                    { 10, 0, 3, "Programação", null, 5 }
                });

            migrationBuilder.InsertData(
                table: "AlunoDisciplinas",
                columns: new[] { "AlunoId", "DisciplinaId", "DataFim", "DataIni", "Nota" },
                values: new object[,]
                {
                    { 2, 1, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4592), null },
                    { 4, 5, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4606), null },
                    { 2, 5, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4597), null },
                    { 1, 5, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4590), null },
                    { 7, 4, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4620), null },
                    { 6, 4, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4615), null },
                    { 5, 4, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4608), null },
                    { 4, 4, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4605), null },
                    { 1, 4, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4569), null },
                    { 7, 3, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4619), null },
                    { 5, 5, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4609), null },
                    { 6, 3, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4613), null },
                    { 7, 2, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4618), null },
                    { 6, 2, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4611), null },
                    { 3, 2, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4600), null },
                    { 2, 2, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4594), null },
                    { 1, 2, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4065), null },
                    { 7, 1, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4616), null },
                    { 6, 1, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4610), null },
                    { 4, 1, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4604), null },
                    { 3, 1, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4599), null },
                    { 3, 3, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4601), null },
                    { 7, 5, null, new DateTime(2024, 12, 20, 15, 5, 46, 50, DateTimeKind.Local).AddTicks(4621), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlunoCursos_CursoId",
                table: "AlunoCursos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_AlunoDisciplinas_DisciplinaId",
                table: "AlunoDisciplinas",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_CursoId",
                table: "Disciplinas",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_PrerequisitoId",
                table: "Disciplinas",
                column: "PrerequisitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_ProfessorId",
                table: "Disciplinas",
                column: "ProfessorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlunoCursos");

            migrationBuilder.DropTable(
                name: "AlunoDisciplinas");

            migrationBuilder.DropTable(
                name: "Alunos");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Professores");
        }
    }
}
