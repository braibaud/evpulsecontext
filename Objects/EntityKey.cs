using System.ComponentModel.DataAnnotations.Schema;

namespace EVPulseContext.Objects
{
    public class EntityKey
    {
        public Guid Id { get; set; }
        public int EntityTypeId { get; set; }

        public EntityKey(Guid id, int entityTypeId)
        {
            Id = id;
            EntityTypeId = entityTypeId;
        }

        public EntityType EntityType
        {
            get
            {
                using (DataContext context = DataContext.GetDefaultContext())
                {
                    EntityType? entityType = context.EntityTypes.Find(EntityTypeId);

                    if (entityType == null)
                    {
                        throw new InvalidOperationException($"EntityType with ID '{EntityTypeId}' not found.");
                    }

                    return entityType;
                }
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is EntityKey other)
            {
                return Id == other.Id && EntityTypeId == other.EntityTypeId;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, EntityTypeId);
        }

        public override string ToString()
        {
            return $"({Id}, {EntityTypeId})";
        }
    }
}
