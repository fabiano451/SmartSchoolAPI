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
    [Route("api/aluno")]
    [ApiController]
    public class AlunoController : ControllerBase
    {

        private readonly IRepository _repository;
        private readonly IMapper _mapper;   
        public AlunoController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {
            var aluno = _repository.GetAllAlunos(true);

			return Ok(_mapper.Map<IEnumerable<AlunoDto>>(aluno));
        }

		// GET: api/<AlunoController>
		[HttpGet("getRegister")]
		public IActionResult GetRegister()
		{
			return Ok(new AlunoRegistrarDto());
		}

		// GET api/<AlunoController>/5
		[HttpGet("{id}")]
        public IActionResult Get(int id)
        {
			var aluno = _repository.GetAllAlunoById(id, false);
			if (aluno == null) return BadRequest("O Aluno não foi encontrado");

			var alunoDto = _mapper.Map<AlunoDto>(aluno);

			return Ok(alunoDto);
		}

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