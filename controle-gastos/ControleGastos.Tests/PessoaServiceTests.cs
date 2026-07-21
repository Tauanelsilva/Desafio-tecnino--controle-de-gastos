using ControleGastos.Api.DTOs;
using ControleGastos.Api.Exceptions;
using ControleGastos.Api.Models;
using ControleGastos.Api.Repositories;
using ControleGastos.Api.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ControleGastos.Tests;

/// <summary>
/// Testes unitários para o PessoaService, validando as operações de
/// criação, listagem, busca por ID e exclusão de pessoas.
/// </summary>
public class PessoaServiceTests
{
    private readonly Mock<IPessoaRepository> _pessoaRepoMock;
    private readonly Mock<ILogger<PessoaService>> _loggerMock;
    private readonly PessoaService _service;

    public PessoaServiceTests()
    {
        _pessoaRepoMock = new Mock<IPessoaRepository>();
        _loggerMock = new Mock<ILogger<PessoaService>>();
        _service = new PessoaService(_pessoaRepoMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateAsync_DadosValidos_DeveRetornarPessoaCriada()
    {
        // Arrange
        var dto = new CreatePessoaDto { Nome = "Ana", Idade = 25 };
        var pessoaSalva = new Pessoa { Id = 1, Nome = "Ana", Idade = 25 };
        _pessoaRepoMock.Setup(r => r.AddAsync(It.IsAny<Pessoa>())).ReturnsAsync(pessoaSalva);

        // Act
        var result = await _service.CreateAsync(dto);

        // Assert
        Assert.Equal(1, result.Id);
        Assert.Equal("Ana", result.Nome);
        Assert.Equal(25, result.Idade);
        _pessoaRepoMock.Verify(r => r.AddAsync(It.IsAny<Pessoa>()), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_DeveRetornarTodasAsPessoas()
    {
        // Arrange
        var pessoas = new List<Pessoa>
        {
            new() { Id = 1, Nome = "Ana", Idade = 25 },
            new() { Id = 2, Nome = "Bruno", Idade = 40 }
        };
        _pessoaRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(pessoas);

        // Act
        var result = (await _service.GetAllAsync()).ToList();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, p => p.Nome == "Ana");
        Assert.Contains(result, p => p.Nome == "Bruno");
    }

    [Fact]
    public async Task GetByIdAsync_PessoaExiste_DeveRetornarPessoa()
    {
        // Arrange
        var pessoa = new Pessoa { Id = 1, Nome = "Ana", Idade = 25 };
        _pessoaRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(pessoa);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.Equal(1, result.Id);
        Assert.Equal("Ana", result.Nome);
    }

    [Fact]
    public async Task GetByIdAsync_PessoaNaoExiste_DeveLancarNotFoundException()
    {
        // Arrange
        _pessoaRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Pessoa)null!);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.GetByIdAsync(99));
    }

    [Fact]
    public async Task DeleteAsync_PessoaExiste_DeveChamarDeleteNoRepositorio()
    {
        // Arrange
        var pessoa = new Pessoa { Id = 1, Nome = "Ana", Idade = 25 };
        _pessoaRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(pessoa);

        // Act
        await _service.DeleteAsync(1);

        // Assert
        _pessoaRepoMock.Verify(r => r.DeleteAsync(1), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_PessoaNaoExiste_DeveLancarNotFoundException()
    {
        // Arrange
        _pessoaRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Pessoa)null!);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(99));
        _pessoaRepoMock.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Never);
    }
}
