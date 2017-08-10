using System;

using Microsoft.EntityFrameworkCore;

namespace EFCoreNullable
{
    public class DatabaseContext : DbContext
    {
        private string path = "";

        public DatabaseContext(string path)
        {
            this.path = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			AddTable<Foo>(modelBuilder, nameof(Foo));
            AddTable<Bar>(modelBuilder, nameof(Bar));
            AddTable<BigFoo>(modelBuilder, nameof(BigFoo));
			AddTable<TenFields>(modelBuilder, nameof(TenFields));
			AddTable<FifteenFields>(modelBuilder, nameof(FifteenFields));
            AddTable<TwentyFields>(modelBuilder, nameof(TwentyFields));
        }

        private void AddTable<T>(ModelBuilder modelBuilder, string name)
            where T : class
		{
			modelBuilder
				.Entity<T>()
				.ToTable(name);
        }
    }
}
