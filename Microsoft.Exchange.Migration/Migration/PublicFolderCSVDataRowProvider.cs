using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000B8 RID: 184
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PublicFolderCSVDataRowProvider : IMigrationDataRowProvider
	{
		// Token: 0x060009FE RID: 2558 RVA: 0x0002A095 File Offset: 0x00028295
		public PublicFolderCSVDataRowProvider(MigrationJob job, IMigrationDataProvider dataProvider)
		{
			MigrationUtil.ThrowOnNullArgument(job, "job");
			MigrationUtil.ThrowOnNullArgument(dataProvider, "dataProvider");
			this.migrationJob = job;
			this.dataProvider = dataProvider;
			this.csvSchema = new PublicFolderMigrationCsvSchema();
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0002A260 File Offset: 0x00028460
		public IEnumerable<IMigrationDataRow> GetNextBatchItem(string cursorPosition, int maxCountHint)
		{
			this.BootstrapUniqueMailboxesCollection();
			int lastProcessedRowIndex = 0;
			if (!string.IsNullOrEmpty(cursorPosition) && !int.TryParse(cursorPosition, out lastProcessedRowIndex))
			{
				throw new ArgumentException("cursorPosition is not an integer value: " + cursorPosition);
			}
			int rowIndex = lastProcessedRowIndex;
			while (rowIndex < this.uniqueTargetMailboxes.Count && maxCountHint > 0)
			{
				yield return new PublicFolderMigrationDataRow(rowIndex + 1, this.uniqueTargetMailboxes[rowIndex]);
				maxCountHint--;
				rowIndex++;
			}
			yield break;
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0002A28C File Offset: 0x0002848C
		private void BootstrapUniqueMailboxesCollection()
		{
			if (this.uniqueTargetMailboxes != null)
			{
				return;
			}
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			List<string> list = new List<string>();
			using (IMigrationMessageItem migrationMessageItem = this.migrationJob.FindMessageItem(this.dataProvider, this.migrationJob.InitializationPropertyDefinitions))
			{
				using (IMigrationAttachment attachment = migrationMessageItem.GetAttachment("Request.csv", PropertyOpenMode.ReadOnly))
				{
					Stream stream = attachment.Stream;
					foreach (CsvRow csvRow in this.csvSchema.Read(stream))
					{
						string item;
						if (csvRow.Index != 0 && csvRow.TryGetColumnValue("TargetMailbox", out item) && hashSet.Add(item))
						{
							list.Add(item);
						}
					}
				}
			}
			this.uniqueTargetMailboxes = list;
		}

		// Token: 0x040003F6 RID: 1014
		private readonly IMigrationDataProvider dataProvider;

		// Token: 0x040003F7 RID: 1015
		private readonly MigrationJob migrationJob;

		// Token: 0x040003F8 RID: 1016
		private readonly MigrationCsvSchemaBase csvSchema;

		// Token: 0x040003F9 RID: 1017
		private List<string> uniqueTargetMailboxes;
	}
}
