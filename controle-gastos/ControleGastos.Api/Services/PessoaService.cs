using ControleGastos.Api.DTOs;
using ControleGastos.Api.Models;
using ControleGastos.Api.Repositories;

namespace ControleGastos.Api.Services;

public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _pessoaRepository;

    public PessoaService(IPessoaRepository pessoaRepository)
    {
        _pessoaRepository = pessoaRepository;
    }

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

    public async Task DeleteAsync(int id)
    {
        await _pessoaRepository.DeleteAsync(id);
    }
}
