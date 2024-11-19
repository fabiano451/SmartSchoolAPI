using Microsoft.AspNetCore.Mvc;
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

        public AlunoController() { }
         

        public List<Aluno> Alunos = new List<Aluno>
        {
            new Aluno()
            {
                Id= 1,
                Nome = "Marcos",
                SobreNome = "Silva",
                Telefone = "11968390857"
            },
             new Aluno()
            {
                Id= 2,
                Nome = "Mateus",
                SobreNome = "Rista",
                Telefone = "119683987899"
            },


        };
        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(Alunos);
        }

        // GET api/<AlunoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var aluno = Alunos.ToList().FirstOrDefault(x => x.Id == id);
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
            var aluno = Alunos.ToList()
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
            return Ok(Alunos);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int Id, Aluno aluno)
        {

            if (aluno == null)
            {
                return BadRequest("Aluno não foi encontrado.");
            }
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int Id, Aluno aluno)
        {

            if (aluno == null)
            {
                return BadRequest("Aluno não foi encontrado.");
            }
            return Ok(aluno);
        }
       
        [HttpDelete]
        public IActionResult Delete(int Id)
        {

            return Ok();
        }
    }
}