using System;
using System.Collections.Generic;

namespace EVPulseContext.Objects;

public partial class Attribute
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; } = null!;

    public string? DefaultUnit { get; set; }

    public bool IsInheritable { get; set; }

    public bool IsOverridable { get; set; }

    public int GroupId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<EntityAttribute> EntityAttributes { get; set; } = new List<EntityAttribute>();

    public virtual Group Group { get; set; } = null!;

    public virtual ICollection<VehicleAttribute> VehicleAttributes { get; set; } = new List<VehicleAttribute>();
}
