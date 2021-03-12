using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.LogAnalyzer.Extensions.MigrationLog;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000028 RID: 40
	internal class MRSWorkloadAvailabilityLogProcessor : BaseLogProcessor
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00007BEB File Offset: 0x00005DEB
		public MRSWorkloadAvailabilityLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, "Logging\\MailboxReplicationService\\MRSAvailability"), "MRSWorkloadAvailability", MigrationMonitor.MRSWorkloadAvailabilityCsvSchemaInstance, "MRSAvailability_*.LOG")
		{
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00007C11 File Offset: 0x00005E11
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetMRSAvailabilityLogUpdateTimestamp";
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00007C18 File Offset: 0x00005E18
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertMRSAvailability_BatchUpload";
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00007C1F File Offset: 0x00005E1F
		protected override string SqlParamName
		{
			get
			{
				return "MrsWorkloadAvailabilityList";
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00007C26 File Offset: 0x00005E26
		protected override string SqlTypeName
		{
			get
			{
				return "dbo.MrsWorkloadAvailabilityData";
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00007C30 File Offset: 0x00005E30
		protected override string[] DistinctColumns
		{
			get
			{
				return new string[]
				{
					"WorkloadTypeId"
				};
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007D00 File Offset: 0x00005F00
		protected override void AddValuesInDataTable(DataTable table, List<CsvRow> allLogRows, DateTime lastLogUpdateTs, out int recordsInFile, out int errorsInFile)
		{
			recordsInFile = 0;
			errorsInFile = 0;
			foreach (CsvRow csvRow in from logRow in allLogRows
			where logRow.Index != 0
			select logRow)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				if (csvRow.ColumnMap.Contains("EventData") && MRSAvailabilityLogLine.TryParseWorkloadStates(csvRow["EventData"], dictionary))
				{
					foreach (KeyValuePair<string, int> kvp in dictionary)
					{
						DateTime? dateTime = new DateTime?(MigMonUtilities.GetColumnValue<DateTime>(csvRow, this.CsvSchemaInstance.TimeStampColumnName));
						if (dateTime == null || !(dateTime.Value <= lastLogUpdateTs))
						{
							recordsInFile++;
							DataRow dataRow = table.NewRow();
							KeyValuePair<string, int> kvp1 = kvp;
							Action<DataRow, CsvRow> insertAction = delegate(DataRow dr, CsvRow lr)
							{
								this.AddCommonDataRowValues(dr, lr);
								int? valueFromIdMap = MigMonUtilities.GetValueFromIdMap(kvp1.Key, KnownStringType.RequestWorkloadType, KnownStringsHelper.KnownStringToSqlLookupParam[KnownStringType.RequestWorkloadType]);
								dr["WorkloadTypeId"] = ((valueFromIdMap != null) ? valueFromIdMap.Value : DBNull.Value);
								dr["Version"] = MigMonUtilities.GetColumnValue<int>(lr, "Version");
								dataRow["Availability"] = (float)kvp1.Value;
							};
							if (!base.HandleInsertCsvRowExceptions(insertAction, dataRow, csvRow))
							{
								errorsInFile++;
							}
							table.Rows.Add(dataRow);
						}
					}
				}
			}
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007E98 File Offset: 0x00006098
		protected override bool TryAddSchemaSpecificDataRowValues(DataRow dataRow, CsvRow row)
		{
			return true;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007E9B File Offset: 0x0000609B
		protected override void LogUploadStoredProcedureHandlerError(SqlQueryFailedException ex)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error uploading MRS workload availability logs. Will attempt again next cycle.", new object[0]);
			throw new UploadMrsAvailabilityLogInBatchFailureException(ex.InnerException);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007EC4 File Offset: 0x000060C4
		protected override void LogInsertCsvRowHandlerError(FormatException ex, CsvRow row)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error parsing MRS availability log.", new object[0]);
		}

		// Token: 0x040000EF RID: 239
		public new const string KeyNameIsLogProcessorEnabled = "IsMRSWorkloadAvailabilityLogProcessorEnabled";

		// Token: 0x040000F0 RID: 240
		private const string LogTypeName = "MRSWorkloadAvailability";
	}
}
