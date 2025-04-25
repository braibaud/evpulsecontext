using Microsoft.EntityFrameworkCore;
using EVPulseContext.Objects;

namespace EVPulseContext
{
    public partial class DataContext : DbContext
    {
        public async Task<List<EntityAttribute>> AssignEntityAttributesAsync(Guid entityId, int entityTypeId, List<string> attributes, string addOrReplace = "ADD")
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_entity_id", entityId),
                GetDbParam("p_entity_type_id", entityTypeId),
                GetDbParam("p_entity_attributes", attributes),
                GetDbParam("p_add_or_replace_attributes", addOrReplace)
            };

            List<EntityAttribute> result = await Database.SqlQueryRaw<EntityAttribute>(
                "SELECT * FROM dbo.assign_entity_attributes(@p_entity_id, @p_entity_type_id, @p_entity_attributes, @p_add_or_replace_attributes)",
                parameters
            ).ToListAsync();

            return result;
        }

        // Method to call assign_entity_features function
        public async Task<List<EntityFeature>> AssignEntityFeaturesAsync(Guid entityId, int entityTypeId, List<string> features, string addOrReplace = "ADD")
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_entity_id", entityId),
                GetDbParam("p_entity_type_id", entityTypeId),
                GetDbParam("p_entity_features", features),
                GetDbParam("p_add_or_replace_features", addOrReplace)
            };

            List<EntityFeature> result = await Database.SqlQueryRaw<EntityFeature>(
                "SELECT * FROM dbo.assign_entity_features(@p_entity_id, @p_entity_type_id, @p_entity_features, @p_add_or_replace_features)",
                parameters
            ).ToListAsync();

            return result;
        }

        // Method to call create_attribute function
        public async Task<Objects.Attribute?> CreateAttributeAsync(string name, string groupName, string defaultUnit, bool isActive = true, bool isInheritable = true, bool isOverridable = true)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_name", name),
                GetDbParam("p_group_name", groupName),
                GetDbParam("p_default_unit", defaultUnit),
                GetDbParam("p_is_active", isActive),
                GetDbParam("p_is_inheritable", isInheritable),
                GetDbParam("p_is_overridable", isOverridable)
            };

            int attribute_id = await Database.ExecuteSqlRawAsync(
                "SELECT * FROM dbo.create_attribute(@p_name, @p_group_name, @p_default_unit, @p_is_active, @p_is_inheritable, @p_is_overridable)",
                parameters
            );

            return await GetAttributeByIdAsync(attribute_id);
        }

        // Method to call create_entity function
        public async Task<EntityKey?> CreateEntityAsync(string name, string entityTypeName, Guid? parentId, int? parentEntityTypeId, bool isVirtual = false, bool isActive = true)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_name", name),
                GetDbParam("p_entity_type_name", entityTypeName),
                GetDbParam("p_parent_id", parentId),
                GetDbParam("p_parent_entity_type_id", parentEntityTypeId),
                GetDbParam("p_is_virtual", isVirtual),
                GetDbParam("p_is_active", isActive)
            };

            EntityKey? result = await Database.SqlQueryRaw<EntityKey>(
                "SELECT * FROM dbo.create_entity(@p_name, @p_entity_type_name, @p_parent_id, @p_parent_entity_type_id, @p_is_virtual, @p_is_active)",
                parameters
            ).FirstOrDefaultAsync();

            return result;
        }

        // Method to call create_feature function
        public async Task<Feature?> CreateFeatureAsync(string name, string groupName, string defaultCurrency, bool isActive = true, bool isInheritable = true, bool isOverridable = true)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_name", name),
                GetDbParam("p_group_name", groupName),
                GetDbParam("p_default_currency", defaultCurrency),
                GetDbParam("p_is_active", isActive),
                GetDbParam("p_is_inheritable", isInheritable),
                GetDbParam("p_is_overridable", isOverridable)
            };

            int feature_id = await Database.ExecuteSqlRawAsync(
                "SELECT * FROM dbo.create_feature(@p_name, @p_group_name, @p_default_currency, @p_is_active, @p_is_inheritable, @p_is_overridable)",
                parameters
            );

            return await GetFeatureByIdAsync(feature_id);
        }

        // Method to call create_option_pack function
        public async Task<EntityKey?> CreateOptionPackAsync(Guid parentEntityId, int parentEntityTypeId, string optionPackName, List<string> featureNames, string addOrReplace = "ADD")
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_parent_entity_id", parentEntityId),
                GetDbParam("p_parent_entity_type_id", parentEntityTypeId),
                GetDbParam("p_option_pack_name", optionPackName),
                GetDbParam("p_feature_names", featureNames),
                GetDbParam("p_add_or_replace_features", addOrReplace)
            };

            EntityKey? result = await Database.SqlQueryRaw<EntityKey>(
                "SELECT * FROM dbo.create_option_pack(@p_parent_entity_id, @p_parent_entity_type_id, @p_option_pack_name, @p_feature_names, @p_add_or_replace_features)",
                parameters
            ).FirstOrDefaultAsync();

            return result;
        }

        // Method to call create_vehicule function
        public async Task<Guid> CreateVehiculeAsync(string name, List<Guid> entities, List<int> entitiesTypeId, bool isActive = true)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_name", name),
                GetDbParam("p_entities", entities),
                GetDbParam("p_entities_type_id", entitiesTypeId),
                GetDbParam("p_is_active", isActive)
            };

            Guid result = await Database.SqlQueryRaw<Guid>(
                "SELECT * FROM dbo.create_vehicule(@p_name, @p_entities, @p_entities_type_id, @p_is_active)",
                parameters
            ).FirstOrDefaultAsync();

            return result;
        }

        // Method to call get_attribute_id function
        public async Task<int> GetAttributeIdAsync(string attributeName)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_attribute_name", attributeName)
            };

            int result = await Database.SqlQueryRaw<int>(
                "SELECT * FROM dbo.get_attribute_id(@p_attribute_name)",
                parameters
            ).FirstOrDefaultAsync();

            return result;
        }

        // Method to call get_entity_attributes function
        public async Task<List<EntityAttribute>> GetEntityAttributesAsync(Guid entityId, int entityTypeId)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_id", entityId),
                GetDbParam("p_entity_type_id", entityTypeId)
            };

            List<EntityAttribute> result = await Database.SqlQueryRaw<EntityAttribute>(
                "SELECT * FROM dbo.get_entity_attributes(@p_id, @p_entity_type_id)",
                parameters
            ).ToListAsync();

            return result;
        }

        // Method to call get_entity_features function
        public async Task<List<EntityFeature>> GetEntityFeaturesAsync(Guid entityId, int entityTypeId)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_id", entityId),
                GetDbParam("p_entity_type_id", entityTypeId)
            };

            List<EntityFeature> result = await Database.SqlQueryRaw<EntityFeature>(
                "SELECT * FROM dbo.get_entity_features(@p_id, @p_entity_type_id)",
                parameters
            ).ToListAsync();

            return result;
        }

        // Method to call get_entity_path function
        public async Task<string?> GetEntityPathAsync(Guid entityId, int entityTypeId)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_entity_id", entityId),
                GetDbParam("p_entity_type_id", entityTypeId)
            };

            string? result = await Database.SqlQueryRaw<string>(
                "SELECT * FROM dbo.get_entity_path(@p_entity_id, @p_entity_type_id)",
                parameters
            ).FirstOrDefaultAsync();

            return result;
        }

        // Method to call get_entity_type_id function
        public async Task<int> GetEntityTypeIdAsync(string entityTypeName)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_entity_type_name", entityTypeName)
            };

            int result = await Database.SqlQueryRaw<int>(
                "SELECT * FROM dbo.get_entity_type_id(@p_entity_type_name)",
                parameters
            ).FirstOrDefaultAsync();

            return result;
        }

        // Method to call get_entity_type_name function
        public async Task<string?> GetEntityTypeNameAsync(int entityTypeId)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_entity_type_id", entityTypeId)
            };

            string? result = await Database.SqlQueryRaw<string>(
                "SELECT * FROM dbo.get_entity_type_name(@p_entity_type_id)",
                parameters
            ).FirstOrDefaultAsync();

            return result;
        }

        // Method to call get_feature_id function
        public async Task<int> GetFeatureIdAsync(string featureName)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_feature_name", featureName)
            };

            int result = await Database.SqlQueryRaw<int>(
                "SELECT * FROM dbo.get_feature_id(@p_feature_name)",
                parameters
            ).FirstOrDefaultAsync();

            return result;
        }

        // Method to call get_nearest_parent function
        public async Task<EntityKey?> GetNearestParentAsync(Guid entityId, int entityTypeId, int targetEntityTypeId, int maxDepth = 10)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_id", entityId),
                GetDbParam("p_entity_type_id", entityTypeId),
                GetDbParam("p_target_entity_type_id", targetEntityTypeId),
                GetDbParam("p_max_depth", maxDepth)
            };

            EntityKey? result = await Database.SqlQueryRaw<EntityKey>(
                "SELECT * FROM dbo.get_nearest_parent(@p_id, @p_entity_type_id, @p_target_entity_type_id, @p_max_depth)",
                parameters
            ).FirstOrDefaultAsync();

            return result;
        }

        // Method to call get_nearest_parent_entity function
        public async Task<Entity?> GetNearestParentEntityAsync(Guid entityId, int entityTypeId, int targetEntityTypeId, int maxDepth = 10)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_id", entityId),
                GetDbParam("p_entity_type_id", entityTypeId),
                GetDbParam("p_target_entity_type_id", targetEntityTypeId),
                GetDbParam("p_max_depth", maxDepth)
            };

            Entity? result = await Database.SqlQueryRaw<Entity>(
                "SELECT * FROM dbo.get_nearest_parent_entity(@p_id, @p_entity_type_id, @p_target_entity_type_id, @p_max_depth)",
                parameters
            ).FirstOrDefaultAsync();

            return result;
        }

        // Method to call get_vehicle_attributes function
        public async Task<List<EntityAttribute>> GetVehicleAttributesAsync(Guid vehicleId)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_vehicle_id", vehicleId)
            };

            List<EntityAttribute> result = await Database.SqlQueryRaw<EntityAttribute>(
                "SELECT * FROM dbo.get_vehicle_attributes(@p_vehicle_id)",
                parameters
            ).ToListAsync();

            return result;
        }

        // Method to call get_vehicle_features function
        public async Task<List<EntityFeature>> GetVehicleFeaturesAsync(Guid vehicleId)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_vehicle_id", vehicleId)
            };

            List<EntityFeature> result = await Database.SqlQueryRaw<EntityFeature>(
                "SELECT * FROM dbo.get_vehicle_features(@p_vehicle_id)",
                parameters
            ).ToListAsync();

            return result;
        }

        // Method to call set_entity_attribute_value function
        public async Task SetEntityAttributeValueAsync(Guid entityId, int entityTypeId, int attributeId, string value, string unit, bool propagateDown = false)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_id", entityId),
                GetDbParam("p_entity_type_id", entityTypeId),
                GetDbParam("p_attribute_id", attributeId),
                GetDbParam("p_value", value),
                GetDbParam("p_unit", unit),
                GetDbParam("p_propagate_down", propagateDown)
            };

            await Database.ExecuteSqlRawAsync(
                "SELECT * FROM dbo.set_entity_attribute_value(@p_id, @p_entity_type_id, @p_attribute_id, @p_value, @p_unit, @p_propagate_down)",
                parameters
            );
        }

        // Method to call set_entity_feature_value function
        public async Task SetEntityFeatureValueAsync(Guid entityId, int entityTypeId, int featureId, decimal price, string currency, bool propagateDown = false)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_id", entityId),
                GetDbParam("p_entity_type_id", entityTypeId),
                GetDbParam("p_feature_id", featureId),
                GetDbParam("p_price", price),
                GetDbParam("p_currency", currency),
                GetDbParam("p_propagate_down", propagateDown)
            };

            await Database.ExecuteSqlRawAsync(
                "SELECT * FROM dbo.set_entity_feature_value(@p_id, @p_entity_type_id, @p_feature_id, @p_price, @p_currency, @p_propagate_down)",
                parameters
            );
        }

        // Method to call set_vehicle_attribute_value function
        public async Task SetVehicleAttributeValueAsync(Guid vehicleId, int attributeId, string value, string unit)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_vehicle_id", vehicleId),
                GetDbParam("p_attribute_id", attributeId),
                GetDbParam("p_value", value),
                GetDbParam("p_unit", unit)
            };

            await Database.ExecuteSqlRawAsync(
                "SELECT * FROM dbo.set_vehicle_attribute_value(@p_vehicle_id, @p_attribute_id, @p_value, @p_unit)",
                parameters
            );
        }

        // Method to call set_vehicle_feature_value function
        public async Task SetVehicleFeatureValueAsync(Guid vehicleId, int featureId, decimal price, string currency)
        {
            Npgsql.NpgsqlParameter[] parameters = new[]
            {
                GetDbParam("p_vehicle_id", vehicleId),
                GetDbParam("p_feature_id", featureId),
                GetDbParam("p_price", price),
                GetDbParam("p_currency", currency)
            };

            await Database.ExecuteSqlRawAsync(
                "SELECT * FROM dbo.set_vehicle_feature_value(@p_vehicle_id, @p_feature_id, @p_price, @p_currency)",
                parameters
            );
        }

        public async Task<Entity?> GetEntityByKeyAsync(EntityKey entityKey)
        {
            return await Entities.FirstOrDefaultAsync(
                e => e.Id == entityKey.Id && e.EntityTypeId == entityKey.EntityTypeId);
        }

        public async Task<Objects.Attribute?> GetAttributeByIdAsync(int attributeId)
        {
            return await Attributes.FirstOrDefaultAsync(
                e => e.Id == attributeId);
        }

        public async Task<Feature?> GetFeatureByIdAsync(int featureId)
        {
            return await Features.FirstOrDefaultAsync(
                e => e.Id == featureId);
        }
    }
}
