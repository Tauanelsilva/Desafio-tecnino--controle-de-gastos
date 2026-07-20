using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.DTOs;

public class CreatePessoaDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome deve ter entre 1 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A idade é obrigatória.")]
    [Range(1, 150, ErrorMessage = "A idade deve estar entre 1 e 150.")]
    public int Idade { get; set; }
}
