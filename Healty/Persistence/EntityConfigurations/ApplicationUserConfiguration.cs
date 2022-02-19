using Healty.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace Healty.Persistence.EntityConfigurations
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            Property(u => u.DisplayName).IsRequired().HasMaxLength(100);

            HasMany(u => u.Todos)
                .WithRequired(t => t.ApplicationUser)
                .HasForeignKey(u => u.ApplicationUserId)

                .WillCascadeOnDelete(false)

                ;
        }
    }
}