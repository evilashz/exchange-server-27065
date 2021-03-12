using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000B6 RID: 182
	internal sealed class PublicFolderMigrationBatchCsvProcessor : MigrationBatchCsvProcessor
	{
		// Token: 0x060009F7 RID: 2551 RVA: 0x00029EE4 File Offset: 0x000280E4
		public PublicFolderMigrationBatchCsvProcessor(PublicFolderMigrationCsvSchema schema, IMigrationDataProvider dataProvider) : base(schema)
		{
			this.hierarchyMailboxName = dataProvider.ADProvider.GetPublicFolderHierarchyMailboxName();
			if (this.hierarchyMailboxName == null)
			{
				throw new PublicFolderMailboxesNotProvisionedException();
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00029F37 File Offset: 0x00028137
		protected override bool ValidationWarningAsError
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x00029F3C File Offset: 0x0002813C
		protected override LocalizedException InternalProcessRow(CsvRow row, out bool isDataRow)
		{
			string text = row["FolderPath"];
			string text2 = row["TargetMailbox"];
			if (row.IsValid && !this.folderList.Add(text))
			{
				isDataRow = false;
				return new DuplicateFolderInCSVException(row.Index, text, text2);
			}
			if ("\\".Equals(text, StringComparison.InvariantCultureIgnoreCase))
			{
				if (!this.hierarchyMailboxName.Equals(text2, StringComparison.InvariantCultureIgnoreCase))
				{
					isDataRow = false;
					return new InvalidRootFolderMappingInCSVException(row.Index, text, text2, this.hierarchyMailboxName);
				}
				this.validPublicFolderMappingFound = true;
			}
			isDataRow = this.mailboxes.Add(text2);
			return null;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x00029FD7 File Offset: 0x000281D7
		protected override LocalizedException Validate()
		{
			if (!this.validPublicFolderMappingFound)
			{
				return new MissingRootFolderMappingInCSVException(this.hierarchyMailboxName);
			}
			return null;
		}

		// Token: 0x040003EB RID: 1003
		private const string RootFolderPath = "\\";

		// Token: 0x040003EC RID: 1004
		private readonly HashSet<string> folderList = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x040003ED RID: 1005
		private readonly HashSet<string> mailboxes = new HashSet<string>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x040003EE RID: 1006
		private readonly string hierarchyMailboxName;

		// Token: 0x040003EF RID: 1007
		private bool validPublicFolderMappingFound;
	}
}
