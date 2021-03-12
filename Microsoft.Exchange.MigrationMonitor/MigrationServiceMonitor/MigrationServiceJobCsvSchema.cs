using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor.MigrationServiceMonitor
{
	// Token: 0x02000005 RID: 5
	internal class MigrationServiceJobCsvSchema : BaseMigMonCsvSchema
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000030A9 File Offset: 0x000012A9
		public MigrationServiceJobCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(MigrationServiceJobCsvSchema.requiredColumnsIds, MigrationServiceJobCsvSchema.requiredColumnsAsIs, "Time"), BaseMigMonCsvSchema.GetOptionalColumns(MigrationServiceJobCsvSchema.optionalColumnsIds, MigrationServiceJobCsvSchema.optionalColumnsAsIs), null)
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000030D5 File Offset: 0x000012D5
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return MigrationServiceJobCsvSchema.requiredColumnsIds;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000030DC File Offset: 0x000012DC
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return MigrationServiceJobCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000030E3 File Offset: 0x000012E3
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return MigrationServiceJobCsvSchema.optionalColumnsIds;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000030EA File Offset: 0x000012EA
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return MigrationServiceJobCsvSchema.optionalColumnsAsIs;
		}

		// Token: 0x0400000E RID: 14
		public const string JobIdColumn = "JobId";

		// Token: 0x0400000F RID: 15
		public const string TargetDeliveryDomainColumn = "TargetDeliveryDomain";

		// Token: 0x04000010 RID: 16
		public const string InitialSyncDurationColumn = "InitialSyncDuration";

		// Token: 0x04000011 RID: 17
		public static readonly ColumnDefinition<int> SourceEndpointGuid = new ColumnDefinition<int>("SourceEndpointGuid", "SourceEndpointGuidId", KnownStringType.EndpointGuid);

		// Token: 0x04000012 RID: 18
		public static readonly ColumnDefinition<int> TargetEndpointGuid = new ColumnDefinition<int>("TargetEndpointGuid", "TargetEndpointGuidId", KnownStringType.EndpointGuid);

		// Token: 0x04000013 RID: 19
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>();

		// Token: 0x04000014 RID: 20
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<Guid>("JobId")
		};

		// Token: 0x04000015 RID: 21
		private static readonly List<ColumnDefinition<int>> optionalColumnsIds = new List<ColumnDefinition<int>>
		{
			MigrationServiceProcessorsCommonHelpers.TenantName,
			MigrationServiceProcessorsCommonHelpers.MigrationType,
			MigrationServiceProcessorsCommonHelpers.Status,
			MigrationServiceProcessorsCommonHelpers.WatsonHash,
			new ColumnDefinition<int>("Direction", "DirectionId", KnownStringType.MigrationDirection),
			new ColumnDefinition<int>("Locale", "LocaleId", KnownStringType.Locale),
			new ColumnDefinition<int>("BatchFlags", "BatchFlagsId", KnownStringType.MigrationBatchFlags),
			new ColumnDefinition<int>("SkipSteps", "SkipStepsId", KnownStringType.MigrationSkipSteps),
			MigrationServiceJobCsvSchema.SourceEndpointGuid,
			MigrationServiceJobCsvSchema.TargetEndpointGuid
		};

		// Token: 0x04000016 RID: 22
		private static readonly List<IColumnDefinition> optionalColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<SqlDateTime>("CreationTime"),
			new ColumnDefinition<SqlDateTime>("StartTime"),
			new ColumnDefinition<SqlDateTime>("InitialSyncTime"),
			new ColumnDefinition<SqlDateTime>("FinalizedTime"),
			new ColumnDefinition<SqlDateTime>("LastSyncTime"),
			new ColumnDefinition<SqlDateTime>("StartAfterTime"),
			new ColumnDefinition<SqlDateTime>("CompleteAfter"),
			new ColumnDefinition<string>("InitialSyncDuration"),
			new ColumnDefinition<int>("TotalCount"),
			new ColumnDefinition<int>("ActiveCount"),
			new ColumnDefinition<int>("StoppedCount"),
			new ColumnDefinition<int>("SyncedCount"),
			new ColumnDefinition<int>("FinalizedCount"),
			new ColumnDefinition<int>("FailedCount"),
			new ColumnDefinition<int>("PendingCount"),
			new ColumnDefinition<int>("ProvisionedCount"),
			new ColumnDefinition<int>("ValidationWarningCount"),
			new ColumnDefinition<int>("AutoRetryCount"),
			new ColumnDefinition<int>("CurrentRetryCount"),
			new ColumnDefinition<int>("BadItemLimit"),
			new ColumnDefinition<int>("LargeItemLimit"),
			new ColumnDefinition<bool>("PrimaryOnly"),
			new ColumnDefinition<bool>("ArchiveOnly"),
			new ColumnDefinition<string>("TargetDeliveryDomain"),
			new ColumnDefinition<int>("ObjectVersion"),
			new ColumnDefinition<string>("LocalizedError"),
			new ColumnDefinition<string>("InternalError"),
			new ColumnDefinition<long>("ProcessingDuration")
		};
	}
}
