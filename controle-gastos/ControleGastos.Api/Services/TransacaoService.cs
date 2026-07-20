using ControleGastos.Api.DTOs;
using ControleGastos.Api.Exceptions;
using ControleGastos.Api.Models;
using ControleGastos.Api.Repositories;

namespace ControleGastos.Api.Services;

/// <summary>
/// Implementação do serviço de transações, responsável pelas regras de negócio.
/// </summary>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public async Task<TransacaoDto> CreateAsync(CreateTransacaoDto dto)
    {
        // Regra de negócio 1: A pessoa informada deve existir
        var pessoa = await _pessoaRepository.GetByIdAsync(dto.PessoaId);
        if (pessoa == null)
        {
            throw new NotFoundException("A pessoa referenciada não existe.");
        }

        // Regra de negócio 2: Menores de 18 anos só podem cadastrar despesas
        if (pessoa.Idade < 18 && dto.Tipo == (int)TipoTransacao.Receita)
        {
            throw new BusinessRuleException("Menores de 18 anos só podem cadastrar despesas.");
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

    /// <inheritdoc/>
    public async Task<TotaisDto> GetTotaisAsync()
    {
        var transacoes = await _transacaoRepository.GetAllAsync();
        var pessoas = await _pessoaRepository.GetAllAsync();

        var totaisDto = new TotaisDto();

        // Agrupa as transações por pessoa e calcula os totais de cada uma (Regra de Negócio principal)
        foreach (var pessoa in pessoas)
        {
            var transacoesDaPessoa = transacoes.Where(t => t.PessoaId == pessoa.Id).ToList();
            
            var totalReceitasPessoa = transacoesDaPessoa
                .Where(t => t.Tipo == TipoTransacao.Receita)
                .Sum(t => t.Valor);
                
            var totalDespesasPessoa = transacoesDaPessoa
                .Where(t => t.Tipo == TipoTransacao.Despesa)
                .Sum(t => t.Valor);
                
            totaisDto.Pessoas.Add(new TotaisPorPessoaDto
            {
                PessoaId = pessoa.Id,
                Nome = pessoa.Nome,
                Idade = pessoa.Idade,
                TotalReceitas = totalReceitasPessoa,
                TotalDespesas = totalDespesasPessoa,
                Saldo = totalReceitasPessoa - totalDespesasPessoa
            });
        }

        // Calcula os totais gerais somando os totais individuais
        totaisDto.TotalReceitasGeral = totaisDto.Pessoas.Sum(p => p.TotalReceitas);
        totaisDto.TotalDespesasGeral = totaisDto.Pessoas.Sum(p => p.TotalDespesas);
        totaisDto.SaldoGeral = totaisDto.TotalReceitasGeral - totaisDto.TotalDespesasGeral;

        return totaisDto;
    }
}
