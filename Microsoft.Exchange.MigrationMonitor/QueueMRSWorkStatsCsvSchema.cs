using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200002B RID: 43
	internal class QueueMRSWorkStatsCsvSchema : BaseMigMonCsvSchema
	{
		// Token: 0x06000172 RID: 370 RVA: 0x0000826D File Offset: 0x0000646D
		public QueueMRSWorkStatsCsvSchema() : base(BaseMigMonCsvSchema.GetRequiredColumns(QueueMRSWorkStatsCsvSchema.requiredColumnsIds, QueueMRSWorkStatsCsvSchema.requiredColumnsAsIs, "Time"), BaseMigMonCsvSchema.GetOptionalColumns(QueueMRSWorkStatsCsvSchema.optionalColumnsIds, QueueMRSWorkStatsCsvSchema.optionalColumnsAsIs), null)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000829C File Offset: 0x0000649C
		public override DataTable GetCsvSchemaDataTable()
		{
			return base.GetCsvSchemaDataTable();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000082B1 File Offset: 0x000064B1
		public override List<ColumnDefinition<int>> GetRequiredColumnsIds()
		{
			return QueueMRSWorkStatsCsvSchema.requiredColumnsIds;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000082B8 File Offset: 0x000064B8
		public override List<IColumnDefinition> GetRequiredColumnsAsIs()
		{
			return QueueMRSWorkStatsCsvSchema.requiredColumnsAsIs;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000082BF File Offset: 0x000064BF
		public override List<ColumnDefinition<int>> GetOptionalColumnsIds()
		{
			return QueueMRSWorkStatsCsvSchema.optionalColumnsIds;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000082C6 File Offset: 0x000064C6
		public override List<IColumnDefinition> GetOptionalColumnsAsIs()
		{
			return QueueMRSWorkStatsCsvSchema.optionalColumnsAsIs;
		}

		// Token: 0x040000F9 RID: 249
		public const string QueuedAndRelinquishedJobsColumnName = "QueuedAndRelinquishedJobs";

		// Token: 0x040000FA RID: 250
		public const string InprogressJobsColumnName = "InprogressJobs";

		// Token: 0x040000FB RID: 251
		public const string LastJobPickupTimeColumnName = "LastJobPickupTime";

		// Token: 0x040000FC RID: 252
		public const string LastScanFailureColumnName = "LastScanFailure";

		// Token: 0x040000FD RID: 253
		public const string LastScanTimestampColumnName = "LastScanTimestamp";

		// Token: 0x040000FE RID: 254
		public const string LastScanDurationMsColumnName = "LastScanDurationMs";

		// Token: 0x040000FF RID: 255
		public const string NextScanTimestampColumnName = "NextScanTimestamp";

		// Token: 0x04000100 RID: 256
		public const string MdbDiscoveryTimeStampColumnName = "MdbDiscoveryTimeStamp";

		// Token: 0x04000101 RID: 257
		public const string LastActiveJobFinishTimeColumnName = "LastActiveJobFinishTime";

		// Token: 0x04000102 RID: 258
		public const string LastActiveJobFinishedColumnName = "LastActiveJobFinished";

		// Token: 0x04000103 RID: 259
		public const string LongQueuedTenantUpgradeColumnName = "LongQueuedTenantUpgradeMoves";

		// Token: 0x04000104 RID: 260
		public const string LongQueuedCustomerMovesColumnName = "LongQueuedCustomerMoves";

		// Token: 0x04000105 RID: 261
		public const string LastScanFailureColumnId = "LastScanFailureId";

		// Token: 0x04000106 RID: 262
		private static readonly List<ColumnDefinition<int>> requiredColumnsIds = new List<ColumnDefinition<int>>();

		// Token: 0x04000107 RID: 263
		private static readonly List<IColumnDefinition> requiredColumnsAsIs = new List<IColumnDefinition>
		{
			new ColumnDefinition<int>("QueuedAndRelinquishedJobs"),
			new ColumnDefinition<int>("InprogressJobs"),
			new ColumnDefinition<SqlDateTime>("LastJobPickupTime"),
			new ColumnDefinition<SqlDateTime>("LastScanTimestamp"),
			new ColumnDefinition<int>("LastScanDurationMs"),
			new ColumnDefinition<SqlDateTime>("NextScanTimestamp"),
			new ColumnDefinition<SqlDateTime>("MdbDiscoveryTimeStamp"),
			new ColumnDefinition<SqlDateTime>("LastActiveJobFinishTime"),
			new ColumnDefinition<Guid>("LastActiveJobFinished"),
			new ColumnDefinition<int>("LongQueuedTenantUpgradeMoves"),
			new ColumnDefinition<int>("LongQueuedCustomerMoves")
		};

		// Token: 0x04000108 RID: 264
		private static readonly List<ColumnDefinition<int>> optionalColumnsIds = new List<ColumnDefinition<int>>
		{
			new ColumnDefinition<int>("LastScanFailure", "LastScanFailureId", KnownStringType.LastScanFailureFailureType)
		};

		// Token: 0x04000109 RID: 265
		private static readonly List<IColumnDefinition> optionalColumnsAsIs = new List<IColumnDefinition>();
	}
}
