using ControleGastos.Api.DTOs;

namespace ControleGastos.Api.Services;

/// <summary>
/// Interface para o serviço de transações.
/// Contém a lógica de negócio relacionada à entidade Transacao.
/// </summary>
public interface ITransacaoService
{
    /// <summary>Retorna todas as transações em formato DTO.</summary>
    Task<IEnumerable<TransacaoDto>> GetAllAsync();
    
    /// <summary>Cria uma nova transação validando as regras de negócio.</summary>
    Task<TransacaoDto> CreateAsync(CreateTransacaoDto dto);
    
    /// <summary>Calcula e retorna os totais financeiros por pessoa e o total geral do sistema.</summary>
    Task<TotaisDto> GetTotaisAsync();
}
