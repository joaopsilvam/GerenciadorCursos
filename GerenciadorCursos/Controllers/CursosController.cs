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
    public class CursosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CursosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("disponiveis")]
        public IActionResult ListarCursosDisponiveis()
        {
            var cursos = _context.Cursos
                .Select(c => new CursoDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Descricao = c.Descricao
                }).ToList();

            return Ok(cursos);
        }

        [HttpGet]
        public IActionResult GetCursos()
        {
            var cursos = _context.Cursos
                .Select(c => new CursoDto
                {
                    Id = c.Id,
                    Nome = c.Nome,
                    Descricao = c.Descricao
                }).ToList();

            return Ok(cursos);
        }

        [HttpPost]
        public IActionResult CriarCurso([FromBody] CursoCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var curso = new Curso
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao
            };

            _context.Cursos.Add(curso);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCursos), new { id = curso.Id }, curso);
        }

        [HttpPut("{id}")]
        public IActionResult EditarCurso(int id, [FromBody] CursoUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var curso = _context.Cursos.Find(id);
            if (curso == null)
                return NotFound();

            curso.Nome = dto.Nome;
            curso.Descricao = dto.Descricao;

            _context.SaveChanges();
            return Ok(curso);
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirCurso(int id)
        {
            var curso = _context.Cursos.Find(id);
            if (curso == null)
                return NotFound();

            _context.Cursos.Remove(curso);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
