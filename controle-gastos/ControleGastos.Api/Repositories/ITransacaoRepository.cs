using ControleGastos.Api.Models;

namespace ControleGastos.Api.Repositories;

/// <summary>
/// Interface para o repositório de transações.
/// Define os métodos de acesso a dados para a entidade Transacao.
/// </summary>
public interface ITransacaoRepository
{
    /// <summary>Retorna todas as transações cadastradas, incluindo os dados da pessoa associada.</summary>
    Task<IEnumerable<Transacao>> GetAllAsync();

    /// <summary>Adiciona uma nova transação ao banco de dados.</summary>
    Task<Transacao> AddAsync(Transacao transacao);
}
