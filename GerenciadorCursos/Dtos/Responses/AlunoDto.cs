namespace GerenciadorCursos.Dtos.Responses
{
    public class AlunoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
    }
}
