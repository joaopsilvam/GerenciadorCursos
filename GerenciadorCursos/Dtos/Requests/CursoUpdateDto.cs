using System.ComponentModel.DataAnnotations;

namespace GerenciadorCursos.Dtos.Requests
{
    public class CursoUpdateDto
    {
        [Required(ErrorMessage = "O nome do curso é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição do curso é obrigatória.")]
        [StringLength(300, ErrorMessage = "A descrição deve ter no máximo 300 caracteres.")]
        public string Descricao { get; set; } = string.Empty;
    }
}
