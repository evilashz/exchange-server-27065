using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Migration.Logging;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000137 RID: 311
	internal static class MigrationSuccessReportCsvSchema
	{
		// Token: 0x06000FAE RID: 4014 RVA: 0x00043120 File Offset: 0x00041320
		public static void WriteHeader(StreamWriter streamWriter, MigrationType migrationType, bool isBatchCompletionReport, bool isStaged)
		{
			ReportCsvSchema schema = MigrationSuccessReportCsvSchema.GetSchema(migrationType, isBatchCompletionReport, isStaged);
			schema.WriteHeader(streamWriter);
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x00043140 File Offset: 0x00041340
		public static void Write(StreamWriter streamWriter, MigrationType migrationType, IEnumerable<MigrationJobItem> jobItemCollection, bool isBatchCompletionReport, bool isStaged)
		{
			ReportCsvSchema schema = MigrationSuccessReportCsvSchema.GetSchema(migrationType, isBatchCompletionReport, isStaged);
			foreach (MigrationJobItem jobItem in jobItemCollection)
			{
				schema.WriteRow(streamWriter, jobItem);
			}
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00043194 File Offset: 0x00041394
		internal static ReportCsvSchema GetSchema(MigrationType migrationType, bool isBatchCompletionReport, bool isStaged)
		{
			if (migrationType <= MigrationType.ExchangeOutlookAnywhere)
			{
				if (migrationType == MigrationType.IMAP)
				{
					return new MigrationImapSuccessReportCsvSchema();
				}
				if (migrationType == MigrationType.ExchangeOutlookAnywhere)
				{
					return new MigrationExchangeSuccessReportCsvSchema(isBatchCompletionReport && !isStaged);
				}
			}
			else if (migrationType == MigrationType.ExchangeRemoteMove || migrationType == MigrationType.ExchangeLocalMove || migrationType == MigrationType.PublicFolder)
			{
				return new MigrationMoveSuccessReportCsvSchema();
			}
			string text = "MigrationSuccessReportCsvSchema invoked with unsupported migrationType " + migrationType;
			MigrationLogger.Log(MigrationEventType.Error, text, new object[0]);
			throw new InvalidOperationException(text);
		}

		// Token: 0x0400058F RID: 1423
		public const int InternalMaximumRowCount = 2147483647;

		// Token: 0x04000590 RID: 1424
		public const string PasswordColumnName = "Password";

		// Token: 0x04000591 RID: 1425
		public const string EmailColumnName = "EmailAddress";

		// Token: 0x04000592 RID: 1426
		public const string StatusColumnName = "Status";

		// Token: 0x04000593 RID: 1427
		public const string ItemsMigratedColumnName = "ItemsMigrated";

		// Token: 0x04000594 RID: 1428
		public const string ItemsSkippedColumnName = "ItemsSkipped";

		// Token: 0x04000595 RID: 1429
		public const string TypeColumnName = "ObjectType";

		// Token: 0x04000596 RID: 1430
		public const string AdditionalComments = "AdditionalComments";
	}
}
