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
	// Token: 0x0200005A RID: 90
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationErrorCsvSchema : CsvSchema
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x00010054 File Offset: 0x0000E254
		public MigrationErrorCsvSchema() : base(int.MaxValue, MigrationErrorCsvSchema.requiredColumns, null, null)
		{
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000102D8 File Offset: 0x0000E4D8
		public static IEnumerable<MigrationBatchError> ReadErrors(Stream sourceStream)
		{
			MigrationErrorCsvSchema csvSchema = new MigrationErrorCsvSchema();
			foreach (CsvRow csvRow in csvSchema.Read(sourceStream))
			{
				CsvRow csvRow2 = csvRow;
				if (csvRow2.Index != 0)
				{
					MigrationBatchError error = new MigrationBatchError();
					int rowIndex = 0;
					CsvRow csvRow3 = csvRow;
					int.TryParse(csvRow3["RowIndex"], out rowIndex);
					error.RowIndex = rowIndex;
					MigrationError migrationError = error;
					CsvRow csvRow4 = csvRow;
					migrationError.EmailAddress = csvRow4["EmailAddress"];
					if (string.IsNullOrEmpty(error.EmailAddress))
					{
						error.EmailAddress = ServerStrings.EmailAddressMissing;
					}
					MigrationError migrationError2 = error;
					CsvRow csvRow5 = csvRow;
					migrationError2.LocalizedErrorMessage = new LocalizedString(csvRow5["ErrorMessage"]);
					yield return error;
				}
			}
			yield break;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000102F8 File Offset: 0x0000E4F8
		public static int WriteHeaderAndErrors(StreamWriter streamWriter, IEnumerable<MigrationBatchError> errorCollection)
		{
			streamWriter.WriteCsvLine(new string[]
			{
				"RowIndex",
				"EmailAddress",
				"ErrorMessage"
			});
			return MigrationErrorCsvSchema.WriteErrors(streamWriter, errorCollection);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00010334 File Offset: 0x0000E534
		public static int WriteErrors(StreamWriter streamWriter, IEnumerable<MigrationBatchError> errorCollection)
		{
			int num = 0;
			if (errorCollection != null)
			{
				foreach (MigrationBatchError migrationBatchError in errorCollection)
				{
					streamWriter.WriteCsvLine(new string[]
					{
						migrationBatchError.RowIndex.ToString(),
						migrationBatchError.EmailAddress,
						migrationBatchError.LocalizedErrorMessage
					});
					num++;
				}
			}
			return num;
		}

		// Token: 0x04000159 RID: 345
		public const string RowIndexColumnName = "RowIndex";

		// Token: 0x0400015A RID: 346
		public const string EmailColumnName = "EmailAddress";

		// Token: 0x0400015B RID: 347
		public const string ErrorMessageColumnName = "ErrorMessage";

		// Token: 0x0400015C RID: 348
		private const int InternalMaximumRowCount = 2147483647;

		// Token: 0x0400015D RID: 349
		private static Dictionary<string, ProviderPropertyDefinition> requiredColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"RowIndex",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("RowIndex", null)
			},
			{
				"EmailAddress",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("EmailAddress", null)
			},
			{
				"ErrorMessage",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("ErrorMessage", null)
			}
		};
	}
}
