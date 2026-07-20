using ControleGastos.Api.DTOs;
using ControleGastos.Api.Exceptions;
using ControleGastos.Api.Models;
using ControleGastos.Api.Repositories;

namespace ControleGastos.Api.Services;

/// <summary>
/// Implementação do serviço de pessoas, responsável pelas regras de negócio.
/// </summary>
public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _pessoaRepository;

    public PessoaService(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<PessoaDto>> GetAllAsync()
    {
        var pessoas = await _pessoaRepository.GetAllAsync();
        return pessoas.Select(p => new PessoaDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Idade = p.Idade
        });
    }
    
    /// <inheritdoc/>
    public async Task<PessoaDto> GetByIdAsync(int id)
    {
        var pessoa = await _pessoaRepository.GetByIdAsync(id);
        if (pessoa == null)
        {
            throw new NotFoundException($"Pessoa com ID {id} não encontrada.");
        }
        
        return new PessoaDto
        {
            Id = pessoa.Id,
            Nome = pessoa.Nome,
            Idade = pessoa.Idade
        };
    }

    /// <inheritdoc/>
    public async Task<PessoaDto> CreateAsync(CreatePessoaDto dto)
    {
        var pessoa = new Pessoa
        {
            Nome = dto.Nome,
            Idade = dto.Idade
        };

        var result = await _pessoaRepository.AddAsync(pessoa);

        return new PessoaDto
        {
            Id = result.Id,
            Nome = result.Nome,
            Idade = result.Idade
        };
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id)
    {
        var pessoa = await _pessoaRepository.GetByIdAsync(id);
        if (pessoa == null)
        {
            throw new NotFoundException($"Pessoa com ID {id} não encontrada para exclusão.");
        }
        
        await _pessoaRepository.DeleteAsync(id);
    }
}
