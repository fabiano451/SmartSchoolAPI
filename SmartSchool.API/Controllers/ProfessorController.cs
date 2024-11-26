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
	[ApiController]
	[ApiVersion("2.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class ProfessorController : ControllerBase
	{

		private readonly IRepository _repository;
		private readonly IMapper _mapper;
		public ProfessorController(IRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		// GET: api/<professorController>
		[HttpGet]
		public IActionResult Get()
		{
			var professor = _repository.GetAllProfessores(true);

			return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professor));
		}

		// GET: api/<professorController>
		[HttpGet("getRegister")]
		public IActionResult GetRegister()
		{
			return Ok(new ProfessorRegistrarDto());
		}

		// GET api/<professorController>/5
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var professor = _repository.GetAllProfessoresById(id, false);
			if (professor == null) return BadRequest("O professor não foi encontrado");

			var professorDto = _mapper.Map<ProfessorDto>(professor);

			return Ok(professorDto);
		}

		[HttpPost]
		public IActionResult Post(ProfessorRegistrarDto model)
		{

			var professor = _mapper.Map<Professor>(model);

			_repository.Add(professor);
			if (_repository.SaveChanges())
			{
				return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
			}

			return BadRequest("professor não cadastrado");
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, ProfessorRegistrarDto model)
		{
			var professor = _repository.GetAllProfessoresById(id, false);
			if (professor == null) return BadRequest("professor não encontrado");

			_mapper.Map(model, professor);

			_repository.Update(professor);
			if (_repository.SaveChanges())
			{
				return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
			}

			return BadRequest("professor não Atualizado");
		}


		[HttpPatch("{id}")]
		public IActionResult Patch(int id, ProfessorRegistrarDto model)
		{
			var professor = _repository.GetAllProfessoresById(id, false);
			if (professor == null) return BadRequest("professor não encontrado");

			_mapper.Map(model, professor);

			_repository.Update(professor);
			if (_repository.SaveChanges())
			{
				return Created($"/api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professor));
			}

			return BadRequest("professor não Atualizado");
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var professor = _repository.GetAllProfessoresById(id, false);
			if (professor == null) return BadRequest("professor não encontrado");

			_repository.Delete(professor);
			if (_repository.SaveChanges())
			{
				return Ok("professor deletado");
			}

			return BadRequest("professor não deletado");
		}
	}
}