using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Hygiene.Data.DataProvider;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x0200000C RID: 12
	internal class HygieneSession
	{
		// Token: 0x0600004A RID: 74 RVA: 0x000027E0 File Offset: 0x000009E0
		public static SqlPropertyDefinition[] FindPropertyDefinition(DatabaseType databaseType, string entityName = null, string propertyName = null, int? entityId = null, int? propertyId = null)
		{
			IConfigDataProvider configDataProvider = ConfigDataProviderFactory.Default.Create(databaseType);
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, SqlPropertyDefinition.EntityNameProp, entityName),
				new ComparisonFilter(ComparisonOperator.Equal, SqlPropertyDefinition.PropertyNameProp, propertyName),
				new ComparisonFilter(ComparisonOperator.Equal, SqlPropertyDefinition.EntityIdProp, entityId),
				new ComparisonFilter(ComparisonOperator.Equal, SqlPropertyDefinition.PropertyIdProp, propertyId)
			});
			return configDataProvider.Find<SqlPropertyDefinition>(filter, null, true, null).Cast<SqlPropertyDefinition>().ToArray<SqlPropertyDefinition>();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002864 File Offset: 0x00000A64
		public static void SavePropertyDefinition(DatabaseType databaseType, SqlPropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			IConfigDataProvider configDataProvider = ConfigDataProviderFactory.Default.Create(databaseType);
			configDataProvider.Save(propertyDefinition);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002894 File Offset: 0x00000A94
		public static void DeletePropertyDefinition(DatabaseType databaseType, SqlPropertyDefinition propertyDefinition)
		{
			if (propertyDefinition == null)
			{
				throw new ArgumentNullException("propertyDefinition");
			}
			IConfigDataProvider configDataProvider = ConfigDataProviderFactory.Default.Create(databaseType);
			configDataProvider.Delete(propertyDefinition);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000028C4 File Offset: 0x00000AC4
		internal static void ValidateDBVersion(DatabaseType databaseType)
		{
			IConfigDataProvider configDataProvider = ConfigDataProviderFactory.Default.Create(databaseType);
			configDataProvider.Find<SqlDBVersion>(null, null, false, null).Cast<SqlDBVersion>().ToArray<SqlDBVersion>();
		}
	}
}
