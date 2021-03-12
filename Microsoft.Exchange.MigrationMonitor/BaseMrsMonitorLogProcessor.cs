using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200000D RID: 13
	internal abstract class BaseMrsMonitorLogProcessor : BaseLogProcessor
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00003A5D File Offset: 0x00001C5D
		protected BaseMrsMonitorLogProcessor(string logDirectoryPath, string logFileTypeName, BaseMigMonCsvSchema csvSchemaInstance) : base(logDirectoryPath, logFileTypeName, csvSchemaInstance, null)
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003A6C File Offset: 0x00001C6C
		protected override void LogInsertCsvRowHandlerError(FormatException ex, CsvRow row)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error parsing MRS log, request guid is {0}", new object[]
			{
				MigMonUtilities.GetColumnValue<Guid>(row, "RequestGuid")
			});
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003AAA File Offset: 0x00001CAA
		protected override void LogUploadStoredProcedureHandlerError(SqlQueryFailedException ex)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error updating MRS log to NewMan. Will attempt again next cycle.", new object[0]);
			throw new UploadMrsLogInBatchFailureException(ex.InnerException);
		}

		// Token: 0x0400002E RID: 46
		public new const string KeyNameIsLogProcessorEnabled = "IsMRSLogProcessorEnabled";
	}
}
