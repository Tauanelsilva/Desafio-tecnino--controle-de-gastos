namespace ControleGastos.Api.DTOs;

/// <summary>
/// DTO (Data Transfer Object) utilizado para retornar os dados de uma transação.
/// Inclui dados auxiliares como o nome da pessoa associada para facilitar a exibição no frontend.
/// </summary>
public class TransacaoDto
{
    /// <summary>ID da transação.</summary>
    public int Id { get; set; }

    /// <summary>Descrição da transação.</summary>
    public string Descricao { get; set; } = string.Empty;

    /// <summary>Valor da transação.</summary>
    public decimal Valor { get; set; }

    /// <summary>Tipo da transação (texto formatado: "Receita" ou "Despesa").</summary>
    public string Tipo { get; set; } = string.Empty;

    /// <summary>ID da pessoa associada.</summary>
    public int PessoaId { get; set; }

    /// <summary>Nome da pessoa associada (evita join extra no frontend).</summary>
    public string PessoaNome { get; set; } = string.Empty;
}
