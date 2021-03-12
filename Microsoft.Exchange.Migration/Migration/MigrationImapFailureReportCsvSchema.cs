using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200012D RID: 301
	internal class MigrationImapFailureReportCsvSchema : FailureReportCsvSchema
	{
		// Token: 0x06000F4C RID: 3916 RVA: 0x00041BBC File Offset: 0x0003FDBC
		public MigrationImapFailureReportCsvSchema(bool isCompletionReport) : base(int.MaxValue, MigrationImapFailureReportCsvSchema.requiredColumns.Value, MigrationImapFailureReportCsvSchema.optionalColumns.Value, null)
		{
			this.isCompletionReport = isCompletionReport;
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00041BE5 File Offset: 0x0003FDE5
		private string[] Headers
		{
			get
			{
				if (this.isCompletionReport)
				{
					return MigrationImapFailureReportCsvSchema.CompletionHeaders;
				}
				return MigrationImapFailureReportCsvSchema.FinalizationHeaders;
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00041BFA File Offset: 0x0003FDFA
		public override void WriteHeader(StreamWriter streamWriter)
		{
			streamWriter.WriteCsvLine(this.Headers);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00041C08 File Offset: 0x0003FE08
		public override void WriteRow(StreamWriter streamWriter, MigrationJobItem jobItem)
		{
			MigrationUtil.ThrowOnNullArgument(jobItem, "jobItem");
			List<string> list = new List<string>(this.Headers.Length);
			if (this.isCompletionReport)
			{
				list.Add(jobItem.CursorPosition.ToString(CultureInfo.InvariantCulture));
			}
			list.Add(jobItem.Identifier);
			list.Add(jobItem.LocalizedError ?? LocalizedString.Empty);
			streamWriter.WriteCsvLine(list);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00041C8C File Offset: 0x0003FE8C
		public override void WriteRow(StreamWriter streamWriter, MigrationBatchError batchError)
		{
			MigrationUtil.ThrowOnNullArgument(batchError, "batchError");
			List<string> list = new List<string>(this.Headers.Length);
			if (this.isCompletionReport)
			{
				list.Add(batchError.RowIndex.ToString(CultureInfo.InvariantCulture));
			}
			list.Add(batchError.EmailAddress);
			list.Add(batchError.LocalizedErrorMessage);
			streamWriter.WriteCsvLine(list);
		}

		// Token: 0x04000539 RID: 1337
		private static readonly string[] RequiredColumnNames = new string[]
		{
			"EmailAddress",
			"ErrorMessage"
		};

		// Token: 0x0400053A RID: 1338
		private static readonly string[] OptionalColumnNames = new string[]
		{
			"RowIndex"
		};

		// Token: 0x0400053B RID: 1339
		private static Lazy<Dictionary<string, ProviderPropertyDefinition>> optionalColumns = new Lazy<Dictionary<string, ProviderPropertyDefinition>>(() => MigrationCsvSchemaBase.GenerateDefaultColumnInfo(MigrationImapFailureReportCsvSchema.OptionalColumnNames), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x0400053C RID: 1340
		private static Lazy<Dictionary<string, ProviderPropertyDefinition>> requiredColumns = new Lazy<Dictionary<string, ProviderPropertyDefinition>>(() => MigrationCsvSchemaBase.GenerateDefaultColumnInfo(MigrationImapFailureReportCsvSchema.RequiredColumnNames), LazyThreadSafetyMode.ExecutionAndPublication);

		// Token: 0x0400053D RID: 1341
		private static readonly string[] CompletionHeaders = new string[]
		{
			"RowIndex",
			"EmailAddress",
			"ErrorMessage"
		};

		// Token: 0x0400053E RID: 1342
		private static readonly string[] FinalizationHeaders = new string[]
		{
			"EmailAddress",
			"ErrorMessage"
		};

		// Token: 0x0400053F RID: 1343
		private readonly bool isCompletionReport;
	}
}
