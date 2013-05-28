using System.Data.Entity;
using _2013.March.Domain;

namespace _2013.March.Data
{
    public class MyDataContext : DbContext
    {
        // this is optional but gives you control
        public MyDataContext() : base("DevSessions"){} 

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(k => k.Id);

            //modelBuilder.Entity<Category>()
            //    .HasRequired(k => k.Name);
        }
    }
}
