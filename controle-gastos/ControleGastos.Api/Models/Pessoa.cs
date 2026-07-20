using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Api.Models;

public class Pessoa
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [Range(1, 150)]
    public int Idade { get; set; }

    public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
}
