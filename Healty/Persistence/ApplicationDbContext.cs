using System.Data.Entity;
using Healty.Core.Models;
using Healty.Persistence.EntityConfigurations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Healty.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>//DbContext
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Todo> Todos { get; set; }
        //public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUserConfiguration());
            modelBuilder.Configurations.Add(new ToDoConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

}