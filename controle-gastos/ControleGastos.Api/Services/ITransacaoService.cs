using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Services;

public interface ITransacaoService
{
    Task<IEnumerable<TransacaoDto>> GetAllAsync();
    Task<TransacaoDto> CreateAsync(CreateTransacaoDto dto);
    Task<TotaisDto> GetTotaisAsync();
}
