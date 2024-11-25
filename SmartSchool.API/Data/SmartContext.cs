using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Model;
using System;
using System.Collections.Generic;

namespace SmartSchool.API.Data
{
    public class SmartContext : DbContext
    {
        public SmartContext(DbContextOptions options) : base(options) { }  
       
        public DbSet<Aluno> Alunos { get; set; }

        public DbSet<Professor> Professores { get; set; }

        public DbSet<Disciplina> Disciplinas { get; set; }

        public DbSet<AlunoDisciplina> AlunoDisciplinas { get; set; }

		public DbSet<AlunoCurso> AlunoCursos { get; set; }

		public DbSet<Curso> Cursos { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlunoDisciplina>().HasKey(ad => new { ad.AlunoId, ad.DisciplinaId });
			modelBuilder.Entity<AlunoCurso>().HasKey(ad => new { ad.AlunoId, ad.CursoId });

			base.OnModelCreating(modelBuilder);
			
			modelBuilder.Entity<Professor>()
				.HasData(new List<Professor>(){
					new Professor(1,1,"Lauro", "Matias","398541085"),
					new Professor(2,2,"Roberto", "Lima","4545454"),
					new Professor(3,3, "Ronaldo", "JUIL","48878"),
					new Professor(4,4, "Rodrigo", "Stola", "4545454"),
					new Professor(5, 5,"Alexandre","Mik", "4545454"),
				});

			modelBuilder.Entity<Curso>()
				.HasData(new List<Curso>{
					new Curso(1, "Tecnologia da Informação"),
					new Curso(2, "Sistema da Informação"),
					new Curso(3, "Ciência da Computação"),
				});

			modelBuilder.Entity<Disciplina>()
				.HasData(new List<Disciplina>{
					new Disciplina(1, "Matemática", 1,1),
					new Disciplina(2, "Matemática", 1,3),
					new Disciplina(3, "Física", 2,3),
					new Disciplina(4, "Português", 3,1),
					new Disciplina(5, "Inglês", 4, 1),
					new Disciplina(6, "Inglês", 4, 2),
					new Disciplina(7, "Inglês", 4, 3),
					new Disciplina(8, "Programação", 5, 1),
					new Disciplina(9, "Programação", 5, 2),
					new Disciplina(10, "Programação", 5, 3)
				});

			modelBuilder.Entity<Aluno>()
				.HasData(new List<Aluno>(){
					new Aluno(1,1, "Marta", "Kent", "33225555",DateTime.Parse("05/28/2005")),
					new Aluno(2,2, "Fabiano", "Silva", "33225555",DateTime.Parse("04/08/1982")),
					new Aluno(3,3, "Marta", "Kent", "33225555",DateTime.Parse("03/28/2000")),
					new Aluno(4,4, "Maria", "AKent", "33225478",DateTime.Parse("10/08/1999")),
					new Aluno(5,5, "Marta", "Kent", "33225555",DateTime.Parse("05/28/2005")),
					new Aluno(6,6, "Marta", "Kent", "33225555",DateTime.Parse("05/28/2004")),
					new Aluno(7,7, "Gilmar Mendes", "Kent Live", "3324125",DateTime.Parse("01/01/1997")),
				});

			modelBuilder.Entity<AlunoDisciplina>()
				.HasData(new List<AlunoDisciplina>() {
					new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 2 },
					new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 4 },
					new AlunoDisciplina() {AlunoId = 1, DisciplinaId = 5 },
					new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 1 },
					new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 2 },
					new AlunoDisciplina() {AlunoId = 2, DisciplinaId = 5 },
					new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 1 },
					new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 2 },
					new AlunoDisciplina() {AlunoId = 3, DisciplinaId = 3 },
					new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 1 },
					new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 4 },
					new AlunoDisciplina() {AlunoId = 4, DisciplinaId = 5 },
					new AlunoDisciplina() {AlunoId = 5, DisciplinaId = 4 },
					new AlunoDisciplina() {AlunoId = 5, DisciplinaId = 5 },
					new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 1 },
					new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 2 },
					new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 3 },
					new AlunoDisciplina() {AlunoId = 6, DisciplinaId = 4 },
					new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 1 },
					new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 2 },
					new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 3 },
					new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 4 },
					new AlunoDisciplina() {AlunoId = 7, DisciplinaId = 5 }
				});
		}

    }
}
