using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.Entidades.MicrosoftIdentity;

namespace CostAndProductConsolidator.Datos.EntityConfigurations.MicrosoftIdentity
{
    public class UserClamConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable(nameof(UserClaim));
        }
    }
}
