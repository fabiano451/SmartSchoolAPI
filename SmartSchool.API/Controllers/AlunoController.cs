using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Model;

namespace SmartSchool.API.Controllers
{
    [Route("api/aluno")]
    [ApiController]
    public class AlunoController : ControllerBase
    {

        private readonly IRepository _repository;
        public AlunoController(SmartContext context,IRepository repository)
        {
            _repository = repository;
        }
        
        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repository.GetAllAlunos(true);

			return Ok(result);
        }

        // GET api/<AlunoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var aluno = _repository.GetAllAlunoById(id, false);
            if (aluno == null)
            {
                return BadRequest("Aluno não foi encontrado.");
            }
            return Ok(aluno);
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            
            if (aluno == null)
            {
                return BadRequest("Aluno não foi encontrado.");
            }
            _repository.Add(aluno);
           
            if (_repository.SaveChanges())
            {
                return Ok(aluno);
            }
            
            return BadRequest("Aluno não foi cadastrado.");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int Id, Aluno aluno)
        {
            // busca o recurso no banco de dados mas não faz o bloqueio.
			var alunoFind = _repository.GetAllAlunoById(Id, false);

			if (alunoFind == null)
            {
                return BadRequest("Aluno não foi encontrado.");
            }

			_repository.Update(aluno);

			if (_repository.SaveChanges())
			{
				return Ok(aluno);
			}

			return BadRequest("Aluno não foi cadastrado.");

        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int Id, Aluno aluno)
        {
			var alunoFind = _repository.GetAllAlunoById(Id, false);

			if (alunoFind == null)
            {
                return BadRequest("Aluno não foi atualizado.");
            }

			_repository.Update(aluno);

			if (_repository.SaveChanges())
			{
				return Ok(aluno);
			}

			return BadRequest("Aluno não foi cadastrado.");

        }
       
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
			var alunoFind = _repository.GetAllAlunoById(Id, false);

			if (alunoFind == null)
			{
				return BadRequest("Aluno não foi encontrado.");
			}

			_repository.Delete(alunoFind);

			if (_repository.SaveChanges())
			{
				return Ok("Aluno excluído com sucesso");
			}

			return BadRequest("Aluno não foi cadastrado.");
        }
    }
}