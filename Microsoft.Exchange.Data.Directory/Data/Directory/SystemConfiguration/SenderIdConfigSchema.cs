using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004D8 RID: 1240
	internal sealed class SenderIdConfigSchema : MessageHygieneAgentConfigSchema
	{
		// Token: 0x040025A5 RID: 9637
		public static readonly PropertyDefinition SpoofedDomainAction = new ADPropertyDefinition("SpoofedDomainAction", ExchangeObjectVersion.Exchange2007, typeof(SenderIdAction), "msExchMessageHygieneSpoofedDomainAction", ADPropertyDefinitionFlags.None, SenderIdAction.StampStatus, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(SenderIdAction))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040025A6 RID: 9638
		public static readonly PropertyDefinition TempErrorAction = new ADPropertyDefinition("TempErrorAction", ExchangeObjectVersion.Exchange2007, typeof(SenderIdAction), "msExchMessageHygieneTempErrorAction", ADPropertyDefinitionFlags.None, SenderIdAction.StampStatus, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(SenderIdAction))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040025A7 RID: 9639
		public static readonly PropertyDefinition BypassedRecipients = SharedPropertyDefinitions.BypassedRecipients;

		// Token: 0x040025A8 RID: 9640
		public static readonly PropertyDefinition BypassedSenderDomains = new ADPropertyDefinition("BypassedSenderDomains", ExchangeObjectVersion.Exchange2007, typeof(SmtpDomain), "msExchMessageHygieneBypassedSenderDomain", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
