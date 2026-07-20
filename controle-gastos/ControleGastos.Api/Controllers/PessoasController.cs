using ControleGastos.Api.DTOs;
using ControleGastos.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace ControleGastos.Api.Controllers;

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
    [HttpPost]
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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PessoaDto>>> GetAll()
    {
        var pessoas = await _pessoaService.GetAllAsync();
        return Ok(pessoas);
    }

    /// <summary>
    /// Retorna uma pessoa pelo ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<PessoaDto>> GetById(int id)
    {
        var pessoas = await _pessoaService.GetAllAsync();
        var pessoa = pessoas.FirstOrDefault(p => p.Id == id);

        if (pessoa == null)
        {
            return NotFound();
        }

        return Ok(pessoa);
    }

    /// <summary>
    /// Exclui uma pessoa e todas as suas transações (cascade delete).
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _pessoaService.DeleteAsync(id);
        return NoContent();
    }
}
