using ControleGastos.Api.DTOs;
using ControleGastos.Api.Models;
using ControleGastos.Api.Repositories;

namespace ControleGastos.Api.Services;

public class TransacaoService : ITransacaoService
{
    private readonly ITransacaoRepository _transacaoRepository;
    private readonly IPessoaRepository _pessoaRepository;

    public TransacaoService(
        ITransacaoRepository transacaoRepository,
        IPessoaRepository pessoaRepository)
    {
        _transacaoRepository = transacaoRepository;
        _pessoaRepository = pessoaRepository;
    }

    public async Task<IEnumerable<TransacaoDto>> GetAllAsync()
    {
        var transacoes = await _transacaoRepository.GetAllAsync();
        return transacoes.Select(t => new TransacaoDto
        {
            Id = t.Id,
            Descricao = t.Descricao,
            Valor = t.Valor,
            Tipo = t.Tipo.ToString(),
            PessoaId = t.PessoaId,
            PessoaNome = t.Pessoa?.Nome ?? string.Empty
        });
    }

    public async Task<TransacaoDto> CreateAsync(CreateTransacaoDto dto)
    {
        // Regra de negócio: Pessoa deve existir
        var pessoa = await _pessoaRepository.GetByIdAsync(dto.PessoaId);
        if (pessoa == null)
        {
            throw new Exception("A pessoa referenciada não existe.");
        }

        // Regra de negócio: Menores de 18 anos só podem cadastrar despesas
        if (pessoa.Idade < 18 && dto.Tipo == (int)TipoTransacao.Receita)
        {
            throw new Exception("Menores de 18 anos só podem cadastrar despesas.");
        }

        var transacao = new Transacao
        {
            Descricao = dto.Descricao,
            Valor = dto.Valor,
            Tipo = (TipoTransacao)dto.Tipo,
            PessoaId = dto.PessoaId
        };

        var result = await _transacaoRepository.AddAsync(transacao);

        return new TransacaoDto
        {
            Id = result.Id,
            Descricao = result.Descricao,
            Valor = result.Valor,
            Tipo = result.Tipo.ToString(),
            PessoaId = result.PessoaId,
            PessoaNome = pessoa.Nome
        };
    }

    public async Task<TotaisDto> GetTotaisAsync()
    {
        var transacoes = await _transacaoRepository.GetAllAsync();

        var totalReceitas = transacoes
            .Where(t => t.Tipo == TipoTransacao.Receita)
            .Sum(t => t.Valor);

        var totalDespesas = transacoes
            .Where(t => t.Tipo == TipoTransacao.Despesa)
            .Sum(t => t.Valor);

        return new TotaisDto
        {
            TotalReceitas = totalReceitas,
            TotalDespesas = totalDespesas,
            Saldo = totalReceitas - totalDespesas
        };
    }
}
