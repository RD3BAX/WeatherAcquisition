using Microsoft.EntityFrameworkCore;
using WeatherAcquisition.DAL.Entities;

namespace WeatherAcquisition.DAL.Context
{
    public class DataDB : DbContext
    {
        public DbSet<DataValue> Values { get; set; }

        public DbSet<DataSource> Sources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DataSource>()
                .HasMany<DataValue>()
                .WithOne(v => v.Source)
                .OnDelete(DeleteBehavior.Cascade);

            //// Индексировать колонку Name и контролировать уникальность значений
            //modelBuilder.Entity<DataSource>()
            //    .HasIndex(source => source.Name)
            //    .IsUnique(true);
        }

        #region Конструктор

        public DataDB(DbContextOptions<DataDB> options) :base(options) { }

        #endregion // Конструктор
    }
}
