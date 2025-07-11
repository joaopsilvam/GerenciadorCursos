﻿namespace GerenciadorCursos.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public List<Matricula> Matriculas { get; set; } = new();
    }
}
