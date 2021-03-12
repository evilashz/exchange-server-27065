using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000002 RID: 2
	internal abstract class BaseLogProcessor
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		protected BaseLogProcessor(string logDirectoryPath, string logFileTypeName, BaseMigMonCsvSchema csvSchemaInstance, string logFileSearchPattern = null)
		{
			this.logDirectoryPath = logDirectoryPath;
			this.logFileTypeName = logFileTypeName;
			this.CsvSchemaInstance = csvSchemaInstance;
			this.logFileSearchPattern = (string.IsNullOrEmpty(logFileSearchPattern) ? "*.log" : logFileSearchPattern);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002105 File Offset: 0x00000305
		public string LogFileTypeName
		{
			get
			{
				return this.logFileTypeName;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3
		protected abstract string StoredProcNameToGetLastUpdateTimeStamp { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4
		protected abstract string SqlSprocNameToHandleUpload { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000005 RID: 5
		protected abstract string SqlParamName { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000006 RID: 6
		protected abstract string SqlTypeName { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000007 RID: 7 RVA: 0x0000210D File Offset: 0x0000030D
		protected virtual bool AlwaysUploadLatestLog
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002110 File Offset: 0x00000310
		protected virtual bool AvoidAccessingLockedLogs
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002113 File Offset: 0x00000313
		protected virtual string[] DistinctColumns
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002118 File Offset: 0x00000318
		public void ProcessLogs()
		{
			string[] logFiles = this.GetLogFiles();
			if (logFiles == null)
			{
				return;
			}
			DateTime logLastUpdateTS = this.GetLogLastUpdateTS();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Processing all files modified after {0}", new object[]
			{
				logLastUpdateTS.ToString()
			});
			string[] array = logFiles;
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i];
				DateTime t = DateTime.UtcNow;
				try
				{
					t = File.GetLastWriteTime(text);
				}
				catch (IOException ex)
				{
					this.LogExceptionAndSendWatson("Error accessing log file for last write time", text, ex);
					goto IL_173;
				}
				goto IL_83;
				IL_173:
				i++;
				continue;
				IL_83:
				if ((num3 <= 1 && this.AlwaysUploadLatestLog) || !(t <= logLastUpdateTS))
				{
					num3++;
					MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Processing file {0}", new object[]
					{
						Path.GetFileName(text)
					});
					List<CsvRow> allLogRows;
					try
					{
						using (FileStream fileStream = this.OpenFileToRead(text, this.AvoidAccessingLockedLogs))
						{
							allLogRows = this.CsvSchemaInstance.Read(fileStream, null, false, false).Reverse<CsvRow>().ToList<CsvRow>();
						}
					}
					catch (CsvValidationException ex2)
					{
						this.LogExceptionAndSendWatson("Invalid CSV format.", text, ex2);
						goto IL_173;
					}
					int num4;
					int num5;
					this.InsertLogs(allLogRows, logLastUpdateTS, out num4, out num5);
					num += num4;
					num2 += num5;
					MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Finished processing file {0}. File Stats: Records inserted/updated: {1}; Records skipped due to errors: {2}.", new object[]
					{
						Path.GetFileName(text),
						num4,
						num5
					});
					goto IL_173;
				}
				break;
			}
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Finished processing {0} logs. Files processed: {1}; Records inserted/updated: {2}; Records skipped due to errors: {3}.", new object[]
			{
				this.logFileTypeName,
				num3,
				num,
				num2
			});
		}

		// Token: 0x0600000B RID: 11
		protected abstract void LogUploadStoredProcedureHandlerError(SqlQueryFailedException ex);

		// Token: 0x0600000C RID: 12
		protected abstract bool TryAddSchemaSpecificDataRowValues(DataRow dataRow, CsvRow row);

		// Token: 0x0600000D RID: 13
		protected abstract void LogInsertCsvRowHandlerError(FormatException ex, CsvRow row);

		// Token: 0x0600000E RID: 14 RVA: 0x0000231C File Offset: 0x0000051C
		protected virtual bool ValidateLogForUpload(List<CsvRow> allLogRows)
		{
			return true;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000235C File Offset: 0x0000055C
		protected virtual void AddValuesInDataTable(DataTable table, List<CsvRow> allLogRows, DateTime lastLogUpdateTs, out int recordsInFile, out int errorsInFile)
		{
			recordsInFile = 0;
			errorsInFile = 0;
			foreach (CsvRow csvRow in from logRow in allLogRows
			where logRow.Index != 0
			select logRow)
			{
				DateTime columnValue = MigMonUtilities.GetColumnValue<DateTime>(csvRow, this.CsvSchemaInstance.TimeStampColumnName);
				if (!(columnValue <= lastLogUpdateTs))
				{
					recordsInFile++;
					DataRow dataRow = table.NewRow();
					this.CsvSchemaInstance.GetRequiredColumnsIds().ForEach(delegate(ColumnDefinition<int> rc)
					{
						BaseLogProcessor.InitialInitDataRowLookups(rc.DataTableKeyColumnName, dataRow);
					});
					this.CsvSchemaInstance.GetOptionalColumnsIds().ForEach(delegate(ColumnDefinition<int> oc)
					{
						BaseLogProcessor.InitialInitDataRowLookups(oc.DataTableKeyColumnName, dataRow);
					});
					if (!this.HandleInsertCsvRowExceptions(new Action<DataRow, CsvRow>(this.InsertCsvRowInDataTable), dataRow, csvRow))
					{
						errorsInFile++;
					}
					else if (!this.TryAddSchemaSpecificDataRowValues(dataRow, csvRow))
					{
						errorsInFile++;
					}
					else
					{
						table.Rows.Add(dataRow);
					}
				}
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000248C File Offset: 0x0000068C
		protected void AddCommonDataRowValues(DataRow dataRow, CsvRow row)
		{
			dataRow[this.CsvSchemaInstance.TimeStampColumnName] = MigMonUtilities.GetColumnValue<SqlDateTime>(row, this.CsvSchemaInstance.TimeStampColumnName);
			dataRow["LoggingServerId"] = MigMonUtilities.GetLocalServerId();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000024CC File Offset: 0x000006CC
		protected bool TryAddStringValueByLookupId(DataRow dataRow, CsvRow row, KnownStringType knownStringType, string errorString, bool isOptional = true)
		{
			ColumnDefinition<int> lookupColumnDefinition = MigMonUtilities.GetLookupColumnDefinition(isOptional ? this.CsvSchemaInstance.GetOptionalColumnsIds() : this.CsvSchemaInstance.GetRequiredColumnsIds(), knownStringType);
			return this.TryAddStringValueByLookupId(lookupColumnDefinition, dataRow, row, errorString, isOptional);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000250C File Offset: 0x0000070C
		protected bool TryAddStringValueByLookupId(ColumnDefinition<int> columnDefinition, DataRow dataRow, CsvRow row, string errorString, bool isOptional = true)
		{
			if (columnDefinition == null || columnDefinition.KnownStringType == KnownStringType.None)
			{
				return isOptional;
			}
			string columnName = columnDefinition.ColumnName;
			string dataTableKeyColumnName = columnDefinition.DataTableKeyColumnName;
			KnownStringType knownStringType = columnDefinition.KnownStringType;
			string convertedRowString = columnDefinition.GetConvertedRowString(row);
			if (string.IsNullOrWhiteSpace(convertedRowString) && !isOptional)
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, errorString ?? string.Empty, new object[0]);
				return false;
			}
			int? valueFromIdMap = MigMonUtilities.GetValueFromIdMap(convertedRowString, knownStringType, KnownStringsHelper.KnownStringToSqlLookupParam[knownStringType]);
			dataRow[dataTableKeyColumnName] = ((valueFromIdMap != null) ? valueFromIdMap.Value : DBNull.Value);
			return isOptional || (!(dataRow[dataTableKeyColumnName] is DBNull) && (int)dataRow[dataTableKeyColumnName] != 0);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000260C File Offset: 0x0000080C
		protected void TryAddSimpleOptionalKnownStrings(DataRow dataRow, CsvRow row)
		{
			List<ColumnDefinition<int>> list = (from c in this.CsvSchemaInstance.GetOptionalColumnsIds()
			where !KnownStringsHelper.SpecialKnownStrings.Contains(c.KnownStringType)
			select c).ToList<ColumnDefinition<int>>();
			list.ForEach(delegate(ColumnDefinition<int> oc)
			{
				this.TryAddStringValueByLookupId(oc, dataRow, row, null, true);
			});
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000267C File Offset: 0x0000087C
		protected bool HandleInsertCsvRowExceptions(Action<DataRow, CsvRow> insertAction, DataRow dataRow, CsvRow row)
		{
			bool result;
			try
			{
				insertAction(dataRow, row);
				result = true;
			}
			catch (FormatException ex)
			{
				this.LogInsertCsvRowHandlerError(ex, row);
				result = false;
			}
			return result;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000026B4 File Offset: 0x000008B4
		private static void InitialInitDataRowLookups(string rowKey, DataRow dataRow)
		{
			dataRow[rowKey] = 0;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000026C3 File Offset: 0x000008C3
		private void InsertCsvRowInDataTable(DataRow dataRow, CsvRow row)
		{
			this.AddCommonDataRowValues(dataRow, row);
			this.InsertValuesInDataRow(dataRow, row);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000026D8 File Offset: 0x000008D8
		private void InsertValuesInDataRow(DataRow dataRow, CsvRow row)
		{
			foreach (IColumnDefinition columnDefinition in this.CsvSchemaInstance.GetRequiredColumnsAsIs())
			{
				columnDefinition.InsertColumnValueInDataRow(dataRow, row);
			}
			foreach (IColumnDefinition columnDefinition2 in this.CsvSchemaInstance.GetOptionalColumnsAsIs())
			{
				columnDefinition2.InsertColumnValueInDataRow(dataRow, row);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000277C File Offset: 0x0000097C
		private void InsertLogs(List<CsvRow> allLogRows, DateTime lastLogUpdateTs, out int recordsInFile, out int errorsInFile)
		{
			recordsInFile = 0;
			errorsInFile = 0;
			if (!this.ValidateLogForUpload(allLogRows))
			{
				return;
			}
			DataTable csvSchemaDataTable = this.CsvSchemaInstance.GetCsvSchemaDataTable();
			this.AddValuesInDataTable(csvSchemaDataTable, allLogRows, lastLogUpdateTs, out recordsInFile, out errorsInFile);
			this.RemoveDuplicateRowsFromDataTable(csvSchemaDataTable);
			this.InvokeUploadStoredProcedure(csvSchemaDataTable, this.SqlParamName, this.SqlTypeName);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000027CC File Offset: 0x000009CC
		private void RemoveDuplicateRowsFromDataTable(DataTable dataTable)
		{
			if (this.DistinctColumns == null)
			{
				return;
			}
			Dictionary<string, object[]> dictionary = new Dictionary<string, object[]>();
			DataTable dataTable2 = dataTable.Clone();
			foreach (object obj in dataTable.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				string text = string.Empty;
				foreach (string columnName in this.DistinctColumns)
				{
					text += dataRow[columnName].ToString();
				}
				if (dictionary.ContainsKey(text))
				{
					dataTable2.Rows.Add(dictionary[text]);
					if ((SqlDateTime)dataTable2.Rows[0][this.CsvSchemaInstance.TimeStampColumnName] < (SqlDateTime)dataRow[this.CsvSchemaInstance.TimeStampColumnName])
					{
						dictionary[text] = (dataRow.ItemArray.Clone() as object[]);
					}
					dataTable2.Rows.Clear();
				}
				else
				{
					dictionary[text] = (dataRow.ItemArray.Clone() as object[]);
				}
			}
			dataTable.Rows.Clear();
			foreach (KeyValuePair<string, object[]> keyValuePair in dictionary)
			{
				dataTable.Rows.Add(keyValuePair.Value);
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002970 File Offset: 0x00000B70
		private void InvokeUploadStoredProcedure(DataTable table, string sqlParamName, string sqlTypeName)
		{
			List<SqlParameter> list = new List<SqlParameter>();
			SqlParameter item = new SqlParameter(sqlParamName, table)
			{
				SqlDbType = SqlDbType.Structured,
				TypeName = sqlTypeName
			};
			list.Add(item);
			this.HandleUploadStoredProcedureCall(list);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000029AC File Offset: 0x00000BAC
		private void HandleUploadStoredProcedureCall(List<SqlParameter> paramList)
		{
			int config = MigrationMonitor.MigrationMonitorContext.Config.GetConfig<int>("BulkInsertSqlCommandTimeout");
			try
			{
				MigrationMonitor.SqlHelper.ExecuteSprocNonQuery(this.SqlSprocNameToHandleUpload, paramList, config);
			}
			catch (SqlQueryFailedException ex)
			{
				this.LogUploadStoredProcedureHandlerError(ex);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000029FC File Offset: 0x00000BFC
		private DateTime GetLogLastUpdateTS()
		{
			List<SqlParameter> list = new List<SqlParameter>();
			MigrationMonitor.SqlHelper.AddSqlParameter(list, "serverName", MigrationMonitor.ComputerName, false, false);
			return MigrationMonitor.SqlHelper.ExecuteSprocScalar<DateTime>(this.StoredProcNameToGetLastUpdateTimeStamp, list);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002A3C File Offset: 0x00000C3C
		private string[] GetLogFiles()
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Starting to process {0} logs.", new object[]
			{
				this.logFileTypeName
			});
			string[] result;
			try
			{
				result = this.GetFilesInReverseModifiedOrder(this.logDirectoryPath);
			}
			catch (DirectoryNotExistException)
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, "No directory for {0} log files found in server {1}", new object[]
				{
					this.logFileTypeName,
					MigrationMonitor.ComputerName
				});
				result = null;
			}
			catch (LogFileLoadException ex)
			{
				this.LogExceptionAndSendWatson("Error loading logs for the log file type", null, ex);
				result = null;
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002AE8 File Offset: 0x00000CE8
		private string[] GetFilesInReverseModifiedOrder(string directoryName)
		{
			List<string> list = new List<string>();
			string[] result;
			try
			{
				if (!Directory.Exists(directoryName))
				{
					throw new DirectoryNotExistException(directoryName);
				}
				DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);
				FileInfo[] array = (from p in directoryInfo.GetFiles(this.logFileSearchPattern)
				orderby p.LastWriteTime descending
				select p).ToArray<FileInfo>();
				foreach (FileInfo fileInfo in array)
				{
					list.Add(fileInfo.FullName);
				}
				result = list.ToArray();
			}
			catch (IOException ex)
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Information, "Error while loading files from directory {0}. Exception: {1}", new object[]
				{
					directoryName,
					ex
				});
				throw new LogFileLoadException(directoryName, ex.InnerException);
			}
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002BC4 File Offset: 0x00000DC4
		private void LogExceptionAndSendWatson(string errorStr, string filePath, Exception ex)
		{
			MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "{0} log file type {1}, file path {2}", new object[]
			{
				errorStr,
				this.logFileTypeName,
				filePath
			});
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(errorStr);
			stringBuilder.AppendLine(string.Format("Log File Type: {0}", this.logFileTypeName));
			if (!string.IsNullOrEmpty(filePath))
			{
				stringBuilder.AppendLine(string.Format("Log File path: {0}", filePath));
			}
			stringBuilder.AppendLine(string.Format("Exception: {0}", ex));
			ExWatson.SendReport(ex, ReportOptions.None, stringBuilder.ToString());
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002C60 File Offset: 0x00000E60
		private FileStream OpenFileToRead(string inputFile, bool onlyReadExlusiveFromWrite)
		{
			FileStream result;
			try
			{
				result = new FileStream(inputFile, FileMode.Open, FileAccess.Read, onlyReadExlusiveFromWrite ? FileShare.Read : FileShare.ReadWrite);
			}
			catch (IOException ex)
			{
				MigrationMonitor.MigrationMonitorContext.Logger.Log(MigrationEventType.Error, ex, "Error reading {0} File. record. File name: {1}. Will try again in the next cycle.", new object[]
				{
					this.logFileTypeName,
					inputFile
				});
				throw new LogFileReadException(inputFile, ex);
			}
			return result;
		}

		// Token: 0x04000001 RID: 1
		public const string KeyNameIsLogProcessorEnabled = "IsBaseLogProcessorEnabled";

		// Token: 0x04000002 RID: 2
		protected const string LogFileExtension = "*.log";

		// Token: 0x04000003 RID: 3
		protected readonly BaseMigMonCsvSchema CsvSchemaInstance;

		// Token: 0x04000004 RID: 4
		private readonly string logFileTypeName;

		// Token: 0x04000005 RID: 5
		private readonly string logDirectoryPath;

		// Token: 0x04000006 RID: 6
		private readonly string logFileSearchPattern;
	}
}
