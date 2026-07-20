using ControleGastos.Api.DTOs;
using ControleGastos.Api.Exceptions;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

/// <summary>
/// Controller responsável por gerenciar as operações de Pessoas.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PessoasController : ControllerBase
{
    private readonly IPessoaService _pessoaService;

    public PessoasController(IPessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    /// <summary>
    /// Cadastra uma nova pessoa.
    /// </summary>
    /// <param name="dto">Dados da pessoa a ser cadastrada.</param>
    /// <returns>A pessoa recém-cadastrada.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PessoaDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PessoaDto>> Create([FromBody] CreatePessoaDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var pessoa = await _pessoaService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = pessoa.Id }, pessoa);
    }

    /// <summary>
    /// Retorna todas as pessoas cadastradas.
    /// </summary>
    /// <returns>Lista de pessoas.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PessoaDto>))]
    public async Task<ActionResult<IEnumerable<PessoaDto>>> GetAll()
    {
        var pessoas = await _pessoaService.GetAllAsync();
        return Ok(pessoas);
    }

    /// <summary>
    /// Retorna uma pessoa específica pelo seu ID.
    /// </summary>
    /// <param name="id">ID da pessoa.</param>
    /// <returns>Dados da pessoa solicitada.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PessoaDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PessoaDto>> GetById(int id)
    {
        try
        {
            var pessoa = await _pessoaService.GetByIdAsync(id);
            return Ok(pessoa);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Exclui uma pessoa e todas as suas transações (cascade delete).
    /// </summary>
    /// <param name="id">ID da pessoa a ser excluída.</param>
    /// <returns>Sem conteúdo (204) em caso de sucesso.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _pessoaService.DeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
