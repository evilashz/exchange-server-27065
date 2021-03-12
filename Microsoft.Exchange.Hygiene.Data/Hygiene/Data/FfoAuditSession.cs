using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Hygiene.Data.DataProvider;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000087 RID: 135
	[Serializable]
	internal class FfoAuditSession : IFfoAuditSession
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0001069A File Offset: 0x0000E89A
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x000106A2 File Offset: 0x0000E8A2
		public DatabaseType DatabaseType { get; private set; }

		// Token: 0x060004E9 RID: 1257 RVA: 0x000106B8 File Offset: 0x0000E8B8
		public FfoAuditSession(DatabaseType databaseType) : this(ConfigDataProviderFactory.Default.Create(databaseType))
		{
			if (!FfoAuditSession.IsDatabaseTypeSupported(databaseType))
			{
				IFormatProvider invariantCulture = CultureInfo.InvariantCulture;
				string format = "The session FfoAuditSession doesn't support the database {0}. The following types are supported: {1}.";
				object[] array = new object[2];
				array[0] = databaseType;
				array[1] = string.Join(", ", from dt in FfoAuditSession.supportedDatabases
				select dt.ToString());
				throw new ArgumentException(string.Format(invariantCulture, format, array), "databaseType");
			}
			this.DatabaseType = databaseType;
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x00010744 File Offset: 0x0000E944
		internal FfoAuditSession(IConfigDataProvider dataProvider)
		{
			this.dataProvider = dataProvider;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00010754 File Offset: 0x0000E954
		public IEnumerable<AuditProperty> FindAuditPropertiesByInstance(Guid partitionId, Guid instanceId, string entityName)
		{
			if (instanceId == Guid.Empty)
			{
				throw new ArgumentNullException("instanceId");
			}
			if (string.IsNullOrEmpty(entityName))
			{
				throw new ArgumentNullException("entityName");
			}
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, AuditProperty.EntityNameProp, entityName),
				new ComparisonFilter(ComparisonOperator.Equal, AuditProperty.InstanceIdProp, instanceId),
				new ComparisonFilter(ComparisonOperator.Equal, AuditProperty.PartitionIdProp, partitionId)
			});
			return this.dataProvider.Find<AuditProperty>(filter, null, true, null).Cast<AuditProperty>();
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000107E8 File Offset: 0x0000E9E8
		public IEnumerable<AuditProperty> FindAuditPropertiesByAuditId(Guid partitionId, Guid auditId)
		{
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, AuditProperty.AuditIdProp, auditId),
				new ComparisonFilter(ComparisonOperator.Equal, AuditProperty.PartitionIdProp, partitionId)
			});
			return this.dataProvider.Find<AuditProperty>(filter, null, true, null).Cast<AuditProperty>();
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00010840 File Offset: 0x0000EA40
		public IEnumerable<AuditHistoryResult> FindAuditHistory(string entityName, Guid? entityInstanceId, Guid partitionId, DateTime startTime, DateTime? endTime)
		{
			if (string.IsNullOrWhiteSpace(entityName) && (entityInstanceId == null || entityInstanceId == Guid.Empty))
			{
				throw new ArgumentException("You must provide a value to either the entityName or the entityInstanceId.", "entityName");
			}
			if (partitionId == Guid.Empty)
			{
				throw new ArgumentException("You must provide a non-empty partitionId.", "partitionId");
			}
			if (endTime != null && startTime > endTime)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The endTime (endTime = {0}) cannot be less than the startTime (startTime = {1}).", new object[]
				{
					endTime.Value,
					startTime
				}), "endTime");
			}
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, AuditHistoryResult.PartitionIdParameterDefinition, partitionId),
				new ComparisonFilter(ComparisonOperator.Equal, AuditHistoryResult.EntityNameParameterDefinition, entityName),
				new ComparisonFilter(ComparisonOperator.Equal, AuditHistoryResult.EntityInstanceIdParameterDefinition, entityInstanceId),
				new ComparisonFilter(ComparisonOperator.Equal, AuditHistoryResult.StartTimeParameterDefinition, startTime),
				new ComparisonFilter(ComparisonOperator.Equal, AuditHistoryResult.EndTimeParameterDefinition, endTime)
			});
			IConfigurable[] array = this.dataProvider.Find<AuditHistoryResult>(filter, null, true, null);
			return (array != null) ? array.Cast<AuditHistoryResult>() : Enumerable.Empty<AuditHistoryResult>();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000109B4 File Offset: 0x0000EBB4
		public IEnumerable<AuditHistoryResult> SearchAuditHistory(string entityName, string searchString, Guid? entityInstanceId, Guid partitionId, DateTime startTime, DateTime? endTime)
		{
			if (string.IsNullOrWhiteSpace(entityName))
			{
				throw new ArgumentException("You must provide a value to entityName.", "entityName");
			}
			if (string.IsNullOrWhiteSpace(searchString))
			{
				throw new ArgumentException("You must provide a non-empty pattern to search.", "searchString");
			}
			if (endTime != null && startTime > endTime)
			{
				throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The endTime (endTime = {0}) cannot be less than the startTime (startTime = {1}).", new object[]
				{
					endTime.Value,
					startTime
				}), "endTime");
			}
			QueryFilter filter = QueryFilter.AndTogether(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, AuditHistoryResult.PartitionIdParameterDefinition, partitionId),
				new ComparisonFilter(ComparisonOperator.Equal, AuditHistoryResult.EntityNameParameterDefinition, entityName),
				new ComparisonFilter(ComparisonOperator.Equal, AuditHistoryResult.EntityInstanceIdParameterDefinition, entityInstanceId),
				new ComparisonFilter(ComparisonOperator.Equal, AuditHistoryResult.PropertyValueStringDefinition, string.Format("%{0}%", searchString)),
				new ComparisonFilter(ComparisonOperator.Equal, AuditHistoryResult.StartTimeParameterDefinition, startTime),
				new ComparisonFilter(ComparisonOperator.Equal, AuditHistoryResult.EndTimeParameterDefinition, endTime)
			});
			IConfigurable[] array = this.dataProvider.Find<AuditHistoryResult>(filter, null, true, null);
			return (array != null) ? array.Cast<AuditHistoryResult>() : Enumerable.Empty<AuditHistoryResult>();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00010B10 File Offset: 0x0000ED10
		public void SetEntityData(Guid partitionId, string tableName, string columnName, string condition, string newValue)
		{
			if (partitionId == Guid.Empty)
			{
				throw new ArgumentException("You must provide a non-empty partitionId.", "partitionId");
			}
			if (string.IsNullOrWhiteSpace(tableName))
			{
				throw new ArgumentNullException("tableName");
			}
			if (string.IsNullOrWhiteSpace(columnName))
			{
				throw new ArgumentNullException("columnName");
			}
			if (string.IsNullOrWhiteSpace(condition))
			{
				throw new ArgumentNullException("condition");
			}
			this.dataProvider.Save(new SetEntityDataRequest
			{
				PartitionId = partitionId,
				TableName = tableName,
				ColumnName = columnName,
				Condition = condition,
				NewValue = newValue
			});
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00010BAB File Offset: 0x0000EDAB
		private static bool IsDatabaseTypeSupported(DatabaseType databaseType)
		{
			return FfoAuditSession.supportedDatabases.Contains(databaseType);
		}

		// Token: 0x04000332 RID: 818
		private static readonly HashSet<DatabaseType> supportedDatabases = new HashSet<DatabaseType>
		{
			DatabaseType.BackgroundJobBackend,
			DatabaseType.Directory,
			DatabaseType.Domain,
			DatabaseType.Kes,
			DatabaseType.Mtrt,
			DatabaseType.Spam
		};

		// Token: 0x04000333 RID: 819
		private IConfigDataProvider dataProvider;
	}
}
