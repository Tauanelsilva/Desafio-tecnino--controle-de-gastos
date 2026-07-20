using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.Models;

public class Transacao
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Valor { get; set; }

    [Required]
    public TipoTransacao Tipo { get; set; }

    [Required]
    public int PessoaId { get; set; }

    public Pessoa? Pessoa { get; set; }
}

public enum TipoTransacao
{
    Receita = 1,
    Despesa = 2
}
