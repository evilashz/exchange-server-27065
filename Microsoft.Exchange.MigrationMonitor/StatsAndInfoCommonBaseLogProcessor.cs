using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000011 RID: 17
	internal abstract class StatsAndInfoCommonBaseLogProcessor : BaseLogProcessor
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00003D75 File Offset: 0x00001F75
		protected StatsAndInfoCommonBaseLogProcessor(string logDirectoryPath, string logFileTypeName, BaseMigMonCsvSchema csvSchemaInstance, string logFileSearchPattern) : base(logDirectoryPath, logFileTypeName, csvSchemaInstance, logFileSearchPattern)
		{
			this.uploadedDatabases = new List<string>();
			this.CurrentDatabaseName = string.Empty;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003D98 File Offset: 0x00001F98
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003DA0 File Offset: 0x00001FA0
		protected string CurrentDatabaseName { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003DA9 File Offset: 0x00001FA9
		protected override bool AlwaysUploadLatestLog
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003DAC File Offset: 0x00001FAC
		protected override bool AvoidAccessingLockedLogs
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003DBC File Offset: 0x00001FBC
		protected override bool ValidateLogForUpload(List<CsvRow> allLogRows)
		{
			string columnName = this.CsvSchemaInstance.GetRequiredColumnsIds().First((ColumnDefinition<int> lookupCols) => lookupCols.KnownStringType == KnownStringType.DatabaseName).ColumnName;
			return this.ValidateLogForUpload(allLogRows, columnName);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003E04 File Offset: 0x00002004
		protected bool TryAddDatabaseIdKeyValue(DataRow dataRow)
		{
			if (this.currentDatabaseId == 0)
			{
				return false;
			}
			ColumnDefinition<int> lookupColumnDefinition = MigMonUtilities.GetLookupColumnDefinition(this.CsvSchemaInstance.GetRequiredColumnsIds(), KnownStringType.DatabaseName);
			if (lookupColumnDefinition == null || lookupColumnDefinition.KnownStringType == KnownStringType.None)
			{
				return false;
			}
			string dataTableKeyColumnName = lookupColumnDefinition.DataTableKeyColumnName;
			dataRow[dataTableKeyColumnName] = this.currentDatabaseId;
			return (int)dataRow[dataTableKeyColumnName] != 0;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003E8C File Offset: 0x0000208C
		private bool ValidateLogRows(List<CsvRow> allLogRows, string databaseNameColumn, out string databaseName)
		{
			databaseName = null;
			if (allLogRows == null || allLogRows.Count <= 1)
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Not enough log data to continue with uploading.", new object[0]);
				return false;
			}
			if (string.IsNullOrEmpty(databaseNameColumn))
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Database name log column is empty - cannot continue with uploading.", new object[0]);
				return false;
			}
			databaseName = (from lr in allLogRows
			where lr.Index != 0
			select lr into logRow
			select MigMonUtilities.GetColumnStringValue(logRow, databaseNameColumn)).FirstOrDefault<string>();
			if (string.IsNullOrWhiteSpace(databaseName))
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Unable to read database name - cannot continue with uploading.", new object[0]);
				return false;
			}
			if (this.uploadedDatabases.Contains(databaseName))
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "We already uploaded database stats for {0}, skip all other log files for the same database", new object[]
				{
					databaseName
				});
				return false;
			}
			this.uploadedDatabases.Add(databaseName);
			return true;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003FA0 File Offset: 0x000021A0
		private bool ValidateLogForUpload(List<CsvRow> allLogRows, string databaseNameColumn)
		{
			this.CurrentDatabaseName = string.Empty;
			this.currentDatabaseId = 0;
			string text;
			if (!this.ValidateLogRows(allLogRows, databaseNameColumn, out text))
			{
				return false;
			}
			this.CurrentDatabaseName = text;
			int? valueFromIdMap = MigMonUtilities.GetValueFromIdMap(text, KnownStringType.DatabaseName, KnownStringsHelper.KnownStringToSqlLookupParam[KnownStringType.DatabaseName]);
			if (valueFromIdMap != null)
			{
				this.currentDatabaseId = valueFromIdMap.Value;
			}
			return true;
		}

		// Token: 0x04000036 RID: 54
		private readonly List<string> uploadedDatabases;

		// Token: 0x04000037 RID: 55
		private int currentDatabaseId;
	}
}
