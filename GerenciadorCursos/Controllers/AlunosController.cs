using GerenciadorCursos.Data;
using GerenciadorCursos.Dtos;
using GerenciadorCursos.Dtos.Requests;
using GerenciadorCursos.Dtos.Responses;
using GerenciadorCursos.Models;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCursos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAlunos()
        {
            var alunos = _context.Alunos
                .Select(a => new AlunoDto
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Email = a.Email,
                    DataNascimento = a.DataNascimento
                }).ToList();

            return Ok(alunos);
        }

        [HttpGet("matriculados")]
        public IActionResult AlunosMatriculados()
        {
            var alunos = _context.Alunos
                .Where(a => a.Matriculas.Any())
                .Select(a => new AlunoDto
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Email = a.Email,
                    DataNascimento = a.DataNascimento
                }).ToList();

            return Ok(alunos);
        }

        [HttpGet("porcurso/{cursoId}")]
        public IActionResult AlunosPorCurso(int cursoId)
        {
            var alunos = _context.Matriculas
                .Where(m => m.CursoId == cursoId)
                .Select(m => new AlunoDto
                {
                    Id = m.Aluno.Id,
                    Nome = m.Aluno.Nome,
                    Email = m.Aluno.Email,
                    DataNascimento = m.Aluno.DataNascimento
                }).Distinct().ToList();

            return Ok(alunos);
        }

        [HttpPost]
        public IActionResult CriarAluno([FromBody] AlunoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var idade = DateTime.Today.Year - dto.DataNascimento.Year;
            if (dto.DataNascimento > DateTime.Today.AddYears(-idade)) idade--;

            if (idade < 18)
                return BadRequest("O aluno deve ser maior de idade.");

            var aluno = new Aluno
            {
                Nome = dto.Nome,
                Email = dto.Email,
                DataNascimento = dto.DataNascimento
            };

            _context.Alunos.Add(aluno);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetAlunos), new { id = aluno.Id }, aluno);
        }

        [HttpPut("{id}")]
        public IActionResult EditarAluno(int id, [FromBody] AlunoUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var aluno = _context.Alunos.Find(id);
            if (aluno == null)
                return NotFound();

            var idade = DateTime.Today.Year - dto.DataNascimento.Year;
            if (dto.DataNascimento > DateTime.Today.AddYears(-idade)) idade--;

            if (idade < 18)
                return BadRequest("O aluno deve ser maior de idade.");

            aluno.Nome = dto.Nome;
            aluno.Email = dto.Email;
            aluno.DataNascimento = dto.DataNascimento;

            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirAluno(int id)
        {
            var aluno = _context.Alunos.Find(id);
            if (aluno == null)
                return NotFound();

            _context.Alunos.Remove(aluno);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _context.Alunos.Find(id);
            if (aluno == null)
                return NotFound();

            return Ok(aluno);
        }

    }
}
