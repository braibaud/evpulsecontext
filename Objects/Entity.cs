using System;
using System.Collections.Generic;

namespace EVPulseContext.Objects;

public partial class Entity
{
    public Guid Id { get; set; }

    public int EntityTypeId { get; set; }

    public Guid? ParentId { get; set; }

    public int? ParentEntityTypeId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsVirtual { get; set; }

    public bool IsActive { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<EntityAttribute> EntityAttributes { get; set; } = new List<EntityAttribute>();

    public virtual ICollection<EntityFeature> EntityFeatures { get; set; } = new List<EntityFeature>();

    public virtual Entity? EntityNavigation { get; set; }

    public virtual EntityType EntityType { get; set; } = null!;

    public virtual ICollection<Entity> InverseEntityNavigation { get; set; } = new List<Entity>();

    public virtual ICollection<VehiclePart> VehicleParts { get; set; } = new List<VehiclePart>();
}
