
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EVPulseContext.Objects;

[MetadataType(typeof(IEntityMetaData))]
public partial class Entity
{
    public override string ToString()
    {
        return LongDescription;
    }

    [NotMapped]
    public string ShortDescription
    {
        get => $"{EntityKey.EntityType.Name}: {Name}";
    }

    [NotMapped]
    public string LongDescription
    {
        get
        {
            if (EntityNavigation != null)
            {
                return $"{EntityNavigation.LongDescription} > {ShortDescription}";
            }
            else
            {
                return ShortDescription;
            }
        }
    }

    [NotMapped]
    public EntityKey EntityKey
    {
        get => new EntityKey(Id, EntityTypeId);
        set
        {
            Id = value.Id;
            EntityTypeId = value.EntityTypeId;
        }
    }

    [NotMapped]
    public virtual ICollection<Entity> RelatedEntities { get; set; } = new List<Entity>();
}

internal interface IEntityMetaData
{
    [DisplayName("Parent")]
    object EntityNavigation { get; set; }
}