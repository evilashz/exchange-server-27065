using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000AC RID: 172
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationLocalMoveCsvSchema : MigrationMoveCsvSchema
	{
		// Token: 0x0600097E RID: 2430 RVA: 0x00028636 File Offset: 0x00026836
		public MigrationLocalMoveCsvSchema() : base(int.MaxValue, MigrationLocalMoveCsvSchema.requiredColumns, MigrationLocalMoveCsvSchema.optionalColumns, null)
		{
		}

		// Token: 0x040003B6 RID: 950
		private static readonly Dictionary<string, ProviderPropertyDefinition> requiredColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"EmailAddress",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("EmailAddress", MigrationCsvSchemaBase.EmailAddressConstraint)
			}
		};

		// Token: 0x040003B7 RID: 951
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
				"MailboxType",
				MigrationMoveCsvSchema.MailboxTypePropertyDefinition
			}
		};
	}
}
