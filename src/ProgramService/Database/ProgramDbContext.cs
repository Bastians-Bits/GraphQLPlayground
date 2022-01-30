using System;
using Microsoft.EntityFrameworkCore;

namespace ProgramService.Database
{
	public class ProgramDbContext : DbContext
	{
        private ILoggerFactory _loggerFactory;

		public DbSet<Entity.ProgramEntity> Programs { get; set; }
        public DbSet<Entity.TestEntity> Tests { get; set; }

        public ProgramDbContext(
            DbContextOptions<ProgramDbContext> options,
            ILoggerFactory loggerFactory)
        : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entity.ProgramEntity>()
                .ToTable("Program");

            modelBuilder.Entity<Entity.VersionEntity>()
                .ToTable("Version");

            modelBuilder.Entity<Entity.TestEntity>()
                .ToTable("Test")
                .HasKey(k => new { k.TestPk1, k.TestPk2 });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .UseSqlite("Data Source=program_service.db")
                .EnableSensitiveDataLogging()
                .UseLoggerFactory(_loggerFactory);
        }
    }
}

