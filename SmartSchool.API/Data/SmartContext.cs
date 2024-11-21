using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Model;

namespace SmartSchool.API.Data
{
    public class SmartContext : DbContext
    {
        public SmartContext(DbContextOptions options) : base(options) { }  
       
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Disciplina> Disciplinas { get; set; }
        public DbSet<AlunoDisciplina> AlunoDisciplinas { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlunoDisciplina>().HasKey(ad => new { ad.AlunoId, ad.DisciplinaId });
            base.OnModelCreating(modelBuilder);
        }

    }
}
