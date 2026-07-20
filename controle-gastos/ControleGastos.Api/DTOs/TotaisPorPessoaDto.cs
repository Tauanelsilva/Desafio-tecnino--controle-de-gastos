namespace ControleGastos.Api.DTOs;

/// <summary>
/// DTO (Data Transfer Object) que representa o resumo financeiro individual de uma pessoa.
/// Contém as informações da pessoa e o agrupamento de suas receitas, despesas e saldo.
/// </summary>
public class TotaisPorPessoaDto
{
    /// <summary>Identificador único da pessoa.</summary>
    public int PessoaId { get; set; }

    /// <summary>Nome da pessoa.</summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>Idade da pessoa.</summary>
    public int Idade { get; set; }

    /// <summary>Soma de todas as receitas cadastradas para esta pessoa.</summary>
    public decimal TotalReceitas { get; set; }

    /// <summary>Soma de todas as despesas cadastradas para esta pessoa.</summary>
    public decimal TotalDespesas { get; set; }

    /// <summary>Saldo líquido da pessoa (TotalReceitas - TotalDespesas).</summary>
    public decimal Saldo { get; set; }
}
