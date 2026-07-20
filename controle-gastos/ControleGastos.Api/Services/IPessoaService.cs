using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Services;

/// <summary>
/// Interface para o serviço de pessoas.
/// Contém a lógica de negócio relacionada à entidade Pessoa.
/// </summary>
public interface IPessoaService
{
    /// <summary>Retorna todas as pessoas em formato DTO.</summary>
    Task<IEnumerable<PessoaDto>> GetAllAsync();
    
    /// <summary>Retorna uma pessoa pelo seu identificador único.</summary>
    Task<PessoaDto> GetByIdAsync(int id);
    
    /// <summary>Cria uma nova pessoa validando as regras de negócio.</summary>
    Task<PessoaDto> CreateAsync(CreatePessoaDto dto);
    
    /// <summary>Exclui uma pessoa e suas transações vinculadas.</summary>
    Task DeleteAsync(int id);
}
