using ControleGastos.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Api.Data;

/// <summary>
/// Contexto de banco de dados da aplicação utilizando Entity Framework Core.
/// Gerencia as entidades Pessoa e Transacao, configurando seus relacionamentos
/// e mapeamentos para o banco de dados SQLite.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    /// <summary>
    /// Tabela de pessoas cadastradas no sistema.
    /// </summary>
    public DbSet<Pessoa> Pessoas { get; set; }

    /// <summary>
    /// Tabela de transações financeiras (receitas e despesas).
    /// </summary>
    public DbSet<Transacao> Transacoes { get; set; }

    /// <summary>
    /// Configura o modelo de dados e os relacionamentos entre as entidades.
    /// - Relacionamento 1:N entre Pessoa e Transacao com delete em cascata.
    /// - Conversão do enum TipoTransacao para inteiro no banco de dados.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relacionamento Pessoa -> Transacao (1:N)
        // Quando uma pessoa é excluída, todas as suas transações são removidas automaticamente
        modelBuilder.Entity<Pessoa>()
            .HasMany(p => p.Transacoes)
            .WithOne(t => t.Pessoa)
            .HasForeignKey(t => t.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Armazena o enum TipoTransacao como inteiro no banco (1 = Receita, 2 = Despesa)
        modelBuilder.Entity<Transacao>()
            .Property(t => t.Tipo)
            .HasConversion<int>();
    }
}
