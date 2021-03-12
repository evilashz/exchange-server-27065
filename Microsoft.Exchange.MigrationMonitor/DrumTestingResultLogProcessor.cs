using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000014 RID: 20
	internal class DrumTestingResultLogProcessor : BaseLogProcessor
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00004229 File Offset: 0x00002429
		public DrumTestingResultLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, "Logging\\DrumTestingResults"), "Drum Testing Results", MigrationMonitor.DrumTestingResultCsvSchemaInstance, null)
		{
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000424B File Offset: 0x0000244B
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetDrumTestingResultLogUpdateTimestamp";
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004252 File Offset: 0x00002452
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertDrumTestingResult_BatchUpload";
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004259 File Offset: 0x00002459
		protected override string SqlParamName
		{
			get
			{
				return "DrumTestingResultList";
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004260 File Offset: 0x00002460
		protected override string SqlTypeName
		{
			get
			{
				return "dbo.DrumTestingResultData";
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004267 File Offset: 0x00002467
		protected override void LogUploadStoredProcedureHandlerError(SqlQueryFailedException ex)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error uploading Drum Testing result logs. Will attempt again next cycle.", new object[0]);
			throw new UploadDrumTestingResultLogInBatchFailureException(ex.InnerException);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000042D4 File Offset: 0x000024D4
		protected override bool TryAddSchemaSpecificDataRowValues(DataRow dataRow, CsvRow row)
		{
			List<ColumnDefinition<int>> requiredColumnsIds = this.CsvSchemaInstance.GetRequiredColumnsIds();
			requiredColumnsIds.ForEach(delegate(ColumnDefinition<int> oc)
			{
				this.TryAddStringValueByLookupId(oc, dataRow, row, null, true);
			});
			List<ColumnDefinition<int>> optionalColumnsIds = this.CsvSchemaInstance.GetOptionalColumnsIds();
			optionalColumnsIds.ForEach(delegate(ColumnDefinition<int> oc)
			{
				this.TryAddStringValueByLookupId(oc, dataRow, row, null, true);
			});
			dataRow["ResultDetails"] = MigMonUtilities.TruncateMessage(MigMonUtilities.GetColumnStringValue(row, "ResultDetails"), 500);
			return true;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004363 File Offset: 0x00002563
		protected override void LogInsertCsvRowHandlerError(FormatException ex, CsvRow row)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error parsing Drum Testing Result log.", new object[0]);
		}

		// Token: 0x0400004F RID: 79
		public new const string KeyNameIsLogProcessorEnabled = "IsDrumTestingResultLogProcessorEnabled";

		// Token: 0x04000050 RID: 80
		private const string LogTypeName = "Drum Testing Results";
	}
}
