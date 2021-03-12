using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management.Migration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000B1 RID: 177
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PSTImportCsvSchema : MigrationBatchCsvSchema
	{
		// Token: 0x060009B4 RID: 2484 RVA: 0x000293A7 File Offset: 0x000275A7
		public PSTImportCsvSchema() : base(int.MaxValue, PSTImportCsvSchema.requiredColumns, PSTImportCsvSchema.optionalColumns, null)
		{
		}

		// Token: 0x040003CE RID: 974
		internal const string PSTPathFileNameColumnName = "PSTPathFileName";

		// Token: 0x040003CF RID: 975
		internal const string TargetMailboxIdColumnName = "TargetMailboxId";

		// Token: 0x040003D0 RID: 976
		internal const string TargetMailboxTypeColumnName = "TargetMailboxType";

		// Token: 0x040003D1 RID: 977
		internal const string SourceRootFolderColumnName = "SourceRootFolderName";

		// Token: 0x040003D2 RID: 978
		internal const string TargetRootFolderColumnName = "TargetRootFolderName";

		// Token: 0x040003D3 RID: 979
		internal static readonly ProviderPropertyDefinition TargetMailboxTypePropertyDefinition = new SimpleProviderPropertyDefinition("TargetMailboxType", ExchangeObjectVersion.Exchange2012, typeof(MigrationMailboxType), PropertyDefinitionFlags.None, MigrationMailboxType.PrimaryOnly, new PropertyDefinitionConstraint[]
		{
			new ValueDefinedConstraint<MigrationMailboxType>(new MigrationMailboxType[]
			{
				MigrationMailboxType.PrimaryOnly,
				MigrationMailboxType.ArchiveOnly
			}, true)
		}, PropertyDefinitionConstraint.None);

		// Token: 0x040003D4 RID: 980
		private static readonly Dictionary<string, ProviderPropertyDefinition> requiredColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"PSTPathFileName",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("PSTPathFileName", PSTImportCsvSchema.PSTFilePathNameConstraints)
			},
			{
				"TargetMailboxId",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("TargetMailboxId", MigrationCsvSchemaBase.EmailAddressConstraint)
			}
		};

		// Token: 0x040003D5 RID: 981
		private static readonly Dictionary<string, ProviderPropertyDefinition> optionalColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"TargetMailboxType",
				PSTImportCsvSchema.TargetMailboxTypePropertyDefinition
			},
			{
				"SourceRootFolderName",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("SourceRootFolderName", PSTImportCsvSchema.RootFolderNameConstraints)
			},
			{
				"TargetRootFolderName",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("TargetRootFolderName", PSTImportCsvSchema.RootFolderNameConstraints)
			}
		};

		// Token: 0x040003D6 RID: 982
		private static readonly PropertyDefinitionConstraint[] PSTFilePathNameConstraints = new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 256),
			new NoLeadingOrTrailingWhitespaceConstraint()
		};

		// Token: 0x040003D7 RID: 983
		private static readonly PropertyDefinitionConstraint[] RootFolderNameConstraints = new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 1024),
			new NoLeadingOrTrailingWhitespaceConstraint()
		};
	}
}
