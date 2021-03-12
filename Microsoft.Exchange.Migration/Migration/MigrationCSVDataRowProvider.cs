using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000013 RID: 19
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class MigrationCSVDataRowProvider : IMigrationDataRowProvider
	{
		// Token: 0x06000060 RID: 96 RVA: 0x00003156 File Offset: 0x00001356
		internal MigrationCSVDataRowProvider(MigrationJob job, IMigrationDataProvider dataProvider, MigrationCsvSchemaBase csvSchema)
		{
			MigrationUtil.ThrowOnNullArgument(dataProvider, "DataProvider");
			this.migrationJob = job;
			this.dataProvider = dataProvider;
			this.csvSchema = csvSchema;
			this.csvSchema.AllowUnknownColumnsInCSV = job.AllowUnknownColumnsInCsv;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000318F File Offset: 0x0000138F
		protected IMigrationDataProvider DataProvider
		{
			get
			{
				return this.dataProvider;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003197 File Offset: 0x00001397
		protected MigrationJob MigrationJob
		{
			get
			{
				return this.migrationJob;
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000031A0 File Offset: 0x000013A0
		public static MigrationCsvSchemaBase CreateCsvSchema(MigrationType type, bool isStaged, bool isTenantOnboarding)
		{
			if (type <= MigrationType.ExchangeRemoteMove)
			{
				switch (type)
				{
				case MigrationType.IMAP:
					return new MigrationBatchCsvSchema();
				case MigrationType.XO1:
					return new XO1CsvSchema();
				case MigrationType.IMAP | MigrationType.XO1:
					break;
				case MigrationType.ExchangeOutlookAnywhere:
					if (!isStaged)
					{
						return null;
					}
					return new ExchangeMigrationBatchCsvSchema();
				default:
					if (type == MigrationType.ExchangeRemoteMove)
					{
						if (!isTenantOnboarding)
						{
							return new MigrationRemoteMoveCsvSchema();
						}
						return new MigrationRemoteMoveOnboardingCsvSchema();
					}
					break;
				}
			}
			else
			{
				if (type == MigrationType.ExchangeLocalMove)
				{
					return new MigrationLocalMoveCsvSchema();
				}
				if (type == MigrationType.PSTImport)
				{
					return new PSTImportCsvSchema();
				}
				if (type == MigrationType.PublicFolder)
				{
					return new PublicFolderMigrationCsvSchema();
				}
			}
			return null;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003220 File Offset: 0x00001420
		public static MigrationCsvSchemaBase CreateCsvSchema(MigrationJob job)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			return MigrationCSVDataRowProvider.CreateCsvSchema(job.MigrationType, job.IsStaged, !string.IsNullOrEmpty(job.TenantName) && job.SourceEndpoint != null);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003574 File Offset: 0x00001774
		public virtual IEnumerable<IMigrationDataRow> GetNextBatchItem(string cursorPosition, int maxCountHint)
		{
			int lastProcessedRowIndex = 0;
			if (!string.IsNullOrEmpty(cursorPosition) && !int.TryParse(cursorPosition, out lastProcessedRowIndex))
			{
				throw new ArgumentException("LastProcessedIndex should be an int but was " + cursorPosition);
			}
			using (IMigrationMessageItem message = this.migrationJob.FindMessageItem(this.DataProvider, this.migrationJob.InitializationPropertyDefinitions))
			{
				using (IMigrationAttachment attachment = message.GetAttachment("Request.csv", PropertyOpenMode.ReadOnly))
				{
					Stream csvStream = attachment.Stream;
					foreach (CsvRow row in this.csvSchema.Read(csvStream))
					{
						CsvRow csvRow = row;
						if (csvRow.Index > lastProcessedRowIndex)
						{
							yield return this.CreateDataRow(row);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003598 File Offset: 0x00001798
		internal InvalidDataRow GetInvalidDataRow(CsvRow row, MigrationType migrationType)
		{
			MigrationBatchError rowValidationError = MigrationBatchCsvProcessor.GetRowValidationError(row, this.csvSchema);
			if (rowValidationError == null)
			{
				return null;
			}
			return new InvalidDataRow(row.Index, rowValidationError, migrationType);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000035C5 File Offset: 0x000017C5
		internal InvalidDataRow GetInvalidDataRow(CsvRow row, LocalizedString errorMessage, MigrationType migrationType)
		{
			return new InvalidDataRow(row.Index, this.csvSchema.CreateValidationError(row, errorMessage), migrationType);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000035E4 File Offset: 0x000017E4
		protected static bool TryConvertStringToBool(string boolString, out bool value)
		{
			string key;
			switch (key = boolString.ToLowerInvariant())
			{
			case "$true":
			case "true":
			case "1":
			case "yes":
				value = true;
				return true;
			case "$false":
			case "false":
			case "0":
			case "no":
				value = false;
				return true;
			}
			value = false;
			return false;
		}

		// Token: 0x06000069 RID: 105
		protected abstract IMigrationDataRow CreateDataRow(CsvRow row);

		// Token: 0x0400001B RID: 27
		public const string True1 = "$true";

		// Token: 0x0400001C RID: 28
		public const string True2 = "true";

		// Token: 0x0400001D RID: 29
		public const string True3 = "1";

		// Token: 0x0400001E RID: 30
		public const string True4 = "yes";

		// Token: 0x0400001F RID: 31
		public const string False1 = "$false";

		// Token: 0x04000020 RID: 32
		public const string False2 = "false";

		// Token: 0x04000021 RID: 33
		public const string False3 = "0";

		// Token: 0x04000022 RID: 34
		public const string False4 = "no";

		// Token: 0x04000023 RID: 35
		private readonly IMigrationDataProvider dataProvider;

		// Token: 0x04000024 RID: 36
		private readonly MigrationJob migrationJob;

		// Token: 0x04000025 RID: 37
		private readonly MigrationCsvSchemaBase csvSchema;
	}
}
