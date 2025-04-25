using System;
using System.Collections.Generic;

namespace EVPulseContext.Objects;

public partial class VehicleFeature
{
    public Guid VehicleId { get; set; }

    public int FeatureId { get; set; }

    public bool IsOptional { get; set; }

    public bool IsActive { get; set; }

    public float Price { get; set; }

    public string? Currency { get; set; }

    public virtual Feature Feature { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
