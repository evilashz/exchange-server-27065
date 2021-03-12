using System;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000031 RID: 49
	internal class JobPickupResultsLogProcessor : SlowMRSDetectorBaseLogProcessor
	{
		// Token: 0x06000197 RID: 407 RVA: 0x0000899E File Offset: 0x00006B9E
		public JobPickupResultsLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, "Logging\\MailboxReplicationService\\JobPickupResults"), "JobPickupResults", MigrationMonitor.JobPickupResultsCsvSchemaInstance, "JobPickupResults*.LOG")
		{
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000198 RID: 408 RVA: 0x000089C4 File Offset: 0x00006BC4
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetJobPickupResultsUpdateTimestamp";
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000089CB File Offset: 0x00006BCB
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertJobPickupResults_BatchUpload";
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000089D2 File Offset: 0x00006BD2
		protected override string SqlParamName
		{
			get
			{
				return "JobPickupResultsList";
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000089D9 File Offset: 0x00006BD9
		protected override string SqlTypeName
		{
			get
			{
				return "JobPickupResultsData";
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000089E0 File Offset: 0x00006BE0
		protected override void LogUploadStoredProcedureHandlerError(SqlQueryFailedException ex)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error uploading Job Pickup Results logs. Will attempt again next cycle.", new object[0]);
			throw new UploadJobPickupResultsLogInBatchFailureException(ex.InnerException);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00008A09 File Offset: 0x00006C09
		protected override void LogInsertCsvRowHandlerError(FormatException ex, CsvRow row)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error parsing Job Pickup Results log.", new object[0]);
		}

		// Token: 0x0400013B RID: 315
		public new const string KeyNameIsLogProcessorEnabled = "IsJobPickupResultsLogProcessorEnabled";

		// Token: 0x0400013C RID: 316
		public const string LogTypeName = "JobPickupResults";

		// Token: 0x0400013D RID: 317
		public const string RelativeLogPath = "Logging\\MailboxReplicationService\\JobPickupResults";
	}
}
