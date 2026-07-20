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
    private readonly ILogger<PessoaService> _logger;

    public PessoaService(IPessoaRepository pessoaRepository, ILogger<PessoaService> logger)
    {
        _pessoaRepository = pessoaRepository;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<PessoaDto>> GetAllAsync()
    {
        _logger.LogInformation("Buscando todas as pessoas no banco de dados.");
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
        _logger.LogInformation("Buscando pessoa pelo ID: {Id}", id);
        var pessoa = await _pessoaRepository.GetByIdAsync(id);
        if (pessoa == null)
        {
            _logger.LogWarning("Pessoa com ID {Id} não encontrada.", id);
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
        _logger.LogInformation("Cadastrando nova pessoa: {Nome}", dto.Nome);
        
        var pessoa = new Pessoa
        {
            Nome = dto.Nome,
            Idade = dto.Idade
        };

        var result = await _pessoaRepository.AddAsync(pessoa);
        
        _logger.LogInformation("Pessoa {Nome} (ID: {Id}) cadastrada com sucesso.", result.Nome, result.Id);

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
        _logger.LogInformation("Solicitada exclusão da pessoa ID: {Id}", id);
        
        var pessoa = await _pessoaRepository.GetByIdAsync(id);
        if (pessoa == null)
        {
            _logger.LogWarning("Falha na exclusão: Pessoa com ID {Id} não encontrada.", id);
            throw new NotFoundException($"Pessoa com ID {id} não encontrada para exclusão.");
        }
        
        await _pessoaRepository.DeleteAsync(id);
        _logger.LogInformation("Pessoa ID: {Id} excluída com sucesso.", id);
    }
}
