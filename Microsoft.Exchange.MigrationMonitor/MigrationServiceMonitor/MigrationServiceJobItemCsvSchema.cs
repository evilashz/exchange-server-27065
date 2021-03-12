using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor.MigrationServiceMonitor
{
	// Token: 0x02000006 RID: 6
	internal class MigrationServiceJobItemCsvSchema : BaseMigMonCsvSchema
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000033C9 File Offset: 0x000015C9
		public MigrationServiceJobItemCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(MigrationServiceJobItemCsvSchema.requiredColumnsIds, MigrationServiceJobItemCsvSchema.requiredColumnsAsIs, "Time"), BaseMigMonCsvSchema.GetOptionalColumns(MigrationServiceJobItemCsvSchema.optionalColumnsIds, MigrationServiceJobItemCsvSchema.optionalColumnsAsIs), null)
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000033F5 File Offset: 0x000015F5
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return MigrationServiceJobItemCsvSchema.requiredColumnsIds;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000033FC File Offset: 0x000015FC
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return MigrationServiceJobItemCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003403 File Offset: 0x00001603
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return MigrationServiceJobItemCsvSchema.optionalColumnsIds;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000340A File Offset: 0x0000160A
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return MigrationServiceJobItemCsvSchema.optionalColumnsAsIs;
		}

		// Token: 0x04000017 RID: 23
		public const string MigrationJobItemGuidColumn = "JobItemGuid";

		// Token: 0x04000018 RID: 24
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>();

		// Token: 0x04000019 RID: 25
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<Guid>("JobItemGuid"),
			new ColumnDefinition<string>("SubscriptionId")
		};

		// Token: 0x0400001A RID: 26
		private static readonly List<ColumnDefinition<int>> optionalColumnsIds = new List<ColumnDefinition<int>>
		{
			MigrationServiceProcessorsCommonHelpers.TenantName,
			MigrationServiceProcessorsCommonHelpers.MigrationType,
			MigrationServiceProcessorsCommonHelpers.Status,
			MigrationServiceProcessorsCommonHelpers.WatsonHash
		};

		// Token: 0x0400001B RID: 27
		private static readonly List<IColumnDefinition> optionalColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<int>("ObjectVersion"),
			new ColumnDefinition<string>("LocalizedError"),
			new ColumnDefinition<string>("InternalError"),
			new ColumnDefinition<Guid>("MigrationJobId"),
			new ColumnDefinition<long>("ItemsSynced"),
			new ColumnDefinition<long>("ItemsSkipped"),
			new ColumnDefinition<long>("OverallCmdletDuration"),
			new ColumnDefinition<long>("SubscriptionInjectionDuration"),
			new ColumnDefinition<long>("ProvisioningDuration"),
			new ColumnDefinition<SqlDateTime>("ProvisionedTime"),
			new ColumnDefinition<SqlDateTime>("SubscriptionQueuedTime")
		};
	}
}
