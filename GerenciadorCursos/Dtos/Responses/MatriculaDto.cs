namespace GerenciadorCursos.Dtos.Responses
{
    public class MatriculaDto
    {
        public int Id { get; set; }
        public int AlunoId { get; set; }
        public int CursoId { get; set; }
        public string NomeAluno { get; set; } = string.Empty;
        public string NomeCurso { get; set; } = string.Empty;
    }
}
