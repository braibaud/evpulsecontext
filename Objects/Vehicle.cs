using System;
using System.Collections.Generic;

namespace EVPulseContext.Objects;

public partial class Vehicle
{
    public Guid VehicleId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<VehicleAttribute> VehicleAttributes { get; set; } = new List<VehicleAttribute>();

    public virtual ICollection<VehicleFeature> VehicleFeatures { get; set; } = new List<VehicleFeature>();

    public virtual ICollection<VehiclePart> VehicleParts { get; set; } = new List<VehiclePart>();
}
