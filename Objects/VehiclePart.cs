using System;
using System.Collections.Generic;

namespace EVPulseContext.Objects;

public partial class VehiclePart
{
    public Guid VehicleId { get; set; }

    public Guid EntityId { get; set; }

    public int EntityTypeId { get; set; }

    public bool IsActive { get; set; }

    public virtual Entity Entity { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
