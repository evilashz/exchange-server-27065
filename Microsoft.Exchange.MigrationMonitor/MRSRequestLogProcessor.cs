using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000026 RID: 38
	internal class MRSRequestLogProcessor : BaseMrsMonitorLogProcessor
	{
		// Token: 0x0600014B RID: 331 RVA: 0x000078B5 File Offset: 0x00005AB5
		public MRSRequestLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, "Logging\\MailboxReplicationService\\Requests"), "MRS Request", MigrationMonitor.MRSRequestCsvSchemaInstance)
		{
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000078D6 File Offset: 0x00005AD6
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetRequestLogUpdateTimestamp";
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000078DD File Offset: 0x00005ADD
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertMRSRequest_BatchUploadV5";
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000078E4 File Offset: 0x00005AE4
		protected override string SqlParamName
		{
			get
			{
				return "MrsRequestList";
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000078EB File Offset: 0x00005AEB
		protected override string SqlTypeName
		{
			get
			{
				return "dbo.MrsRequestDataV5";
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000078F4 File Offset: 0x00005AF4
		protected override string[] DistinctColumns
		{
			get
			{
				return new string[]
				{
					"RequestGuid"
				};
			}
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007944 File Offset: 0x00005B44
		protected override bool TryAddSchemaSpecificDataRowValues(DataRow dataRow, CsvRow row)
		{
			List<ColumnDefinition<int>> list = (from c in this.CsvSchemaInstance.GetOptionalColumnsIds()
			where c.KnownStringType != KnownStringType.TenantName
			select c).ToList<ColumnDefinition<int>>();
			list.ForEach(delegate(ColumnDefinition<int> oc)
			{
				this.TryAddStringValueByLookupId(oc, dataRow, row, null, true);
			});
			ColumnDefinition<int> lookupColumnDefinition = MigMonUtilities.GetLookupColumnDefinition(this.CsvSchemaInstance.GetOptionalColumnsIds(), KnownStringType.TenantName);
			if (lookupColumnDefinition == null || lookupColumnDefinition.KnownStringType == KnownStringType.None)
			{
				return true;
			}
			string columnName = lookupColumnDefinition.ColumnName;
			string dataTableKeyColumnName = lookupColumnDefinition.DataTableKeyColumnName;
			string columnStringValue = MigMonUtilities.GetColumnStringValue(row, columnName);
			if (!string.IsNullOrWhiteSpace(columnStringValue))
			{
				dataRow[dataTableKeyColumnName] = MigMonUtilities.GetTenantNameId(columnStringValue);
			}
			ColumnDefinition<int> columnDefinition = new ColumnDefinition<int>("RequestType", "RequestTypeId", KnownStringType.RequestType);
			base.TryAddStringValueByLookupId(columnDefinition, dataRow, row, null, true);
			if (!dataRow.Table.Columns.Contains(columnDefinition.DataTableKeyColumnName) || string.IsNullOrWhiteSpace(dataRow[columnDefinition.DataTableKeyColumnName].ToString()))
			{
				dataRow[columnDefinition.DataTableKeyColumnName] = MigMonUtilities.GetValueFromIdMap("Move", KnownStringType.RequestType, KnownStringsHelper.KnownStringToSqlLookupParam[KnownStringType.RequestType]);
			}
			return true;
		}

		// Token: 0x040000E6 RID: 230
		private const string LogTypeName = "MRS Request";
	}
}
