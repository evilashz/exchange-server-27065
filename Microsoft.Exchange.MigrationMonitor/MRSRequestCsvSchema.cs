using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000025 RID: 37
	internal class MRSRequestCsvSchema : BaseMrsMonitorCsvSchema
	{
		// Token: 0x06000144 RID: 324 RVA: 0x00007330 File Offset: 0x00005530
		public MRSRequestCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(MRSRequestCsvSchema.requiredColumnsIds, MRSRequestCsvSchema.requiredColumnsAsIs, "Time"), BaseMigMonCsvSchema.GetOptionalColumns(MRSRequestCsvSchema.optionalColumnsIds, MRSRequestCsvSchema.optionalColumnsAsIs), null)
		{
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000735C File Offset: 0x0000555C
		public override DataTable GetCsvSchemaDataTable()
		{
			DataTable csvSchemaDataTable = base.GetCsvSchemaDataTable();
			csvSchemaDataTable.Columns.Add("RequestTypeId", typeof(int));
			return csvSchemaDataTable;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000738C File Offset: 0x0000558C
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return MRSRequestCsvSchema.requiredColumnsIds;
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00007393 File Offset: 0x00005593
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return MRSRequestCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000739A File Offset: 0x0000559A
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return MRSRequestCsvSchema.optionalColumnsIds;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x000073A1 File Offset: 0x000055A1
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return MRSRequestCsvSchema.optionalColumnsAsIs;
		}

		// Token: 0x040000E2 RID: 226
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>();

		// Token: 0x040000E3 RID: 227
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<Guid>("RequestGuid")
		};

		// Token: 0x040000E4 RID: 228
		private static readonly List<ColumnDefinition<int>> optionalColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("UserOrgName", "UserOrgNameId", KnownStringType.TenantName),
			new ColumnDefinition<int>("Status", "StatusId", KnownStringType.RequestStatus),
			new ColumnDefinition<int>("StatusDetail", "StatusDetailId", KnownStringType.RequestStatusDetail),
			new ColumnDefinition<int>("Priority", "PriorityId", KnownStringType.RequestPriority),
			new ColumnDefinition<int>("BatchName", "BatchNameId", KnownStringType.RequestBatchName),
			new ColumnDefinition<int>("SourceVersion", "SourceVersionId", KnownStringType.Version),
			new ColumnDefinition<int>("SourceServer", "SourceServerId", KnownStringType.ServerName),
			new ColumnDefinition<int>("SourceDatabase", "SourceDatabaseId", KnownStringType.DatabaseName),
			new ColumnDefinition<int>("SourceArchiveDatabase", "SourceArchiveDatabaseId", KnownStringType.DatabaseName),
			new ColumnDefinition<int>("TargetVersion", "TargetVersionId", KnownStringType.Version),
			new ColumnDefinition<int>("TargetDatabase", "TargetDatabaseId", KnownStringType.DatabaseName),
			new ColumnDefinition<int>("TargetServer", "TargetServerId", KnownStringType.ServerName),
			new ColumnDefinition<int>("RemoteHostName", "RemoteHostNameId", KnownStringType.RemoteHostName),
			new ColumnDefinition<int>("RemoteDatabaseName", "RemoteDatabaseNameId", KnownStringType.DatabaseName),
			new ColumnDefinition<int>("TargetDeliveryDomain", "TargetDeliveryDomainId", KnownStringType.TargetDeliveryDomain),
			new ColumnDefinition<int>("MRSServerName", "MRSServerNameId", KnownStringType.ServerName),
			new ColumnDefinition<int>("FailureType", "FailureTypeId", KnownStringType.FailureType),
			new ColumnDefinition<int>("FailureSide", "FailureSideId", KnownStringType.FailureSide),
			new ColumnDefinition<int>("SyncStage", "SyncStageId", KnownStringType.RequestSyncStage),
			new ColumnDefinition<int>("JobType", "JobTypeId", KnownStringType.RequestJobType),
			new ColumnDefinition<int>("WorkloadType", "WorkloadTypeId", KnownStringType.RequestWorkloadType),
			new ColumnDefinition<int>("SyncProtocol", "SyncProtocolID", KnownStringType.SyncProtocol)
		};

		// Token: 0x040000E5 RID: 229
		private static readonly List<IColumnDefinition> optionalColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<string>("Identity"),
			new ColumnDefinition<string>("Message"),
			new ColumnDefinition<SqlDateTime>("TS_Creation"),
			new ColumnDefinition<SqlDateTime>("TS_Start"),
			new ColumnDefinition<SqlDateTime>("TS_LastUpdate"),
			new ColumnDefinition<SqlDateTime>("TS_InitialSeedingCompleted"),
			new ColumnDefinition<SqlDateTime>("TS_FinalSync"),
			new ColumnDefinition<SqlDateTime>("TS_Completion"),
			new ColumnDefinition<SqlDateTime>("TS_Suspended"),
			new ColumnDefinition<SqlDateTime>("TS_Failure"),
			new ColumnDefinition<SqlDateTime>("TS_StartAfter"),
			new ColumnDefinition<int>("PercentComplete"),
			new ColumnDefinition<int>("BadItemLimit"),
			new ColumnDefinition<int>("BadItemsEncountered"),
			new ColumnDefinition<int>("TotalMailboxItemCount"),
			new ColumnDefinition<int>("ItemsTransferred"),
			new ColumnDefinition<int>("JobInternalFlags"),
			new ColumnDefinition<int>("FailureCode"),
			new ColumnDefinition<int>("LargeItemLimit"),
			new ColumnDefinition<int>("LargeItemsEncountered"),
			new ColumnDefinition<int>("MissingItemsEncountered"),
			new ColumnDefinition<int>("PoisonCount"),
			new ColumnDefinition<int>("Flags"),
			new ColumnDefinition<long>("Duration_OverallMove"),
			new ColumnDefinition<long>("Duration_Finalization"),
			new ColumnDefinition<long>("Duration_DataReplicationWait"),
			new ColumnDefinition<long>("Duration_Suspended"),
			new ColumnDefinition<long>("Duration_Failed"),
			new ColumnDefinition<long>("Duration_Queued"),
			new ColumnDefinition<long>("Duration_InProgress"),
			new ColumnDefinition<long>("Duration_StalledDueToCI"),
			new ColumnDefinition<long>("Duration_StalledDueToHA"),
			new ColumnDefinition<long>("Duration_StalledDueToReadThrottle"),
			new ColumnDefinition<long>("Duration_StalledDueToWriteThrottle"),
			new ColumnDefinition<long>("Duration_StalledDueToReadCpu"),
			new ColumnDefinition<long>("Duration_StalledDueToWriteCpu"),
			new ColumnDefinition<long>("Duration_StalledDueToMailboxLock"),
			new ColumnDefinition<long>("Duration_TransientFailure"),
			new ColumnDefinition<long>("Duration_Idle"),
			new ColumnDefinition<long>("TotalMailboxSize"),
			new ColumnDefinition<long>("BytesTransferred"),
			new ColumnDefinition<Guid>("ExchangeGuid"),
			new ColumnDefinition<Guid>("SourceExchangeGuid"),
			new ColumnDefinition<Guid>("TargetExchangeGuid"),
			new ColumnDefinition<string>("CancelRequest")
		};
	}
}
