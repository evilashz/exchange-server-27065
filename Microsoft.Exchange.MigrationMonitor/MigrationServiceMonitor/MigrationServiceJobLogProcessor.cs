using System;
using System.Data;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor.MigrationServiceMonitor
{
	// Token: 0x0200000A RID: 10
	internal class MigrationServiceJobLogProcessor : MigrationServiceBaseLogProcessor
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00003852 File Offset: 0x00001A52
		public MigrationServiceJobLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, "Logging\\MigrationReports\\MigrationJob"), "Migration Service Job Log", MigrationMonitor.MigrationServiceJobCsvSchemaInstance)
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003873 File Offset: 0x00001A73
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetMigrationJobLogUpdateTimestamp";
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000387A File Offset: 0x00001A7A
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertMigrationServiceJob_BatchUploadV3";
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003881 File Offset: 0x00001A81
		protected override string SqlParamName
		{
			get
			{
				return "MigrationServiceJobList";
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003888 File Offset: 0x00001A88
		protected override string SqlTypeName
		{
			get
			{
				return "dbo.MigrationServiceJobDataV3";
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003890 File Offset: 0x00001A90
		protected override string[] DistinctColumns
		{
			get
			{
				return new string[]
				{
					"JobId"
				};
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000038AD File Offset: 0x00001AAD
		protected override void LogUploadStoredProcedureHandlerError(SqlQueryFailedException ex)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error updating migration job data to NewMan. Will attempt again next cycle.", new object[0]);
			throw new UploadMigrationJobInBatchFailureException(ex.InnerException);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000038D8 File Offset: 0x00001AD8
		protected override void LogInsertCsvRowHandlerError(FormatException ex, CsvRow row)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error parsing migration job log, job id is {0}", new object[]
			{
				MigMonUtilities.GetColumnValue<Guid>(row, "JobId")
			});
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003918 File Offset: 0x00001B18
		protected override bool TryAddSchemaSpecificDataRowValues(DataRow dataRow, CsvRow row)
		{
			dataRow["TargetDeliveryDomain"] = MigMonUtilities.TruncateMessage(MigMonUtilities.GetColumnStringValue(row, "TargetDeliveryDomain"), 500);
			dataRow[MigrationServiceJobCsvSchema.SourceEndpointGuid.DataTableKeyColumnName] = DBNull.Value;
			dataRow[MigrationServiceJobCsvSchema.TargetEndpointGuid.DataTableKeyColumnName] = DBNull.Value;
			string columnStringValue = MigMonUtilities.GetColumnStringValue(row, MigrationServiceJobCsvSchema.SourceEndpointGuid.ColumnName);
			if (!string.IsNullOrWhiteSpace(columnStringValue))
			{
				dataRow[MigrationServiceJobCsvSchema.SourceEndpointGuid.DataTableKeyColumnName] = MigMonUtilities.GetEndpointId(columnStringValue);
			}
			string columnStringValue2 = MigMonUtilities.GetColumnStringValue(row, MigrationServiceJobCsvSchema.TargetEndpointGuid.ColumnName);
			if (!string.IsNullOrWhiteSpace(columnStringValue2))
			{
				dataRow[MigrationServiceJobCsvSchema.TargetEndpointGuid.DataTableKeyColumnName] = MigMonUtilities.GetEndpointId(columnStringValue2);
			}
			dataRow["InitialSyncDuration"] = DBNull.Value;
			return base.TryAddMigrationServiceCommonDataRowValues(dataRow, row);
		}

		// Token: 0x04000025 RID: 37
		private const string LogTypeName = "Migration Service Job Log";
	}
}
