using System;
using System.Collections.Generic;

namespace EVPulseContext.Objects;

public partial class EntityAttribute
{
    public Guid EntityId { get; set; }

    public int EntityTypeId { get; set; }

    public int AttributeId { get; set; }

    public string? Value { get; set; }

    public string? Unit { get; set; }

    public bool IsActive { get; set; }

    public virtual Attribute Attribute { get; set; } = null!;

    public virtual Entity Entity { get; set; } = null!;
}
