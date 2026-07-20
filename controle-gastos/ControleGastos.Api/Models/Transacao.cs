using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.Models;

/// <summary>
/// Entidade que representa uma transação financeira (receita ou despesa)
/// vinculada a uma pessoa do sistema. Cada transação possui uma descrição,
/// valor monetário, tipo (receita ou despesa) e referência à pessoa responsável.
/// </summary>
public class Transacao
{
    /// <summary>
    /// Identificador único da transação, gerado automaticamente pelo banco de dados.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Descrição da transação. Exemplo: "Compra de mercado", "Salário mensal".
    /// Campo obrigatório com no máximo 200 caracteres.
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Descricao { get; set; } = string.Empty;

    /// <summary>
    /// Valor monetário da transação em reais (R$). Deve ser maior que zero.
    /// </summary>
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Valor { get; set; }

    /// <summary>
    /// Tipo da transação: Receita (1) ou Despesa (2).
    /// Regra de negócio: menores de 18 anos só podem cadastrar Despesa.
    /// </summary>
    [Required]
    public TipoTransacao Tipo { get; set; }

    /// <summary>
    /// Chave estrangeira para a pessoa responsável por esta transação.
    /// A pessoa referenciada deve existir no banco de dados.
    /// </summary>
    [Required]
    public int PessoaId { get; set; }

    /// <summary>
    /// Propriedade de navegação para a entidade Pessoa associada.
    /// </summary>
    public Pessoa? Pessoa { get; set; }
}

/// <summary>
/// Enum que define os tipos possíveis de transação financeira.
/// </summary>
public enum TipoTransacao
{
    /// <summary>Entrada de dinheiro (salário, venda, etc.)</summary>
    Receita = 1,

    /// <summary>Saída de dinheiro (compra, conta, etc.)</summary>
    Despesa = 2
}
