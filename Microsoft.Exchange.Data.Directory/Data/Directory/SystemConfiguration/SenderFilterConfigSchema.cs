using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004D3 RID: 1235
	internal sealed class SenderFilterConfigSchema : MessageHygieneAgentConfigSchema
	{
		// Token: 0x04002593 RID: 9619
		public static readonly PropertyDefinition BlockedSenderAction = new ADPropertyDefinition("BlockedSenderAction", ExchangeObjectVersion.Exchange2007, typeof(BlockedSenderAction), "msExchMessageHygieneBlockedSenderAction", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.Directory.SystemConfiguration.BlockedSenderAction.Reject, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(BlockedSenderAction))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002594 RID: 9620
		public static readonly PropertyDefinition BlockedSenders = new ADPropertyDefinition("BlockedSenders", ExchangeObjectVersion.Exchange2007, typeof(SmtpAddress), "msExchMessageHygieneBlockedSender", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new ValidSmtpAddressConstraint()
		}, null, null);

		// Token: 0x04002595 RID: 9621
		public static readonly PropertyDefinition BlockedDomains = new ADPropertyDefinition("BlockedDomains", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomain), "msExchMessageHygieneBlockedDomain", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002596 RID: 9622
		public static readonly PropertyDefinition BlockedDomainAndSubdomains = new ADPropertyDefinition("BlockedDomainAndSubdomains", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomain), "msExchMessageHygieneBlockedDomainAndSubdomains", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002597 RID: 9623
		public static readonly ADPropertyDefinition BlockBlankSenders = new ADPropertyDefinition("BlockBlankSenders", ExchangeObjectVersion.Exchange2007, typeof(bool), null, ADPropertyDefinitionFlags.Calculated, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MessageHygieneAgentConfigSchema.AgentsFlags
		}, null, ADObject.FlagGetterDelegate(4096, MessageHygieneAgentConfigSchema.AgentsFlags), ADObject.FlagSetterDelegate(4096, MessageHygieneAgentConfigSchema.AgentsFlags), null, null);

		// Token: 0x04002598 RID: 9624
		public static readonly PropertyDefinition RecipientBlockedSenderAction = new ADPropertyDefinition("RecipientBlockedSenderAction", ExchangeObjectVersion.Exchange2007, typeof(RecipientBlockedSenderAction), "msExchMessageHygieneRecipientBlockedSenderAction", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.Directory.SystemConfiguration.RecipientBlockedSenderAction.Reject, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(RecipientBlockedSenderAction))
		}, PropertyDefinitionConstraint.None, null, null);
	}
}
