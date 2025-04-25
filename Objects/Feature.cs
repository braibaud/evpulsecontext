using System;
using System.Collections.Generic;

namespace EVPulseContext.Objects;

public partial class Feature
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; } = null!;

    public string? DefaultCurrency { get; set; }

    public bool IsInheritable { get; set; }

    public bool IsOverridable { get; set; }

    public int GroupId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<EntityFeature> EntityFeatures { get; set; } = new List<EntityFeature>();

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<VehicleFeature> VehicleFeatures { get; set; } = new List<VehicleFeature>();
}
