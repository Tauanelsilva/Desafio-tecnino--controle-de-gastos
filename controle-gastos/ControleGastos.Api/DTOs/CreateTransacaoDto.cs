using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.DTOs;

public class CreateTransacaoDto
{
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "A descrição deve ter entre 1 e 200 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O valor é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    [Required(ErrorMessage = "O tipo é obrigatório.")]
    public int Tipo { get; set; }

    [Required(ErrorMessage = "O ID da pessoa é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID da pessoa deve ser válido.")]
    public int PessoaId { get; set; }
}
