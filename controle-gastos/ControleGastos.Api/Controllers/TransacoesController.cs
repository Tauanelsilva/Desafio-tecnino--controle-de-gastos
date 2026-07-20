using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

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
    /// Cadastra uma nova transação.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<TransacaoDto>> Create([FromBody] CreateTransacaoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var transacao = await _transacaoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), transacao);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Retorna todas as transações cadastradas.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TransacaoDto>>> GetAll()
    {
        var transacoes = await _transacaoService.GetAllAsync();
        return Ok(transacoes);
    }

    /// <summary>
    /// Retorna os totais financeiros (receitas, despesas e saldo).
    /// </summary>
    [HttpGet("totais")]
    public async Task<ActionResult<TotaisDto>> GetTotais()
    {
        var totais = await _transacaoService.GetTotaisAsync();
        return Ok(totais);
    }
}
