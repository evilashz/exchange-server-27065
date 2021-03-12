using System;
using System.Data;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x02000022 RID: 34
	internal class MRSBadItemLogProcessor : BaseMrsMonitorLogProcessor
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00006FA0 File Offset: 0x000051A0
		public MRSBadItemLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, "Logging\\MailboxReplicationService\\BadItems"), "MRS Bad Item", MigrationMonitor.MRSBadItemCsvSchemaInstance)
		{
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006FC1 File Offset: 0x000051C1
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetBadItemLogUpdateTimestamp";
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006FC8 File Offset: 0x000051C8
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertMRSBadItem_BatchUpload";
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006FCF File Offset: 0x000051CF
		protected override string SqlParamName
		{
			get
			{
				return "MrsBadItemList";
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006FD6 File Offset: 0x000051D6
		protected override string SqlTypeName
		{
			get
			{
				return "dbo.MrsBadItemData";
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006FE0 File Offset: 0x000051E0
		protected override bool TryAddSchemaSpecificDataRowValues(DataRow dataRow, CsvRow row)
		{
			string empty = string.Empty;
			base.TryAddStringValueByLookupId(dataRow, row, KnownStringType.BadItemKind, empty, true);
			base.TryAddStringValueByLookupId(dataRow, row, KnownStringType.BadItemWkfTypeId, empty, true);
			base.TryAddStringValueByLookupId(dataRow, row, KnownStringType.BadItemMessageClass, empty, true);
			base.TryAddStringValueByLookupId(dataRow, row, KnownStringType.BadItemCategory, empty, true);
			string columnStringValue = MigMonUtilities.GetColumnStringValue(row, "BadItemKind");
			string columnStringValue2 = MigMonUtilities.GetColumnStringValue(row, "WKFType");
			string columnStringValue3 = MigMonUtilities.GetColumnStringValue(row, "MessageClass");
			string columnStringValue4 = MigMonUtilities.GetColumnStringValue(row, "Category");
			dataRow["BadItemKind"] = columnStringValue;
			dataRow["WKFType"] = columnStringValue2;
			dataRow["MessageClass"] = columnStringValue3;
			dataRow["Category"] = columnStringValue4;
			string text = MigMonUtilities.GetColumnStringValue(row, "FailureMessage");
			text = MigMonUtilities.TruncateMessage(text, 500);
			dataRow["FailureMessage"] = text;
			return true;
		}

		// Token: 0x040000D2 RID: 210
		private const string LogTypeName = "MRS Bad Item";
	}
}
