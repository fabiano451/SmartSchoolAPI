using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.Controllers
{
    [Route("api/aluno")]
    [ApiController]
    public class AlunoController : ControllerBase
    {

        private readonly SmartContext _context;
        public AlunoController(SmartContext context)
        {
            _context = context; 
        }
        
        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        // GET api/<AlunoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var aluno = _context.Alunos.ToList().FirstOrDefault(x => x.Id == id);
            if (aluno == null)
            {
                return BadRequest("Aluno não foi encontrado.");
            }
            return Ok(aluno);
        }

        //utiliza parametro via rota
        //[HttpGet("ByName/{nome}")]
        //modelo mais utilizado em api é via query string
        //exemplo: https://localhost:44317/api/aluno/ByName?nome=Marcos&sobrenome=Silva
        [HttpGet("ByName")]
        public IActionResult GetByName(string nome, string sobreNome)
        {
            var aluno = _context.Alunos.ToList()
                              .FirstOrDefault(x => x.Nome.Contains(nome)
                                           && x.SobreNome.Contains(sobreNome));
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
            _context.Add(aluno);
            _context.SaveChanges(); 
            return Ok(_context.Alunos);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int Id, Aluno aluno)
        {
            // busca o recurso no banco de dados mas não faz o bloqueio.
			var alunoFind = _context.Alunos
                              .AsNoTracking() // usado para não travar o recurso
							  .FirstOrDefault(x => x.Id.Equals(Id));
			if (alunoFind == null)
            {
                return BadRequest("Aluno não foi encontrado.");
            }

			_context.Update(aluno);
			_context.SaveChanges();
			
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int Id, Aluno aluno)
        {
			var alunoFind = _context.Alunos
							  .AsNoTracking() // usado para não travar o recurso
							  .FirstOrDefault(x => x.Id.Equals(Id));

			if (alunoFind == null)
            {
                return BadRequest("Aluno não foi encontrado.");
            }

			_context.Update(aluno);
			_context.SaveChanges();

			return Ok(aluno);
        }
       
        [HttpDelete("{id}")]
        public IActionResult Delete(int Id)
        {
			var alunoFind = _context.Alunos
							  .FirstOrDefault(x => x.Id.Equals(Id));
			if (alunoFind == null)
			{
				return BadRequest("Aluno não foi encontrado.");
			}
			_context.Remove(alunoFind);
            _context.SaveChanges();
			
            return Ok();
        }
    }
}