using System;
using System.Collections.Generic;

namespace EVPulseContext.Objects;

public partial class EntityType
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Entity> Entities { get; set; } = new List<Entity>();
}
