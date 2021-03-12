﻿using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000426 RID: 1062
	internal sealed class RetentionPolicySchema : MailboxPolicySchema
	{
		// Token: 0x06002FCB RID: 12235 RVA: 0x000C1246 File Offset: 0x000BF446
		internal static QueryFilter IsDefaultFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(MailboxPolicySchema.MailboxPolicyFlags, 1UL));
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x000C125A File Offset: 0x000BF45A
		internal static QueryFilter IsDefaultArbitrationMailboxFilterBuilder(SinglePropertyFilter filter)
		{
			return ADObject.BoolFilterBuilder(filter, new BitMaskAndFilter(MailboxPolicySchema.MailboxPolicyFlags, 2UL));
		}

		// Token: 0x04002041 RID: 8257
		public static readonly ADPropertyDefinition AssociatedUsers = new ADPropertyDefinition("AssociatedUsers", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchMailboxTemplateBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002042 RID: 8258
		public static readonly ADPropertyDefinition RetentionPolicyTagLinks = new ADPropertyDefinition("RetentionPolicyLinks", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchElcFolderLink", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002043 RID: 8259
		public static readonly ADPropertyDefinition RetentionId = new ADPropertyDefinition("RetentionId", ExchangeObjectVersion.Exchange2010, typeof(Guid), "msExchUnmergedAttsPt", ADPropertyDefinitionFlags.Binary, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002044 RID: 8260
		public static readonly ADPropertyDefinition IsDefault = new ADPropertyDefinition("IsDefault", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxPolicySchema.MailboxPolicyFlags
		}, new CustomFilterBuilderDelegate(RetentionPolicySchema.IsDefaultFilterBuilder), ADObject.FlagGetterDelegate(MailboxPolicySchema.MailboxPolicyFlags, 1), ADObject.FlagSetterDelegate(MailboxPolicySchema.MailboxPolicyFlags, 1), null, null);

		// Token: 0x04002045 RID: 8261
		public static readonly ADPropertyDefinition IsDefaultArbitrationMailbox = new ADPropertyDefinition("IsDefaultArbitrationMailbox", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxPolicySchema.MailboxPolicyFlags
		}, new CustomFilterBuilderDelegate(RetentionPolicySchema.IsDefaultArbitrationMailboxFilterBuilder), ADObject.FlagGetterDelegate(MailboxPolicySchema.MailboxPolicyFlags, 2), ADObject.FlagSetterDelegate(MailboxPolicySchema.MailboxPolicyFlags, 2), null, null);
	}
}
