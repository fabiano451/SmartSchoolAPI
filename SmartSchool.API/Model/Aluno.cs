using System.Collections.Generic;
namespace SmartSchool.API.Model
{
    public class Aluno
    {
        public Aluno()
        {
            
        }
        public Aluno(int id, string Nome, string sobreNome, string Telefone) 
        {
            this.Id = id;
             this.Nome = Nome;
            this.SobreNome = sobreNome;
            this.Telefone = Telefone;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Telefone {get; set; }
         public IEnumerable<AlunoDisciplina> AlunosDisciplinas { get; set; } 

    }
}
