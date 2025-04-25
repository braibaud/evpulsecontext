using System;
using System.Collections.Generic;

namespace EVPulseContext.Objects;

public partial class Group
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();

    public virtual ICollection<Feature> Features { get; set; } = new List<Feature>();
}
