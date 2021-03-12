using System;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200002D RID: 45
	internal class QueueMRSWorkStatsLogProcessor : SlowMRSDetectorBaseLogProcessor
	{
		// Token: 0x0600017B RID: 379 RVA: 0x000084D4 File Offset: 0x000066D4
		public QueueMRSWorkStatsLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, "Logging\\MailboxReplicationService\\QueueStats"), "QueueMRSStats", MigrationMonitor.QueueMRSWorkStatsCsvSchemaInstance, "QueueMRSStats*.LOG")
		{
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600017C RID: 380 RVA: 0x000084FA File Offset: 0x000066FA
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetQueueMRSStatsUpdateTimestamp";
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00008501 File Offset: 0x00006701
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertQueueMRSStats_BatchUpload";
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00008508 File Offset: 0x00006708
		protected override string SqlParamName
		{
			get
			{
				return "QueueMRSStatsList";
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000850F File Offset: 0x0000670F
		protected override string SqlTypeName
		{
			get
			{
				return "QueueMRSStatsData";
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00008516 File Offset: 0x00006716
		protected override void LogUploadStoredProcedureHandlerError(SqlQueryFailedException ex)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error uploading Queue Statistics logs. Will attempt again next cycle.", new object[0]);
			throw new UploadQueueStatsLogInBatchFailureException(ex.InnerException);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000853F File Offset: 0x0000673F
		protected override void LogInsertCsvRowHandlerError(FormatException ex, CsvRow row)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error parsing Queue Statistics log.", new object[0]);
		}

		// Token: 0x0400010A RID: 266
		public new const string KeyNameIsLogProcessorEnabled = "IsQueueMRSWorkStatsLogProcessorEnabled";

		// Token: 0x0400010B RID: 267
		public const string LogTypeName = "QueueMRSStats";

		// Token: 0x0400010C RID: 268
		public const string RelativeLogPath = "Logging\\MailboxReplicationService\\QueueStats";
	}
}
