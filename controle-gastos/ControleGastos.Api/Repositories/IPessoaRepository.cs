using ControleGastos.Api.Models;

namespace ControleGastos.Api.Repositories;

public interface IPessoaRepository
{
    Task<IEnumerable<Pessoa>> GetAllAsync();
    Task<Pessoa?> GetByIdAsync(int id);
    Task<Pessoa> AddAsync(Pessoa pessoa);
    Task DeleteAsync(int id);
}
