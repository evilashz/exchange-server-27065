using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200012C RID: 300
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class MigrationFailureReportCsvSchema
	{
		// Token: 0x06000F48 RID: 3912 RVA: 0x00041A90 File Offset: 0x0003FC90
		public static void WriteHeader(StreamWriter streamWriter, MigrationType migrationType, bool isBatchCompletionReport)
		{
			FailureReportCsvSchema schema = MigrationFailureReportCsvSchema.GetSchema(migrationType, isBatchCompletionReport);
			schema.WriteHeader(streamWriter);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00041AAC File Offset: 0x0003FCAC
		public static void Write(StreamWriter streamWriter, MigrationType migrationType, IEnumerable<MigrationJobItem> jobItemCollection, bool isBatchCompletionReport)
		{
			FailureReportCsvSchema schema = MigrationFailureReportCsvSchema.GetSchema(migrationType, isBatchCompletionReport);
			foreach (MigrationJobItem jobItem in jobItemCollection)
			{
				schema.WriteRow(streamWriter, jobItem);
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00041B00 File Offset: 0x0003FD00
		public static void Write(MigrationType migrationType, StreamWriter streamWriter, IEnumerable<MigrationBatchError> errorCollection, bool isBatchCompletionReport)
		{
			FailureReportCsvSchema schema = MigrationFailureReportCsvSchema.GetSchema(migrationType, isBatchCompletionReport);
			foreach (MigrationBatchError batchError in errorCollection)
			{
				schema.WriteRow(streamWriter, batchError);
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00041B54 File Offset: 0x0003FD54
		internal static FailureReportCsvSchema GetSchema(MigrationType migrationType, bool isBatchCompletionReport)
		{
			if (migrationType <= MigrationType.ExchangeOutlookAnywhere)
			{
				if (migrationType == MigrationType.IMAP)
				{
					return new MigrationImapFailureReportCsvSchema(isBatchCompletionReport);
				}
				if (migrationType != MigrationType.ExchangeOutlookAnywhere)
				{
					goto IL_35;
				}
			}
			else if (migrationType != MigrationType.ExchangeRemoteMove && migrationType != MigrationType.ExchangeLocalMove && migrationType != MigrationType.PublicFolder)
			{
				goto IL_35;
			}
			return new MigrationExchangeFailureReportCsvSchema();
			IL_35:
			string text = "MigrationSuccessReportCsvSchema invoked with unsupported migrationType " + migrationType;
			MigrationLogger.Log(MigrationEventType.Error, text, new object[0]);
			throw new InvalidOperationException(text);
		}

		// Token: 0x04000535 RID: 1333
		public const int InternalMaximumRowCount = 2147483647;

		// Token: 0x04000536 RID: 1334
		public const string RowIndexColumnName = "RowIndex";

		// Token: 0x04000537 RID: 1335
		public const string EmailColumnName = "EmailAddress";

		// Token: 0x04000538 RID: 1336
		public const string ErrorMessageColumnName = "ErrorMessage";
	}
}
