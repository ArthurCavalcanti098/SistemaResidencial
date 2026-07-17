using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ControleGastos.Api.Models;

namespace ControleGastos.Api.Data.Configurations;

/// <summary>
/// Configuração da entidade Transacao para o EF Core.
/// Tabela: transacoes, schema: public.
/// Constraints: PK UUID, Valor > 0 CHECK, FK PessoaId ON DELETE CASCADE, índice em PessoaId.
/// </summary>
public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("transacoes", t =>
        {
            t.HasCheckConstraint("CK_transacoes_valor_positivo", "\"Valor\" > 0");
        });

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.Descricao)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(t => t.Valor)
            .IsRequired()
            .HasColumnType("numeric(18,2)");

        builder.Property(t => t.Tipo)
            .IsRequired()
            .HasConversion<int>();

        builder.HasOne(t => t.Pessoa)
            .WithMany(p => p.Transacoes)
            .HasForeignKey(t => t.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.PessoaId);
    }
}