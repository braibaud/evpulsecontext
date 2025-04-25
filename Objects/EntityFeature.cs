using System;
using System.Collections.Generic;

namespace EVPulseContext.Objects;

public partial class EntityFeature
{
    public Guid EntityId { get; set; }

    public int EntityTypeId { get; set; }

    public int FeatureId { get; set; }

    public float? Price { get; set; }

    public string? Currency { get; set; }

    public bool IsActive { get; set; }

    public virtual Entity Entity { get; set; } = null!;

    public virtual Feature Feature { get; set; } = null!;
}
