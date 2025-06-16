namespace GerenciadorCursos.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public List<Matricula> Matriculas { get; set; } = new();
    }
}
