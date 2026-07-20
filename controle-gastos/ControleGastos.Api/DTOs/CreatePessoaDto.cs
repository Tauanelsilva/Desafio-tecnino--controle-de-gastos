using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.DTOs;

/// <summary>
/// DTO (Data Transfer Object) utilizado para receber os dados de criação de uma nova pessoa.
/// Realiza a validação dos dados de entrada antes de chegarem ao Service.
/// </summary>
public class CreatePessoaDto
{
    /// <summary>Nome completo da pessoa.</summary>
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "O nome deve ter entre 1 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;

    /// <summary>Idade da pessoa. Utilizada para validação da regra de negócio de transações.</summary>
    [Required(ErrorMessage = "A idade é obrigatória.")]
    [Range(1, 150, ErrorMessage = "A idade deve estar entre 1 e 150.")]
    public int Idade { get; set; }
}
