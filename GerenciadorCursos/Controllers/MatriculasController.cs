using GerenciadorCursos.Data;
using GerenciadorCursos.Dtos;
using GerenciadorCursos.Dtos.Requests;
using GerenciadorCursos.Dtos.Responses;
using GerenciadorCursos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorCursos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatriculasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetMatriculas()
        {
            var matriculas = _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Curso)
                .Select(m => new MatriculaDto
                {
                    Id = m.Id,
                    AlunoId = m.AlunoId,
                    CursoId = m.CursoId,
                    NomeAluno = m.Aluno.Nome,
                    NomeCurso = m.Curso.Nome
                }).ToList();

            return Ok(matriculas);
        }

        [HttpPost]
        public IActionResult Matricular([FromBody] MatriculaCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var alunoExiste = _context.Alunos.Any(a => a.Id == dto.AlunoId);
            if (!alunoExiste)
                return NotFound("Aluno não encontrado.");

            var cursosValidos = _context.Cursos
                .Where(c => dto.CursoIds.Contains(c.Id))
                .Select(c => c.Id)
                .ToList();

            var cursosInvalidos = dto.CursoIds.Except(cursosValidos).ToList();
            if (cursosInvalidos.Any())
                return BadRequest(new { Erro = "IDs de curso inválidos.", CursosInvalidos = cursosInvalidos });

            foreach (var cursoId in dto.CursoIds)
            {
                var jaMatriculado = _context.Matriculas.Any(m => m.AlunoId == dto.AlunoId && m.CursoId == cursoId);
                if (!jaMatriculado)
                {
                    var matricula = new Matricula
                    {
                        AlunoId = dto.AlunoId,
                        CursoId = cursoId
                    };

                    _context.Matriculas.Add(matricula);
                }
            }

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete]
        public IActionResult RemoverMatricula([FromQuery] int alunoId, [FromQuery] int cursoId)
        {
            var matricula = _context.Matriculas
                .FirstOrDefault(m => m.AlunoId == alunoId && m.CursoId == cursoId);

            if (matricula == null)
                return NotFound("Matrícula não encontrada.");

            _context.Matriculas.Remove(matricula);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
