using ApiAuth.models;
using Data.Mapping;
using Microsoft.EntityFrameworkCore;

namespace ApiAuth.Context
{
    public class MyContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMapping());
        }
    }
}
