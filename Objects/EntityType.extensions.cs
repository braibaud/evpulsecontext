using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EVPulseContext.Objects;

public partial class EntityType
{
    public override string ToString()
    {
        return LongDescription;
    }

    [NotMapped]
    public string ShortDescription
    {
        get => $"{Id:D2}";
    }

    [NotMapped]
    public string LongDescription
    {
        get => $"{ShortDescription} - {Name}";
    }
}