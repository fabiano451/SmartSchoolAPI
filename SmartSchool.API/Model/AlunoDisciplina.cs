namespace SmartSchool.API.Model
{
    public class AlunoDisciplina
    {
        public AlunoDisciplina(){}
        public AlunoDisciplina(int alunoId, int disciplinaid) 
        {
            this.AlunoId = alunoId;
            this.Disciplinaid = disciplinaid;
        }
        public int AlunoId { get; set; }
        public Aluno Aluno { get; set; }
        public int Disciplinaid { get; set; }
        public Disciplina Disciplina { get; set; }
    }
}
