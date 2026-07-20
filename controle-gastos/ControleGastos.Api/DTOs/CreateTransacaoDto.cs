using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.DTOs;

/// <summary>
/// DTO (Data Transfer Object) utilizado para receber os dados de criação de uma nova transação.
/// Realiza a validação primária (campos obrigatórios, limites) dos dados recebidos da requisição HTTP.
/// </summary>
public class CreateTransacaoDto
{
    /// <summary>Descrição da transação. Ex: "Conta de luz".</summary>
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "A descrição deve ter entre 1 e 200 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    /// <summary>Valor da transação monetária. Deve ser um valor positivo.</summary>
    [Required(ErrorMessage = "O valor é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public decimal Valor { get; set; }

    /// <summary>
    /// Tipo da transação, onde 1 = Receita e 2 = Despesa.
    /// É validado para permitir apenas estes dois valores específicos.
    /// </summary>
    [Required(ErrorMessage = "O tipo é obrigatório.")]
    [Range(1, 2, ErrorMessage = "O tipo deve ser 1 (Receita) ou 2 (Despesa).")]
    public int Tipo { get; set; }

    /// <summary>Id da pessoa responsável por esta transação.</summary>
    [Required(ErrorMessage = "O ID da pessoa é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID da pessoa deve ser válido.")]
    public int PessoaId { get; set; }
}
