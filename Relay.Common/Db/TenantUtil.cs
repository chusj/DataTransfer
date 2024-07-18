using Relay.Model.Tenants;
using SqlSugar;
using System.Reflection;

namespace Relay.Common.Db
{
    public static class TenantUtil
    {
        public static List<Type> GetTenantEntityTypes(TenantTypeEnum? tenantType = null)
        {
            return RepositorySetting.Entitys
                .Where(u => !u.IsInterface && !u.IsAbstract && u.IsClass)
                .Where(s => IsTenantEntity(s, tenantType))
                .ToList();
        }

        public static bool IsTenantEntity(this Type u, TenantTypeEnum? tenantType = null)
        {
            var mta = u.GetCustomAttribute<MultiTenantAttribute>();
            if (mta is null)
            {
                return false;
            }

            if (tenantType != null)
            {
                if (mta.TenantType != tenantType)
                {
                    return false;
                }
            }

            return true;
        }

        public static string GetTenantTableName(this Type type, ISqlSugarClient db, string id)
        {
            var entityInfo = db.EntityMaintenance.GetEntityInfo(type);

            //约定大于配置，约定(多表多租户的)表名等于 => 表名_租户ID
            return $@"{entityInfo.DbTableName}_{id}";
        }

        public static void SetTenantTable(this ISqlSugarClient db, string id)
        {
            var types = GetTenantEntityTypes(TenantTypeEnum.Tables);

            foreach (var type in types)
            {
                db.MappingTables.Add(type.Name, type.GetTenantTableName(db, id));
            }
        }
    }
}
