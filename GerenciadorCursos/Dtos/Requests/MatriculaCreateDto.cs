using System.ComponentModel.DataAnnotations;

namespace GerenciadorCursos.Dtos.Requests
{
    public class MatriculaCreateDto
    {
        [Required(ErrorMessage = "O ID do aluno é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "ID do aluno inválido.")]
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "Pelo menos um curso deve ser selecionado.")]
        [MinLength(1, ErrorMessage = "Selecione pelo menos um curso.")]
        public List<int> CursoIds { get; set; } = new();
    }
}
