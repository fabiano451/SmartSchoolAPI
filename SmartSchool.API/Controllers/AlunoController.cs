using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Dto;
using SmartSchool.API.Model;
using System.Collections;
using System.Collections.Generic;

namespace SmartSchool.API.Controllers
{
    /// <summary>
	/// 
	/// </summary>
    [ApiController]
	[ApiVersion("1.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class AlunoController : ControllerBase
    {

        private readonly IRepository _repository;
        private readonly IMapper _mapper;   
        public AlunoController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

		/// <summary>
		/// Metodo responsavel para rertornar todos meus alunos
		/// </summary>
		[HttpGet]
        public IActionResult Get()
        {
            var aluno = _repository.GetAllAlunos(true);

			return Ok(_mapper.Map<IEnumerable<AlunoDto>>(aluno));
        }

		/// <summary>
		/// Metodo responsavel para rertornar um unico aluno dto
		/// </summary>
		[HttpGet("getRegister")]
		public IActionResult GetRegister()
		{
			return Ok(new AlunoRegistrarDto());
		}

		/// <summary>
		/// Metodo responsavel para retornar um aluno por Id
		/// </summary>
		[HttpGet("{id}")]
        public IActionResult Get(int id)
        {
			var aluno = _repository.GetAllAlunoById(id, false);
			if (aluno == null) return BadRequest("O Aluno não foi encontrado");

			var alunoDto = _mapper.Map<AlunoDto>(aluno);

			return Ok(alunoDto);
		}

		/// <summary>
		/// Metodo responsavel para inserir um aluno
		/// </summary>
		[HttpPost]
        public IActionResult Post(AlunoRegistrarDto model)
        {

			var aluno = _mapper.Map<Aluno>(model);

			_repository.Add(aluno);
			if (_repository.SaveChanges())
			{
				return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
			}

			return BadRequest("Aluno não cadastrado");
		}

		/// <summary>
		/// Metodo responsavel para atualizar aluno
		/// </summary>
		[HttpPut("{id}")]
		public IActionResult Put(int id, AlunoRegistrarDto model)
		{
			var aluno = _repository.GetAllAlunoById(id);
			if (aluno == null) return BadRequest("Aluno não encontrado");

			_mapper.Map(model, aluno);

			_repository.Update(aluno);
			if (_repository.SaveChanges())
			{
				return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
			}

			return BadRequest("Aluno não Atualizado");
		}

		/// <summary>
		/// Metodo responsavel para atualizar aluno parcialmente
		/// </summary>
		[HttpPatch("{id}")]
        public IActionResult Patch(int id, AlunoRegistrarDto model)
		{
			var aluno = _repository.GetAllAlunoById(id);
			if (aluno == null) return BadRequest("Aluno não encontrado");

			_mapper.Map(model, aluno);

			_repository.Update(aluno);
			if (_repository.SaveChanges())
			{
				return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
			}

			return BadRequest("Aluno não Atualizado");
		}

		/// <summary>
		/// Metodo responsavel para deletar aluno
		/// </summary>
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var aluno = _repository.GetAllAlunoById(id);
			if (aluno == null) return BadRequest("Aluno não encontrado");

			_repository.Delete(aluno);
			if (_repository.SaveChanges())
			{
				return Ok("Aluno deletado");
			}

			return BadRequest("Aluno não deletado");
		}
	}
}