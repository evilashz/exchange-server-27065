using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000131 RID: 305
	internal class MigrationMoveSuccessReportCsvSchema : ReportCsvSchema
	{
		// Token: 0x06000F70 RID: 3952 RVA: 0x00042363 File Offset: 0x00040563
		public MigrationMoveSuccessReportCsvSchema() : base(int.MaxValue, MigrationMoveSuccessReportCsvSchema.requiredColumns.Value, MigrationMoveSuccessReportCsvSchema.optionalColumns.Value, null)
		{
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x00042385 File Offset: 0x00040585
		public override void WriteHeader(StreamWriter streamWriter)
		{
			streamWriter.WriteCsvLine(MigrationMoveSuccessReportCsvSchema.Headers);
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x00042394 File Offset: 0x00040594
		public override void WriteRow(StreamWriter streamWriter, MigrationJobItem jobItem)
		{
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			List<string> list = new List<string>(MigrationMoveSuccessReportCsvSchema.Headers.Length);
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

		// Token: 0x0400055F RID: 1375
		private static readonly string[] RequiredColumnNames = new string[]
		{
			"EmailAddress",
			"Status",
			"ItemsMigrated",
			"ItemsSkipped"
		};

		// Token: 0x04000560 RID: 1376
		private static readonly string[] OptionalColumnNames = new string[]
		{
			"AdditionalComments"
		};

		// Token: 0x04000561 RID: 1377
		private static Lazy<Dictionary<string, ProviderPropertyDefinition>> optionalColumns = new Lazy<Dictionary<string, ProviderPropertyDefinition>>(() => MigrationCsvSchemaBase.GenerateDefaultColumnInfo(MigrationMoveSuccessReportCsvSchema.OptionalColumnNames), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04000562 RID: 1378
		private static Lazy<Dictionary<string, ProviderPropertyDefinition>> requiredColumns = new Lazy<Dictionary<string, ProviderPropertyDefinition>>(() => MigrationCsvSchemaBase.GenerateDefaultColumnInfo(MigrationMoveSuccessReportCsvSchema.RequiredColumnNames), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04000563 RID: 1379
		private static readonly string[] Headers = new string[]
		{
			"EmailAddress",
			"Status",
			"ItemsMigrated",
			"ItemsSkipped"
		};
	}
}
