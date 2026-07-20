using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.Models;

/// <summary>
/// Entidade que representa uma pessoa cadastrada no sistema de controle de gastos.
/// Cada pessoa pode ter várias transações (receitas e despesas) associadas a ela.
/// Quando uma pessoa é excluída, todas as suas transações são removidas em cascata.
/// </summary>
public class Pessoa
{
    /// <summary>
    /// Identificador único da pessoa, gerado automaticamente pelo banco de dados.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome completo da pessoa. Campo obrigatório com no máximo 100 caracteres.
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Idade da pessoa em anos. Utilizada para aplicar regras de negócio:
    /// menores de 18 anos só podem registrar despesas (não podem registrar receitas).
    /// </summary>
    [Required]
    [Range(1, 150)]
    public int Idade { get; set; }

    /// <summary>
    /// Coleção de transações associadas a esta pessoa.
    /// Configurada com delete em cascata no banco de dados.
    /// </summary>
    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
}
