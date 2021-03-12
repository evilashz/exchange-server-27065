using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000CB0 RID: 3248
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PublicFolderMigrationRequestCsvSchema : CsvSchema
	{
		// Token: 0x06007C9A RID: 31898 RVA: 0x001FE3C1 File Offset: 0x001FC5C1
		public PublicFolderMigrationRequestCsvSchema() : base(1000, PublicFolderMigrationRequestCsvSchema.requiredColumns, null, null)
		{
		}

		// Token: 0x04003D86 RID: 15750
		internal const string FolderPathColumnName = "FolderPath";

		// Token: 0x04003D87 RID: 15751
		internal const string TargetMailboxColumnName = "TargetMailbox";

		// Token: 0x04003D88 RID: 15752
		private const int InternalMaximumRowCount = 1000;

		// Token: 0x04003D89 RID: 15753
		private static PropertyDefinitionConstraint[] FolderPathConstraints = new PropertyDefinitionConstraint[]
		{
			new NotNullOrEmptyConstraint(),
			new NoLeadingOrTrailingWhitespaceConstraint(),
			new StringLengthConstraint(1, 0)
		};

		// Token: 0x04003D8A RID: 15754
		private static readonly ProviderPropertyDefinition FolderPathPropertyDefinition = new SimpleProviderPropertyDefinition("FolderPath", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PublicFolderMigrationRequestCsvSchema.FolderPathConstraints, PublicFolderMigrationRequestCsvSchema.FolderPathConstraints);

		// Token: 0x04003D8B RID: 15755
		private static Dictionary<string, ProviderPropertyDefinition> requiredColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"FolderPath",
				PublicFolderMigrationRequestCsvSchema.FolderPathPropertyDefinition
			},
			{
				"TargetMailbox",
				ADObjectSchema.Name
			}
		};
	}
}
