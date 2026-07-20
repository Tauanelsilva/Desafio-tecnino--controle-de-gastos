namespace ControleGastos.Api.DTOs;

/// <summary>
/// DTO (Data Transfer Object) utilizado para retornar os dados de uma pessoa.
/// Protege a entidade do banco de dados evitando expor campos desnecessários.
/// </summary>
public class PessoaDto
{
    /// <summary>ID da pessoa.</summary>
    public int Id { get; set; }
    
    /// <summary>Nome da pessoa.</summary>
    public string Nome { get; set; } = string.Empty;
    
    /// <summary>Idade da pessoa.</summary>
    public int Idade { get; set; }
}
