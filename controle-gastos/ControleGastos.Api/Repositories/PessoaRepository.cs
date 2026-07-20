using ControleGastos.Api.Data;
using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly AppDbContext _context;

    public PessoaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pessoa>> GetAllAsync()
    {
        return await _context.Pessoas.ToListAsync();
    }

    public async Task<Pessoa?> GetByIdAsync(int id)
    {
        return await _context.Pessoas.FindAsync(id);
    }

    public async Task<Pessoa> AddAsync(Pessoa pessoa)
    {
        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();
        return pessoa;
    }

    public async Task DeleteAsync(int id)
    {
        var pessoa = await _context.Pessoas
            .Include(p => p.Transacoes)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pessoa != null)
        {
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
        }
    }
}
