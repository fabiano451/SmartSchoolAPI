using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Model;

namespace SmartSchool.API.Controllers
{
	[Route("api/professor")]
	[ApiController]
	public class ProfessorController : ControllerBase
	{

		private readonly IRepository _repository;
		public ProfessorController(SmartContext context, IRepository repository)
		{
			_repository = repository;
		}

		// GET: api/<ProfessorController>
		[HttpGet]
		public IActionResult Get()
		{
			var result = _repository.GetAllProfessores(true);

			return Ok(result);
		}

		// GET api/<ProfessorController>/5
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var Professor = _repository.GetAllProfessoresById(id, false);
			if (Professor == null)
			{
				return BadRequest("Professor não foi encontrado.");
			}
			return Ok(Professor);
		}

		[HttpPost]
		public IActionResult Post(Professor professor)
		{

			if (professor == null)
			{
				return BadRequest("Professor não foi encontrado.");
			}
			_repository.Add(professor);

			if (_repository.SaveChanges())
			{
				return Ok(professor);
			}

			return BadRequest("Professor não foi cadastrado.");
		}

		[HttpPut("{id}")]
		public IActionResult Put(int Id, Professor Professor)
		{
			// busca o recurso no banco de dados mas não faz o bloqueio.
			var ProfessorFind = _repository.GetAllProfessoresById(Id, false);

			if (ProfessorFind == null)
			{
				return BadRequest("Professor não foi encontrado.");
			}

			_repository.Update(Professor);

			if (_repository.SaveChanges())
			{
				return Ok(Professor);
			}

			return BadRequest("Professor não foi cadastrado.");

		}

		[HttpPatch("{id}")]
		public IActionResult Patch(int Id, Professor Professor)
		{
			var ProfessorFind = _repository.GetAllProfessoresById(Id, false);

			if (ProfessorFind == null)
			{
				return BadRequest("Professor não foi atualizado.");
			}

			_repository.Update(Professor);

			if (_repository.SaveChanges())
			{
				return Ok(Professor);
			}

			return BadRequest("Professor não foi cadastrado.");

		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int Id)
		{
			var ProfessorFind = _repository.GetAllProfessoresById(Id, false);

			if (ProfessorFind == null)
			{
				return BadRequest("Professor não foi encontrado.");
			}

			_repository.Delete(ProfessorFind);

			if (_repository.SaveChanges())
			{
				return Ok("Professor excluído com sucesso");
			}

			return BadRequest("Professor não foi cadastrado.");
		}
	}
}