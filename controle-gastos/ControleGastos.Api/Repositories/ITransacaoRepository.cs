using ControleGastos.Api.Models;

namespace ControleGastos.Api.Repositories;

public interface ITransacaoRepository
{
    Task<IEnumerable<Transacao>> GetAllAsync();
    Task<Transacao> AddAsync(Transacao transacao);
}
