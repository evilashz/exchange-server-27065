using System;
using System.Data;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor.MigrationServiceMonitor
{
	// Token: 0x02000009 RID: 9
	internal class MigrationServiceEndpointLogProcessor : MigrationServiceBaseLogProcessor
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003737 File Offset: 0x00001937
		public MigrationServiceEndpointLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, "Logging\\MigrationReports\\MigrationEndpoint"), "Migration Service Endpoint Log", MigrationMonitor.MigrationServiceEndpointCsvSchemaInstance)
		{
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003758 File Offset: 0x00001958
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetMigrationEndpointLogUpdateTimestamp";
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000375F File Offset: 0x0000195F
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertMigrationServiceEndpoint_BatchUpload";
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003766 File Offset: 0x00001966
		protected override string SqlParamName
		{
			get
			{
				return "MigrationServiceEndpointList";
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000376D File Offset: 0x0000196D
		protected override string SqlTypeName
		{
			get
			{
				return "dbo.MigrationServiceEndpointData";
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003774 File Offset: 0x00001974
		protected override string[] DistinctColumns
		{
			get
			{
				return new string[]
				{
					"EndpointGuid"
				};
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003791 File Offset: 0x00001991
		protected override void LogUploadStoredProcedureHandlerError(SqlQueryFailedException ex)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error updating migration endpoint data to NewMan. Will attempt again next cycle.", new object[0]);
			throw new UploadMigrationEndpointInBatchFailureException(ex.InnerException);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000037BC File Offset: 0x000019BC
		protected override void LogInsertCsvRowHandlerError(FormatException ex, CsvRow row)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error parsing migration endpoint log, endpoint guid is {0}", new object[]
			{
				MigMonUtilities.GetColumnValue<Guid>(row, "EndpointGuid")
			});
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000037FC File Offset: 0x000019FC
		protected override bool TryAddSchemaSpecificDataRowValues(DataRow dataRow, CsvRow row)
		{
			base.TryAddSimpleOptionalKnownStrings(dataRow, row);
			dataRow["EndpointName"] = MigMonUtilities.TruncateMessage(MigMonUtilities.GetColumnStringValue(row, "EndpointName"), 500);
			dataRow["EndpointRemoteServer"] = MigMonUtilities.TruncateMessage(MigMonUtilities.GetColumnStringValue(row, "EndpointRemoteServer"), 500);
			return true;
		}

		// Token: 0x04000024 RID: 36
		private const string LogTypeName = "Migration Service Endpoint Log";
	}
}
