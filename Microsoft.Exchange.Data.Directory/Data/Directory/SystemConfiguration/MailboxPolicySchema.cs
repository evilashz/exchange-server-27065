using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000317 RID: 791
	internal abstract class MailboxPolicySchema : ADLegacyVersionableObjectSchema
	{
		// Token: 0x04001681 RID: 5761
		public static readonly ADPropertyDefinition MailboxPolicyFlags = new ADPropertyDefinition("MailboxPolicyFlags", ExchangeObjectVersion.Exchange2007, typeof(int), "msExchRecipientTemplateFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, new PropertyDefinitionConstraint[]
		{
			new RangedValueConstraint<int>(0, 2)
		}, PropertyDefinitionConstraint.None, null, null);
	}
}
