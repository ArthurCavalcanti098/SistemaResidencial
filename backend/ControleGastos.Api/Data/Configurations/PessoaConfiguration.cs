using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ControleGastos.Api.Models;

namespace ControleGastos.Api.Data.Configurations;

/// <summary>
/// Configuração da entidade Pessoa para o EF Core.
/// Tabela: pessoas, schema: public.
/// Constraints: PK UUID, Nome NOT NULL e CHECK length > 0, Idade CHECK 0-150.
/// </summary>
public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.ToTable("pessoas", t =>
        {
            t.HasCheckConstraint("CK_pessoas_nome_nao_vazio", "LENGTH(TRIM(\"Nome\")) > 0");
            t.HasCheckConstraint("CK_pessoas_idade_intervalo", "\"Idade\" >= 0 AND \"Idade\" <= 150");
        });

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Idade)
            .IsRequired();

        builder.HasMany(p => p.Transacoes)
            .WithOne(t => t.Pessoa)
            .HasForeignKey(t => t.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}