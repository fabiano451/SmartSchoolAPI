using AutoMapper;
using SmartSchool.API.Dto;
using SmartSchool.API.Model;

namespace SmartSchool.API.Helpers
{
	public class SmartSchoolProfile : Profile
	{
		public SmartSchoolProfile()
		{

			CreateMap<Aluno, AlunoDto>()
				.ForMember(
					dest => dest.Nome,
					opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
				)
				.ForMember(
					dest => dest.Idade,
					opt => opt.MapFrom(src => src.DataNasc.GetCurrentAge())
				);

			CreateMap<AlunoDto, Aluno>();
			CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();
			CreateMap<Aluno, AlunoPatchDto>().ReverseMap();

			CreateMap<Professor, ProfessorDto>()
				.ForMember(
					dest => dest.Nome,
					opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
				);

			CreateMap<ProfessorDto, Professor>();
			CreateMap<Professor, ProfessorRegistrarDto>().ReverseMap();

			CreateMap<DisciplinaDto, Disciplina>().ReverseMap();
			CreateMap<CursoDto, Curso>().ReverseMap();
		}

	}
}
