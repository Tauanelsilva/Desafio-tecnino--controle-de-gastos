using ControleGastos.Api.Models;

namespace ControleGastos.Api.Repositories;

/// <summary>
/// Interface para o repositório de pessoas.
/// Define os métodos de acesso a dados para a entidade Pessoa.
/// </summary>
public interface IPessoaRepository
{
    /// <summary>Retorna todas as pessoas cadastradas.</summary>
    Task<IEnumerable<Pessoa>> GetAllAsync();

    /// <summary>Busca uma pessoa pelo seu identificador único.</summary>
    Task<Pessoa?> GetByIdAsync(int id);

    /// <summary>Adiciona uma nova pessoa ao banco de dados.</summary>
    Task<Pessoa> AddAsync(Pessoa pessoa);

    /// <summary>Remove uma pessoa do banco de dados (exclusão em cascata das transações).</summary>
    Task DeleteAsync(int id);
}
