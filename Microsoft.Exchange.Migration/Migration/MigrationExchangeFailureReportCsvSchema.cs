using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200012A RID: 298
	internal class MigrationExchangeFailureReportCsvSchema : FailureReportCsvSchema
	{
		// Token: 0x06000F3A RID: 3898 RVA: 0x00041666 File Offset: 0x0003F866
		public MigrationExchangeFailureReportCsvSchema() : base(int.MaxValue, MigrationExchangeFailureReportCsvSchema.requiredColumns.Value, MigrationExchangeFailureReportCsvSchema.optionalColumns.Value, null)
		{
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00041688 File Offset: 0x0003F888
		public override void WriteHeader(StreamWriter streamWriter)
		{
			streamWriter.WriteCsvLine(MigrationExchangeFailureReportCsvSchema.Headers);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00041698 File Offset: 0x0003F898
		public override void WriteRow(StreamWriter streamWriter, MigrationJobItem jobItem)
		{
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			string[] columnData = new string[]
			{
				jobItem.Identifier,
				jobItem.LocalizedError ?? LocalizedString.Empty
			};
			streamWriter.WriteCsvLine(columnData);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x000416F0 File Offset: 0x0003F8F0
		public override void WriteRow(StreamWriter streamWriter, MigrationBatchError batchError)
		{
			MigrationUtil.ThrowOnNullArgument(batchError, "batchError");
			string[] columnData = new string[]
			{
				batchError.EmailAddress,
				batchError.LocalizedErrorMessage
			};
			streamWriter.WriteCsvLine(columnData);
		}

		// Token: 0x04000525 RID: 1317
		private static readonly string[] RequiredColumnNames = new string[]
		{
			"EmailAddress",
			"ErrorMessage"
		};

		// Token: 0x04000526 RID: 1318
		private static readonly string[] OptionalColumnNames = new string[0];

		// Token: 0x04000527 RID: 1319
		private static Lazy<Dictionary<string, ProviderPropertyDefinition>> optionalColumns = new Lazy<Dictionary<string, ProviderPropertyDefinition>>(() => MigrationCsvSchemaBase.GenerateDefaultColumnInfo(MigrationExchangeFailureReportCsvSchema.OptionalColumnNames), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04000528 RID: 1320
		private static Lazy<Dictionary<string, ProviderPropertyDefinition>> requiredColumns = new Lazy<Dictionary<string, ProviderPropertyDefinition>>(() => MigrationCsvSchemaBase.GenerateDefaultColumnInfo(MigrationExchangeFailureReportCsvSchema.RequiredColumnNames), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x04000529 RID: 1321
		private static readonly string[] Headers = new string[]
		{
			"EmailAddress",
			"ErrorMessage"
		};
	}
}
