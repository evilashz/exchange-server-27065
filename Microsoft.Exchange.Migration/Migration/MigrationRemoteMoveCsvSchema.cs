using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000AD RID: 173
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationRemoteMoveCsvSchema : MigrationMoveCsvSchema
	{
		// Token: 0x06000980 RID: 2432 RVA: 0x000286D9 File Offset: 0x000268D9
		public MigrationRemoteMoveCsvSchema() : base(int.MaxValue, MigrationRemoteMoveCsvSchema.requiredColumns, MigrationRemoteMoveCsvSchema.optionalColumns, null)
		{
		}

		// Token: 0x040003B8 RID: 952
		private static readonly Dictionary<string, ProviderPropertyDefinition> requiredColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"EmailAddress",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("EmailAddress", MigrationCsvSchemaBase.EmailAddressConstraint)
			}
		};

		// Token: 0x040003B9 RID: 953
		private static readonly Dictionary<string, ProviderPropertyDefinition> optionalColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"TargetDatabase",
				ADUserSchema.DatabaseName
			},
			{
				"TargetArchiveDatabase",
				ADUserSchema.DatabaseName
			},
			{
				"BadItemLimit",
				MigrationMoveCsvSchema.BadItemLimit
			},
			{
				"LargeItemLimit",
				MigrationMoveCsvSchema.LargeItemLimit
			},
			{
				"MailboxType",
				MigrationMoveCsvSchema.MailboxTypePropertyDefinition
			}
		};
	}
}
