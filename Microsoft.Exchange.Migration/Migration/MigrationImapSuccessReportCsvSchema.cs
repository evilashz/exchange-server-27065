using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200012E RID: 302
	internal class MigrationImapSuccessReportCsvSchema : ReportCsvSchema
	{
		// Token: 0x06000F54 RID: 3924 RVA: 0x00041DE1 File Offset: 0x0003FFE1
		public MigrationImapSuccessReportCsvSchema() : base(int.MaxValue, MigrationImapSuccessReportCsvSchema.requiredColumns.Value, MigrationImapSuccessReportCsvSchema.optionalColumns.Value, null)
		{
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00041E03 File Offset: 0x00040003
		public override void WriteHeader(StreamWriter streamWriter)
		{
			streamWriter.WriteCsvLine(MigrationImapSuccessReportCsvSchema.Headers);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x00041E10 File Offset: 0x00040010
		public override void WriteRow(StreamWriter streamWriter, MigrationJobItem jobItem)
		{
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			List<string> list = new List<string>(MigrationImapSuccessReportCsvSchema.Headers.Length);
			list.Add(jobItem.Identifier);
			if (jobItem.ItemsSkipped != 0L)
			{
				list.Add(ServerStrings.MigrationStatisticsPartiallyCompleteStatus);
			}
			else
			{
				list.Add(ServerStrings.MigrationStatisticsCompleteStatus);
			}
			list.Add(jobItem.ItemsSynced.ToString(CultureInfo.CurrentCulture));
			list.Add(jobItem.ItemsSkipped.ToString(CultureInfo.CurrentCulture));
			streamWriter.WriteCsvLine(list);
		}

		// Token: 0x04000542 RID: 1346
		private static readonly string[] RequiredColumnNames = new string[]
		{
			"EmailAddress",
			"Status",
			"ItemsMigrated",
			"ItemsSkipped"
		};

		// Token: 0x04000543 RID: 1347
		private static readonly string[] OptionalColumnNames = new string[]
		{
			"AdditionalComments"
		};

		// Token: 0x04000544 RID: 1348
		private static Lazy<Dictionary<string, ProviderPropertyDefinition>> optionalColumns = new Lazy<Dictionary<string, ProviderPropertyDefinition>>(() => MigrationCsvSchemaBase.GenerateDefaultColumnInfo(MigrationImapSuccessReportCsvSchema.OptionalColumnNames), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04000545 RID: 1349
		private static Lazy<Dictionary<string, ProviderPropertyDefinition>> requiredColumns = new Lazy<Dictionary<string, ProviderPropertyDefinition>>(() => MigrationCsvSchemaBase.GenerateDefaultColumnInfo(MigrationImapSuccessReportCsvSchema.RequiredColumnNames), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04000546 RID: 1350
		private static readonly string[] Headers = new string[]
		{
			"EmailAddress",
			"Status",
			"ItemsMigrated",
			"ItemsSkipped"
		};
	}
}
