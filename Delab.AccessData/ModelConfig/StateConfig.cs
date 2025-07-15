using Delab.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delab.AccessData.ModelConfig;

public class StateConfig : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasKey(e => e.StateId);
        //Index Compuesto, Cuando un Stado tienen el mismo nombre en diferentes paises.
        builder.HasIndex(e => new { e.Name, e.CountryId }).IsUnique();
        //Para evitar borrar en cascada.
        builder.HasOne(e => e.Country).WithMany(e => e.States).OnDelete(DeleteBehavior.Restrict);

    }
}
