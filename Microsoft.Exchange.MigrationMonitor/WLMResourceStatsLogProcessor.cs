using System;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200002E RID: 46
	internal class WLMResourceStatsLogProcessor : SlowMRSDetectorBaseLogProcessor
	{
		// Token: 0x06000182 RID: 386 RVA: 0x0000855D File Offset: 0x0000675D
		public WLMResourceStatsLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, "Logging\\MailboxReplicationService\\WLMResourceStatss"), "WLMResourceStats", MigrationMonitor.WLMResourceStatsCsvSchemaInstance, "WLMResourceStats*.LOG")
		{
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00008583 File Offset: 0x00006783
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetWLMResouceStatsUpdateTimestamp";
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000858A File Offset: 0x0000678A
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertWLMResouceStats_BatchUpload";
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00008591 File Offset: 0x00006791
		protected override string SqlParamName
		{
			get
			{
				return "WLMResouceStatsList";
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00008598 File Offset: 0x00006798
		protected override string SqlTypeName
		{
			get
			{
				return "WLMResouceStatsData";
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000859F File Offset: 0x0000679F
		protected override void LogUploadStoredProcedureHandlerError(SqlQueryFailedException ex)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error uploading WLM Resource Statistics logs. Will attempt again next cycle.", new object[0]);
			throw new UploadWLMResourceStatsLogInBatchFailureException(ex.InnerException);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x000085C8 File Offset: 0x000067C8
		protected override void LogInsertCsvRowHandlerError(FormatException ex, CsvRow row)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error parsing WLM Resource Statistics log.", new object[0]);
		}

		// Token: 0x0400010D RID: 269
		public new const string KeyNameIsLogProcessorEnabled = "IsWLMResourceStatsLogProcessorEnabled";

		// Token: 0x0400010E RID: 270
		public const string LogTypeName = "WLMResourceStats";

		// Token: 0x0400010F RID: 271
		public const string RelativeLogPath = "Logging\\MailboxReplicationService\\WLMResourceStatss";
	}
}
