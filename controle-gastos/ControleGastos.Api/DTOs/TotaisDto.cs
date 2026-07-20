namespace ControleGastos.Api.DTOs;

/// <summary>
/// DTO (Data Transfer Object) que representa o resumo financeiro completo do sistema.
/// Contém o detalhamento por pessoa e os totais agregados de todos os cadastros,
/// cumprindo o requisito de exibir os totais individuais e o total geral.
/// </summary>
public class TotaisDto
{
    /// <summary>
    /// Lista contendo os totais (receitas, despesas e saldo) discriminados por cada pessoa cadastrada.
    /// </summary>
    public List<TotaisPorPessoaDto> Pessoas { get; set; } = new List<TotaisPorPessoaDto>();

    /// <summary>Soma geral de todas as receitas do sistema.</summary>
    public decimal TotalReceitasGeral { get; set; }

    /// <summary>Soma geral de todas as despesas do sistema.</summary>
    public decimal TotalDespesasGeral { get; set; }

    /// <summary>Saldo líquido geral do sistema (TotalReceitasGeral - TotalDespesasGeral).</summary>
    public decimal SaldoGeral { get; set; }
}
