﻿using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Model;
using SQLitePCL;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SmartSchool.API.Data
{
	public class Repository : IRepository
	{
		private readonly SmartContext _context;

		public Repository(SmartContext context)
		{
			_context = context;
		}
		public void Add<T>(T entity) where T : class
		{
			_context.Add(entity);
		}

		public void Update<T>(T entity) where T : class
		{
			_context.Update(entity);
		}
		public void Delete<T>(T entity) where T : class
		{
			_context.Remove(entity);
		}

		public bool SaveChanges()
		{
			return (_context.SaveChanges() > 0);
		}

		public Aluno[] GetAllAlunos(bool includeProfessor = false)
		{
			IQueryable<Aluno> query = _context.Alunos;

			if (includeProfessor)
			{
				query = query.Include(a => a.AlunosDisciplinas)
					.ThenInclude(ad => ad.Disciplina)
					.ThenInclude(d => d.Professsor);

				query = query.AsNoTracking().OrderBy(a => a.Id);

				return query.ToArray();

			}

			query = query.AsNoTracking().OrderBy(a => a.Id);

			return query.ToArray();
		}

		public Aluno[] GetAllalunosByDisciplina(int disciplinaId, bool includeProfessor = false)
		{
			IQueryable<Aluno> query = _context.Alunos;
			if (includeProfessor)
			{
				query = query.Include(a => a.AlunosDisciplinas)
							 .ThenInclude(ad => ad.Disciplina)
							 .ThenInclude(d => d.Professsor);

				query = query.AsNoTracking().OrderBy(a => a.Id);
				return query.ToArray();
			}

			query = query.AsNoTracking()
						 .OrderBy(a => a.Id)
						 .Where(aluno => aluno.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId));

			return query.ToArray();

		}

		public Aluno GetAllAlunoById(int Id, bool includeProfessor = false)
		{

			IQueryable<Aluno> query = _context.Alunos;

			if (includeProfessor)
			{
				query = query.Include(a => a.AlunosDisciplinas)
					.ThenInclude(ad => ad.Disciplina)
					.ThenInclude(d => d.Professsor);
			}

			query = query.AsNoTracking()
						 .OrderBy(a => a.Id)
						 .Where(aluno => aluno.Id == Id);

			return query.FirstOrDefault();
		}

		public Professor[] GetAllProfessores(bool includeAluno = false)
		{
			IQueryable<Professor> query = _context.Professores;

			if (includeAluno)
			{
				query = query.Include(a => a.Disciplinas)
							 .ThenInclude(d => d.AlunosDisciplinas)
							 .ThenInclude(d => d.Aluno);

			}

			return query.ToArray();
		}


		public Professor[] GetAllProfessoresByDisciplina(int disciplinaId, bool includeAluno = false)
		{
			IQueryable<Professor> query = _context.Professores;
			if (includeAluno)
			{
				query = query.Include(a => a.Disciplinas)
							 .ThenInclude(d => d.AlunosDisciplinas)
							 .ThenInclude(d => d.Aluno);

			}

			query = query.AsNoTracking()
						 .OrderBy(a => a.Id)
						 .Where(aluno => aluno.Disciplinas.Any(d => d.AlunosDisciplinas.Any(ad => ad.DisciplinaId == disciplinaId)));

			return query.ToArray();
		}

		public Professor GetAllProfessoresById(int Id, bool includeAluno = false)
		{
			IQueryable<Professor> query = _context.Professores;

			if (includeAluno)
			{
				query = query.Include(a => a.Disciplinas)
							 .ThenInclude(d => d.Professsor);
			}

			query = query.AsNoTracking()
						 .OrderBy(a => a.Id)
						 .Where(professor => professor.Id == Id);

			return query.FirstOrDefault();
		}
	}
}
