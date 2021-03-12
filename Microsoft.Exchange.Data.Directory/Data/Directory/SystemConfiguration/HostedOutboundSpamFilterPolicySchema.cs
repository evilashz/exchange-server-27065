using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000475 RID: 1141
	internal class HostedOutboundSpamFilterPolicySchema : ADConfigurationObjectSchema
	{
		// Token: 0x0400233E RID: 9022
		internal const int NotifyOutboundSpamShift = 0;

		// Token: 0x0400233F RID: 9023
		internal const int BccSuspiciousOutboundMailShift = 2;

		// Token: 0x04002340 RID: 9024
		public static readonly ADPropertyDefinition OutboundSpamFilterFlags = new ADPropertyDefinition("OutboundSpamFilterFlags", ExchangeObjectVersion.Exchange2012, typeof(int), "msExchSpamFlags", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002341 RID: 9025
		public static readonly ADPropertyDefinition NotifyOutboundSpamRecipients = new ADPropertyDefinition("NotifyOutboundSpamRecipients", ExchangeObjectVersion.Exchange2012, typeof(SmtpAddress), "msExchSpamNotifyOutboundRecipients", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002342 RID: 9026
		public static readonly ADPropertyDefinition BccSuspiciousOutboundAdditionalRecipients = new ADPropertyDefinition("BccSuspiciousOutboundAdditionalRecipients", ExchangeObjectVersion.Exchange2012, typeof(SmtpAddress), "msExchSpamOutboundSpamCc", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04002343 RID: 9027
		public static readonly ADPropertyDefinition NotifyOutboundSpam = ADObject.BitfieldProperty("NotifyOutboundSpam", 0, HostedOutboundSpamFilterPolicySchema.OutboundSpamFilterFlags);

		// Token: 0x04002344 RID: 9028
		public static readonly ADPropertyDefinition BccSuspiciousOutboundMail = ADObject.BitfieldProperty("BccSuspiciousOutboundMail", 2, HostedOutboundSpamFilterPolicySchema.OutboundSpamFilterFlags);
	}
}
