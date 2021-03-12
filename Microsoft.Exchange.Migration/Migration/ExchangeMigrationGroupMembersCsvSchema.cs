using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200009C RID: 156
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ExchangeMigrationGroupMembersCsvSchema
	{
		// Token: 0x060008F0 RID: 2288 RVA: 0x00026860 File Offset: 0x00024A60
		public static IEnumerable<string> Read(Stream sourceStream)
		{
			CsvSchema csvSchema = new CsvSchema(50000, ExchangeMigrationGroupMembersCsvSchema.requiredColumns, null, null);
			foreach (CsvRow csvRow in csvSchema.Read(sourceStream))
			{
				CsvRow csvRow2 = csvRow;
				if (csvRow2.Index != 0)
				{
					CsvRow csvRow3 = csvRow;
					string smtpAddress = csvRow3["EmailAddress"];
					if (!string.IsNullOrEmpty(smtpAddress))
					{
						yield return smtpAddress;
					}
				}
			}
			yield break;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00026880 File Offset: 0x00024A80
		public static void Write(StreamWriter streamWriter, IEnumerable<string> members)
		{
			streamWriter.WriteCsvLine(new string[]
			{
				"EmailAddress"
			});
			foreach (string text in members)
			{
				streamWriter.WriteCsvLine(new string[]
				{
					text
				});
			}
		}

		// Token: 0x0400036E RID: 878
		public const string AttachmentName = "GroupMembers.csv";

		// Token: 0x0400036F RID: 879
		private const int MaximumRowCount = 50000;

		// Token: 0x04000370 RID: 880
		private const string EmailColumnName = "EmailAddress";

		// Token: 0x04000371 RID: 881
		private static readonly Dictionary<string, ProviderPropertyDefinition> requiredColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"EmailAddress",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("EmailAddress", MigrationCsvSchemaBase.EmailAddressConstraint)
			}
		};
	}
}
