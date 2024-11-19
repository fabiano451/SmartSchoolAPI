﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.Controllers
{
    [Route("api/professor")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {

        public ProfessorController() { }    
        
        // GET: api/<ProfessorController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Professors: Marta, Paula, Lucas, Rafa");
        }
        

        // GET api/<ProfessorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProfessorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProfessorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProfessorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}