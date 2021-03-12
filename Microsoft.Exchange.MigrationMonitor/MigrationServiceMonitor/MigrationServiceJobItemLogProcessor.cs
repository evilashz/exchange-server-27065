using System;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor.MigrationServiceMonitor
{
	// Token: 0x02000007 RID: 7
	internal class MigrationServiceJobItemLogProcessor : MigrationServiceBaseLogProcessor
	{
		// Token: 0x0600003C RID: 60 RVA: 0x0000354B File Offset: 0x0000174B
		public MigrationServiceJobItemLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, "Logging\\MigrationReports\\MigrationJobItem"), "Migration Service Job Item Log", MigrationMonitor.MigrationServiceJobItemCsvSchemaInstance)
		{
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000356C File Offset: 0x0000176C
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetMigrationJobItemLogUpdateTimestamp";
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003573 File Offset: 0x00001773
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertMigrationServiceJobItem_BatchUploadV3";
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000357A File Offset: 0x0000177A
		protected override string SqlParamName
		{
			get
			{
				return "MigrationServiceJobItemList";
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003581 File Offset: 0x00001781
		protected override string SqlTypeName
		{
			get
			{
				return "dbo.MigrationServiceJobItemDataV3";
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003588 File Offset: 0x00001788
		protected override string[] DistinctColumns
		{
			get
			{
				return new string[]
				{
					"JobItemGuid"
				};
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000035A5 File Offset: 0x000017A5
		protected override void LogUploadStoredProcedureHandlerError(SqlQueryFailedException ex)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error updating migration job item data to NewMan. Will attempt again next cycle.", new object[0]);
			throw new UploadMigrationJobItemInBatchFailureException(ex.InnerException);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000035D0 File Offset: 0x000017D0
		protected override void LogInsertCsvRowHandlerError(FormatException ex, CsvRow row)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error parsing migration job item log, job item id is {0}", new object[]
			{
				MigMonUtilities.GetColumnValue<Guid>(row, "JobItemGuid")
			});
		}

		// Token: 0x0400001C RID: 28
		private const string LogTypeName = "Migration Service Job Item Log";
	}
}
