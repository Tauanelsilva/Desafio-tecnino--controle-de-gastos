using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

/// <summary>
/// Controller responsável por gerenciar as transações financeiras.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TransacoesController : ControllerBase
{
    private readonly ITransacaoService _transacaoService;

    public TransacoesController(ITransacaoService transacaoService)
    {
        _transacaoService = transacaoService;
    }

    /// <summary>
    /// Cadastra uma nova transação financeira.
    /// </summary>
    /// <param name="dto">Dados da transação.</param>
    /// <returns>A transação recém-cadastrada.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TransacaoDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransacaoDto>> Create([FromBody] CreateTransacaoDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Exceções como BusinessRuleException e NotFoundException 
        // serão capturadas pelo GlobalExceptionHandlerMiddleware
        var transacao = await _transacaoService.CreateAsync(dto);
        
        return CreatedAtAction(nameof(GetAll), transacao);
    }

    /// <summary>
    /// Retorna todas as transações cadastradas.
    /// </summary>
    /// <returns>Lista de transações.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TransacaoDto>))]
    public async Task<ActionResult<IEnumerable<TransacaoDto>>> GetAll()
    {
        var transacoes = await _transacaoService.GetAllAsync();
        return Ok(transacoes);
    }

    /// <summary>
    /// Retorna os totais financeiros (receitas, despesas e saldo) agrupados por pessoa e o total geral.
    /// </summary>
    /// <returns>Totais financeiros agrupados e totais gerais.</returns>
    [HttpGet("totais")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TotaisDto))]
    public async Task<ActionResult<TotaisDto>> GetTotais()
    {
        var totais = await _transacaoService.GetTotaisAsync();
        return Ok(totais);
    }
}
