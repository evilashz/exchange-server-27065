using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x020000AE RID: 174
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MigrationRemoteMoveOnboardingCsvSchema : MigrationMoveCsvSchema
	{
		// Token: 0x06000982 RID: 2434 RVA: 0x0002878D File Offset: 0x0002698D
		public MigrationRemoteMoveOnboardingCsvSchema() : base(int.MaxValue, MigrationRemoteMoveOnboardingCsvSchema.requiredColumns, MigrationRemoteMoveOnboardingCsvSchema.optionalColumns, null)
		{
		}

		// Token: 0x040003BA RID: 954
		private static readonly Dictionary<string, ProviderPropertyDefinition> requiredColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"EmailAddress",
				MigrationCsvSchemaBase.GetDefaultPropertyDefinition("EmailAddress", MigrationCsvSchemaBase.EmailAddressConstraint)
			}
		};

		// Token: 0x040003BB RID: 955
		private static readonly Dictionary<string, ProviderPropertyDefinition> optionalColumns = new Dictionary<string, ProviderPropertyDefinition>(StringComparer.OrdinalIgnoreCase)
		{
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
