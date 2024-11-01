using DbAccess.Configure;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DbAccess.Context
{
    public class LogisticContext : DbContext
    {
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Settlement> Settlements { get; set; }

        public LogisticContext(DbContextOptions<LogisticContext> builder) : base(builder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CargoConfiguration());
            modelBuilder.ApplyConfiguration(new RouteConfiguration());
            modelBuilder.ApplyConfiguration(new SettlementConfiguration());
        }
    }
}
