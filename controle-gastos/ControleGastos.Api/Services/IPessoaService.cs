using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Services;

public interface IPessoaService
{
    Task<IEnumerable<PessoaDto>> GetAllAsync();
    Task<PessoaDto> CreateAsync(CreatePessoaDto dto);
    Task DeleteAsync(int id);
}
