using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000420 RID: 1056
	internal sealed class ManagedFolderMailboxPolicySchema : MailboxPolicySchema
	{
		// Token: 0x04002017 RID: 8215
		public static readonly ADPropertyDefinition AssociatedUsers = new ADPropertyDefinition("AssociatedUsers", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchMailboxTemplateBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002018 RID: 8216
		public static readonly ADPropertyDefinition ManagedFolderLinks = new ADPropertyDefinition("ManagedFolderLinks", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "msExchElcFolderLink", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
