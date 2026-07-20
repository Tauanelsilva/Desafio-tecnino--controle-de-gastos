namespace ControleGastos.Api.DTOs;

public class TransacaoDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public int PessoaId { get; set; }
    public string PessoaNome { get; set; } = string.Empty;
}
