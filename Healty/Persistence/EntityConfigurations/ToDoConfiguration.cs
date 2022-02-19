using Healty.Core.Models;
using System.Data.Entity.ModelConfiguration;

namespace Healty.Persistence.EntityConfigurations
{
    public class ToDoConfiguration : EntityTypeConfiguration<Todo>
    {
        public ToDoConfiguration()
        {
            Property(t => t.ApplicationUserId)
                 .IsRequired().HasMaxLength(128);
            Property(t => t.Title).IsRequired().HasMaxLength(150);
        }
    }
}