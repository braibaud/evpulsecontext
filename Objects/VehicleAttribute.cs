using System;
using System.Collections.Generic;

namespace EVPulseContext.Objects;

public partial class VehicleAttribute
{
    public Guid VehicleId { get; set; }

    public int AttributeId { get; set; }

    public bool IsActive { get; set; }

    public string Value { get; set; } = null!;

    public string? Unit { get; set; }

    public virtual Attribute Attribute { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
