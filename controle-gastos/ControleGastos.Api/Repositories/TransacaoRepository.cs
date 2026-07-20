using ControleGastos.Api.Data;
using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Repositories;

/// <summary>
/// Implementação do repositório de transações utilizando Entity Framework Core.
/// </summary>
public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;

    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Transacao>> GetAllAsync()
    {
        return await _context.Transacoes
            .Include(t => t.Pessoa)
            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<Transacao> AddAsync(Transacao transacao)
    {
        _context.Transacoes.Add(transacao);
        await _context.SaveChangesAsync();
        return transacao;
    }
}
