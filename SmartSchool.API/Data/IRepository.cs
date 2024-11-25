﻿using SmartSchool.API.Model;
using System.Net.NetworkInformation;

namespace SmartSchool.API.Data
{
	public interface IRepository
	{
		// vai receber um tipo T esse tipo tem que ser entity e esse T tipo tem que ser uma classe
		// usado bastante quando temos praticamente os mesmos metodos para classe diferente
		void Add<T>(T entity) where T : class;
		void Update<T>(T entity) where T : class;
		void Delete<T>(T entity) where T : class;
		bool SaveChanges();

		// Alunos
		Aluno[] GetAllAlunos(bool includeProfessor = false);
		Aluno[] GetAllalunosByDisciplina(int AlunoId, bool includeProfessor = false);
		Aluno GetAllAlunoById(int Id, bool includeProfessor = false);

		//Professores
		Professor[] GetAllProfessores(bool includeAluno = false);
		Professor[] GetAllProfessoresByDisciplina(int AlunoId, bool includeAluno = false);
		Professor GetAllProfessoresById(int Id, bool includeAluno = false);
	}
}
