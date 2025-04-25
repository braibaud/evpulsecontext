using Microsoft.EntityFrameworkCore;
using EVPulseContext.Objects;

namespace EVPulseContext
{
    public partial class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Objects.Attribute> Attributes { get; set; }

        public virtual DbSet<Entity> Entities { get; set; }

        public virtual DbSet<EntityAttribute> EntityAttributes { get; set; }

        public virtual DbSet<EntityFeature> EntityFeatures { get; set; }

        public virtual DbSet<EntityType> EntityTypes { get; set; }

        public virtual DbSet<Feature> Features { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<Vehicle> Vehicles { get; set; }

        public virtual DbSet<VehicleAttribute> VehicleAttributes { get; set; }

        public virtual DbSet<VehicleFeature> VehicleFeatures { get; set; }

        public virtual DbSet<VehiclePart> VehicleParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Objects.Attribute>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pk_attribute");

                entity.ToTable("attribute", "dbo");

                entity.HasIndex(e => e.GroupId, "ix_attribute_group_id");

                entity.HasIndex(e => e.Name, "uk_attribute_name").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.DefaultUnit).HasColumnName("default_unit");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.GroupId).HasColumnName("group_id");
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");
                entity.Property(e => e.IsInheritable)
                    .HasDefaultValue(true)
                    .HasColumnName("is_inheritable");
                entity.Property(e => e.IsOverridable)
                    .HasDefaultValue(true)
                    .HasColumnName("is_overridable");
                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasOne(d => d.Group).WithMany(p => p.Attributes)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_attribute_group_group");
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity
                    .HasKey("Id", "EntityTypeId")
                    .HasName("pk_entity");

                entity
                    .ToTable("entity", "dbo");

                entity
                    .HasIndex(e => e.ParentEntityTypeId, "ix_entity_parent_entity_type_id");

                entity
                    .HasIndex(e => e.ParentId, "ix_entity_parent_id");

                entity
                    .HasIndex(["Name", "EntityTypeId"], "ux_entity_name_entity_type_id")
                    .IsUnique();

                entity
                    .Property(e => e.Id)
                    .HasColumnName("id");

                entity
                    .Property(e => e.EntityTypeId)
                    .HasColumnName("entity_type_id");

                entity
                    .Property(e => e.Description)
                    .HasColumnName("description");

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");

                entity
                    .Property(e => e.IsVirtual)
                    .HasDefaultValue(true)
                    .HasColumnName("is_virtual");

                entity
                    .Property(e => e.Name)
                    .HasColumnName("name");

                entity
                    .Property(e => e.ParentEntityTypeId)
                    .HasColumnName("parent_entity_type_id");

                entity
                    .Property(e => e.ParentId)
                    .HasColumnName("parent_id");

                entity
                    .HasOne(d => d.EntityType)
                    .WithMany(p => p.Entities)
                    .HasForeignKey(d => d.EntityTypeId)
                    .HasConstraintName("fk_entity_entity_type");

                entity
                    .HasOne(d => d.EntityNavigation)
                    .WithMany(p => p.InverseEntityNavigation)
                    .HasForeignKey("ParentId", "ParentEntityTypeId")
                    .HasConstraintName("fk_entity_parent_key_entity");
            });

            modelBuilder.Entity<EntityAttribute>(entity =>
            {
                entity.HasKey(e => new { e.EntityId, e.EntityTypeId, e.AttributeId }).HasName("pk_entity_attribute");

                entity.ToTable("entity_attribute", "dbo");

                entity.HasIndex(e => e.AttributeId, "ix_entity_attribute_attribute_id");

                entity.HasIndex(e => e.EntityId, "ix_entity_attribute_entity_id");

                entity.HasIndex(e => e.EntityTypeId, "ix_entity_attribute_entity_type_id");

                entity.Property(e => e.EntityId).HasColumnName("entity_id");
                entity.Property(e => e.EntityTypeId).HasColumnName("entity_type_id");
                entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");
                entity.Property(e => e.Unit).HasColumnName("unit");
                entity.Property(e => e.Value).HasColumnName("value");

                entity
                    .HasOne(d => d.Attribute)
                    .WithMany(p => p.EntityAttributes)
                    .HasForeignKey(d => d.AttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_entity_attribute_attribute_attribute");

                entity
                    .HasOne(d => d.Entity)
                    .WithMany(p => p.EntityAttributes)
                    .HasForeignKey(d => new { d.EntityId, d.EntityTypeId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_entity_attribute_entity_entity");
            });

            modelBuilder.Entity<EntityFeature>(entity =>
            {
                entity.HasKey(e => new { e.EntityId, e.EntityTypeId, e.FeatureId }).HasName("pk_entity_feature");

                entity.ToTable("entity_feature", "dbo");

                entity.HasIndex(e => e.EntityId, "ix_entity_feature_entity_id");

                entity.HasIndex(e => e.EntityTypeId, "ix_entity_feature_entity_type_id");

                entity.HasIndex(e => e.FeatureId, "ix_entity_feature_feature_id");

                entity.Property(e => e.EntityId).HasColumnName("entity_id");
                entity.Property(e => e.EntityTypeId).HasColumnName("entity_type_id");
                entity.Property(e => e.FeatureId).HasColumnName("feature_id");
                entity.Property(e => e.Currency).HasColumnName("currency");
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");
                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.Feature).WithMany(p => p.EntityFeatures)
                    .HasForeignKey(d => d.FeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_entity_feature_feature_feature");

                entity.HasOne(d => d.Entity).WithMany(p => p.EntityFeatures)
                    .HasForeignKey(d => new { d.EntityId, d.EntityTypeId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_entity_feature_entity_entity");
            });

            modelBuilder.Entity<EntityType>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pk_entity_type");

                entity.ToTable("entity_type", "dbo");

                entity.HasIndex(e => e.Name, "uk_entity_type_name").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");
                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Feature>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pk_feature");

                entity.ToTable("feature", "dbo");

                entity.HasIndex(e => e.GroupId, "ix_feature_group_id");

                entity.HasIndex(e => e.Name, "uk_feature_name").IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.DefaultCurrency).HasColumnName("default_currency");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.GroupId).HasColumnName("group_id");
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");
                entity.Property(e => e.IsInheritable)
                    .HasDefaultValue(true)
                    .HasColumnName("is_inheritable");
                entity.Property(e => e.IsOverridable)
                    .HasDefaultValue(true)
                    .HasColumnName("is_overridable");
                entity.Property(e => e.Name).HasColumnName("name");

                entity.HasOne(d => d.Group).WithMany(p => p.Features)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_feature_group_group");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("pk_group");

                entity.ToTable("group", "dbo");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");
                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.HasKey(e => e.VehicleId).HasName("pk_vehicle");

                entity.ToTable("vehicle", "dbo");

                entity.HasIndex(e => e.Name, "ix_vehicle_name");

                entity.Property(e => e.VehicleId)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .HasColumnName("vehicle_id");
                entity.Property(e => e.Description).HasColumnName("description");
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");
                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<VehicleAttribute>(entity =>
            {
                entity.HasKey(e => new { e.VehicleId, e.AttributeId }).HasName("pk_vehicle_attribute");

                entity.ToTable("vehicle_attribute", "dbo");

                entity.HasIndex(e => e.VehicleId, "ix_vehicle_attribute_vehicle_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
                entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");
                entity.Property(e => e.Unit).HasColumnName("unit");
                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Attribute).WithMany(p => p.VehicleAttributes)
                    .HasForeignKey(d => d.AttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vehicle_attribute_attribute_id_attribute");

                entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleAttributes)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vehicle_attribute_vehicle_id_vehicle");
            });

            modelBuilder.Entity<VehicleFeature>(entity =>
            {
                entity.HasKey(e => new { e.VehicleId, e.FeatureId }).HasName("pk_vehicle_feature");

                entity.ToTable("vehicle_feature", "dbo");

                entity.HasIndex(e => e.FeatureId, "ix_vehicle_feature_feature_id");

                entity.HasIndex(e => e.VehicleId, "ix_vehicle_feature_vehicle_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
                entity.Property(e => e.FeatureId).HasColumnName("feature_id");
                entity.Property(e => e.Currency).HasColumnName("currency");
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");
                entity.Property(e => e.IsOptional)
                    .HasDefaultValue(true)
                    .HasColumnName("is_optional");
                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.Feature).WithMany(p => p.VehicleFeatures)
                    .HasForeignKey(d => d.FeatureId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vehicle_feature_feature_id_feature");

                entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleFeatures)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vehicle_feature_vehicle_id_vehicle");
            });

            modelBuilder.Entity<VehiclePart>(entity =>
            {
                entity.HasKey(e => new { e.VehicleId, e.EntityId, e.EntityTypeId }).HasName("pk_vehicle_part");

                entity.ToTable("vehicle_part", "dbo");

                entity.HasIndex(e => e.EntityId, "ix_vehicle_part_entity_id");

                entity.HasIndex(e => e.EntityTypeId, "ix_vehicle_part_entity_type_id");

                entity.HasIndex(e => e.VehicleId, "ix_vehicle_part_vehicle_id");

                entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
                entity.Property(e => e.EntityId).HasColumnName("entity_id");
                entity.Property(e => e.EntityTypeId).HasColumnName("entity_type_id");
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .HasColumnName("is_active");

                entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleParts)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vehicle_part_vehicle_id_vehicle");

                entity.HasOne(d => d.Entity).WithMany(p => p.VehicleParts)
                    .HasForeignKey(d => new { d.EntityId, d.EntityTypeId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_vehicle_part_entity_entity");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}