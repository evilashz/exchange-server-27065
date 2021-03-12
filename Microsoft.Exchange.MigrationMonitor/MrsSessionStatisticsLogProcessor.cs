using System;
using System.Data;
using System.IO;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200002A RID: 42
	internal class MrsSessionStatisticsLogProcessor : BaseMrsMonitorLogProcessor
	{
		// Token: 0x0600016B RID: 363 RVA: 0x000081DE File Offset: 0x000063DE
		public MrsSessionStatisticsLogProcessor() : base(Path.Combine(MigrationMonitor.ExchangeInstallPath, "Logging\\MailboxReplicationService\\SessionStatistics"), "MRS SessionStatistics", MigrationMonitor.MrsSessionStatisticsCsvSchemaInstance)
		{
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00008200 File Offset: 0x00006400
		protected override string[] DistinctColumns
		{
			get
			{
				return new string[]
				{
					"RequestGuid",
					"SessionId",
					"SessionId_Archive"
				};
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000822D File Offset: 0x0000642D
		protected override string StoredProcNameToGetLastUpdateTimeStamp
		{
			get
			{
				return "MIGMON_GetSessionStatisticsLogUpdateTimestamp";
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00008234 File Offset: 0x00006434
		protected override string SqlSprocNameToHandleUpload
		{
			get
			{
				return "MIGMON_InsertMrsSessionStatistics_BatchUpload";
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000823B File Offset: 0x0000643B
		protected override string SqlParamName
		{
			get
			{
				return "MrsSessionStatisticsList";
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00008242 File Offset: 0x00006442
		protected override string SqlTypeName
		{
			get
			{
				return "dbo.MrsSessionStatisticsData";
			}
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000824C File Offset: 0x0000644C
		protected override bool TryAddSchemaSpecificDataRowValues(DataRow dataRow, CsvRow row)
		{
			string empty = string.Empty;
			base.TryAddStringValueByLookupId(dataRow, row, KnownStringType.MaxProviderDurationMethodName, empty, true);
			return true;
		}

		// Token: 0x040000F7 RID: 247
		public new const string KeyNameIsLogProcessorEnabled = "IsMrsSessionStatisticsLogProcessorEnabled";

		// Token: 0x040000F8 RID: 248
		private const string LogTypeName = "MRS SessionStatistics";
	}
}
