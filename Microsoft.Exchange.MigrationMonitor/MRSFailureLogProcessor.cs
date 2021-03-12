using System;
using System.Data;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000024 RID: 36
	internal class MRSFailureLogProcessor : BaseMrsMonitorLogProcessor
	{
		// Token: 0x0600013E RID: 318 RVA: 0x00007212 File Offset: 0x00005412
		public MRSFailureLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, "Logging\\MailboxReplicationService\\Failures"), "MRS Failure", MigrationMonitor.MRSFailureCsvSchemaInstance)
		{
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00007233 File Offset: 0x00005433
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetFailureLogUpdateTimestamp";
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000723A File Offset: 0x0000543A
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertMRSFailure_BatchUploadV4";
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00007241 File Offset: 0x00005441
		protected override string SqlParamName
		{
			get
			{
				return "MrsFailureList";
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00007248 File Offset: 0x00005448
		protected override string SqlTypeName
		{
			get
			{
				return "dbo.MrsFailureDataV4";
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00007250 File Offset: 0x00005450
		protected override bool TryAddSchemaSpecificDataRowValues(DataRow dataRow, CsvRow row)
		{
			base.TryAddSimpleOptionalKnownStrings(dataRow, row);
			string text = MigMonUtilities.GetColumnStringValue(row, "OperationType");
			text = MigMonUtilities.TruncateMessage(text, 500);
			dataRow["OperationType"] = text;
			dataRow["StackTrace"] = DBNull.Value;
			dataRow[MRSFailureCsvSchema.WatsonHashColumn.DataTableKeyColumnName] = DBNull.Value;
			string columnStringValue = MigMonUtilities.GetColumnStringValue(row, MRSFailureCsvSchema.WatsonHashColumn.ColumnName);
			string columnStringValue2 = MigMonUtilities.GetColumnStringValue(row, "StackTrace");
			if (!string.IsNullOrWhiteSpace(columnStringValue) && !string.IsNullOrWhiteSpace(columnStringValue2))
			{
				int? watsonHashId = MigMonUtilities.GetWatsonHashId(columnStringValue, columnStringValue2, "MRS");
				if (watsonHashId != null)
				{
					dataRow[MRSFailureCsvSchema.WatsonHashColumn.DataTableKeyColumnName] = watsonHashId;
				}
			}
			string errorString = string.Format("Error parsing MRS failure log. Request guid is {0}", MigMonUtilities.GetColumnValue<Guid>(row, "RequestGuid"));
			return base.TryAddStringValueByLookupId(dataRow, row, KnownStringType.FailureType, errorString, false);
		}

		// Token: 0x040000E1 RID: 225
		private const string LogTypeName = "MRS Failure";
	}
}
