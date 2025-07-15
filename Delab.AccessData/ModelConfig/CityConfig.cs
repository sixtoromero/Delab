using Delab.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Delab.AccessData.ModelConfig
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(e => e.CityId);
            //Index Compuesto, Cuando una Ciudad tienen el mismo nombre en diferentes estados.
            builder.HasIndex(e => new { e.Name, e.StateId }).IsUnique();
            //Para evitar borrar en cascada.
            builder.HasOne(e => e.State).WithMany(e => e.Cities).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
