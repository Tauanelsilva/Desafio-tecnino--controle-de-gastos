using ControleGastos.Api.DTOs;
using ControleGastos.Api.Exceptions;
using ControleGastos.Api.Models;
using ControleGastos.Api.Repositories;
using ControleGastos.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ControleGastos.Tests;

public class TransacaoServiceTests
{
    private readonly Mock<ITransacaoRepository> _transacaoRepoMock;
    private readonly Mock<IPessoaRepository> _pessoaRepoMock;
    private readonly Mock<ILogger<TransacaoService>> _loggerMock;
    private readonly TransacaoService _service;

    public TransacaoServiceTests()
    {
        _transacaoRepoMock = new Mock<ITransacaoRepository>();
        _pessoaRepoMock = new Mock<IPessoaRepository>();
        _loggerMock = new Mock<ILogger<TransacaoService>>();

        _service = new TransacaoService(
            _transacaoRepoMock.Object,
            _pessoaRepoMock.Object,
            _loggerMock.Object);
    }

    [Fact]
    public async Task CreateAsync_PessoaNaoExiste_DeveLancarNotFoundException()
    {
        // Arrange
        var dto = new CreateTransacaoDto { PessoaId = 99, Tipo = 1, Valor = 100, Descricao = "Teste" };
        _pessoaRepoMock.Setup(r => r.GetByIdAsync(dto.PessoaId)).ReturnsAsync((Pessoa)null!);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.CreateAsync(dto));
    }

    [Fact]
    public async Task CreateAsync_PessoaMenorDeIdade_CriandoReceita_DeveLancarBusinessRuleException()
    {
        // Arrange
        var pessoa = new Pessoa { Id = 1, Nome = "Menor", Idade = 17 };
        var dto = new CreateTransacaoDto { PessoaId = 1, Tipo = 1 /* Receita */, Valor = 100, Descricao = "Mesada" };
        
        _pessoaRepoMock.Setup(r => r.GetByIdAsync(dto.PessoaId)).ReturnsAsync(pessoa);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<BusinessRuleException>(() => _service.CreateAsync(dto));
        Assert.Equal("Menores de 18 anos só podem cadastrar despesas.", ex.Message);
    }

    [Fact]
    public async Task CreateAsync_PessoaMaiorDeIdade_CriandoReceita_DeveCriarComSucesso()
    {
        // Arrange
        var pessoa = new Pessoa { Id = 1, Nome = "Maior", Idade = 18 };
        var dto = new CreateTransacaoDto { PessoaId = 1, Tipo = 1 /* Receita */, Valor = 100, Descricao = "Salário" };
        var transacaoSalva = new Transacao { Id = 10, PessoaId = 1, Tipo = TipoTransacao.Receita, Valor = 100, Descricao = "Salário" };
        
        _pessoaRepoMock.Setup(r => r.GetByIdAsync(dto.PessoaId)).ReturnsAsync(pessoa);
        _transacaoRepoMock.Setup(r => r.AddAsync(It.IsAny<Transacao>())).ReturnsAsync(transacaoSalva);

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(10, result.Id);
        Assert.Equal("Receita", result.Tipo);
        _transacaoRepoMock.Verify(r => r.AddAsync(It.IsAny<Transacao>()), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_PessoaMenorDeIdade_CriandoDespesa_DeveCriarComSucesso()
    {
        // Arrange
        var pessoa = new Pessoa { Id = 1, Nome = "Menor", Idade = 15 };
        var dto = new CreateTransacaoDto { PessoaId = 1, Tipo = 2 /* Despesa */, Valor = 50, Descricao = "Lanche" };
        var transacaoSalva = new Transacao { Id = 11, PessoaId = 1, Tipo = TipoTransacao.Despesa, Valor = 50, Descricao = "Lanche" };
        
        _pessoaRepoMock.Setup(r => r.GetByIdAsync(dto.PessoaId)).ReturnsAsync(pessoa);
        _transacaoRepoMock.Setup(r => r.AddAsync(It.IsAny<Transacao>())).ReturnsAsync(transacaoSalva);

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(11, result.Id);
        Assert.Equal("Despesa", result.Tipo);
    }

    [Fact]
    public async Task GetTotaisAsync_DeveCalcularTotaisPorPessoaEGeral()
    {
        // Arrange
        var pessoas = new List<Pessoa>
        {
            new() { Id = 1, Nome = "João", Idade = 30 },
            new() { Id = 2, Nome = "Maria", Idade = 17 }
        };

        var transacoes = new List<Transacao>
        {
            new() { Id = 1, PessoaId = 1, Tipo = TipoTransacao.Receita, Valor = 1000, Descricao = "Salário" },
            new() { Id = 2, PessoaId = 1, Tipo = TipoTransacao.Despesa, Valor = 200, Descricao = "Conta" },
            new() { Id = 3, PessoaId = 2, Tipo = TipoTransacao.Despesa, Valor = 50, Descricao = "Lanche" }
        };

        _pessoaRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(pessoas);
        _transacaoRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(transacoes);

        // Act
        var result = await _service.GetTotaisAsync();

        // Assert
        Assert.Equal(2, result.Pessoas.Count);

        var joao = result.Pessoas.First(p => p.PessoaId == 1);
        Assert.Equal(1000, joao.TotalReceitas);
        Assert.Equal(200, joao.TotalDespesas);
        Assert.Equal(800, joao.Saldo);

        var maria = result.Pessoas.First(p => p.PessoaId == 2);
        Assert.Equal(0, maria.TotalReceitas);
        Assert.Equal(50, maria.TotalDespesas);
        Assert.Equal(-50, maria.Saldo);

        Assert.Equal(1000, result.TotalReceitasGeral);
        Assert.Equal(250, result.TotalDespesasGeral);
        Assert.Equal(750, result.SaldoGeral);
    }
}
