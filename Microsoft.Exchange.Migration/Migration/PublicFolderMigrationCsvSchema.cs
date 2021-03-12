using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000B7 RID: 183
	internal sealed class PublicFolderMigrationCsvSchema : MigrationCsvSchemaBase
	{
		// Token: 0x060009FB RID: 2555 RVA: 0x00029FEE File Offset: 0x000281EE
		public PublicFolderMigrationCsvSchema() : base(1000, PublicFolderMigrationCsvSchema.requiredColumns, null, null)
		{
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002A002 File Offset: 0x00028202
		public override string GetIdentifier(CsvRow row)
		{
			return row["TargetMailbox"];
		}

		// Token: 0x040003F0 RID: 1008
		internal const string FolderPathColumnName = "FolderPath";

		// Token: 0x040003F1 RID: 1009
		internal const string TargetMailboxColumnName = "TargetMailbox";

		// Token: 0x040003F2 RID: 1010
		private const int InternalMaximumRowCount = 1000;

		// Token: 0x040003F3 RID: 1011
		private static readonly PropertyDefinitionConstraint[] FolderPathConstraints = new PropertyDefinitionConstraint[]
		{
			new NotNullOrEmptyConstraint(),
			new NoLeadingOrTrailingWhitespaceConstraint()
		};

		// Token: 0x040003F4 RID: 1012
		private static readonly ProviderPropertyDefinition FolderPathPropertyDefinition = new SimpleProviderPropertyDefinition("FolderPath", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PublicFolderMigrationCsvSchema.FolderPathConstraints, PublicFolderMigrationCsvSchema.FolderPathConstraints);

		// Token: 0x040003F5 RID: 1013
		private static readonly Dictionary<string, ProviderPropertyDefinition> requiredColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"FolderPath",
				PublicFolderMigrationCsvSchema.FolderPathPropertyDefinition
			},
			{
				"TargetMailbox",
				ADObjectSchema.Name
			}
		};
	}
}
