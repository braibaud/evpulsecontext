using Microsoft.EntityFrameworkCore;
using EVPulseContext.Objects;

namespace EVPulseContext
{
    public partial class DataContext : DbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity>(
                entity =>
                {
                    entity
                        .HasMany(e => e.RelatedEntities)
                        .WithMany()
                        .UsingEntity<Dictionary<string, object>>(
                            "RelatedEntity",
                            r => r.HasOne<Entity>()
                                .WithMany()
                                .HasForeignKey("RelatedEntityId", "RelatedEntityTypeId")
                                .HasConstraintName("fk_related_entity_related_entity")
                                .OnDelete(DeleteBehavior.Restrict),
                            l => l.HasOne<Entity>()
                                .WithMany()
                                .HasForeignKey("EntityId", "EntityTypeId")
                                .HasConstraintName("fk_related_entity_entity")
                                .OnDelete(DeleteBehavior.Restrict),
                            j =>
                            {
                                j.HasKey("EntityId", "EntityTypeId", "RelatedEntityId", "RelatedEntityTypeId");
                                j.ToTable("related_entity", "dbo");
                            }
                        );
                });
        }

        public static object GetDbParamValue(object? value)
        {
            return value ?? DBNull.Value;
        }

        public static Npgsql.NpgsqlParameter GetDbParam(string name, object? value)
        {
            return new Npgsql.NpgsqlParameter(name, GetDbParamValue(value));
        }

        private static string? _connectionString = null;

        public static string? ConnectionString
        {
            set
            {
                _connectionString = value;
            }
        }

        public static DataContext GetDefaultContext()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("Connection string is not set.");
            }

            return new DataContext(
                new DbContextOptionsBuilder<DataContext>()
                    .UseNpgsql(_connectionString)
                    .Options);
        }
    }
}
